using DoList.Models;
using DoList.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace DoList
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string PATH = $"{Environment.CurrentDirectory}\\todoData.json";
        private BindingList<ToDoModel> _todoData;
        private FileIOService _fileIOService;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _fileIOService = new FileIOService(PATH);

            try
            {
                _todoData = _fileIOService.LoadData();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                Close();
                    
            }
            


            dgDoList.ItemsSource = _todoData;
            _todoData.ListChanged += _todoData_ListChanged;
        }

        private void _todoData_ListChanged(object sender, ListChangedEventArgs e)
        {

            if(e.ListChangedType == ListChangedType.ItemAdded || e.ListChangedType == ListChangedType.ItemDeleted || e.ListChangedType == ListChangedType.ItemMoved)
            {
                try
                {
                    _fileIOService.SaveData(sender);
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                    Close();

                }
            }




           // switch (e.ListChangedType)
           // {
           //     case ListChangedType.Reset:
           //         break;
           //     case ListChangedType.ItemAdded:
           //         break;
           //     case ListChangedType.ItemDeleted:
           //         break;
           //     case ListChangedType.ItemMoved:
           //         break;
           //     case ListChangedType.PropertyDescriptorAdded:
           //         break;
           //     case ListChangedType.PropertyDescriptorChanged:
           //         break;
           //     case ListChangedType.PropertyDescriptorDeleted:
           //         break;
           //     default:
           //         break;
           // }
            
        }
    }
}
