using Carmax.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Carmax.Application.Common.Interfaces;

public interface IApplicationDbContext
{

   DbSet<Tenant> Tenants { get; }
    DbSet<Event> Events { get; }
    DbSet<User> Users { get; }

    DbSet<Invitation> Invitations { get; }
    DbSet<SMTPConfig> SMTPConfigs { get; }
    DbSet<Template> Templates { get; }
    DbSet<UserInvite> UserInvites { get; }
    DbSet<Country> Countries { get; }
    DbSet<Agenda> Agendas { get; }
    DbSet<AgendasSpeaker> AgendasSpeakers { get; }
    DbSet<AgendaType> AgendaTypes { get; }
    DbSet<SocialMedia> SocialMedias { get; }
    DbSet<Speaker> Speakers { get; }
    DbSet<SpeakerType> SpeakerTypes { get; }
    object UserStatus { get; }
    object UserRole { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
