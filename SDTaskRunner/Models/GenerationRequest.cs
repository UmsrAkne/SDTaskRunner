using Prism.Mvvm;

namespace SDTaskRunner.Models
{
    public class GenerationRequest : BindableBase
    {
        private GenerationStatus status = GenerationStatus.Waiting;

        public string Header { get; set; } = string.Empty;

        public RequestField<string> Prompt { get; init; } = RequestField<string>.Undefined();

        public RequestField<string> NegativePrompt { get; init; } = RequestField<string>.Undefined();

        public RequestField<int> Width { get; init; } = RequestField<int>.Undefined();

        public RequestField<int> Height { get; init; } = RequestField<int>.Undefined();

        public RequestField<int> Steps { get; init; } = RequestField<int>.Undefined();

        public RequestField<long> Seed { get; init; } = RequestField<long>.Undefined();

        public GenerationStatus Status { get => status; set => SetProperty(ref status, value); }
    }
}