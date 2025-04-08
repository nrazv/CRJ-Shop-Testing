using Microsoft.EntityFrameworkCore;
using Microsoft.Playwright;
using System;
using TechTalk.SpecFlow;

namespace E2ETesting.Steps
{
    [Binding]
    public class UserStepDefinitions
    {
        private IPlaywright _playwright;
        private IBrowser _browser;
        private IBrowserContext _context;
        private IPage _page;

        [BeforeScenario]
        public async Task Setup()
        {
            _playwright = await Playwright.CreateAsync();
            _browser = await _playwright.Chromium.LaunchAsync(new() { Headless = false, SlowMo = 200 });
            _context = await _browser.NewContextAsync();
            _page = await _context.NewPageAsync();
        }

        [AfterScenario]
        public async Task Teardown()
        {
            await _browser.CloseAsync();
            _playwright.Dispose();
        }


        [Given(@"I'm on the register page")]
        public async Task GivenImOnTheRegisterPage()
        {
            await _page.GotoAsync("http://localhost:5000/Account/Register");
        }

        [When(@"I enter ""([^""]*)"" as the email")]
        public async Task WhenIEnterAsTheEmail(string p0)
        {
            await _page.FillAsync("[data-testid='email']", p0);
        }

        [When(@"I enter ""([^""]*)"" as the first name")]
        public async Task WhenIEnterAsTheFirstName(string test)
        {
            await _page.FillAsync("[data-testid='firstname']", test);

        }

        [When(@"I enter ""([^""]*)"" as the last name")]
        public async Task WhenIEnterAsTheLastName(string joe)
        {
            await _page.FillAsync("[data-testid='lastname']", joe);
        }

        [When(@"I enter ""([^""]*)"" as the address")]
        public async Task WhenIEnterAsTheAddress(string p0)
        {
            await _page.FillAsync("[data-testid='address']", p0);
        }

        [When(@"I enter ""([^""]*)"" as the password")]
        public async Task WhenIEnterAsThePassword(string p0)
        {
            await _page.FillAsync("[data-testid='password']", p0);
        }

        [When(@"I enter ""([^""]*)"" as the password confirmation")]
        public async Task WhenIEnterAsThePasswordConfirmation(string p0)
        {
            await _page.FillAsync("[data-testid='passwordConfirmation']", p0);
        }


        [When(@"I submit the form data")]
        public async Task WhenISubmitTheFormData()
        {
            await _page.ClickAsync("[data-testid='submitButton']");
            await _page.WaitForURLAsync("http://localhost:5000/Account/Login");
        }


        [Then(@"I should be redirected to the login page")]
        public async Task ThenIShouldBeRedirectedToTheLoginPage()
        {
            var expected = "http://localhost:5000/Account/Login";
            var url = _page.Url;
            Assert.EndsWith(expected, url);
        }



        [Given(@"I am on the register page")]
        public async Task GivenIAmOnTheRegisterPage()
        {
            await _page.GotoAsync("http://localhost:5000/Account/Register");
        }

        [Given(@"I try to register a user with an existing email")]
        public async Task GivenITryToRegisterAUserWithAnExistingEmail()
        {
            await _page.FillAsync("[data-testid='email']", "test@gmail.com");
            await _page.FillAsync("[data-testid='firstname']", "Joe");
            await _page.FillAsync("[data-testid='lastname']", "Thom");
            await _page.FillAsync("[data-testid='address']", "Test Address");
            await _page.FillAsync("[data-testid='password']", "Password123!");
            await _page.FillAsync("[data-testid='passwordConfirmation']", "Password123!");
        }

        [Given(@"I click register")]
        public async Task GivenIClickRegister()
        {
            await _page.ClickAsync("[data-testid='submitButton']");
        }

        [Then(@"I should see an error message")]
        public async Task ThenIShouldSeeAnErrorMessage()
        {
            var error = await _page.QuerySelectorAsync("[data-testid='error']");
            var errorMessage = await error.InnerTextAsync();
            Assert.NotNull(error);
            Assert.Equal("Error, try again.", errorMessage);
        }

    }
}
