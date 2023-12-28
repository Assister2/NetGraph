using CyConex.Graph;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace CyConex.Helpers
{
    public class CacheHelper
    {
        public string nodeRepoKey = "nodeRepository";
        public string nodeCategoryKey = "nodeCategory";

        private ObjectCache _cacheObj = MemoryCache.Default;
        CacheItemPolicy _cacheItemPolicy = new CacheItemPolicy();

        public CacheHelper()
        {
            _cacheItemPolicy.AbsoluteExpiration = DateTime.Now.AddHours(24.0); 
        }

        public void ClearCache()
        {
            _cacheObj.Remove(nodeRepoKey);
            _cacheObj.Remove(nodeCategoryKey);
        }
        
        public void Set(string key, object value) { 
            if (_cacheObj == null || _cacheObj.Get(key) == null)
            {
                _cacheObj.Add(key, value, _cacheItemPolicy);
            }
            else
            {
                _cacheObj.Set(key, value, _cacheItemPolicy);
            }
        }

        public bool UpdateNodeRepo(Node node)
        {
            bool flag = true;
            List<Node> _nodeList = _cacheObj.Get(nodeRepoKey) as List<Node>;
            foreach (Node _node in _nodeList)
            {
                if (_node.ID == node.ID)
                {
                    flag = false;
                    break;
                }
            }
            if (flag)
            {
                _nodeList.Add(node);
                this.Set(nodeRepoKey, _nodeList);
            }
            
            return flag;
        }

        public bool RemoveNodeRepoItem(string nodeID)
        {
            bool flag = false;
            List<Node> _nodeList = _cacheObj.Get(nodeRepoKey) as List<Node>;
            if (_nodeList == null) return flag;
            
            foreach (Node _node in _nodeList)
            {
                if (_node.ID == nodeID)
                {
                    _nodeList.Remove(_node);
                    flag = true;
                }
            }
            this.Set(nodeRepoKey, _nodeList);
            return flag;
        }

        public void Remove(string key)
        {
            _cacheObj.Remove(key);
        }

        public object Get(string key)
        {
            return _cacheObj.Get(key) == null ? new JArray() : _cacheObj.Get(key);
        }
    }
}
