using System.Linq.Expressions;

namespace FlashCards.Api.Bl.Facades.Interfaces;

public interface IFacade<TEntity, TQueryObject, TListModel, TDetailModel>
{
    public Task<IQueryable<TListModel>> GetAsync(TQueryObject queryObject);
    public Task<TDetailModel?> GetByIdAsync(Guid id);
    public Task<TDetailModel> SaveAsync(TDetailModel model);
    public Task<TDetailModel?> DeleteAsync(Guid entityId);
    public Task<int> GetCountAsync(Expression<Func<TEntity, bool>>? filter = null);
}