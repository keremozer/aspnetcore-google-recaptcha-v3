using System;
using System.Collections.Generic;
using System.Text;

namespace GoogleReCaptcha.V3.Interfaces
{
    public interface IRecaptchaValidator
    {
        bool IsRecaptchaValid(string token);
    }
}
