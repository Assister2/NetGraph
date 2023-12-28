namespace CyConex.Graph
{
    public class LocalEdge
    {
        public string edgeID { get; set; }
        public string impactedValue { get; set; }
        public string edgeStrengthValue { get; set; }
        public string edgeStrengthMinValue { get; set; }
        public string edgeStrengthDistribution { get; set; }
        public string edgeStrengthScore { get; set; }
        public bool enabled {get; set; }
    }
}
