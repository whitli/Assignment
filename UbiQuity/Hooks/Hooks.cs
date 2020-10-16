using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;
using UbiQuity.Steps;

namespace UbiQuity.Hooks
{
    [Binding]
    public class Hooks : BaseClassForSteps
    {


        [BeforeTestRun]
        public static void BeforeTestRun()
        {

            Driver = PageFactory.InitiateWebDriver();

        }
        [AfterTestRun]
        public static void AfterTestRun()
        {
            Driver.Close();
            Driver.Quit();
            Driver.Dispose();
        }
    }
}
