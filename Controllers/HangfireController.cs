using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Q1Q2Q4.Services;

namespace Q1Q2Q4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangfireController : ControllerBase
    {
        private IReminderService _emailService;
        private IBackgroundJobClient _backgroundJobClient;
        private IRecurringJobManager _recurringJobManager;

        public HangfireController(IReminderService emailService, IBackgroundJobClient backgroundJobClient,
            IRecurringJobManager recurringJobManager)
        {
            _emailService = emailService;
            _backgroundJobClient = backgroundJobClient;
            _recurringJobManager = recurringJobManager;
        }

        [HttpGet]
        [Route("PaymentReminder")]
        public ActionResult SendPaymentReminder()
        {
            var jobId = "jobId";

            _recurringJobManager.AddOrUpdate(jobId, () => _emailService.SendReminder(), Cron.Minutely);

            return Ok();
        }
    }
}
