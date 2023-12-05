using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DGReLab_Implementation
{
    public class Node
    {
        public Tuple<int, int, int> Id { get; set; }
        
        public XElement XElement { get; set; }
        public Node(XElement xElement)
        {
            Id = new Tuple<int, int, int>(1, 0, 0);
            XElement = xElement;
        }

        public Node(Tuple<int, int, int> id, XElement xElement)
        {
            Id = id;
            XElement = xElement;
        }

        public bool HasChildren()
        {
            return XElement != null && XElement.Elements() != null && XElement.Elements().Any(); 
        }

        public string GetValue()
        {
            return XElement.Value;
        }
        public string GetName()
        {
            return XElement.Name.ToString();
        }
    }
}
