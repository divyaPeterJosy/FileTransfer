using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BusinessLayer
{
    public class XmlWriter : AbstractWriter
    {
        public override void WriteToOutPutFile(ReaderClass content, string outPutPath)
        {
            if (!Directory.Exists(outPutPath))
            {
                Directory.CreateDirectory(outPutPath);
            }
            List<string> codes = !string.IsNullOrEmpty(content.Code)? content.Code.Split(',').ToList() : new List<string>();
            List<string> description = !string.IsNullOrEmpty(content.FileDescription) ? content.FileDescription.Split(',').ToList() : new List<string>();

            XmlTextWriter writer = new XmlTextWriter(outPutPath + "OutPut.xml", System.Text.Encoding.UTF8);
            writer.WriteStartDocument(true);
            writer.Formatting = Formatting.Indented;
            writer.Indentation = 2;
            writer.WriteStartElement("logData");
            int fileCount = 0;
            foreach( string file in content.FileNames.Split(',') )
            {
                writer.WriteStartElement("file");
                writer.WriteAttributeString("name", file);
                createNode("code", codes[fileCount], writer);
                createNode("description", description[fileCount], writer);
                writer.WriteEndElement();
                fileCount++;
            }

            writer.WriteStartElement("datasection");
            createNode("paramList", content.Code, writer);
            foreach (string fileData in content.Datas)
            {            
                createNode("data", fileData, writer);                
            }
            writer.WriteEndElement();

            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();
        }


        private void createNode(string node, string nodeData, XmlTextWriter writer)
        {
            writer.WriteStartElement(node);            
            writer.WriteString(nodeData);
            writer.WriteEndElement();
        }

    }
}
