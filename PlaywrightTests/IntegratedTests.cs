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

        await Page.GetByRole(AriaRole.Textbox).ClickAsync();

        await Page.GetByRole(AriaRole.Textbox).FillAsync("Divik-kid/BDSA00");

        await Page.GetByRole(AriaRole.Button, new() { NameString = "Run Analysis" }).ClickAsync();

        var fq = Page.GetByRole(AriaRole.Heading, new() { NameString = "FQMode Barchart" });

        await Expect(fq).ToBeVisibleAsync();

        await Expect(Page.GetByRole(AriaRole.Heading, new() { NameString = "AuthMode Barchart(s)" })).ToBeVisibleAsync();

        await Expect(Page.GetByRole(AriaRole.Heading, new() { NameString = "ForkMode Table" })).ToBeVisibleAsync();

        await Expect(Page.GetByRole(AriaRole.Heading, new() { NameString = "Distribution of changes to files" })).ToBeVisibleAsync();

    }

    [Test]
    public async Task IntegrTest()
    {

        await Page.GotoAsync("https://localhost:7111/");

        await Page.GetByRole(AriaRole.Textbox).ClickAsync();

        await Page.GetByRole(AriaRole.Textbox).FillAsync("Divik-kid/BDSA00");

        await Page.GetByRole(AriaRole.Button, new() { NameString = "Run Analysis" }).ClickAsync();

        await Page.Locator(".rz-column-series > path").First.ClickAsync();

        await Page.GetByText("08-09-2022").First.ClickAsync();

        await Page.GetByRole(AriaRole.Heading, new() { NameString = "AuthMode Barchart(s)" }).ClickAsync();

        await Page.GetByText("Chris").ClickAsync();

        await Page.GetByText("08-09-2022").Nth(1).ClickAsync();

        await Page.GetByRole(AriaRole.Heading, new() { NameString = "ForkMode Table" }).ClickAsync();

        await Page.GetByRole(AriaRole.Cell, new() { NameString = "BDSA00" }).First.ClickAsync();

        await Page.GetByRole(AriaRole.Cell, new() { NameString = "Nooja1012" }).First.ClickAsync();

        await Page.GetByRole(AriaRole.Cell, new() { NameString = "https://github.com/Nooja1012/BDSA00" }).ClickAsync();

        await Page.Locator("div").Filter(new() { HasTextString = "Distribution of changes to files .github/workflows/Actions.yml solApp/solApp.sln" }).Nth(2).ClickAsync();

    }

}