using System;
using System.Diagnostics;
using System.Windows;
using Prism.Commands;
using Prism.Mvvm;
using SDTaskRunner.Core;
using SDTaskRunner.Core.Progress;
using SDTaskRunner.Models;
using SDTaskRunner.Utils;

namespace SDTaskRunner.ViewModels;

public class MainWindowViewModel : BindableBase
{
    private readonly AppVersionInfo appVersionInfo = new();
    private readonly GenerationWorker worker = new();
    private GenerationRequest activeGenerationRequest;
    private IProgressSource progressSource;

    public MainWindowViewModel()
    {
        PendingRequestsViewModel.SelectedItemChanged += request => ActiveGenerationRequest = request;
        RunningRequestsViewModel.SelectedItemChanged += request => ActiveGenerationRequest = request;

        worker.RequestStarted += OnRequestStarted;

        // worker.ProgressUpdated += OnProgressUpdated;
        worker.RequestCompleted += request =>
        {
            request.Status = GenerationStatus.Finished;
            Console.WriteLine($"Request {request.Header} completed.");
        };

        SetDummies();
    }

    public string Title => appVersionInfo.GetAppNameWithVersion();

    public GenerationRequestListViewModel PendingRequestsViewModel { get; } = new ();

    public GenerationRequestListViewModel RunningRequestsViewModel { get; } = new ();

    public DelegateCommand GenerateAsyncCommand => new (() =>
    {
        var request = ActiveGenerationRequest;
        if (request.Steps.Value == 0)
        {
            request.Steps.Value = 5;
        }

        worker.Enqueue(request);
    });

    public GenerationRequest ActiveGenerationRequest
    {
        get => activeGenerationRequest;
        set => SetProperty(ref activeGenerationRequest, value);
    }

    private void OnRequestStarted(GenerationRequest request)
    {
        // Running に追加
        Application.Current.Dispatcher.Invoke(() =>
        {
            RunningRequestsViewModel.Items.Add(request);
        });

        // UI 編集対象を更新（任意）
        // ActiveGenerationRequest = request;
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