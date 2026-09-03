using Carubbi.LayoutTemplateEngine.Razor;

namespace Carubbi.LayoutTemplateEngine.Tests;

public class RazorEngineTests
{
    private RazorEngine _sut = null!;
    private static readonly IDictionary<string, object> Data = new Dictionary<string, object> { ["Name"] = "World" };

    private void CreateSut() => _sut = new RazorEngine();

    [Test]
    public async Task RenderFromContentTemplate_When_GivenData_Then_SubstitutesVariables()
    {
        CreateSut();

        var result = _sut.RenderFromContentTemplate("Hello @Model[\"Name\"]!", Data);

        await Assert.That(result).IsEqualTo("Hello World!");
    }

    [Test]
    public async Task RenderTemplate_When_GivenFileAndData_Then_RendersFileContent()
    {
        CreateSut();
        var path = WriteTempFile("Hi @Model[\"Name\"]");

        var result = _sut.RenderTemplate(path, Data);

        await Assert.That(result).IsEqualTo("Hi World");
    }

    [Test]
    public async Task RenderTemplate_When_GivenMasterAndData_Then_EmbedsBodyInMaster()
    {
        CreateSut();
        var master = WriteTempFile("<html><body>@Model[\"TemplateBody\"]</body></html>");
        var template = WriteTempFile("Greetings @Model[\"Name\"]");

        var result = _sut.RenderTemplate(master, template, Data);

        await Assert.That(result).IsEqualTo("<html><body>Greetings World</body></html>");
    }

    private static string WriteTempFile(string content)
    {
        var path = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName() + ".cshtml");
        File.WriteAllText(path, content);
        return path;
    }
}
