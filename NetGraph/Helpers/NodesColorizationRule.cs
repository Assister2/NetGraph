using System.Drawing;

namespace CyConex
{
    public class NodesColorizationRule
	{
		public double Value { get; set; }

		public Color Color { get; set; }

		public NodesColorizationRule()
		{
			Value = 0;
			Color = Color.Black;
		}

		public NodesColorizationRule(double value, Color color)
		{
			Value = value;
			Color = color;
		}
	}
}
