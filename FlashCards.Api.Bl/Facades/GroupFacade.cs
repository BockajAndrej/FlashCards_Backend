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
                throw new InvalidOperationException($"Group s ID {entity.Id} neexistuje.");

            dbContext.Entry(existingGroup).CurrentValues.SetValues(entity);

            var incomingUserIds = model.Users
                .Select(u => u.Id)
                .ToHashSet();

            var usersToRemove = existingGroup.UsersBelong
                .Where(gu => !incomingUserIds.Contains(gu.UserId))
                .ToList();

            foreach (var removeUser in usersToRemove)
            {
                existingGroup.UsersBelong.Remove(removeUser);
            }

            var existingUserIds = existingGroup.UsersBelong
                .Select(gu => gu.UserId)
                .ToHashSet();

            foreach (var incoming in model.Users)
            {
                if (existingUserIds.Contains(incoming.Id))
                {
                    var existing = existingGroup.UsersBelong
                        .First(gu => gu.UserId == incoming.Id);

                    var newRole = incoming.Role ?? EnumUserRole.Member;
                    if (existing.Role != newRole)
                    {
                        existing.Role = newRole;
                    }
                }
                else
                {
                    existingGroup.UsersBelong.Add(new GroupUserEntity
                    {
                        GroupId = existingGroup.Id,
                        UserId = incoming.Id,
                        Role = incoming.Role ?? EnumUserRole.Member
                    });
                }
            }
        }

        await dbContext.SaveChangesAsync();

        mapper.Map(entity, model);

        return model;
    }
    
    public async Task RemoveUserFromGroupAsync(Guid groupId, Guid userId)
    {
        var group = await dbContext.Set<GroupEntity>()
            .Include(g => g.UsersBelong)
            .FirstOrDefaultAsync(g => g.Id == groupId);

        if (group == null)
            throw new KeyNotFoundException("Group not found");

        var membership = group.UsersBelong.FirstOrDefault(gu => gu.UserId == userId);

        if (membership != null)
        {
            group.UsersBelong.Remove(membership);
            await dbContext.SaveChangesAsync();
        }
    }

}