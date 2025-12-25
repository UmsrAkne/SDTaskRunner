using System.Diagnostics;
using Prism.Mvvm;
using SDTaskRunner.Models;
using SDTaskRunner.Utils;

namespace SDTaskRunner.ViewModels;

public class MainWindowViewModel : BindableBase
{
    private readonly AppVersionInfo appVersionInfo = new();
    private GenerationRequest generationRequest;

    public MainWindowViewModel()
    {
        SetDummies();
    }

    public string Title => appVersionInfo.GetAppNameWithVersion();

    public GenerationRequest GenerationRequest
    {
        get => generationRequest;
        set => SetProperty(ref generationRequest, value);
    }

    [Conditional("DEBUG")]
    private void SetDummies()
    {
        GenerationRequest = new();

        GenerationRequest.Height.Value = 512;
        GenerationRequest.Width.Value = 480;
        GenerationRequest.Steps.Value = 10;
        GenerationRequest.Prompt.Value = "A painting of a squirrel eating a burger";
        GenerationRequest.NegativePrompt.Value = "Negative prompt test.";
        GenerationRequest.Seed.Value = 1234;
    }
}