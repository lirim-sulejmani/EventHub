using System;
using System.Reflection;
using System.Reflection.Emit;
using Carmax.Application.Common.Interfaces;
using Carmax.Domain.Entities;
using Carmax.Infrastructure.Identity;
using Carmax.Infrastructure.Persistence.Configurations;
using Carmax.Infrastructure.Persistence.Interceptors;
using Duende.IdentityServer.EntityFramework.Options;
using MediatR;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using static IdentityModel.OidcConstants;

namespace Carmax.Infrastructure.Persistence;

public class ApplicationDbContext : BaseDbContext, IApplicationDbContext
{
    private readonly IMediator _mediator;
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

    //public ApplicationDbContext(
    //    DbContextOptions<ApplicationDbContext> options,
    //    IMediator mediator,
    //    AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor)
    //    : base(options)
    //{
    //    _mediator = mediator;
    //    _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
    //}

    public ApplicationDbContext(DbContextOptions<BaseDbContext> options,ITenantResolver tenantResolver) : base(options,tenantResolver)
    {
        
    }

    public DbSet<Event> Events => Set<Event>();
    public DbSet<Invitation> Invitations => Set<Invitation>();
    public DbSet<User> Users => Set<User>();
    public DbSet<SMTPConfig> SMTPConfigs => Set<SMTPConfig>();
    public DbSet<Template> Templates => Set<Template>();
    public DbSet<UserInvite> UserInvites => Set<UserInvite>();
    public DbSet<Country> Countries => Set<Country>();
    public DbSet<Agenda> Agendas => Set<Agenda>();
    public DbSet<Speaker> Speakers => Set<Speaker>();
    public DbSet<AgendasSpeaker> AgendasSpeakers => Set<AgendasSpeaker>();
    public DbSet<AgendaType> AgendaTypes => Set<AgendaType>();
    public DbSet<SocialMedia> SocialMedias => Set<SocialMedia>();
    public DbSet<SpeakerType> SpeakerTypes => Set<SpeakerType>();

    public object UserStatus => throw new NotImplementedException();
    public object UserRole => throw new NotImplementedException();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        //builder.Entity<User>().ToTable(nameof(Users), t => t.ExcludeFromMigrations());
        builder.ApplyConfiguration(new EventConfiguration());
        builder.ApplyConfiguration(new TenantConfiguration());
        builder.ApplyConfiguration(new InvitationConfiguration());
        builder.ApplyConfiguration(new SMTPConfigConfiguration());
        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new TemplateConfiguration());
        builder.ApplyConfiguration(new UserInviteConfiguration());
        builder.ApplyConfiguration(new CountryConfiguration());



        base.OnModelCreating(builder);

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
        optionsBuilder.EnableSensitiveDataLogging();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.DispatchDomainEvents(this);
        return await base.SaveChangesAsync();
    }
}
