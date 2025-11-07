using FlashCards.Api.Dal.Entities;
using FlashCards.Api.Dal.Entities.InterfacesOrAbstracts;
using FlashCards.Common.Models.Details;
using FlashCards.Common.Models.Lists;

namespace FlashCards.Api.Bl.Facades.Interfaces;

public interface ITagFacade: IFacade<TagEntity, TagListModel, TagDetailModel>
{
    
}