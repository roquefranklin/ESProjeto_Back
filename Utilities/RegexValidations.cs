﻿using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace ESProjeto_Back.Utilities
{
    public class RegexValidations
    {
        public static bool EmailValidation(string email)
        {
            if (string.IsNullOrEmpty(email)) {
                return false;
            }

            try
            {
                email = Regex.Replace(email, @"(@)  (.+)$", DomainMapper, RegexOptions.None, TimeSpan.FromMilliseconds(200));

                string DomainMapper(Match match)
                {
                    var idn = new IdnMapping();

                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e) 
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                   @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                   RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
        }
    }
}
