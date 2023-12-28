using System.Collections.ObjectModel;

namespace CyConex.Graph
{
    internal class NodeAssets
    {
        private ObservableCollection<NodeAsset> _assets;
        public ObservableCollection<NodeAsset> Assets
        {
            get { return _assets; }
            set { _assets = value; }
        }

        public void NodeAssetCollection()
        {
            _assets = new ObservableCollection<NodeAsset>();
        }
    }
}
