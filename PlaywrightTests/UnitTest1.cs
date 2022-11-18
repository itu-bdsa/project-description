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

        await Page.GetByRole(AriaRole.Textbox).First.ClickAsync();

        await Page.GetByRole(AriaRole.Textbox).First.FillAsync("SpaceVikingEik/hewwo");

        await Page.GetByRole(AriaRole.Textbox).First.PressAsync("Tab");

        await Page.GetByRole(AriaRole.Textbox).Nth(1).FillAsync("FQMode");

        await Page.GetByRole(AriaRole.Button, new() { NameString = "Run Analysis" }).ClickAsync();

        await Page.GetByRole(AriaRole.Row, new() { NameString = "SpaceVikingEik/hewwo 12-11-2022 4" }).GetByRole(AriaRole.Cell, new() { NameString = "SpaceVikingEik/hewwo" }).ClickAsync();

        await Page.GetByRole(AriaRole.Row, new() { NameString = "SpaceVikingEik/hewwo 13-11-2022 4" }).GetByRole(AriaRole.Cell, new() { NameString = "SpaceVikingEik/hewwo" }).ClickAsync();

    }
}