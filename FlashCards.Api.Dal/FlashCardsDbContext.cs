using FlashCards.Api.Dal.Entities;
using FlashCards.Common.Enums;
using Microsoft.EntityFrameworkCore;

namespace FlashCards.Api.Dal;

public class FlashCardsDbContext(DbContextOptions<FlashCardsDbContext> options) : DbContext(options)
{
    public DbSet<CardEntity> Card { get; set; }
    public DbSet<CardCollectionEntity> Collection { get; set; }
    public DbSet<CompletedLessonEntity> CompletedLesson { get; set; }
}