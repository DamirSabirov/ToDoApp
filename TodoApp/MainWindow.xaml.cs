using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TodoApp.Models;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TodoApp.Services;

namespace TodoApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string PATH = $"{Environment.CurrentDirectory}\\todoDataList.json";    //Поле с указанием пути json-файла к папке, в которой находится весь проект
        private BindingList<TodoModel> _toDoDataList;                                           //Поле списка всех задач
        private FileIOService _fileIOService;                                                   //Создание экземпляра класса FileIOService                             


        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)                            //Метод для загрузки файла с данными
        {
            _fileIOService = new FileIOService(PATH);

            try
            {
                _toDoDataList = _fileIOService.LoadData();                                      //Попробовать вызвать метод LoadData для загрузки существующего файла 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Close();
            }

            dgTodoList.ItemsSource = _toDoDataList;
            _toDoDataList.ListChanged += ToDoDataList_ListChanged;
        }

        private void ToDoDataList_ListChanged(object sender, ListChangedEventArgs e)           //Метод для сохранения файла с данными
        {
            if (e.ListChangedType == ListChangedType.ItemAdded || e.ListChangedType == ListChangedType.ItemDeleted || e.ListChangedType == ListChangedType.ItemChanged) //Если пришло оповещение о том, что какой-то из элементов был удалён, добавлен или изменён
            {
                try
                {
                    _fileIOService.SaveData(sender);                                            //Попробовать вызвать метод SaveData для сохранения новых данных
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Close();
                }
            }
        }
    }
}
