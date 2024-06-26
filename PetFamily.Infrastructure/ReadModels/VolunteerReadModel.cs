namespace PetFamily.Infrastructure.ReadModels;

public class VolunteerReadModel
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public int YearsExperience { get; init; }
    public int? NumberOfPetsFoundHome { get; init; }
    public string DonationInfo { get; init; } = string.Empty;
    public bool FromShelter { get; init; }
    public List<VolunteerPhotoReadModel> Photos { get; init; } = [];
    public List<SocialMediaReadModel> SocialMedias { get; init; } = [];
    public List<PetReadModel> Pets { get; init; } = [];
}