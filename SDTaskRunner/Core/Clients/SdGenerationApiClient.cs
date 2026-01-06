using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace SDTaskRunner.Core.Clients
{
    public class SdGenerationApiClient : IDisposable
    {
        private readonly HttpClient httpClient = new()
        {
            BaseAddress = new Uri("http://127.0.0.1:7860"),
            Timeout = TimeSpan.FromMinutes(5),
        };

        public async Task<string[]> Txt2ImgAsync(object payload, CancellationToken cancellationToken = default)
        {
            var response = await httpClient.PostAsJsonAsync(
                "/sdapi/v1/txt2img",
                payload,
                cancellationToken);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<Txt2ImgResponse>(
                cancellationToken: cancellationToken);

            return result?.Images ?? Array.Empty<string>();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            httpClient?.Dispose();
        }

        private sealed class Txt2ImgResponse
        {
            public string[] Images { get; set; } = Array.Empty<string>();

            public object Parameters { get; set; }

            public string Info { get; set; }
        }
    }
}