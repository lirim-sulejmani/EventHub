using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Domain.Entities;

namespace Carmax.Application.Common.Interfaces;

public interface ITenantResolver
{
    Tenant? GetCurrentTenant(IEnumerable<Tenant> tenants);
    string? GetCurrentTenantId();
}

