using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace DietCalculator.Logic
{
    [XmlType("ingrediente")]
    public class Ingrediente
    {
        public string nombre;
        public float calorias;
        public float grasas;
    }
}
