using System;
using System.Collections.Generic;
using System.IO;

namespace Lesson5
{
    class Abonent
    {
        internal string Name { get; set; }
        internal string Phone { get; set; }

        internal Abonent(string name, string phone)
        {
            Name = name;
            Phone = phone;
        }
    }
    
    /// <summary>
    /// Класс-одиночка Телефонная книга
    /// </summary>
    internal class Phonebook
    {
        private static Phonebook _instance;
        private static string path;
        private static Dictionary<string, Abonent> abonents = new Dictionary<string, Abonent>();

        /// <summary>
        /// Конструктор Телефонной книги.
        /// </summary>
        /// <param name="path">Путь до текстового файла</param>
        private Phonebook(string path)
        {
            Phonebook.path = path;
            FileInfo pathInfo = new FileInfo(path);
            if (pathInfo.Exists)
            {
                foreach (string line in File.ReadLines(path))
                {
                    string[] parts = line.Split(',');
                    try
                    {
                        Abonent newAbonent = new Abonent(parts[0], parts[1]);
                        Phonebook.AddAbonent(newAbonent, false);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Could not parse \"{line}\"");
                        Console.WriteLine($"Reason: {ex.Message}");
                    }
                    
                }
            }
        }


        /// <summary>
        /// Проверка на наличие абонента в телефонной книге
        /// </summary>
        /// <param name="abonent">Экземпляр абонента</param>
        /// <returns></returns>
        public static bool AbonentExists(Abonent abonent)
        {
            if (abonents.ContainsKey(abonent.Name)) return true;
            return false;
        }

        /// <summary>
        /// Проверка на наличие абонента в телефонной книге
        /// </summary>
        /// <param name="name">Имя абонента</param>
        /// <returns></returns>
        public static bool AbonentExists(string name)
        {
            if (abonents.ContainsKey(name)) return true;
            return false;
        }

        /// <summary>
        /// Добавить абонента в телефонную книгу
        /// </summary>
        /// <param name="abonent">Экземпляр абонента</param>
        /// <param name="writeToFile">Обновить ли текстовый файл</param>
        /// <exception cref="Exception">Абонент уже был добавлен.</exception>
        public static void AddAbonent(Abonent abonent, bool writeToFile)
        {
            if (Phonebook.AbonentExists(abonent))
                throw new Exception("This abonent has already been added");
            abonents.Add(abonent.Name, abonent);
            if (writeToFile) Phonebook.DumpToTextFile();
            Console.WriteLine($"Абонент {abonent.Name} добавлен");
        }

        public static void AddAbonent(Abonent abonent)
        {
            Phonebook.AddAbonent(abonent, true);
        }

        /// <summary>
        /// Выводит в консоль всех абонентов
        /// </summary>
        public static void PrintAbonents()
        {
            foreach (KeyValuePair<string, Abonent> abonent in abonents)
            {
                Console.WriteLine($"{abonent.Value.Name}: {abonent.Value.Phone}");
            }
        }
            /// <summary>
            /// Записывает всех абонентов в текстовый файл
            /// </summary>
        public static void DumpToTextFile()
        {
            string[] abonentLines = new string[abonents.Count];
            int i = 0;
            foreach (KeyValuePair<string, Abonent> abonent in abonents)
            {
                abonentLines[i] = $"{abonent.Value.Name},{abonent.Value.Phone}";
                ++i;
            }
            try
            {
                File.WriteAllLines(path, abonentLines);
                Console.WriteLine("Телефонная книга записана в файл");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Couldn't write into a file because {ex.Message}");
            }
        }

        /// <summary>
        /// Возвращает экземпляр абонента по его имени
        /// </summary>
        /// <param name="name">Имя абонента</param>
        /// <returns>Экземпляр абонента или null</returns>
        public static Abonent GetAbonent(string name)
        {
            return abonents.ContainsKey(name) ? abonents[name] : null;
        }

        /// <summary>
        /// Удаляет абонента из телефонной книги по его имени
        /// </summary>
        /// <param name="name">Имя абонента</param>
        public static void DeleteAbonent(string name)
        {
            abonents.Remove(name);
            Console.WriteLine($"Абонент {name} удален");
        }

        /// <summary>
        /// Удаляет абонента из телефонной книги по его экземпляру
        /// </summary>
        /// <param name="abonent"></param>
        public static void DeleteAbonent(Abonent abonent)
        {
            abonents.Remove(abonent.Name);
            Console.WriteLine($"Абонент {abonent.Name} удален");
        }

        /// <summary>
        /// Возвращает экземпляр телефонной книги
        /// </summary>
        /// <param name="path">Путь до текстового файла со списком абонентов
        /// в формате "name,phone"</param>
        /// <returns>Экземпляр Phonebook</returns>
        public static Phonebook Instance(string path)
        {
            if (_instance == null) _instance = new Phonebook(path);
            return _instance;
        }
    }
}
