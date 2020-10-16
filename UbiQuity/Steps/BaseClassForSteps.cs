using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace UbiQuity.Steps
{
    public class BaseClassForSteps
    {
        public static IWebDriver Driver { get; set; }

        public static bool IsLoggedIn = false;



        
    }
}
