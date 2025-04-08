using Microsoft.Playwright;
using System;
using TechTalk.SpecFlow;

namespace E2ETesting.Steps;

[Binding]
public class HomePageStepDefinitions
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

    [Given(@"I am on the home page")]
    public async Task GivenIAmOnTheHomePage()
    {
        await _page.GotoAsync("http://localhost:5000/");
    }

    [Given(@"Title should be ""([^""]*)""")]
    public async Task GivenTitleShouldBe(string pageTitle)
    {
        await _page.GotoAsync("http://localhost:5000/");

        var title = await _page.TitleAsync();

        Assert.Equal(pageTitle, title);
    }


    [Given(@"I should see more the (.*) product")]
    public async Task GivenIShouldSeeMoreTheProduct(int p0)
    {
        await _page.GotoAsync("http://localhost:5000/");

        var products = await _page.QuerySelectorAllAsync("[data-testid='product']");
        Assert.True(products.Count > p0, "Expected more than one product on the page.");
    }


}
