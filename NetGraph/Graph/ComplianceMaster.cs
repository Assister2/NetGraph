using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyConex.Graph
{
    public class ComplianceMaster : INotifyPropertyChanged
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

        private double _score;
        public double Score
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

        private double? _Strength;
        public double? Strength
        {
            get { return _Strength; }
            set { _Strength = value; }
        }

        private double? _Implemented;
        public double? Implemented
        {
            get { return _Implemented; }
            set { _Implemented = value; }
        }

        private double? _ObjectiveTarget;
        public double? ObjectiveTarget
        {
            get { return _ObjectiveTarget; }
            set { _ObjectiveTarget = value; }
        }

        private double? _objectiveAcheived;
        public double? ObjectiveAcheived
        {
            get { return _objectiveAcheived; }
            set { _objectiveAcheived = value; }
        }

        private string _NodesInPath;
        public string NodesInPath
        {
            get { return _NodesInPath; }
            set { _NodesInPath = value; }
        }

        private string _ControlStrengthText;
        public string ControlStrengthText
        {
            get { return _ControlStrengthText; }
            set { _ControlStrengthText = value; }
        }

        private string _ControlImplementedText;
        public string ControlImplementedText
        {
            get { return _ControlImplementedText; }
            set { _ControlImplementedText = value; }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

    }
}
