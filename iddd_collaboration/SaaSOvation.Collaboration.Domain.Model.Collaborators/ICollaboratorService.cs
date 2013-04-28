namespace SaaSOvation.Collaboration.Domain.Model.Collaborators
{
    using SaaSOvation.Collaboration.Domain.Model.Tenants;

    public interface ICollaboratorService
    {
        Author AuthorFrom(Tenant tenant, string identity);

        Creator CreatorFrom(Tenant tenant, string identity);

        Moderator ModeratorFrom(Tenant tenant, string identity);

        Owner OwnerFrom(Tenant enant, string identity);

        Participant ParticipantFrom(Tenant tenant, string identity);
    }
}
