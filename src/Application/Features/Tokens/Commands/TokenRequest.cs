using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Domain.Enums;

namespace Carmax.Application.Features.Tokens.Commands;
public record TokenRequest(string Email, string Password);
