using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using ViewModel;

namespace Demo.WpfApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для EditingPage.xaml
    /// </summary>
    public partial class EditingPage : Page
    {
        private Presentation VM { get; init; }
        public EditingPage()
        {
            InitializeComponent();
            DataContext = App.ViewModel;
            if (DataContext is Presentation viewmodel) VM = viewmodel;
        }

        private void OnSelectClick(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png|All files (*.*)|*.*"
            };

            bool? result = dialog.ShowDialog();

            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            if (result == true) VM.EditVM.SetImage(
                dialog.FileName, projectDirectory + "\\Resources\\Images\\Products\\");
        }
    }
}
