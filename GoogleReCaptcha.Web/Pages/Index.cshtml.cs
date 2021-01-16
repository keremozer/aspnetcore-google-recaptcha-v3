using GoogleReCaptcha.V3.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoogleReCaptcha.Web.Pages
{
    public class IndexModel : PageModel
    {

        public readonly IRecaptchaValidator RecaptchaValidator;
        public IndexModel(IRecaptchaValidator recaptchaValidator)
        {
            RecaptchaValidator = recaptchaValidator;
        }


        public void OnGet()
        {

        }

        public IActionResult OnPostSubscribeNewsletter(string email, string token)
        {
            if (!RecaptchaValidator.IsRecaptchaValid(token))
            {
                //return error message or something
                return BadRequest();
            }

            return Page();
        }

        public JsonResult OnGetRecaptchaVerify(string token)
        {
            return new JsonResult(RecaptchaValidator.IsRecaptchaValid(token));
        }
    }
}
