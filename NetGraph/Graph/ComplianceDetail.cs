using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyConex.Graph
{
    public class ComplianceDetail : INotifyPropertyChanged
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


        private string _type;
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        private string _relationship;
        public string Releationship
        {
            get { return _relationship; }
            set { _relationship = value; }
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        private string _Description;
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        private string _Framework;
        public string Framework
        {
            get { return _Framework; }
            set { _Framework = value; }
        }

        private string _Reference;
        public string Reference
        {
            get { return _Reference; }
            set { _Reference = value; }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
