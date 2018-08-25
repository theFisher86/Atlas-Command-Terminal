using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Atlas_Command_Terminal.Models;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.ObjectModel;
using MUXC = Microsoft.UI.Xaml.Controls;                //Don't forget me!


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Atlas_Command_Terminal
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.Loaded -= MainPage_Loaded;
        }

        public async Task SaveFile()
        {
            // Save File code here
        }

        private async void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            List<MBINField> fields = new List<MBINField>();

            fields = await LoadMBIN.LoadFileAsync();

            ObservableCollection<MBINField> fieldCollection = new ObservableCollection<MBINField>(fields);

            MainItemsControl.DataContext = this;
            MainItemsControl.ItemsSource = fieldCollection;

            MainTreeView.DataContext = this;
            MainTreeView.ItemsSource = fieldCollection;
            //foreach (MBINField field in fields)
            //{
            //    Debug.WriteLine(field.Name.ToString() + ": " + field.Value.ToString());
            //}
        }
    }
}
