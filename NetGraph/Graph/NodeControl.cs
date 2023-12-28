namespace CyConex.Graph
{
    internal class NodeControl
    {
        string controlStrength;
        string strength;
        string description;
        string color;
        public string ControlStrength
        {
            get { return controlStrength; }
            set { controlStrength = value; }
        }

        public string Strength
        {
            get { return strength; }
            set { strength = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        /*public string Color
        {
            get { return color; }
            set { description = value; }
        }*/

        public NodeControl(string controlStrength, string strength, string description/*, string color = "RGB(0,0,0)"*/)
        {
            this.controlStrength = controlStrength;
            this.strength = strength;
            this.description = description;
            //this.color = color;
        }
    }
}
