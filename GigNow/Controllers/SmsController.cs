using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Twilio;

namespace GigNow.Controllers
{
    
    public class SmsController : Controller
    {
        TwilioRestClient client = new TwilioRestClient("AC2afb2ae21d8426bf118ac7c01b8c832c", "01ef1e9c7ae5cb3acc8e7d9ad1c58a69");
        public void SendMessage(string phoneNumber, string message)
        {

            if (!string.IsNullOrWhiteSpace(phoneNumber))
            {
                string body = ("Gig:Now says:  " + message + "   Log in to your Gig:Now account to see the entire message.");
                client.SendMessage("+12623203180", phoneNumber, body);
            }

        }
    }
}