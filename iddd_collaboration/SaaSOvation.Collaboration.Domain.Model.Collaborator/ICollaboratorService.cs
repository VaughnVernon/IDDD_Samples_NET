namespace iddd_collaboration.SaaSOvation.Collaboration.Domain.Model.Collaborator
{
    public interface ICollaboratorService
    {
        Author AuthorFrom(Tenant.Tenant aTenant, string anIdentity);
        Creator CreatorFrom(Tenant.Tenant aTenant, string anIdentity);
        Moderator ModeratorFrom(Tenant.Tenant aTenant, string anIdentity);
        Owner OwnerFrom(Tenant.Tenant aTenant, string anIdentity);
        Participant ParticipantFrom(Tenant.Tenant aTenant, string anIdentity);
    }
}
