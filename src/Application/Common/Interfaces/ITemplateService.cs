using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carmax.Application.Common.Interfaces;
public interface ITemplateService
{
    string GenerateEmailTemplate<T>(string templateName, T mailTemplateModel);
}
