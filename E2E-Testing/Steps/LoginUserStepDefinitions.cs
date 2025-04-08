using Microsoft.Playwright;
using System;
using TechTalk.SpecFlow;

namespace E2ETesting.Steps
{
    [Binding]
    public class LoginUserStepDefinitions
    {
        private IPlaywright _playwright;
        private IBrowser _browser;
        private IBrowserContext _context;
        private IPage _page;

        [BeforeScenario]
        public async Task Setup()
        {
            _playwright = await Playwright.CreateAsync();
            _browser = await _playwright.Chromium.LaunchAsync(new() { Headless = false });
            _context = await _browser.NewContextAsync();
            _page = await _context.NewPageAsync();
        }

        [AfterScenario]
        public async Task Teardown()
        {
            await _browser.CloseAsync();
            _playwright.Dispose();
        }


        [Given(@"I'm on the login page")]
        public async Task GivenImOnTheLoginPage()
        {
            await _page.GotoAsync("http://localhost:5000/Account/Login");
        }

        [When(@"I enter ""([^""]*)"" as the email of the new user")]
        public async Task WhenIEnterAsTheEmailOfTheNewUser(string p0)
        {
            await _page.FillAsync("[data-testid='email']", p0);
        }

        [When(@"I enter ""([^""]*)"" as the password of the new user")]
        public async Task WhenIEnterAsThePasswordOfTheNewUser(string p0)
        {
            await _page.FillAsync("[data-testid='password']", p0);
        }

        [When(@"I submit the form")]
        public async Task WhenISubmitTheForm()
        {
            await _page.ClickAsync("[data-testid='loginButton']");
            await _page.WaitForURLAsync("http://localhost:5000/");
        }

        [Then(@"I should be redirected to the home page")]
        public async Task ThenIShouldBeRedirectedToTheHomePage()
        {
            var expected = "http://localhost:5000/";
            var url = _page.Url;
            Assert.EndsWith(expected, url);
        }
    }
}
