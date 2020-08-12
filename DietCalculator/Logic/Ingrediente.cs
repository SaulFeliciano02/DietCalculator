using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace DietCalculator.Logic
{
    [XmlType("ingrediente")]
    public class Ingrediente
    {
        public string Nombre { get; set; }
        public int Calorias { get; set; }
        public int Grasas { get; set; }
    }
}
