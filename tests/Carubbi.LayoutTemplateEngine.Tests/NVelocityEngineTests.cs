using Carubbi.LayoutTemplateEngine.NVelocity;

namespace Carubbi.LayoutTemplateEngine.Tests;

public class NVelocityEngineTests
{
    private NVelocityEngine _sut = null!;
    private string _templatesPath = null!;
    private static readonly IDictionary<string, object> Data = new Dictionary<string, object> { ["Name"] = "World" };

    private void CreateSut()
    {
        _templatesPath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
        Directory.CreateDirectory(_templatesPath);
        _sut = new NVelocityEngine(_templatesPath);
    }

    [Test]
    public async Task RenderFromContentTemplate_When_GivenData_Then_SubstitutesVariables()
    {
        CreateSut();

        var result = _sut.RenderFromContentTemplate("Hello $Name!", Data);

        await Assert.That(result).IsEqualTo("Hello World!");
    }

    [Test]
    public async Task RenderTemplate_When_GivenFileAndData_Then_RendersFileContent()
    {
        CreateSut();
        WriteTemplateFile("greeting.vm", "Hi $Name");

        var result = _sut.RenderTemplate("greeting.vm", Data);

        await Assert.That(result).IsEqualTo("Hi World");
    }

    [Test]
    public async Task RenderTemplate_When_GivenMasterAndData_Then_EmbedsBodyInMaster()
    {
        CreateSut();
        WriteTemplateFile("master.vm", "<html><body>$TemplateBody</body></html>");
        WriteTemplateFile("content.vm", "Greetings $Name");

        var result = _sut.RenderTemplate("master.vm", "content.vm", Data);

        await Assert.That(result).IsEqualTo("<html><body>Greetings World</body></html>");
    }

    private void WriteTemplateFile(string name, string content) =>
        File.WriteAllText(Path.Combine(_templatesPath, name), content);
}
