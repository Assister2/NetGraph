using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace CyConex.Graph
{
    public class ShapeTypeConverter : TypeConverter
    {
        public ShapeTypeConverter()
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
            string shapeInCytoscape = value.ToString();
            return NodeShapes.NodesShapes.FirstOrDefault(item => item.Name == shapeInCytoscape);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (value == null)
            {
                return "Not defined";
            }
            else
            {
                return (value as NodeShape).Name;
            }
        }

        private StandardValuesCollection GetValues()
        {
            return new StandardValuesCollection(NodeShapes.NodesShapes);
        }


        protected void SetList(List<NodeShape> list)
        {
            NodeShapes.NodesShapes = list;
        }


        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(NodeShapes.NodesShapes);
        }
    }
}
