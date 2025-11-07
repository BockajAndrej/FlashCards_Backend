using FlashCards.Api.Dal.Entities;
using FlashCards.Api.Dal.Entities.InterfacesOrAbstracts;
using FlashCards.Common.Enums;
using Microsoft.EntityFrameworkCore;

namespace FlashCards.Api.Dal;

public class FlashCardsDbContext(DbContextOptions<FlashCardsDbContext> options) : DbContext(options)
{
	public DbSet<CardEntity> Card { get; set; }
	public DbSet<CardCollectionEntity> Collection { get; set; }
	public DbSet<CompletedLessonEntity> CompletedLesson { get; set; }
	public DbSet<UserEntity> User { get; set; }
	public DbSet<TagEntity> Tag { get; set; }
	public DbSet<LessonAttemptEntity> LessonAttempt { get; set; }


	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		modelBuilder.Entity<CompletedLessonEntity>()
			.HasOne(e => e.User)
			.WithMany(e => e.CompletedLessons)
			.HasForeignKey(e => e.UserId)
			.OnDelete(DeleteBehavior.Restrict);

		modelBuilder.Entity<CardCollectionEntity>()
			.HasMany(collection => collection.Cards)
			.WithOne(card => card.CardCollection)
			.HasForeignKey(card => card.CardCollectionId)
			.OnDelete(DeleteBehavior.Cascade);

		modelBuilder.Entity<CardCollectionEntity>()
			.HasMany(c => c.Tags)
			.WithMany(t => t.CardCollections)
			.UsingEntity(j => j.ToTable("CardCollectionTag"));

		modelBuilder.Entity<LessonAttemptEntity>()
			.HasOne(la => la.CompletedLesson)
			.WithMany(cl => cl.LessonAttempts)
			.HasForeignKey(la => la.CompletedLessonId)
			.OnDelete(DeleteBehavior.Restrict);
	}
}