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
    private GenerationRequest activeGenerationRequest;
    private IProgressSource progressSource;

    public MainWindowViewModel()
    {
        PendingRequestsViewModel.SelectedItemChanged += request => ActiveGenerationRequest = request;
        RunningRequestsViewModel.SelectedItemChanged += request => ActiveGenerationRequest = request;
        SetDummies();
    }

    public string Title => appVersionInfo.GetAppNameWithVersion();

    public GenerationRequestListViewModel PendingRequestsViewModel { get; } = new ();

    public GenerationRequestListViewModel RunningRequestsViewModel { get; } = new ();

    public AsyncRelayCommand GenerateAsyncCommand => new (async () =>
    {
        var request = ActiveGenerationRequest;
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

    public GenerationRequest ActiveGenerationRequest
    {
        get => activeGenerationRequest;
        set => SetProperty(ref activeGenerationRequest, value);
    }

    [Conditional("DEBUG")]
    private void SetDummies()
    {
        ActiveGenerationRequest = new();

        ActiveGenerationRequest.Height.Value = 512;
        ActiveGenerationRequest.Width.Value = 480;
        ActiveGenerationRequest.Steps.Value = 10;
        ActiveGenerationRequest.Prompt.Value = "A painting of a squirrel eating a burger";
        ActiveGenerationRequest.NegativePrompt.Value = "Negative prompt test.";
        ActiveGenerationRequest.Seed.Value = 1234;

        RunningRequestsViewModel.Items.Add(new GenerationRequest() { Header = "Request 1", Width = { Value = 120, }, });
        RunningRequestsViewModel.Items.Add(new GenerationRequest() { Header = "Request 2", Width = { Value = 121, }, });
        RunningRequestsViewModel.Items.Add(new GenerationRequest() { Header = "Request 3", Width = { Value = 122, }, });

        PendingRequestsViewModel.Items.Add(new GenerationRequest() { Header = "Request 11", Width = { Value = 123, }, });
        PendingRequestsViewModel.Items.Add(new GenerationRequest() { Header = "Request 12", Width = { Value = 124, }, });
        PendingRequestsViewModel.Items.Add(new GenerationRequest() { Header = "Request 13", Width = { Value = 125, }, });
        PendingRequestsViewModel.Items.Add(new GenerationRequest() { Header = "Request 14", Width = { Value = 126, }, });
    }
}