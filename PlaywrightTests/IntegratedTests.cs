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
    public override BrowserNewContextOptions ContextOptions()
    {
        return new BrowserNewContextOptions()
        {
            ColorScheme = ColorScheme.Light,
            ViewportSize = new()
            {
                Width = 1920,
                Height = 1080
            },
            IgnoreHTTPSErrors = true
        };
    }

    //Husk at starte websitet op før testing!!
    [Test]
    public async Task LocalHostIsActive()
    { 
        await Page.GotoAsync("https://localhost:7111");

        // Expect a title "to contain" a substring.
        await Expect(Page).ToHaveTitleAsync(new Regex("GitInsight"));

    }
    [Test]
    public async Task FetchDataIsActive()
    {

        await Page.GotoAsync("https://localhost:7111");

        await Page.GetByRole(AriaRole.Link, new() { NameString = "Fetch data" }).ClickAsync();

        await Page.GetByRole(AriaRole.Heading, new() { NameString = "GitInsight" }).ClickAsync();

    }

       [Test]
    public async Task UserIsAbleToAnalyseAnRepo()
    {

        await Page.GotoAsync("https://localhost:7111");

        //await Page.GetByRole(AriaRole.Link, new() { NameString = "Fetch data" }).ClickAsync();

        await Page.GetByRole(AriaRole.Textbox).ClickAsync();

        await Page.GetByRole(AriaRole.Textbox).FillAsync("Divik-kid/BDSA00");

        await Page.GetByRole(AriaRole.Button, new() { NameString = "Run Analysis" }).ClickAsync();

        var FQLocator = Page.Locator("div").Filter(new() { HasTextString = "FQMode Barchart" });
        
        await Expect(FQLocator).ToHaveTitleAsync(new Regex("GitInsight"));


        //-----FQMode expect 13 commits for date 08-09-2022 -------
        /*await Expect(Page).ToHaveTitleAsync(new Regex("GitInsight"));
        await page.GetByText("08-09-2022").First.ClickAsync();

        await page.GetByText("13").ClickAsync();

        //-----ForkMode repo name BDSA00 owner Nooja1012 URL https://github.com/Nooja1012/BDSA00 -------

        await page.GetByText("BDSA00").First.ClickAsync();

        await page.GetByText("Nooja1012").First.ClickAsync();

        await page.GetByText("https://github.com/Nooja1012/BDSA00").ClickAsync();


        // create a locator
        //var FQLocator = Page.GetByRole(AriaRole.Heading, new() { NameString = "FQMode Barchart" });
        var FQLocator = Page.GetByRole(AriaRole.Heading, new() { NameString = "FQMode Barchart" });

        // Expect an attribute "to be strictly equal" to the value.
        //await Expect(FQLocator).ToHaveAttributeAsync("href", "/docs/intro");

        /*await Page.GetByRole(AriaRole.Heading, new() { NameString = "FQMode Barchart" }).ClickAsync();

        await Page.GetByRole(AriaRole.Heading, new() { NameString = "AuthMode Barchart(s)" }).ClickAsync();*/

    }

}