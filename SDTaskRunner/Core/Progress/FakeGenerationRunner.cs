using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace SDTaskRunner.Core.Progress
{
    public sealed class FakeGenerationRunner : IProgressSource
    {
        private readonly int samplingSteps;
        private readonly TimeSpan totalTime;

        public FakeGenerationRunner(int samplingSteps, TimeSpan totalTime)
        {
            this.samplingSteps = samplingSteps;
            this.totalTime = totalTime;
        }

        public async IAsyncEnumerable<ProgressResponse> RunAsync(
            [EnumeratorCancellation] CancellationToken ct = default)
        {
            var start = DateTime.UtcNow;
            var jobId = $"task({Guid.NewGuid():N})";
            var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");

            for (var step = 0; step <= samplingSteps; step++)
            {
                if (ct.IsCancellationRequested)
                {
                    yield break;
                }

                var elapsed = DateTime.UtcNow - start;
                var progress = (double)step / samplingSteps;
                var eta = Math.Max(
                    0,
                    totalTime.TotalSeconds * (1 - progress));

                yield return new ProgressResponse
                {
                    Progress = progress,
                    EtaRelative = eta,
                    State = new ProgressState
                    {
                        Job = jobId,
                        JobCount = -1,
                        JobTimestamp = timestamp,
                        JobNo = 0,
                        SamplingStep = step,
                        SamplingSteps = samplingSteps,
                    },
                };

                await Task.Delay(totalTime / samplingSteps, ct);
            }

            // === SDWebUI では処理が終わったら Progress 0 のレスポンスが来る。
            yield return new ProgressResponse
            {
                Progress = 0,
                EtaRelative = 0,
                State = new ProgressState
                {
                    Job = string.Empty,
                    JobCount = 0,
                    JobTimestamp = timestamp,
                    JobNo = 1,
                    SamplingStep = 0,
                    SamplingSteps = samplingSteps,
                },
            };
        }
    }
}