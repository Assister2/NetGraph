namespace CyConex.Graph
{
    internal class NodeCategory
    {
        string category;
        string parent_category;
        public string NodeCategoryData
        {
            get { return category; }
            set { category = value; }
        }

        public string ParentCategory
        {
            get { return parent_category; }
            set { parent_category = value; }
        }

        public NodeCategory(string cat, string parent_cat = "")
        {
            this.parent_category = parent_cat;
            this.category = cat;
        }
    }
}
