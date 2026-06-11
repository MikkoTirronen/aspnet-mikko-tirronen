
using System.Runtime.InteropServices;
using Application.Abstractions.Services;
using Domain.Entities;

namespace Application.Features.Memberships.Services;

public class MembershipPlanService : IMembershipPlanService
{
    public static List<MembershipPlan> All =>
    [
        new() { Name = "Basic", Price = 299, Description = "Perfect for getting started.", Features= ["Gym Access", "Book Classes"] },
        new() { Name = "Premium", Price = 499, Description = "Most popular plan.", Features=["Gym Access", "Book Classes", "Locker"] },
        new() { Name = "Elite", Price = 799, Description = "Everything included.", Features=["Gym Access", "Priority Classes Booking", "Locker",  "Towel and Shower slippers", "Personal Trainer"] }
    ];

    public List<MembershipPlan> GetPlans()
    {
        return All;
    }
}
