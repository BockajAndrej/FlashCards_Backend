using AutoMapper;
using FlashCards.Api.Bl.Facades.Interfaces;
using FlashCards.Api.Dal;
using FlashCards.Api.Dal.Entities;
using FlashCards.Common.Enums;
using FlashCards.Common.Models.Details;
using FlashCards.Common.Models.Lists;
using FlashCards.Common.QueryObjects;
using Microsoft.EntityFrameworkCore;

namespace FlashCards.Api.Bl.Facades;

public class GroupFacade(FlashCardsDbContext dbContext, IMapper mapper)
    : FacadeBase<GroupEntity, GroupQueryObject, GroupListModel, GroupDetailModel>(dbContext, mapper),
        IGroupFacade
{
    protected override Task<IQueryable<GroupEntity>> CreateFilterQuery(GroupQueryObject queryObject, IQueryable<GroupEntity> query)
    {
        if(queryObject.NameFilter != null)
            query = query.Where(l => l.Name.ToLower().Contains(queryObject.NameFilter.ToLower()));
        
        return Task.FromResult(query);
    }

    protected override IQueryable<GroupEntity> CreateOrderQuery(GroupQueryObject queryObject, IQueryable<GroupEntity> query)
    {
        return query;
    }

    protected override GroupEntity SavaDetail(GroupEntity detail)
    {
        return detail;
    }

    protected override GroupEntity ModifyDetail(GroupEntity detail)
    {
        return detail;
    }
    
    public async Task<IQueryable<GroupListModel>> GetAsync(GroupQueryObject queryObject, Guid userId)
    {
        var query = dbContext.Set<GroupEntity>()
            .Include(g => g.UsersBelong)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(queryObject.NameFilter))
        {
            var filter = queryObject.NameFilter.ToLower();
            query = query.Where(l => l.Name.ToLower().Contains(filter));
        }

        query = query.OrderBy(l => l.Name);

        if (queryObject is { PageNumber: not null, PageSize: not null })
        {
            query = query.Skip((queryObject.PageNumber.Value - 1) * queryObject.PageSize.Value)
                .Take(queryObject.PageSize.Value);
        }

        var groupEntities = await query.ToListAsync();

        var groupListModels = mapper.Map<List<GroupListModel>>(groupEntities);

        for (int i = 0; i < groupListModels.Count; i++)
        {
            var groupModel = groupListModels[i];
            var groupEntity = groupEntities[i]; 
    
            var userGroupLink = groupEntity.UsersBelong
                .FirstOrDefault(ug => ug.UserId == userId);

            if (userGroupLink != null) 
                groupModel.OwnRole = userGroupLink.Role;
            else
                groupModel.OwnRole = null;
        }

        return groupListModels.ToList().AsQueryable();
    }
    
    
    public override async Task<GroupDetailModel> SaveAsync(GroupDetailModel model)
    {
        var entity = mapper.Map<GroupEntity>(model);
        
        if (entity.Id == Guid.Empty)
        {
            dbContext.Set<GroupEntity>().Add(entity);

            var userListModels = model.Users.ToList();

            foreach (var userListModel in userListModels)
            {
                var newJoinEntity = new GroupUserEntity()
                {
                    GroupId = entity.Id,
                    UserId = userListModel.Id,
                    Role = userListModel.Role ?? EnumUserRole.Member
                };
                dbContext.Set<GroupUserEntity>().Add(newJoinEntity);
            }
        }
        else
        {
            var existingGroup = await dbContext.Set<GroupEntity>()
                .Include(g => g.UsersBelong)
                .FirstOrDefaultAsync(g => g.Id == entity.Id);

            if (existingGroup == null)
            {
                throw new InvalidOperationException($"Group s ID {entity.Id} was not found.");
            }

            dbContext.Entry(existingGroup).CurrentValues.SetValues(entity);

            var incomingUserModelIds = model.Users
                .Select(userModel => userModel.Id)
                .ToHashSet();

            var usersToRemove = existingGroup.UsersBelong
                .Where(gu => !incomingUserModelIds.Contains(gu.UserId))
                .ToList();

            dbContext.Set<GroupUserEntity>().RemoveRange(usersToRemove);

            var existingUserIds = existingGroup.UsersBelong.Select(gu => gu.UserId).ToHashSet();

            foreach (var incomingUser in model.Users)
            {
                if (existingUserIds.Contains(incomingUser.Id))
                {
                    var existingJoinEntity = existingGroup.UsersBelong
                        .First(gu => gu.UserId == incomingUser.Id);

                    if (existingJoinEntity.Role != incomingUser.Role)
                        existingJoinEntity.Role = incomingUser.Role ??  EnumUserRole.Member;
                }
                else
                {
                    existingGroup.UsersBelong.Add(new GroupUserEntity
                    {
                        GroupId = existingGroup.Id,
                        UserId = incomingUser.Id,
                        Role = incomingUser.Role ?? EnumUserRole.Member
                    });
                }
            }
        }

        await dbContext.SaveChangesAsync();

        mapper.Map(entity, model);

        return model;
    }
}