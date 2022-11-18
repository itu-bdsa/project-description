using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace PlaywrightTests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class Tests : PageTest
{

    //Startup Website, pgAdmin needs to be turned on
    [Test]
    public async Task WeCanNavigateToFetchDataAndYoinkSomeData()
    {

        await Page.GotoAsync("https://localhost:7111/");

        await Page.GetByRole(AriaRole.Link, new() { NameString = "Fetch data" }).ClickAsync();

        await Page.GetByRole(AriaRole.Textbox).ClickAsync();

        await Page.GetByRole(AriaRole.Textbox).FillAsync("SpaceVikingEik/hewwo");

        await Page.GetByRole(AriaRole.Button, new() { NameString = "Run Analysis" }).ClickAsync();

        await Page.Locator("svg").Filter(new() { HasTextString = "0 5 10 15 20 Commits 12-11-2022 13-11-2022 20 20" }).ClickAsync();

        await Page.Locator("svg").Filter(new() { HasTextString = "8 10 12 12-11-2022 13-11-2022 9 20" }).ClickAsync();

        await Page.Locator("svg").Filter(new() { HasTextString = "0 2 4 12-11-2022 3" }).ClickAsync();

    }
}