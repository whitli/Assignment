using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace UbiQuity.Pages
{
    public class BasePage
    {

        protected IWebDriver _driver { get; set; }
        public BasePage(IWebDriver driver)
        {

            _driver = driver;


        }
        public IWebElement WaitForElementReturned(By byElementCriteria)
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromMinutes(3));
            IWebElement e = wait.Until(ExpectedConditions.ElementExists(byElementCriteria));
            return e;
        }
        public IWebElement WaitForElementClickable(IWebElement element)
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(120));
            IWebElement e=wait.Until(ExpectedConditions.ElementToBeClickable(element));
            return e;
        }


        public bool WaitForElementVisible(By byElementCriteria)
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(120));
            bool status=wait.Until(ExpectedConditions.InvisibilityOfElementLocated(byElementCriteria));
            return status;
            
        }
        public void SetControlValue(IWebElement element, object value)
        {
            Actions actions = new Actions(_driver);
            actions.MoveToElement(element).Perform();
            var a = element.GetAttribute("type");
            if (element.GetAttribute("type") == "text" || element.TagName == "textarea" ||
                element.GetAttribute("type") == "password"
                || element.GetAttribute("type") == "email" || element.GetAttribute("type") == "tel" ||
                element.GetAttribute("type") == "number")
            {

                element.Clear();
                element.SendKeys(value.ToString());
            }
           
            else if (element.GetAttribute("type") == "checkbox")
            {

                var isCheckboxAlreadyChecked = element.Selected;
                if (value is bool)
                {
                    if (isCheckboxAlreadyChecked != (bool)value)
                    {
                        element.Click();
                    }


                }

                else if (value is string)
                {

                    if (isCheckboxAlreadyChecked != bool.Parse(value.ToString()))
                        element.Click();
                }

            }
            else if (element.GetAttribute("type") == "radio")
            {

                var radioButton =
                    _driver.FindElement(By.XPath(string.Format("//input[@type='radio' and @value='{0}']", value)));
                radioButton.Click();
            }
        }

        public void UpdateField(string fieldName, string value)
        {
            try
            {
                string id = String.Format("//li[@id='{0}']/div[@class='field-value']/input", fieldName);
                var field = _driver.FindElement(By.XPath(id));
                SetControlValue(field, value);
                Blur(field);
            }
            catch (Exception e)
            {

                    var errorMessage = string.Format("Exception >>{0} || Error Type: {1}}}", fieldName, e.Message);
                    throw new Exception(errorMessage, e);
            }
        }
        public string ReadValueFromField(string fieldName)
        {
            try
            {
                string id = String.Format("//li[@id='{0}']/div[@class='field-value']/input", fieldName);
                var field = WaitForElementReturned(By.XPath(id));

                string fieldval = field.GetAttribute("value");



                return fieldval;
            }
            catch (Exception e)
            {

                var errorMessage = string.Format("Exception >>{0} || Error Type: {1}}}", fieldName, e.Message);
                throw new Exception(errorMessage, e);
            }
        }

        public string GetValidationMessage()
        {
            
            try
            {
                var element = _driver.FindElement(By.Id("SuccessMessage"));
                var actualMessage = element.Text;
                return actualMessage;
            }
            catch (Exception e)
            {
                var errorMessage = string.Format("Exception >>Message || Error Type: {1}}}",e.Message);

                throw new Exception(errorMessage, e);
            }
        }

        public void Blur(IWebElement element)
        {
            element.SendKeys(Keys.Tab);
            _driver.ExecuteJavaScript("document.activeElement.blur();");
        }
        public IWebElement usernameTxtBox()
        {
            return _driver.FindElement(By.Id("username"));
        }

        public IWebElement passwordTxtBox()
        {
            return _driver.FindElement(By.Id("password"));
        }

        public IWebElement login()
        {
            IWebElement clickableElement = WaitForElementClickable(_driver.FindElement(By.XPath("//button[@class='btn btn-secondary text-center submit login-button']")));
            Console.WriteLine("Wait completed for Login Button"); 
            return clickableElement;
        }

        public IWebElement edit()
        {
            IWebElement e = WaitForElementReturned(By.CssSelector("[href='/editprofile']"));
            Console.WriteLine("Wait completed for Edit Profile Button");
            return e;
        }

        public IWebElement submit()
        {
            IWebElement clickableElement = WaitForElementClickable(_driver.FindElement(By.XPath("//button[@class='submit-button engage-button submit right']")));
            Console.WriteLine("Wait completed for Submit Button");
            return clickableElement;
        }



        public void LoginAs(string username, string password)
        {


            SetControlValue(usernameTxtBox(),username);
            Blur(usernameTxtBox());
            SetControlValue(passwordTxtBox(),password);
            Blur(passwordTxtBox());
            login().Click();
            
        }

        public void ClickOnEditProfilePage()
        {
            _driver.ExecuteJavaScript("arguments[0].click();", edit());

            //edit().Click();
        }

        public void ClickOnSubmitInEditProfilePage()
        {

            submit().Click();
        }

    }
}
