using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace DietCalculator.Logic
{
    [XmlType("receta")]
    public class Receta
    {
        public string nombre;
        public List<Ingrediente> ingredientes = new List<Ingrediente>();
        [XmlArrayItem("herramienta")]
        public List<string> herramientas = new List<string>();
        [XmlArrayItem("paso")]
        public List<string> procedimiento = new List<string>();
    }
}
