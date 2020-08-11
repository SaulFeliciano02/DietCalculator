using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Xml.Schema;

namespace DietCalculator.Logic
{
    public class MainController
    {
        private static MainController _instance = null;

        public string XmlPath { get; set; }
        public bool IsValid { get; set; } = true;

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
    }
}
