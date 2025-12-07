using AutoMapper;
using FlashCards.Api.Bl.Facades.Interfaces;
using FlashCards.Api.Dal;
using FlashCards.Api.Dal.Entities;
using FlashCards.Common.Models.Details;
using FlashCards.Common.Models.Lists;
using FlashCards.Common.QueryObjects;

namespace FlashCards.Api.Bl.Facades;

public class AttemptFacade(FlashCardsDbContext dbContext, IMapper mapper)
    : FacadeBase<AttemptEntity, AttemptQueryObject, AttemptListModel, AttemptDetailModel>(dbContext, mapper),
        IAttemptFacade
{
    protected override Task<IQueryable<AttemptEntity>> CreateFilterQuery(AttemptQueryObject queryObject, IQueryable<AttemptEntity> query)
    {
        return Task.FromResult(query);
    }

    protected override IQueryable<AttemptEntity> CreateOrderQuery(AttemptQueryObject queryObject, IQueryable<AttemptEntity> query)
    {
        return query;
    }

    protected override AttemptEntity SavaDetail(AttemptEntity detail)
    {
        return detail;
    }

    protected override AttemptEntity ModifyDetail(AttemptEntity detail)
    {
        return detail;
    }
}