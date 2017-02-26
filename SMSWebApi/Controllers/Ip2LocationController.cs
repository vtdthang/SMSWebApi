

using System;
using System.Net;
using System.Net.Http;
using System.ServiceModel.Channels;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;
using System.Globalization;

namespace SMSWebApi.Controllers
{
    public class Ip2LocationController : ApiController
    {
        [HttpGet]
        [Route("api/test/location")]
        public IHttpActionResult TestGetLocationByIp()
        {
            var location = GetUserCountryByIp();
            return Ok(location);
        }

        //Get User Location by IpAddress
        public static string GetInfoV1()
        {
            return new WebClient().DownloadString("http://api.hostip.info/get_json.php");
        }

        public static string GetUserCountryByIp()
        {
            var ip = GetPublicIp();
            //IpInfo ipInfo = new IpInfo();
            //try
            //{
            //    string info = new WebClient().DownloadString("http://ipinfo.io/" + ip);
            //    ipInfo = JsonConvert.DeserializeObject<IpInfo>(info);
            //    RegionInfo myRi1 = new RegionInfo(ipInfo.Postal);
            //    ipInfo.Postal = myRi1.TwoLetterISORegionName;
            //}
            //catch (Exception)
            //{
            //    ipInfo.Country = null;
            //}

            //return ipInfo.Postal;

            //string url = "http://freegeoip.net/json/" + ip.ToString();
            //WebClient client = new WebClient();
            //string jsonstring = client.DownloadString(url);
            //dynamic dynObj = JsonConvert.DeserializeObject(jsonstring);
            ////System.Web.HttpContext.Current.Session["UserCountryCode"] = dynObj.country_code;
            //var countryCode = dynObj.country_code;
            //var postalCode = dynObj.zip_code;

            //return postalCode;

            string url = "http://www.geognos.com/api/en/countries/info/" + ip.ToString() + ".json";
            WebClient client = new WebClient();
            string jsonstring = client.DownloadString(url);
            dynamic dynObj = JsonConvert.DeserializeObject(jsonstring);
            //System.Web.HttpContext.Current.Session["UserCountryCode"] = dynObj.country_code;
            var postalCode = dynObj.Results.TelPref;

            return postalCode;
        }

        //Get IP address
        public static string GetPublicIp()
        {
            string url = "http://checkip.dyndns.org";
            System.Net.WebRequest req = System.Net.WebRequest.Create(url);
            System.Net.WebResponse resp = req.GetResponse();

            var sr = new System.IO.StreamReader(resp.GetResponseStream());
            string response = sr.ReadToEnd().Trim();
            string[] a = response.Split(':');
            string a2 = a[1].Substring(1);
            string[] a3 = a2.Split('<');
            string a4 = a3[0];
            return a4;
        }


        protected long Dot2LongIp(string dottedIp)
        {
            double num = 0;
            if (dottedIp == "")
            {
                return 0L;
            }
            else
            {
                int i;
                var arrDec = dottedIp.Split('.');
                for (i = arrDec.Length - 1; i >= 0; i--)
                {
                    num += ((int.Parse(arrDec[i]) % 256) * Math.Pow(256, (3 - i)));
                }
                return Convert.ToInt64(num);
            }
        }
    }

    public class IpInfo
    {

        [JsonProperty("ip")]
        public string Ip { get; set; }

        [JsonProperty("hostname")]
        public string Hostname { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("loc")]
        public string Loc { get; set; }

        [JsonProperty("org")]
        public string Org { get; set; }

        [JsonProperty("postal")]
        public string Postal { get; set; }
    }
}
