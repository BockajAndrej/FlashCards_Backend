using AutoMapper;
using FlashCards.Api.Bl.Facades.Interfaces;
using FlashCards.Api.Dal;
using FlashCards.Api.Dal.Entities;
using FlashCards.Common.Models.Details;
using FlashCards.Common.Models.Lists;
using FlashCards.Common.QueryObjects;
using Microsoft.EntityFrameworkCore;

namespace FlashCards.Api.Bl.Facades;

public class UserFacade(FlashCardsDbContext dbContext, IMapper mapper) : FacadeBase<UserEntity, UserQueryObject, UserListModel, UserDetailModel>(dbContext, mapper), IUserFacade
{
    protected override Task<IQueryable<UserEntity>> CreateFilterQuery(UserQueryObject queryObject, IQueryable<UserEntity> query)
    {
        return Task.FromResult(query);
    }

    protected override IQueryable<UserEntity> CreateOrderQuery(UserQueryObject queryObject, IQueryable<UserEntity> query)
    {
        return query;
    }

    protected override UserEntity SavaDetail(UserEntity detail)
    {
        return detail;
    }

    protected override UserEntity ModifyDetail(UserEntity detail)
    {
        return detail;
    }

    public async Task<UserDetailModel?> GetLocalUserAsync(string userId)
    {
        IQueryable<UserEntity> query = dbContext.Set<UserEntity>();
        var projectedQuery = mapper.ProjectTo<UserDetailModel>(query);
        return await projectedQuery.FirstOrDefaultAsync(e => e.RealUserUrl == userId);
    }
}