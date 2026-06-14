using Application.Abstractions.Commands;
using Application.Abstractions.Queries;
using Application.Features.GymClasses.CreateGymClass;
using Application.Features.GymClasses.DeleteGymClass;
using Application.Features.GymClasses.GetAllClasses;
using Application.Features.GymClasses.GetClassDetails;
using Application.Features.GymClasses.GetGymClassById;
using Application.Features.GymClasses.UpdateGymClass;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.WebApp.Models;

namespace Presentation.WebApp.Controllers;

[Authorize(Roles = "Admin")]
public class AdminClassesController : Controller
{
    private readonly IQueryDispatcher _queries;
    private readonly ICommandDispatcher _commands;

    public AdminClassesController(
        IQueryDispatcher queries,
        ICommandDispatcher commands)
    {
        _queries = queries;
        _commands = commands;
    }

    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken ct)
    {
        var classes = await _queries.Send(new GetAllClassesQuery(), ct);
        return View(classes);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new AdminGymClassFormViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        AdminGymClassFormViewModel model,
        CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return View(model);

        await _commands.Send(
            new CreateGymClassCommand(
                model.Name,
                model.Instructor,
                model.StartTime,
                model.Capacity,
                model.Category),
            ct);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id, CancellationToken ct)
    {
        var gymClass = await _queries.Send(
            new GetGymClassByIdQuery(id),
            ct);

        if (gymClass is null)
            return NotFound();

        var vm = new AdminGymClassFormViewModel
        {
            Id = gymClass.Id,
            Name = gymClass.Name,
            Instructor = gymClass.Instructor,
            StartTime = gymClass.StartTime,
            Capacity = gymClass.Capacity,
            Category = gymClass.Category
        };

        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        AdminGymClassFormViewModel model,
        CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return View(model);

        if (model.Id is null)
            return BadRequest();

        var result = await _commands.Send(
            new UpdateGymClassCommand(
                model.Id.Value,
                model.Name,
                model.Instructor,
                model.StartTime,
                model.Capacity,
                model.Category),
            ct);

        if (!result.Success)
        {
            ModelState.AddModelError("", result.Error ?? "Could not update class.");
            return View(model);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var result = await _commands.Send(
            new DeleteGymClassCommand(id),
            ct);

        if (!result.Success)
        {
            TempData["Error"] = result.Error;
        }

        return RedirectToAction(nameof(Index));
    }
}