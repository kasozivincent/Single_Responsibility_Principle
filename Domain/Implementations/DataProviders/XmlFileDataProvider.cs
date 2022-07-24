using System.Collections.Generic;
using System.IO;
using Domain.Services;
using System;
using System.Xml;
using System.Text;

namespace Domain.Implementations
{
    public class XmlFileDataProvider : IDataProvider
    {
        private string fileName;

        public XmlFileDataProvider(string fileName)
            => this.fileName = fileName;
        public IEnumerable<string> Read()
        {
            IList<string> tradelines = new List<string>();
            XmlDocument document = new XmlDocument();
            document.Load(fileName);
            StringBuilder builder = new StringBuilder();
            var root = document.DocumentElement;
            foreach(XmlNode tradeline in root.ChildNodes)
            {
                foreach(XmlNode node in tradeline)
                {
                    builder.Append(node.InnerText).Append(" ");
                }
                tradelines.Add(builder.ToString());
                builder = new StringBuilder();
            }
            return tradelines;
        }
            
    }
}
