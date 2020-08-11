using DietCalculator.Logic;
using IronScheme;
using Microsoft.Win32;
using Prolog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Schema;

namespace DietCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnOpenXml_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Archivos XML (*.xml)|*.xml"
            };

            if (openFileDialog.ShowDialog().GetValueOrDefault())
            {
                LabelXml.Content = openFileDialog.SafeFileName;
                MainController.Instance.XmlPath = openFileDialog.FileName;
            }
        }

        private void BtnContinue_Click(object sender, RoutedEventArgs e)
        {
            BtnContinue.IsEnabled = false;
            BtnOpenXml.IsEnabled = false;

            MainController.Instance.IsValid = true;

            var xmlResolver = new XmlUrlResolver();
            var settings = new XmlReaderSettings();
            settings.ValidationEventHandler += new ValidationEventHandler(XmlValidationCallBack);
            settings.ValidationType = ValidationType.DTD;
            settings.IgnoreWhitespace = true;
            settings.DtdProcessing = DtdProcessing.Parse;
            settings.XmlResolver = xmlResolver;

            XmlReader reader = XmlReader.Create(MainController.Instance.XmlPath, settings);

            while (reader.Read()) ;

            if (MainController.Instance.IsValid)
            {

            }
        }

        private void XmlValidationCallBack(object sender, ValidationEventArgs e)
        {
            if (e.Severity == XmlSeverityType.Warning)
                MessageBox.Show("Warning: Matching schema not found.  No validation occurred." + e.Message);
            else
                MessageBox.Show("Validation error: " + e.Message);

            BtnContinue.IsEnabled = true;
            BtnOpenXml.IsEnabled = true;

            MainController.Instance.IsValid = false;
        }
    }
}
