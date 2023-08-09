using EmailSenderApp.Site;
using Microsoft.AspNetCore.Mvc;

namespace UmbracoWebApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class HomePageApiController : ControllerBase
	{	

		private readonly ILogger<HomePageApiController> _logger;
		private readonly IemailService emailService;

		public HomePageApiController(ILogger<HomePageApiController> logger, IemailService emailService)
		{
			this.emailService = emailService;
		}

		[HttpPost]
		public async Task<IActionResult> SendEmail(User user)
		{
			bool result = emailService.sendEmail(user.email, user.message, user.subject);
			return Ok(result);
		}
	}
}