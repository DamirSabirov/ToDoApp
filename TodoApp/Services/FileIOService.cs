using Newtonsoft.Json;  //NuGet пакет для работы с JSon файлами
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Models;

namespace TodoApp.Services
{
    class FileIOService
    {
        private readonly string PATH;                                                   //Поле для переменной, в которую записывается путь

        public FileIOService(string path)
        {
            PATH = path;
        }

        public BindingList<TodoModel> LoadData()                                        //Метод для загрузки JSon файла со списком задач
        {
            var fileExists = File.Exists(PATH);                                         //Проверка, существует ли данный файл в указанном пути 
            if (!fileExists)                                                            //Действие, если файла не существует
            {
                File.CreateText(PATH).Dispose();                                        //Создание нового списка по указанному пути 
                return new BindingList<TodoModel>();                                    //Метод возвращает созданный список
            }
            using (var reader = File.OpenText(PATH))
            {
                var fileText = reader.ReadToEnd();                                      //Чтение списка из файла 
                return JsonConvert.DeserializeObject<BindingList<TodoModel>>(fileText); //Метод возвращает десериализованный список 
            }
        }

        public void SaveData(object todoDataList)                                       //Метод для сохранения новых данных в JSon файл 
        {
            using (StreamWriter writer = File.CreateText(PATH))                         //Запись данных в файл
            {
                string output = JsonConvert.SerializeObject(todoDataList);              //Сериализация JSon файла
                writer.Write(output);
            }
        }

    }
}
