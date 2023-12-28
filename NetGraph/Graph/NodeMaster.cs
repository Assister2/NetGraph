using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyConex.Graph
{
    public class NodeMaster : INotifyPropertyChanged
    {
        private string _nodeID;
        public string NodeID
        {
            get
            {
                return _nodeID;
            }
            set
            {
                _nodeID = value;
            }
        }

        private string _score;
        public string Score
        {
            get { return _score; }
            set { _score = value; }
        }

        private string _type;
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        private List<NodeDetail> _nodeDetails;
        public List<NodeDetail> nodeDetails
        {
            get
            {
                return this._nodeDetails;
            }
            set
            {
                this._nodeDetails = value;
                RaisePropertyChanged("NodeDetails");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
