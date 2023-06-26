using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ExamDictionary
{
    [DataContract]
    [Serializable]
    internal class Dictionary
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        private List<Word> words;

        public Dictionary()
        {
            words = new List<Word>();
        }

        public void AddWord()
        {
            Console.WriteLine("Enter a word to add definition for: ");
            string? name = Console.ReadLine();
            if(name == "")
                throw new Exception("Null Value!");
            Word word = new Word(name);
            Console.WriteLine("Write first definition of a word: ");
            name = Console.ReadLine();
            if (name == "")
                throw new Exception("Null Value!");
            word.Translations.Add(name);
            string[] options = { "Add another definition", "Add another word", "Finish adding words" };
            int choice;
            while(true)
            {
                choice = Menu.ChooseItem(options, "Adding Word...", 5, 5);
                switch (choice)
                {
                    case 0:
                        Console.WriteLine("Write new definition of a word: ");
                        name = Console.ReadLine();
                        if (name == "")
                        {
                            Console.WriteLine("Null value!");
                            continue;
                        }
                        word.Translations.Add(name);
                        break;
                    case 1:
                        AddWord();
                        break;
                    case 2:
                        return;
                        break;
                }
            }
        }

        public void AddWord(string wordUser)
        {
            Word word = new Word(wordUser);
            Console.WriteLine("Write first definition of a word: ");
            string name = Console.ReadLine();
            if (name == "")
                throw new Exception("Null Value!");
            word.Translations.Add(name);
            string[] options = { "Add another definition", "Add another word", "Finish adding words" };
            int choice;
            while (true)
            {
                choice = Menu.ChooseItem(options, "Adding Word...", 5, 5);
                switch (choice)
                {
                    case 0:
                        Console.WriteLine("Write new definition of a word: ");
                        name = Console.ReadLine();
                        if (name == "")
                        {
                            Console.WriteLine("Null value!");
                            continue;
                        }
                        word.Translations.Add(name);
                        break;
                    case 1:
                        AddWord();
                        break;
                    case 2:
                        return;
                        break;
                }
            }
        }

        public string[] GetWords()
        {
            var q = from w in words
                    select w.MainWord;

            return q.ToArray();
        }

        public void ManipulateWord(string word)
        {
            string[] options = {"Delete word", "Add Definiton", "Delete Definition" };
        }
    }
}
