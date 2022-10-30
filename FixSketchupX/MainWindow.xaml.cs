using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FixSketchupX
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Left = (System.Windows.SystemParameters.PrimaryScreenWidth - 485)/2 - this.Width;
            this.Top = (System.Windows.SystemParameters.PrimaryScreenHeight - this.Height)/2;
        }
        private void btnConvert_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                Filter = "DirectX Mesh|*.x",
                Title = "Open"
            };
            if (openFileDialog1.ShowDialog() == true)
            {
                String[] lines = System.IO.File.ReadAllLines(openFileDialog1.FileName);
                txtOutput.Text = "File \"" + openFileDialog1.FileName + "\" read successfully.\r\n\r\n";

                for (int i = 0; i < lines.Length - 1; i++)
                {
                    if (lines[i].Contains("   Material "))
                    {
                        txtOutput.Text += "Material: " + lines[i].Replace(" ", String.Empty).Replace("Material", String.Empty).Replace("{", String.Empty) + "\r\n";
                        lines[i + 1] = "        1.000000; 1.000000; 1.000000; 1.000000;;";
                        txtOutput.Text += "- Fixed Diffuse" + "\r\n";
                        if (ckbSpecular.IsChecked == true)
                        {
                            lines[i + 3] = "        0.000000; 0.000000; 0.000000;;";
                            txtOutput.Text += "- Removed Specular" + "\r\n";
                        }
                        /*if (!lines[i + 5].Contains("TextureFilename"))
                        {
                            lines[i + 4] = "\t\t0.000000; 0.000000; 0.000000;;\r\nTextureFilename { \"" + lines[i].Replace(" ", String.Empty).Replace("Material", String.Empty).Replace("{", String.Empty) + ".png\"; }";
                        }*/
                        txtOutput.Text += "\r\n";
                    }
                }

                System.IO.File.WriteAllLines(openFileDialog1.FileName, lines);
                txtOutput.Text += "File \"" + openFileDialog1.FileName + "\" written successfully.";
            }
        }
    }
}
