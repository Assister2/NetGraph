using EnvDTE;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace CyConex.Graph
{
    public class NodeDetail
    {
        private string _masterNodeID;
        public string MasterNodeID
        {
            get { return _masterNodeID; }
            set { _masterNodeID = value; }
        }

        private string _nodeID;
        public string NodeID
        {
            get { return _nodeID; }
            set { _nodeID = value; }
        }

        [Display(Name = "Score")]
        private string _score;
        public string Score
        {
            get { return _score; }
            set { _score = value; }
        }

        [Display(Name = "Relationship")]
        private string _relationship;
        public string Relationship
        {
            get { return _relationship; }
            set { _relationship = value; }  
        }

        [Display(Name = "Type")]
        private string _type;
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        [Display(Name = "Title")]
        private string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public NodeDetail(string masterNodeID, string nodeID, string score, string relationship, string type, string title)
        {
            _masterNodeID = masterNodeID;
            _nodeID = nodeID;
            _score = score; 
            _type = type;
            _title = title;
            _relationship = relationship;
        }
    }
}
