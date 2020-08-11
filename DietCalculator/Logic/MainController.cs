using System;
using System.Collections.Generic;
using System.Text;

namespace DietCalculator.Logic
{
    public class MainController
    {
        private static MainController _instance = null;

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
