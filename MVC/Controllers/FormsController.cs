using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Cms.Web.Website.ActionResults;
using Umbraco.Cms.Web.Website.Controllers;

namespace ROCNSBE.MVC.Controllers
{
  [Route("/api/v1/auth")]
  public class AuthApiController : UmbracoApiController
  {

    [Route("callback")]
    [HttpGet]
    public async Task<IActionResult> Callback(string? code, string? error, [FromQuery(Name = "error_reason")] string? errorReason, [FromQuery(Name = "error_description")] string? errorDescription)
    {
      return await Task.FromResult(Ok());
    }

  }

  public partial class FormsController : SurfaceController
  {
    private readonly ILogger<FormsController> _logger;

    public FormsController(ILogger<FormsController> logger, IUmbracoContextAccessor umbracoContextAccessor, IUmbracoDatabaseFactory databaseFactory, ServiceContext services, AppCaches appCaches, IProfilingLogger profilingLogger, IPublishedUrlProvider publishedUrlProvider) : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
    {
      _logger = logger;
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [AllowAnonymous]
    public IActionResult ContactForm(ContactFormModel model)
    {
      if (!ModelState.IsValid)
      {
        return CurrentUmbracoPage();
      }

      _logger.LogInformation($"Successful contactForm submission from {model.Email}");

      TempData["ContactForm"] = true;
      return RedirectToCurrentUmbracoPage();

    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [AllowAnonymous]
    public IActionResult MailingListForm(MailingListFormModel model)
    {

      IPublishedContent? joinMailingList = null;
      var contentType = UmbracoContext.Content!.GetContentType("joinMailingList");

      if (contentType != null)
      {
        joinMailingList = UmbracoContext.Content!.GetByContentType(contentType).FirstOrDefault();
      }


      if (!ModelState.IsValid)
      {
        return CurrentUmbracoPage();
      }

      _logger.LogInformation($"MailingListForm submission from {model.Email}");

      TempData["MailingListForm"] = true;
      return RedirectToCurrentUmbracoPage();
    }
  }



}
