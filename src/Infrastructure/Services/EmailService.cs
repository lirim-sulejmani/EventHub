using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Interfaces;
using Carmax.Application.Common.Models;
using Carmax.Domain.Entities;
using Carmax.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Razor.Language.Extensions;
using System.Net.Mime;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Hosting;
using Carmax.Domain.Enums;
using static Duende.IdentityServer.Models.IdentityResources;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Carmax.Infrastructure.Services;
public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;
    private readonly ApplicationDbContext _context;
    private readonly IHostingEnvironment _hostEnvironment;
    private readonly ITemplateService _templateService;
    public EmailService(ApplicationDbContext context, IConfiguration configuration, ITemplateService templateService, IHostingEnvironment hostEnvironment)
    {
        _context = context;
        _configuration = configuration;
        _templateService = templateService;
        _hostEnvironment = hostEnvironment;
    }

 
    public async Task<ResponseDto> SendContactUsMessage(EmailTemplateModel data)
    {

        MailMessage message = new MailMessage();
        SmtpClient smtp = new SmtpClient();
        message.From = new MailAddress(_configuration.GetValue<string>("Email:Username"));
        message.To.Add(new MailAddress(data.Email));
        message.Subject = "Thank you for contacting us!";
        message.BodyEncoding = System.Text.Encoding.UTF8;
        var body = _templateService.GenerateEmailTemplate("MessageSentTemplate", data);
        var iconResource = GetLogo();
        body = body.Replace("{{imageSrc}}", "cid:" + iconResource.ContentId);
        message.IsBodyHtml = true; //to make message body as html  
        smtp.Port = 587;
        smtp.Host = "smtp.gmail.com"; //for gmail host  
        smtp.EnableSsl = true;
        smtp.UseDefaultCredentials = false;
        smtp.Credentials = new NetworkCredential(_configuration.GetValue<string>("Email:Username"), _configuration.GetValue<string>("Email:Password"));
        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        AlternateView alternativeView = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);
        alternativeView.LinkedResources.Add(iconResource);
        message.AlternateViews.Add(alternativeView);
        smtp.Send(message);
        return await Task.FromResult(new ResponseDto { Success = true, Message = "Email was sent!" });
    }

    public async Task<ResponseDto> SendApprovedMessage(EmailTemplateModel data)
    {
        MailMessage message = new MailMessage();
        SmtpClient smtp = new SmtpClient();
        message.From = new MailAddress(_configuration.GetValue<string>("Email:Username"));
        message.To.Add(new MailAddress(data.Email));
        message.Subject = "Your Carmax account has been approved!";
        message.BodyEncoding = System.Text.Encoding.UTF8;
        var body = _templateService.GenerateEmailTemplate("ApproveClientTemplate", data);
        var iconResource = GetLogo();
        body = body.Replace("{{imageSrc}}", "cid:" + iconResource.ContentId);
        var redirectUrl = _configuration.GetValue<string>("RedirectUrls:ToLogin");
        body = body.Replace("{{redirectUrl}}", redirectUrl);
        message.IsBodyHtml = true;
        smtp.Port = 587;
        smtp.Host = "smtp.gmail.com";
        smtp.EnableSsl = true;
        smtp.UseDefaultCredentials = false;
        smtp.Credentials = new NetworkCredential(_configuration.GetValue<string>("Email:Username"), _configuration.GetValue<string>("Email:Password"));
        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        AlternateView alternativeView = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);
        alternativeView.LinkedResources.Add(iconResource);
        message.AlternateViews.Add(alternativeView);
        smtp.Send(message);
        return await Task.FromResult(new ResponseDto { Success = true, Message = "Email was sent!" });
    }

    public async Task<ResponseDto> SendRefusedMessage(EmailTemplateModel data)
    {
        MailMessage message = new MailMessage();
        SmtpClient smtp = new SmtpClient();
        message.From = new MailAddress(_configuration.GetValue<string>("Email:Username"));
        message.To.Add(new MailAddress(data.Email));
        message.Subject = "Your Carmax account has been refused!";
        message.BodyEncoding = System.Text.Encoding.UTF8;
        var body = _templateService.GenerateEmailTemplate("RefuseClientTemplate", data);
        var iconResource = GetLogo();
        body = body.Replace("{{imageSrc}}", "cid:" + iconResource.ContentId);
        message.IsBodyHtml = true;
        smtp.Port = 587;
        smtp.Host = "smtp.gmail.com";
        smtp.EnableSsl = true;
        smtp.UseDefaultCredentials = false;
        smtp.Credentials = new NetworkCredential(_configuration.GetValue<string>("Email:Username"), _configuration.GetValue<string>("Email:Password"));
        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        AlternateView alternativeView = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);
        alternativeView.LinkedResources.Add(iconResource);
        message.AlternateViews.Add(alternativeView);
        smtp.Send(message);
        return await Task.FromResult(new ResponseDto { Success = true, Message = "Email was sent!" });
    }


    public async Task<ResponseDto> SendResetPasswordEmail(EmailTemplateModel data)
    {
        MailMessage message = new MailMessage();
        SmtpClient smtp = new SmtpClient();
        var url = data.IsUser ? _configuration.GetValue<string>("RedirectUrls:ToUserResetPassword") : _configuration.GetValue<string>("RedirectUrls:ToClientResetPassword");
        message.From = new MailAddress(_configuration.GetValue<string>("Email:Username"));
        message.To.Add(new MailAddress(data.Email));
        message.Subject = "Carmax - Password Reset";
        message.BodyEncoding = System.Text.Encoding.UTF8;
        var body = _templateService.GenerateEmailTemplate("ResetPasswordTemplate", data);
        var iconResource = GetLogo();
        body = body.Replace("{{imageSrc}}", "cid:" + iconResource.ContentId);
        body = body.Replace("{{redirectUrl}}", url.Replace("{{token}}", data.Token));
        message.IsBodyHtml = true;
        smtp.Port = 587;
        smtp.Host = "smtp.gmail.com";
        smtp.EnableSsl = true;
        smtp.UseDefaultCredentials = false;
        smtp.Credentials = new NetworkCredential(_configuration.GetValue<string>("Email:Username"), _configuration.GetValue<string>("Email:Password"));
        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        AlternateView alternativeView = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);
        alternativeView.LinkedResources.Add(iconResource);
        message.AlternateViews.Add(alternativeView);
        smtp.Send(message);
        return await Task.FromResult(new ResponseDto { Success = true, Message = "Email was sent!" });
    }

    private LinkedResource GetLogo()
    {
        var path = _hostEnvironment.ContentRootPath + _configuration.GetValue<string>("Email:Logo");
        byte[] imageArray = System.IO.File.ReadAllBytes(path);
        string base64ImageRepresentation = Convert.ToBase64String(imageArray);
        System.IO.MemoryStream iconBitmap = new System.IO.MemoryStream(imageArray);
        LinkedResource iconResource = new LinkedResource(iconBitmap, "image/png");
        iconResource.ContentId = "Logo";
        return iconResource;
    }
}