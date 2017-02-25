using System;
using Twilio;

namespace SMSWebApi.Services.TwilioSms
{
    public interface ISmsService
    {
        Message SendMessage(string fromNumber, string toNumber, string body);
    }
}
