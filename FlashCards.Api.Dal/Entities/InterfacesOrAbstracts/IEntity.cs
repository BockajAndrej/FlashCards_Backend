using System.ComponentModel.DataAnnotations;

namespace FlashCards.Api.Dal.Entities.InterfacesOrAbstracts;

public interface IEntity
{
    [Key]
    public Guid Id { get; set; }
}