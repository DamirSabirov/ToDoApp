using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Models
{
    class TodoModel : INotifyPropertyChanged                                //Наследование интерфейса для сохранения изменений 
    {
        public DateTime CreationDate { get; set; } = DateTime.Now;          //Поле для даты создания задачи (колонка "Дата создания")

        private bool _isDone;                                               //Поле выполнения задачи (колонка "Выполнение")
        private string _text;                                               //Поле ввода текста (колонка "Список заданий")

        public bool IsDone                                                  //Поле для настройки параметров выполнения 
        {
            get { return _isDone; }
            set
            {
                if (_isDone == value)
                    return;
                _isDone = value;                                            //Если value изменяется, записываем новое значение value в поле _isDone
                OnPropertyChanged("IsDone");                                //Вызываем метод OnPropertyChanged для обращения к списку всех задач               
            }
        }

        public string Text                                                  //Поле для настройки параметров текста
        {
            get { return _text; }
            set
            {
                if (_text == value)
                    return;
                _text = value;                                              //Если value изменяется, записываем новое значение value в поле _text
                OnPropertyChanged("Text");                                  //Вызываем метод OnPropertyChanged для обращения к списку всех задач    
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;           //Связь с BindingList, уведомляет об изменениях в списке для их обработки

        protected virtual void OnPropertyChanged(string propertyName = "")  //Метод для обращения к событию и передачи двух аргументов 
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
