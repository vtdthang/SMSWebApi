using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Twilio;

namespace SMSWebApi.Services.TwilioSms
{
    public class SmsService : ISmsService
    {
        private readonly TwilioRestClient _client;

        protected string TwilioAccountSid = ConfigurationManager.AppSettings["TwilioAccountSid"];
        protected string TwilioAuthToken = ConfigurationManager.AppSettings["TwilioAuthToken"];
        protected string TwilioPhoneNumber = ConfigurationManager.AppSettings["TwilioPhoneNumber"];

        public SmsService()
        {
            _client = new TwilioRestClient(TwilioAccountSid, TwilioAuthToken);
        }

        public SmsService(TwilioRestClient client)
        {
            _client = client;
        }

        public virtual Message SendMessage(string fromNumber , string toNumber, string body)
        {
            return _client.SendMessage(fromNumber, toNumber, body);
        }
    }
}