
using System.Linq.Expressions;
using System.Security.Claims;
using FlashCards.Api.Bl.Facades.Interfaces;
using FlashCards.Api.Dal.Entities;
using FlashCards.Common.Models.Details;
using FlashCards.Common.Models.Lists;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlashCards.Api.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserFacade facade)
        : ControllerBase<UserEntity, UserListModel, UserDetailModel>(facade)
    {
        protected override Expression<Func<UserEntity, bool>> CreateFilter(string? strFilterAtrib, string? strFilter)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            Expression<Func<UserEntity, bool>> filter = l => l.RealUserUrl == userId;
            if (!string.IsNullOrEmpty(strFilter) && !string.IsNullOrEmpty(strFilterAtrib) && strFilterAtrib.Split(FilterArgumentSpitChar).Length == strFilter.Split(FilterArgumentSpitChar).Length)
            {
                for (int i = 0; i < strFilterAtrib.Split(FilterArgumentSpitChar).Length; i++)
                {
                    var atrib = strFilterAtrib.Split(FilterArgumentSpitChar)[i];
                    var str = strFilter.Split(FilterArgumentSpitChar)[i];
                    switch (atrib)
                    {
                        case nameof(UserEntity.Name):
                            filter = ExpressionAnd(filter, l => l.Name != null && l.Name.ToLower().Contains(str.ToLower()));
                            break;
                        case nameof(UserEntity.Role):
                            filter = ExpressionAnd(filter, l => l.Role.ToString().ToLower().Contains(str.ToLower()));
                            break;
                    }
                }
            }
            return filter;
        }

        protected override Func<IQueryable<UserEntity>, IOrderedQueryable<UserEntity>> CreateOrderBy(string? strSortBy, bool sortDesc)
        {
            Func<IQueryable<UserEntity>, IOrderedQueryable<UserEntity>> orderBy = l => l.OrderBy(s => s.Id);
            switch (strSortBy)
            {
                case nameof(UserEntity.Name):
                    orderBy = sortDesc 
                        ? l => l.OrderBy(s => s.Name) 
                        : l => l.OrderByDescending(s => s.Name);
                    break;
                case nameof(UserEntity.Role):
                    orderBy = sortDesc 
                        ? l => l.OrderBy(s => s.Role) 
                        : l => l.OrderByDescending(s => s.Role);
                    break;
            }
            return orderBy;
        }
        
        // [Authorize(Policy = "AdminRole")]
        [Authorize]
        public override async Task<ActionResult<UserDetailModel>> Post(UserDetailModel model)
        {
            model.Id = Guid.Empty;
            var result = await facade.SaveAsync(model);
            return Ok(result);
        }
    }
}
