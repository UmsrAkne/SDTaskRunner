namespace SDTaskRunner.Models
{
    public class ResolvedGenerationRequest
    {
        public string Prompt { get; init; }

        public string NegativePrompt { get; set; }

        public int Width { get; init; }

        public int Height { get; init; }

        public int Steps { get; init; }

        public long Seed { get; init; }
    }
}