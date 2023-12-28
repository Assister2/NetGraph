using System.ComponentModel;

namespace CyConex.Graph
{
    public class Location
	{
		[Category("Location")]
		[Browsable(true)]
		[ReadOnly(true)]
		[DisplayName("X")]
		public double X { get; set; }
		[Category("Location")]
		[Browsable(true)]
		[ReadOnly(true)]
		[DisplayName("Y")]
		public double Y { get; set; }

		public Location()
		{
			X = 0;
			Y = 0;
		}

		public Location(double x, double y)
		{
			X = x;
			Y = y;
		}
	}
}
