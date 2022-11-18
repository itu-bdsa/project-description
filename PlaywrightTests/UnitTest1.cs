using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Playwright.NUnit;
using GitInsight;
using NUnit.Framework;

namespace PlaywrightTests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class Tests : PageTest
{

    //Startup Website, pgAdmin needs to be turned on
    [Test]
    public async Task HomepageHasPlaywrightInTitleAndGetStartedLinkLinkingtoTheIntroPage()
    {
        await Page.GotoAsync("https://localhost:7111/fetchdata");
        await Expect(Page).ToHaveTitleAsync("GitInsight");
                //await Page.Locator("input type").TypeAsync("SpaceVikingEik/hewwo"); does not work

    }
}