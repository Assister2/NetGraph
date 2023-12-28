using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Configuration;
using System.Drawing.Design;

namespace CyConex.Graph
{
    public class GraphProperties
	{
		public event EventHandler<GraphLayout> LayoutChanged;
		public event EventHandler<OverlapAlgorithm> OverlapChanged;

		[Category("Properties")]
		[Browsable(true)]
		[DisplayName("Name")]
		[StringValidator(MinLength = 0, MaxLength = 250)]
		[Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
		public string Name { get; set; }

		[Category("Properties")]
		[Browsable(true)]
		[DisplayName("Created")]
		public DateTime? Created { get; set; }

		[Category("Properties")]
		[Browsable(true)]
		[DisplayName("Updated")]
		public DateTime? Updated { get; set; }

		[Category("Properties")]
		[Browsable(true)]
		[DisplayName("Description")]
		[StringValidator(MinLength = 0, MaxLength = 250)]
		[Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
		public string Description { get; set; }

		[Category("Properties")]
		[Browsable(true)]
		[DisplayName("Notes")]
		[StringValidator(MinLength = 0, MaxLength = 2000)]
		[Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
		public string Notes { get; set; }

		private GraphLayout _layout;
		[Category("Properties")]
		[Browsable(true)]
		[DisplayName("Layout")]
		public GraphLayout Layout 
		{
			get { return _layout; }
			set 
			{
				if (_layout != value)
				{
					_layout = value;
					LayoutChanged?.Invoke(this, value);
				}
			}
		}

		private OverlapAlgorithm _overlapAlgorithm;
		[Category("Properties")]
		[Browsable(true)]
		[DisplayName("Overlap removal algorithm")]
		public OverlapAlgorithm Overlap 
		{
			get
			{
				return _overlapAlgorithm;
			}
			set 
			{ 
				if (_overlapAlgorithm != value)
				{
					_overlapAlgorithm = value;
					OverlapChanged?.Invoke(this, value);
				}
			}
		}

		[Category("Properties")]
		[Browsable(true)]
		[DisplayName("Show labels for nodes")]
		public bool ShowLabels { get; set; }

		[Category("Version")]
		[Browsable(true)]
		[DisplayName("Major version")]
		public double MajorVersion { get; set; }

		[Category("Version")]
		[Browsable(true)]
		[DisplayName("Minor version")]
		public double MinorVersion { get; set; }

		[Category("Version")]
		[Browsable(true)]
		[DisplayName("Revision")]
		public int Revision { get; set; }

		[Category("Scores")]
		[Browsable(true)]
		[ReadOnly(true)]
		[DisplayName("Max score")]
		public double MaxScore { get; set; }

		[Category("Scores")]
		[Browsable(true)]
		[ReadOnly(true)]
		[DisplayName("Assessed score")]
		public double AssessedScore { get; set; }

		[Category("Scores")]
		[Browsable(true)]
		[ReadOnly(true)]
		[DisplayName("Impacted score")]
		public double ImpactedScore { get; set; }

		public GraphProperties()
		{
			Created = DateTime.Now;
			ShowLabels = true;
			Layout = GraphLayout.Freeform;
			Overlap = OverlapAlgorithm.None;
		}

	}
}
