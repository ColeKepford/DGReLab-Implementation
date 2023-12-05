using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DGReLab_Implementation
{
    public class Utility
    {
        private int _NodeCounter = 0;
        public Dictionary<string, List<Tuple<int, int, int>>> PathIdDictionary = new();
        public Dictionary<Tuple<int, int, int>, Node> IdNodeDictionary = new();
        public List<Tuple<int, int, int>> PathIds = new();

        public Utility()
        {

        }
        public void LoadDocument(string url)
        {
            var loadWatch = new System.Diagnostics.Stopwatch();
            loadWatch.Start();
            XElement rootElement = XElement.Load(url);
            if (rootElement == null)
            {
                throw new NullReferenceException(nameof(rootElement));
            }
            var rootNode = new Node(new Tuple<int, int, int>(1, 0, 0), rootElement);
            DepthFirstSearchAndLabel(rootNode, 0);
            loadWatch.Stop();
            Console.WriteLine($"Loading Time: {loadWatch.ElapsedMilliseconds} ms");
        }
        private int DepthFirstSearchAndLabel(Node node, int parentId)
        {
            int selfId = ++_NodeCounter;
            if (node.HasChildren())
            {
                int regionId = 1;
                foreach (var xNode in node.XElement.Elements())
                {
                    Node newNode = new(xNode);
                    regionId = DepthFirstSearchAndLabel(newNode, selfId);
                }
                node.Id = new Tuple<int, int, int>(selfId, parentId, regionId);
            }
            else
            {
                node.Id = new Tuple<int, int, int>(selfId, parentId, selfId);
            }
            AddPaths(node);
            IdNodeDictionary.Add(node.Id, node);
            return node.Id.Item3;
        }
        private void AddPaths(Node node)
        {
            //Populate Path Dictionary
            var elementsFromCurrent = new LinkedList<string>();
            var currentXElement = node.XElement;
            while (currentXElement != null)
            {
                string path = "//";
                elementsFromCurrent.AddFirst(currentXElement.Name.ToString());
                for (int i = 0; i < elementsFromCurrent.Count; i++)
                {
                    if (i + 1 < elementsFromCurrent.Count)
                    {
                        path += elementsFromCurrent.ElementAt(i) + "/";
                    }
                    else
                    {
                        path += elementsFromCurrent.ElementAt(i);
                    }
                }
                AddPath(path, node.Id);
                currentXElement = currentXElement.Parent;
            }
        }

        private void AddPath(string path, Tuple<int, int, int> id)
        {
            var paths = PathIdDictionary.GetValueOrDefault(path);
            if (paths != null)
            {
                paths.Add(id);
            }
            else
            {
                var newPathsList = new List<Tuple<int, int, int>>
                {
                    id
                };
                PathIdDictionary.Add(path, newPathsList);
            }
        }

        public List<Node> Query(string query)
        {
            List<Node> result = new List<Node>();
            var idList = PathIdDictionary[query];
            if (idList == null)
            {
                Console.WriteLine("No paths matching that query");
                return new List<Node>();
            }
            foreach(var idTuple in idList)
            {
                var node = IdNodeDictionary[idTuple];
                result.Add(node);
            }
            return result;
        }
    }
}
