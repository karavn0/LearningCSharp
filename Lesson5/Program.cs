using System;

namespace Lesson5
{
    internal class Program
    {
        const string FileName = "F:\\projects\\edu_csharp\\LearningCSharp\\Lesson5\\phonebook.txt";
        static void Main(string[] args)
        {
            Phonebook phonebook = Phonebook.Instance(FileName);
            Console.WriteLine(Phonebook.GetAbonent("Sergei").Phone);

        }
    }
}
