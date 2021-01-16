using GoogleReCaptcha.V3.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using GoogleReCaptcha.V3.Models;

namespace GoogleReCaptcha.V3
{
    public class RecaptchaValidator : IRecaptchaValidator
    {
        private const string GoogleRecaptchaAddress = "https://www.google.com/recaptcha/api/siteverify";

        public readonly IConfiguration Configuration;

        public RecaptchaValidator(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public bool IsRecaptchaValid(string token)
        {
            using var client = new HttpClient();
            var response = client.GetStringAsync($@"{GoogleRecaptchaAddress}?secret={Configuration["Google:RecaptchaV3SecretKey"]}&response={token}").Result;
            var recaptchaResponse = JsonConvert.DeserializeObject<RecaptchaResponse>(response);

            if (!recaptchaResponse.Success || recaptchaResponse.Score < Convert.ToDecimal(Configuration["Google:RecaptchaMinScore"]))
            {
                return false;
            }
            return true;
        }

    }
}
