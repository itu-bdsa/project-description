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
    [Test]
    public async Task LocalHostIsActive()
    {

        await Page.GotoAsync("https://localhost:7111/");

        await Page.GetByRole(AriaRole.Heading, new() { NameString = "Hello, world!" }).ClickAsync();

    }
    [Test]
    public async Task FetchDataIsActive()
    {

        await Page.GotoAsync("https://localhost:7111/");

        await Page.GetByRole(AriaRole.Link, new() { NameString = "Fetch data" }).ClickAsync();

        await Page.GetByRole(AriaRole.Heading, new() { NameString = "GitInsight" }).ClickAsync();

    }

       [Test]
    public async Task UserIsAbleToAnalyseAnRepo()
    {

        await Page.GotoAsync("https://localhost:7111/");

        await Page.GetByRole(AriaRole.Link, new() { NameString = "Fetch data" }).ClickAsync();

        await Page.GetByRole(AriaRole.Textbox).ClickAsync();

        await Page.GetByRole(AriaRole.Textbox).FillAsync("SpaceVikingEik/hewwo");

        await Page.GetByRole(AriaRole.Button, new() { NameString = "Run Analysis" }).ClickAsync();

        await Page.GetByRole(AriaRole.Heading, new() { NameString = "FQMode Barchart" }).ClickAsync();

        await Page.GetByRole(AriaRole.Heading, new() { NameString = "AuthMode Barchart(s)" }).ClickAsync();

    }

}