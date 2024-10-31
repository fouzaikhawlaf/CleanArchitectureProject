using CleanArchitecture.UseCases.InterfacesUse;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        public IActionResult SendEmail([FromBody] SendEmailRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.ToAddress) || string.IsNullOrEmpty(request.Subject) || string.IsNullOrEmpty(request.Body))
            {
                return BadRequest("Invalid email request");
            }

            _emailService.SendEmail(request.ToAddress, request.Subject, request.Body);
            return Ok("Success");
        }





        public class SendEmailRequest
        {
            public string ToAddress { get; set; }
            public string Subject { get; set; }
            public string Body { get; set; }
        }

    }
}
