using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Interfaces;
using Carmax.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Carmax.Infrastructure.Services;
public class TenantResolver : ITenantResolver
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public TenantResolver(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    public Tenant? GetCurrentTenant(IEnumerable<Tenant> tenants)
    {

        if (_httpContextAccessor.HttpContext == null)
            return null;
        var hasTenant = _httpContextAccessor.HttpContext.Request.Headers.TryGetValue("tenant", out var tenantId);
        if (!hasTenant)
            return null;
        if (tenantId == "")
            throw new UnauthorizedAccessException("Authentication failed");
        var tenant = tenants.FirstOrDefault(t => t.Id == new Guid(tenantId));
        if (tenant is null)
            return null;
            //tenant = tenants.FirstOrDefault(t => t.SubDomain == tenantId);
        if (tenant is null)
            throw new UnauthorizedAccessException($"Tenant '{tenantId}' is not registered.");
        return tenant;
    }

    public string? GetCurrentTenantId()
    {
        if (_httpContextAccessor.HttpContext == null)
            return null;
        var hasTenant = _httpContextAccessor.HttpContext.Request.Headers.TryGetValue("tenant", out var tenantId);
        if (!hasTenant)
            return null;
        if (tenantId == "")
            throw new UnauthorizedAccessException("Authentication failed");
        return tenantId;
    }
}
