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
using Microsoft.Win32;

namespace Translator
{
    public partial class MainWindow : Window
    {
        private Triangulation triangulation = new Triangulation();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
           
        }

        private void Button_Click_load(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "STL Files (*.stl)|*.stl";

            if (openFileDialog.ShowDialog() == true)
            {
                string fileName = openFileDialog.FileName;

                STLReader reader = new STLReader();
                reader.Read(fileName, triangulation);

                LoadStatusTextBlock.Text = "File loaded successfully from - " + fileName;
            }
        }
        
        private void Button_Click_translator(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "OBJ Files (*.obj)|*.obj";

            if (saveFileDialog.ShowDialog() == true)
            {
                string fileName = saveFileDialog.FileName;

                OBJWriter writer = new OBJWriter();
                writer.Write(fileName, triangulation);

                TranslateStatusTextBlock.Text = "File saved successfully to - " + fileName;
            }

        }

        
    }
}