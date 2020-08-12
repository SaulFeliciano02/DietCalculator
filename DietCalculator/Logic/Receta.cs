using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace DietCalculator.Logic
{
    [XmlType("receta")]
    public class Receta
    {
        public string Nombre { get; set; }

        public List<Ingrediente> Ingredientes { get; set; } = new List<Ingrediente>();

        [XmlArrayItem("herramienta")]
        public List<string> Herramientas { get; set; } = new List<string>();

        [XmlArrayItem("paso")]
        public List<string> Procedimiento { get; set; } = new List<string>();
    }
}
