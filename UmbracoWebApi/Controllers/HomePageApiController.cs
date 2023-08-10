using EmailSenderApp.Site;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using UmbracoWebApi.Models;

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
		public async Task<IActionResult> SendEmail([FromBody] User user)
		{
			ResponseObject<bool> response = new ResponseObject<bool>();
			response.response = emailService.sendEmail(user);			

			return Ok(response);
		}
	}
}