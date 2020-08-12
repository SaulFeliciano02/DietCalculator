using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace DietCalculator.Logic
{
    [XmlType("ingrediente")]
    public class Ingrediente
    {
        [XmlElement("nombre")]
        public string Nombre { get; set; }

        [XmlElement("calorias")]
        public int Calorias { get; set; }

        [XmlElement("grasas")]
        public int Grasas { get; set; }
    }
}
