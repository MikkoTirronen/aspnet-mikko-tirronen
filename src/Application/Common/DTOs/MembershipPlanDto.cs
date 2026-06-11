namespace Application.Common.DTOs;

public record MembershipPlanDto(string Name, decimal Price, string Description, List<string> Features);