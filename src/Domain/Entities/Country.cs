using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carmax.Domain.Entities;
public class Country
{
    public Guid Id { get; set; }

    public string CountryName { get; set; }

    public int CountryCode { get; set; }

    public string Continent { get; set; }

    public string Capital { get; set; }

    public virtual ICollection<Event> Events { get; set; }

}
