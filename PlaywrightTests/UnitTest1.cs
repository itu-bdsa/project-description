using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace PlaywrightTests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class Tests : PageTest
{
    [Test]
    public async Task HomepageHasPlaywrightInTitleAndGetStartedLinkLinkingtoTheIntroPage()
    {
        await Page.GotoAsync("https://localhost:7111/fetchdata");
        await Expect(Page).ToHaveTitleAsync("GitInsight");
        //await Page.Locator("input type").TypeAsync("SpaceVikingEik/hewwo"); does not work
        
    }
}