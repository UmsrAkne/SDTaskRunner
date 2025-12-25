namespace SDTaskRunner.Models
{
    public class GenerationVariant
    {
        public RequestField<string> Prompt { get; init; } = RequestField<string>.Undefined();

        public RequestField<string> NegativePrompt { get; init; } = RequestField<string>.Undefined();

        public RequestField<int> Width { get; init; } = RequestField<int>.Undefined();

        public RequestField<int> Height { get; init; } = RequestField<int>.Undefined();

        public RequestField<int> Steps { get; init; } = RequestField<int>.Undefined();

        public RequestField<long> Seed { get; init; } = RequestField<long>.Undefined();
    }
}