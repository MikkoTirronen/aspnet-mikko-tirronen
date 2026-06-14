using Application.Features.GymClasses.CreateGymClass;
using Application.Features.GymClasses.DeleteGymClass;
using Application.Features.GymClasses.UpdateGymClass;
using FluentAssertions;
using Infrastructure.Repositories;

namespace Integration.Tests;

public class AdminGymClassIntegrationTests
{
    [Fact]
    public async Task CreateGymClass_Should_Save_Class_To_Database()
    {
        await using var db = TestDbContextFactory.Create();

        var classRepo = new GymClassRepository(db);
        var handler = new CreateGymClassHandler(classRepo, db);

        var id = await handler.Handle(
            new CreateGymClassCommand(
                "Boxing",
                "Alex",
                DateTime.UtcNow.AddDays(2),
                12,
                "Combat"),
            CancellationToken.None);

        id.Should().NotBe(Guid.Empty);

        db.GymClasses.Should().ContainSingle();

        var savedClass = db.GymClasses.Single();

        savedClass.Name.Should().Be("Boxing");
        savedClass.Instructor.Should().Be("Alex");
        savedClass.Capacity.Should().Be(12);
        savedClass.Category.Should().Be("Combat");
    }

    [Fact]
    public async Task UpdateGymClass_Should_Change_Class_Details()
    {
        await using var db = TestDbContextFactory.Create();

        var classRepo = new GymClassRepository(db);
        var createHandler = new CreateGymClassHandler(classRepo, db);
        var updateHandler = new UpdateGymClassHandler(classRepo, db);

        var id = await createHandler.Handle(
            new CreateGymClassCommand(
                "Yoga",
                "Anna",
                DateTime.UtcNow.AddDays(1),
                10,
                "Group Training"),
            CancellationToken.None);

        var result = await updateHandler.Handle(
            new UpdateGymClassCommand(
                id,
                "Power Yoga",
                "Mikko",
                DateTime.UtcNow.AddDays(3),
                15,
                "Strength"),
            CancellationToken.None);

        result.Success.Should().BeTrue();

        var updatedClass = db.GymClasses.Single(x => x.Id == id);

        updatedClass.Name.Should().Be("Power Yoga");
        updatedClass.Instructor.Should().Be("Mikko");
        updatedClass.Capacity.Should().Be(15);
        updatedClass.Category.Should().Be("Strength");
    }

    [Fact]
    public async Task DeleteGymClass_Should_Remove_Class_From_Database()
    {
        await using var db = TestDbContextFactory.Create();

        var classRepo = new GymClassRepository(db);
        var createHandler = new CreateGymClassHandler(classRepo, db);
        var deleteHandler = new DeleteGymClassHandler(classRepo, db);

        var id = await createHandler.Handle(
            new CreateGymClassCommand(
                "Padel",
                "Sara",
                DateTime.UtcNow.AddDays(1),
                8,
                "Padel"),
            CancellationToken.None);

        db.GymClasses.Should().ContainSingle();

        var result = await deleteHandler.Handle(
            new DeleteGymClassCommand(id),
            CancellationToken.None);

        result.Success.Should().BeTrue();

        db.ChangeTracker.Clear();

        db.GymClasses.Should().BeEmpty();
    }

    [Fact]
    public async Task UpdateGymClass_Should_Fail_When_Class_Does_Not_Exist()
    {
        await using var db = TestDbContextFactory.Create();

        var classRepo = new GymClassRepository(db);
        var updateHandler = new UpdateGymClassHandler(classRepo, db);

        var result = await updateHandler.Handle(
            new UpdateGymClassCommand(
                Guid.NewGuid(),
                "Missing Class",
                "No Instructor",
                DateTime.UtcNow.AddDays(1),
                10,
                "Unknown"),
            CancellationToken.None);

        result.Success.Should().BeFalse();
        result.Error.Should().Be("Class not found.");
    }
}