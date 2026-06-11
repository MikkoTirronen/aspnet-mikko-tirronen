using Domain.Entities;

namespace Infrastructure.Persistence.Seed;

public class GymClassSeeder
{
    public static void Seed(AppDbContext db)
    {
        if (db.GymClasses.Any())
            return;

        db.GymClasses.AddRange(
            new GymClass("Yoga Flow", "Anna Svensson", new DateTime(2026, 6, 12, 8, 0, 0), 20, "Yoga"),
            new GymClass("HIIT Blast", "Mark Johnson", new DateTime(2026, 6, 12, 17, 30, 0), 15, "HIIT"),
            new GymClass("Spinning Power", "Lisa Karlsson", new DateTime(2026, 6, 13, 12, 0, 0), 25, "Cardio"),
            new GymClass("Pilates Core", "Emma Lind", new DateTime(2026, 6, 13, 10, 0, 0), 18, "Pilates"),
            new GymClass("Strength Basics", "David Nilsson", new DateTime(2026, 6, 14, 18, 0, 0), 12, "Strength")
        );

        db.SaveChanges();
    }
}