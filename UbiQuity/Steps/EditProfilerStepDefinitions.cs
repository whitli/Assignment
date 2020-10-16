using NUnit.Framework;
using TechTalk.SpecFlow;
using UbiQuity.Data;
using UbiQuity.Pages;

namespace UbiQuity.Steps
{
    [Binding]
    public sealed class EditProfilerStepDefinitions : BaseClassForSteps
    {

        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        private readonly ScenarioContext _scenarioContext;

        public EditProfilerStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        public BasePage bp = new BasePage(Driver);
        [Given(@"I am on Edit Profile page")]
        public void GivenIAmOnEditProfilePage()
        {
            if (!IsLoggedIn)
            {
                bp.LoginAs(LoginData.TestUser.username, LoginData.TestUser.password);
            }

            IsLoggedIn = true;
            bp.ClickOnEditProfilePage();
        }
        [When(@"Update the following details (.*) (.*) (.*) in Edit Profile page")]
        public void WhenUpdateTheFollowingDetailsInEditProfilePage(string emailAdress, string dateOfBirth, string password)
        {
            bp.UpdateField("Email", emailAdress);
            bp.UpdateField("DateOfBirth", dateOfBirth);
            bp.UpdateField("NewPassword", password);
            bp.UpdateField("ConfirmNewPassword", password);

        }
        [When(@"I click on Submit button")]
        public void WhenIClickOnSubmitButton()
        {
            bp.ClickOnSubmitInEditProfilePage();
        }
      

        [Then(@"I should be able to see successfully message '(.*)' on the page")]
        public void ThenIShouldBeAbleToSeeSuccessfullyMessageOnThePage(string expectedtext)
        {
            Assert.True(bp.GetValidationMessage().Equals(expectedtext));
        }

        [Then(@"I should be able to see updated Data (.*) (.*) (.*) in in Edit Profile page")]
        public void ThenIShouldBeAbleToSeeUpdatedDataInInEditProfilePage(string emailAdress, string dateOfBirth, string password)
        {
            var c = bp.ReadValueFromField("Email");
            Assert.True(bp.ReadValueFromField("Email").Equals(emailAdress));
            Assert.True(bp.ReadValueFromField("DateOfBirth").Equals(dateOfBirth));
            Assert.True(bp.ReadValueFromField("NewPassword").Equals(password));
            Assert.True(bp.ReadValueFromField("ConfirmNewPassword").Equals(password));
        }


    }
}
