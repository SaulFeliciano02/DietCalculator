﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        public List<Receta> Recetas { get; set; } = new List<Receta>();

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
            Recetas = XmlToObject(XmlContent);
            InitializeProlog();
        }

        public (int, int, int) GetInfo()
        {
            (int, int, int) tuple = (0, 0, 0);

            tuple.Item1 = Recetas.Count;
            tuple.Item2 = Recetas.Select(x => x.Ingredientes.Select(y => y.Nombre)).Distinct().ToList().Count;
            tuple.Item3 = Recetas.Select(x => x.Herramientas).Distinct().ToList().Count;

            return tuple;
        }

        public void GetDtdData(string path)
        {

        }

        private void InitializeProlog()
        {
            //_prologEngine.Consult($"{AppDomain.CurrentDomain.BaseDirectory.Replace("\\", "/")}Resources/recetas.pl");

            //foreach (var receta in Recetas)
            //{
            //    _prologEngine.GetFirstSolution($"asserta(ingredientes({receta.Nombre},[{string.Join(',', receta.Ingredientes.Select(x => x.Nombre).ToList())}])).");
            //    _prologEngine.GetFirstSolution($"asserta(herramientas({receta.Nombre},[{string.Join(',', receta.Herramientas)}])).");
            //}

            //MessageBox.Show(_prologEngine.GetFirstSolution("ingredientes(sopa_de_cebolla, L).").ToString());
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
