using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Emotiv
{
    public class CustomerSecurity
    {

        [DllImport("edk.dll", EntryPoint = "EE_GetSecurityCode")]
        static extern Double Unmanged_EE_GetSecurityCode();
        [DllImport("edk.dll", EntryPoint = "EE_CheckSecurityCode")]
        static extern Int32 Unmanged_EE_CheckSecurityCode(Double x);
        [DllImport("CustomerSecurity.dll", EntryPoint = "emotiv_func")]
        static extern Double Unmanged_emotiv_func(Double x);

        public static Double EE_GetSecurityCode()
        {
            return Unmanged_EE_GetSecurityCode();
        }
        public static Int32 EE_CheckSecurityCode(Double x)
        {
            return Unmanged_EE_CheckSecurityCode(x);
        }
        public static Double emotiv_func(Double x)
        {
            return Unmanged_emotiv_func(x);
        }
    }
}
