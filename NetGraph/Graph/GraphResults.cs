using Newtonsoft.Json.Linq;
using System;
using System.Drawing;

namespace CyConex.Graph
{
    public class graphResultsList
    {
        public string ActorGUID { get; set; }                       // 0
        public string ActorTitle { get; set; }                      // 1
        public double ActorMitigatedScore { get; set; }             // 2
        public string AttackGUID { get; set; }                      // 3
        public string AttackTitle { get; set; }                     // 4
        public double AttackMitigatedScore { get; set; }            // 5
        public double ThreatScore { get; set; }                     // 6
        public string VulnerabilityGUID { get; set; }               // 7
        public string VulnerabilityTitle { get; set; }              // 8
        public double VulnerabilityMitigatedScore { get; set; }     // 9
        public double LikelihoodScore { get; set; }                    // 10
        public string AssetGUID { get; set; }                       // 11
        public string AssetTitle { get; set; }                      // 12
        public double AssetMitigatedScore { get; set; }             // 13
        public double ImpactScore { get; set; }                     // 14
        public string FullNodeEdgePath { get; set; }                // 15
        public string PathGUID { get; set; }                        // 16
        public string RiskStatement { get; set; }                   // 17
        public Color RiskColor { get; set; }                        // 18
        public string RiskStatus { get; set; }                        // 18
    }

}
