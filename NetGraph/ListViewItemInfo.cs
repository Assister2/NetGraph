using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace NetGraph
{
    internal class ListViewItemInfo
    {
        private string itemName;
        private ImageSource image;

        public string ItemName
        {
            get { return this.itemName; }
            set
            {
                this.itemName = value;
                this.RaisePropertyChanged("ItemName");
            }
        }

        public ImageSource ItemImage
        {
            get { return this.image; }
            set
            {
                this.image = value;
                this.RaisePropertyChanged("ItemImage");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(String name)
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
