using Newtonsoft.Json.Linq;

namespace CyConex.Graph
{
    internal class NodeSearch
    {
        string id;
        string title;
        string description;
        string reference;
        string framework;
        string notes;
        string category;
        string subCategory;
        string version;
        string domain;
        string subDomain;
        string level;
        string tags;
        public string ID
        {
            get { return id; }
            set { id = value; }
        }
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public string Reference
        {
            get { return reference; }
            set { reference = value; }
        }

        public string Framework
        {
            get { return framework; }
            set { framework = value; }
        }

        public string Notes
        {
            get { return notes; }
            set { notes = value; }
        }

        public string Category
        {
            get { return category; }
            set { category = value; }
        }

        public string SubCategory
        {
            get { return subCategory; }
            set { subCategory = value; }
        }

        public string Version
        {
            get { return version; }
            set { version = value; }
        }

        public string Domain
        {
            get { return domain; }
            set { domain = value; }
        }

        public string SubDomain
        {
            get { return subDomain; }
            set { subDomain = value; }
        }

        public string Level
        {
            get { return level; }
            set { level = value; }
        }

        public string Tags
        {
            get { return tags; }
            set { tags = value; }
        }
        public NodeSearch(string id, string title, string description, string reference, 
            string framework, string note, string category, string subCategory, string version,
            string domain, string subDomain, string level, string tags)
        {
            this.id = id;
            this.title = title;
            this.description = description;
            this.reference = reference;
            this.framework = framework;
            this.notes = note;
            this.category = category;
            this.subCategory = subCategory;
            this.version = version;
            this.domain = domain;
            this.SubDomain = subDomain;
            this.level = level;
            this.tags = tags;
        }
    }
}
