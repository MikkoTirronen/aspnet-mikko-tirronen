
using System.Runtime.InteropServices;
using Application.Abstractions.Services;
using Domain.Entities;

namespace Application.Features.Memberships.Services;

public class MembershipPlanService : IMembershipPlanService
{
    public static List<MembershipPlan> All =>
[
    new()
    {
        MembershipType = Domain.Enums.MembershipType.Basic,
        Name = "Standard Membership",
        Price = 299,
        Description = "Perfect for getting started with access to the gym and basic training support.",
        Features =
        [
            "Gym Access",
            "Fitness Assessment",
            "General Workout Plan",
            "Locker Room Access"
        ]
    },

    new()
    {
        MembershipType = Domain.Enums.MembershipType.Premium,
        Name = "Premium Membership",
        Price = 499,
        Description = "Best choice for members who want classes, guidance and extra support.",
        Features =
        [
            "Everything in Standard",
            "Book Group Classes",
            "Priority Class Booking",
            "Personal Training Support",
            "Locker Access"
        ]
    }
];

    public List<MembershipPlan> GetPlans()
    {
        return All;
    }
}
