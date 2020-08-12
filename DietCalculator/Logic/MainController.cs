using SbsSW.SwiPlCs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        [XmlElement("recetas")]
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
            InitializeProlog();
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

        private void InitializeProlog()
        {
            try
            {
                // Environment Variables
                Environment.SetEnvironmentVariable("SWI_HOME_DIR", @"C:\\Program Files (x86)\\swipl");
                Environment.SetEnvironmentVariable("Path", @"C:\\Program Files (x86)\\swipl\\bin");
                string[] p = { "-q", "-f", Path.GetFullPath("Resources\\recetas.pl").Replace('\\', '/') };

                // Connect to Prolog Engine
                PlEngine.Initialize(p);

                PlQuery load = new PlQuery($"cargar('{Path.GetFullPath("Resources\\recetas.pl").Replace('\\', '/')}')");
                load.NextSolution();

                foreach (var receta in recetas)
                {
                    PlQuery q = new PlQuery($"asserta(ingredientes({receta.nombre},[{string.Join(',', receta.ingredientes.Select(x => x.nombre).ToList())}]))");
                    q.NextSolution();

                    q = new PlQuery($"asserta(herramientas({receta.nombre},[{string.Join(',', receta.herramientas)}]))");
                    q.NextSolution();
                }

                //PlQuery test = new PlQuery("poseeUnIngrediente(leche,X)");
                //MessageBox.Show(test.SolutionVariables.ToList().Count.ToString());

                //foreach (PlQueryVariables x in test.SolutionVariables)
                //{
                //    MessageBox.Show(x["X"].ToString());
                //}

                // Close Prolog Connection
                //PlEngine.PlCleanup();
            }
            catch
            {
                MessageBox.Show("Connection Error: Could not connect to Prolog Engine");
            }
        }

        public static List<Receta> XmlToObject(string xml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Receta>), new XmlRootAttribute("recetas"));
            var textReader = new StringReader(xml);
            var xmlReader = XmlReader.Create(textReader);
            return (List<Receta>)serializer.Deserialize(xmlReader);
        }
    }
}
