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
        public List<Ingrediente> Ingredientes { get; set; }

        [XmlArrayItem("herramienta")]
        public List<string> Herramientas { get; set; }

        [XmlArrayItem("paso")]
        public List<string> Procedimiento { get; set; }
    }
}
