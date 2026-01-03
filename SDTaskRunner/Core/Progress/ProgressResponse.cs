namespace SDTaskRunner.Core.Progress
{
    public class ProgressResponse
    {
        public double Progress { get; set; }

        public double EtaRelative { get; set; }

        public ProgressState State { get; set; } = new ();

        public string CurrentImage { get; set; }

        public string TextInfo { get; set; }
    }
}