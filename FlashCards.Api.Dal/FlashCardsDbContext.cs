using FlashCards.Api.Dal.Entities;
using FlashCards.Api.Dal.Entities.InterfacesOrAbstracts;
using FlashCards.Common.Enums;
using Microsoft.EntityFrameworkCore;

namespace FlashCards.Api.Dal;

public class FlashCardsDbContext(DbContextOptions<FlashCardsDbContext> options) : DbContext(options)
{
	public DbSet<CardEntity> Card { get; set; }
	public DbSet<CollectionEntity> Collection { get; set; }
	public DbSet<RecordEntity> Record { get; set; }
	public DbSet<UserEntity> User { get; set; }
	public DbSet<TagEntity> Tag { get; set; }
	public DbSet<AttemptEntity> Attempt { get; set; }
	public DbSet<FilterEntity> Filter { get; set; }
	public DbSet<CollectionTagEntity> CollectionTag { get; set; }
	public DbSet<FilterTagEntity> FilterTag { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		modelBuilder.Entity<RecordEntity>()
			.HasOne(e => e.User)
			.WithMany(e => e.Records)
			.HasForeignKey(e => e.UserId)
			.OnDelete(DeleteBehavior.Restrict);

		modelBuilder.Entity<CollectionEntity>()
			.HasMany(collection => collection.Cards)
			.WithOne(card => card.Collection)
			.HasForeignKey(card => card.CardCollectionId)
			.OnDelete(DeleteBehavior.Cascade);

		modelBuilder.Entity<AttemptEntity>()
			.HasOne(la => la.Record)
			.WithMany(cl => cl.Attempts)
			.HasForeignKey(la => la.RecordId)
			.OnDelete(DeleteBehavior.Restrict);
	}
}