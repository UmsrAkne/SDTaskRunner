using System.Collections.Generic;
using System.Threading;

namespace SDTaskRunner.Core.Progress
{
    public interface IProgressSource
    {
        public IAsyncEnumerable<ProgressResponse> RunAsync(CancellationToken ct = default);
    }
}