using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FT450D
{
    class Settings
    {
        List<String> lableSettigs = new List<String>();
        /// <summary>
        /// Создает рускоязычный список названий кнопок, лейблов и т.д.
        /// </summary>
        private void createSettingsRus()
        {
            lableSettigs.Clear();
            //Указатель позицию комбобокса Rate ComPort
            lableSettigs.Add("1");  //Выбор 4800
            lableSettigs.Add("35478");  //Порт для сервера
            lableSettigs.Add("1"); //Выбор языка русский
            lableSettigs.Add("Язык"); //Лейба выбора языка
            lableSettigs.Add("Скорость COM-порта");  //Лэйба выбора скорости порта
            lableSettigs.Add("Порт сервера"); //Лэйба для тексбокса порта сервера
            lableSettigs.Add("Применить"); //Лэйба кнопки применить
            lableSettigs.Add("Настройки"); //Название формы настроек
        }
    }
}
