namespace CyConex
{
    public class NodeListItem
    {
        //private string itemImage;
        private string itemName;
        private string itemType;
        private string itemImage;
        public string ItemName
        {
            get { return itemName; }
            set { itemName = value; }
        }

        public string ItemType
        {
            get { return itemType; }
            set { itemType = value; }
        }

        public string ItemImage
        {
            get {  return itemImage; }
            set { itemImage = value; }
        }
    }
}
