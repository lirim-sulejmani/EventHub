using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Interfaces;
using Microsoft.AspNetCore.Hosting;
using RazorEngineCore;
namespace Carmax.Infrastructure.Services;
public class TemplateService : ITemplateService
{
    private readonly IHostingEnvironment _hostEnvironment;
    public TemplateService(IHostingEnvironment hostEnvironment)
    {
        _hostEnvironment = hostEnvironment;
    }
    public string GenerateEmailTemplate<T>(string templateName, T mailTemplateModel)
    {
        string template = GetTemplate(templateName);
        IRazorEngine razorEngine = new RazorEngine();
        IRazorEngineCompiledTemplate modifiedTemplate = razorEngine.Compile(template);
        return modifiedTemplate.Run(mailTemplateModel);
    }
    private string GetTemplate(string templateName)
    {
        string tmplFolder = Path.Combine(_hostEnvironment.ContentRootPath, "Templates");
        string filePath = Path.Combine(tmplFolder, $"{templateName}.cshtml");
        using var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        using var sr = new StreamReader(fs, Encoding.Default);
        string mailText = sr.ReadToEnd();
        sr.Close();
        return mailText;
    }
}
