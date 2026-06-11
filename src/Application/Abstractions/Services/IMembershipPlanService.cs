using Application.Common.DTOs;
using Domain.Entities;

namespace Application.Abstractions.Services;

public interface IMembershipPlanService
{
    List<MembershipPlan> GetPlans();
}