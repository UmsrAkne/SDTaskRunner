namespace SDTaskRunner.Core.Progress
{
    public class ProgressState
    {
        public bool Skipped { get; set; }

        public bool Interrupted { get; set; }

        public bool StoppingGeneration { get; set; }

        public string Job { get; set; } = string.Empty;

        public int JobCount { get; set; }

        public string JobTimestamp { get; set; } = string.Empty;

        public int JobNo { get; set; }

        public int SamplingStep { get; set; }

        public int SamplingSteps { get; set; }
    }
}