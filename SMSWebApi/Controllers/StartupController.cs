using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using SMSWebApi.Services.TwilioSms;

namespace SMSWebApi.Controllers
{
    public class StartupController : ApiController
    {
        [Route("api/test")]
        [HttpGet]
        public async Task<IHttpActionResult> TestSendSms()
        {
            var twilioService = new SmsService();
            var twilioPhoneNumber = ConfigurationManager.AppSettings["TwilioPhoneNumber"];
            twilioService.SendMessage(twilioPhoneNumber, "toNumber", "Test");

            return this.Ok();
        }
    }
}
