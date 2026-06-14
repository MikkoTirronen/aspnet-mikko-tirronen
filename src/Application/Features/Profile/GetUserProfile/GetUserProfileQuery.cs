using Application.Abstractions.Queries;

namespace Application.Features.Profile.GetUserProfile;
public record GetUserProfileQuery(string UserId)
    : IQuery<UserProfileDto>;