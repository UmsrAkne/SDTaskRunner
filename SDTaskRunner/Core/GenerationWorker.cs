using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using SDTaskRunner.Core.Progress;
using SDTaskRunner.Models;

namespace SDTaskRunner.Core
{
    public class GenerationWorker
    {
        private readonly Channel<GenerationRequest> channel;
        private readonly CancellationTokenSource cts = new ();

        public GenerationWorker()
        {
            channel = Channel.CreateUnbounded<GenerationRequest>(
                new UnboundedChannelOptions
                {
                    SingleReader = true, // ワーカーは1つだけ
                    SingleWriter = false, // UIスレッドなど複数から積める
                });

            _ = Task.Run(ProcessQueueAsync);
        }

        public event Action<ProgressResponse> ProgressUpdated;

        public event Action<GenerationRequest> RequestStarted;

        public event Action<GenerationRequest> RequestCompleted;

        public void Enqueue(GenerationRequest request)
        {
            channel.Writer.TryWrite(request);
        }

        public void Stop()
        {
            cts.Cancel();
        }

        private async Task ProcessQueueAsync()
        {
            await foreach (var request in channel.Reader.ReadAllAsync(cts.Token))
            {
                RequestStarted?.Invoke(request);

                var runner = new FakeGenerationRunner(
                    request.Steps.Value,
                    TimeSpan.FromSeconds(5));

                await foreach (var progress in runner.RunAsync(cts.Token))
                {
                    ProgressUpdated?.Invoke(progress);
                }

                RequestCompleted?.Invoke(request);
            }
        }
    }
}