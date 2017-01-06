using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.Helpers
{
    public class APIKeyGenerator
    {

        private static Random random = new Random();
        public static string GenerateKey()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!?%éíá+ěščřžýáí";
            return new string(Enumerable.Repeat(chars, 20)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }


    }
}