namespace CyConex.Graph
{
    internal class NodeAttribute
    {
        string attrImpact;
        string attrValue;
        string attrDescription;

        public string Title { get { return attrImpact; } set { attrImpact = value; } }
        public string Value { get { return attrValue; } set { attrValue = value; } }
        public string Description { get { return attrDescription; } set { attrDescription = value; } }

        public NodeAttribute(string impact, string value, string desc)
        {
            this.attrImpact = impact;
            this.attrValue = value;
            this.attrDescription = desc;
        }
    }
}
