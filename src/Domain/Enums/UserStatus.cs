using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carmax.Domain.Enums;
public enum UserStatus
{
    NotVerified = -1,
    Pending = 1,
    Approved = 2,
    Refused = 3,
}
