using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Models;
using Carmax.Domain.Entities;

namespace Carmax.Application.Common.Interfaces;
public interface IEmailService
{
    Task<ResponseDto> SendResetPasswordEmail(EmailTemplateModel data);
    Task<ResponseDto> SendContactUsMessage(EmailTemplateModel data);
    Task<ResponseDto> SendApprovedMessage(EmailTemplateModel data);
    Task<ResponseDto> SendRefusedMessage(EmailTemplateModel data);
}
