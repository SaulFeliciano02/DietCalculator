using DietCalculator.Logic;
using DietCalculator.Windows;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Documents;
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

                try
                {
                    MainController.Instance.GetXmlData(openFileDialog.FileName);
                    BtnOpenDtd.IsEnabled = true;
                    BtnContinue.IsEnabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR: " + ex.Message);
                }
            }
        }

        private void BtnContinue_Click(object sender, RoutedEventArgs e)
        {
            BtnContinue.IsEnabled = false;
            BtnOpenXml.IsEnabled = false;

            MainController.Instance.IsValid = true;

            if (MainController.Instance.IsValid)
            {
                try
                {
                    //MainController.Instance.Recetas = MainController.XmlToObject<List<Receta>>();

                    var infoWindow = new InfoWindow();
                    infoWindow.Show();
                    Close();
                }
                catch
                {
                    MessageBox.Show("Error al obtener los datos del archivo XML");
                    BtnContinue.IsEnabled = true;
                    BtnOpenXml.IsEnabled = true;
                }
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

        private void BtnOpenDtd_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Archivos DTD (*.dtd)|*.dtd"
            };

            if (openFileDialog.ShowDialog().GetValueOrDefault())
            {
                LabelDtd.Content = openFileDialog.SafeFileName;

                try
                {
                    MainController.Instance.GetDtdData(openFileDialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR: " + ex.Message);
                }
            }
        }
    }
}
