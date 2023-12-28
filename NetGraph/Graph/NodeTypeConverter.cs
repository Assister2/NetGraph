using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace CyConex.Graph
{
    public class NodeTypeConverter : TypeConverter
    {
        public NodeTypeConverter()
        {

        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }


        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return true;
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            string typeInCytoscape = value.ToString();
            return NodeTypes.NodesTypes.FirstOrDefault(item => item.Name == typeInCytoscape);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (value == null)
            {
                return "Not defined";
            }
            else
            {
                return (value as NodeType).Name;
            }
        }

        private StandardValuesCollection GetValues()
        {
            return new StandardValuesCollection(NodeTypes.NodesTypes);
        }


        protected void SetList(List<NodeType> list)
        {
            NodeTypes.NodesTypes = list;
        }


        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(NodeTypes.NodesTypes);
        }
    }
}
