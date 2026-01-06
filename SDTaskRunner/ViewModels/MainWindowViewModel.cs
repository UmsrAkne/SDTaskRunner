using System;
using System.Diagnostics;
using System.Threading;
using CommunityToolkit.Mvvm.Input;
using Prism.Mvvm;
using SDTaskRunner.Core.Progress;
using SDTaskRunner.Models;
using SDTaskRunner.Utils;

namespace SDTaskRunner.ViewModels;

public class MainWindowViewModel : BindableBase
{
    private readonly AppVersionInfo appVersionInfo = new();
    private GenerationRequest generationRequest;
    private IProgressSource progressSource;

    public MainWindowViewModel()
    {
        SetDummies();
    }

    public string Title => appVersionInfo.GetAppNameWithVersion();

    public GenerationRequestListViewModel PendingRequestsViewModel { get; } = new ();

    public GenerationRequestListViewModel RunningRequestsViewModel { get; } = new ();

    public AsyncRelayCommand GenerateAsyncCommand => new (async () =>
    {
        var request = GenerationRequest;
        request.Steps.Value = 5;

        progressSource = new FakeGenerationRunner(
            samplingSteps: request.Steps.Value,
            totalTime: TimeSpan.FromSeconds(5));

        await foreach (var progress in progressSource.RunAsync(CancellationToken.None))
        {
            // ここが /progress を叩いてるのと同じ
            // UpdateUi(progress);
            Console.WriteLine($"progress = {progress.Progress}");
        }
    });

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

        RunningRequestsViewModel.Items.Add(new GenerationRequest() { Header = "Request 1", });
        RunningRequestsViewModel.Items.Add(new GenerationRequest() { Header = "Request 2", });
        RunningRequestsViewModel.Items.Add(new GenerationRequest() { Header = "Request 3", });

        PendingRequestsViewModel.Items.Add(new GenerationRequest() { Header = "Request 11", });
        PendingRequestsViewModel.Items.Add(new GenerationRequest() { Header = "Request 12", });
        PendingRequestsViewModel.Items.Add(new GenerationRequest() { Header = "Request 13", });
        PendingRequestsViewModel.Items.Add(new GenerationRequest() { Header = "Request 14", });
    }
}