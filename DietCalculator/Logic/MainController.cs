using IronScheme.Scripting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Xml;
using System.Xml.Serialization;

namespace DietCalculator.Logic
{
    public class MainController
    {
        private static MainController _instance = null;
        public string XmlContent { get; set; }
        public string DtdContent { get; set; }
        public bool IsValid { get; set; } = true;

        public List<Receta> recetas = new List<Receta>();

        private MainController()
        {

        }

        public static MainController Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MainController();

                return _instance;
            }
        }

        public void GetXmlData(string path)
        {
            XmlContent = File.ReadAllText(path);
            recetas = XmlToObject(XmlContent);
        }

        public (int, int, int) GetInfo()
        {
            (int, int, int) tuple = (0, 0, 0);

            tuple.Item1 = recetas.Count;
            tuple.Item2 = recetas.Select(x => x.ingredientes.Select(y => y.nombre)).Distinct().ToList().Count;
            tuple.Item3 = recetas.Select(x => x.herramientas).Distinct().ToList().Count;

            return tuple;
        }

        public void GetDtdData(string path)
        {

        }

        public static List<Receta> XmlToObject(string xml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Receta>), new XmlRootAttribute("recetas"));
            var textReader = new StringReader(xml);
            var xmlReader = XmlReader.Create(textReader);
            return (List<Receta>)serializer.Deserialize(xmlReader);
        }

        public static string XmlSerialize(List<Receta> data)
        {
            XmlSerializer serializer = new XmlSerializer(data.GetType(), new XmlRootAttribute("recetas"));
            var sb = new StringBuilder();
            using (var writer = XmlWriter.Create(sb))
            {
                serializer.Serialize(writer, data);
            }
            string dataXML = sb.ToString();
            return dataXML;
        }
    }
}
