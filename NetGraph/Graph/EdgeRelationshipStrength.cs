using Syncfusion.DocIO.DLS;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyConex.Graph
{
    internal class EdgeRelationshipStrength
    {
        string assetStatus;
        //string assetValueFrom;
        //string assetValueTo;
        string asssetValue;
        string assetDescription;
        string width;
        string color;

        public string Status
        {
            get { return assetStatus; }
            set { assetStatus = value; }
        }

        public string Value
        {
            get { return asssetValue; }
            set { asssetValue = value; }
        }

        //public string ValueFrom
        //{
        //    get { return assetValueFrom; }
        //    set { assetValueFrom = value; }
        //}

        //public string ValueTo
        //{
        //    get { return assetValueTo; }
        //    set { assetValueTo = value; }
        //}

        public string Description
        {
            get { return assetDescription; }
            set { assetDescription = value; }
        }

        //public string Width
        //{
        //    get { return width; }
        //    set { width = value; }
        //}

        //public string Color
        //{
        //    get { return color; }
        //    set { color = value; }
        //}

        public EdgeRelationshipStrength(string assetStatus, string assetValue, string assetDescription)
        {
            this.Status = assetStatus;
            //this.ValueFrom = assetValueFrom;
            //this.ValueTo = assetValueTo;
            this.Value = assetValue;
            this.Description = assetDescription;
            //this.width = width;
            //this.color = color;
        }
    }
}
