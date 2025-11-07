using System.Linq.Expressions;
using FlashCards.Api.App.Controllers;
using FlashCards.Api.Bl.Facades.Interfaces;
using FlashCards.Api.Dal.Entities;
using FlashCards.Common.Models.Details;
using FlashCards.Common.Models.Lists;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using FlashCards.Api.Dal.Entities.InterfacesOrAbstracts;

namespace FlashCards.Api.App.Controllers;

[ApiController]
[Route("api/Tag")]
public class TagController(ITagFacade facade) 
    : ControllerBase<TagEntity, TagListModel, TagDetailModel>(facade)
{
    protected override Expression<Func<TagEntity, bool>> CreateFilter(string? strFilterAtrib, string? strFilter)
    {
        Expression<Func<TagEntity, bool>> filter = l => true; 
        
        if (!string.IsNullOrEmpty(strFilter))
        {
            switch (strFilterAtrib)
            {
                case nameof(TagEntity.Tag):
                    filter = l => l.Tag.ToLower().Contains(strFilter.ToLower());
                    break;
            }
        }
        return filter;
    }

    protected override Func<IQueryable<TagEntity>, IOrderedQueryable<TagEntity>> CreateOrderBy(string? strSortBy, bool sortDesc)
    {
        sortDesc = !sortDesc;
        Func<IQueryable<TagEntity>, IOrderedQueryable<TagEntity>> orderBy = l => l.OrderBy(s => s.Id);
        
        switch (strSortBy)
        {
            case nameof(TagEntity.Tag):
                orderBy = sortDesc
                    ? l => l.OrderByDescending(s => s.Tag)
                    : l => l.OrderBy(s => s.Tag);
                break;
        }
        return orderBy;
    }
}