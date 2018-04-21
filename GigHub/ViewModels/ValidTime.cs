using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace GigHub.ViewModels
{
    public class ValidTime : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime time;

            //var isValid = Regex.Match(value.ToString(), "^(?:0?[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$");

            var isValid = DateTime.TryParseExact(Convert.ToString(value),
                "HH:mm",
                CultureInfo.CurrentCulture,
                DateTimeStyles.None,
                out time);

            return (isValid);
        }
    }
}