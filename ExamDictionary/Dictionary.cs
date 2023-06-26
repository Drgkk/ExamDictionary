using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

            string[] q = (from w in words
                          select w.MainWord).ToArray();

            if(Array.IndexOf(q, name) != -1)
            {
                throw new Exception("Word already in dictionary!");
            }

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
                        if (Array.IndexOf(word.Translations.ToArray(), name) != -1)
                        {
                            Console.WriteLine("Definition already exists!");
                            continue;
                        }
                        word.Translations.Add(name);
                        break;
                    case 1:
                        words.Add(word);
                        AddWord();
                        break;
                    case 2:
                        words.Add(word);
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
                        if (Array.IndexOf(word.Translations.ToArray(), name) != -1)
                        {
                            Console.WriteLine("Definition already exists!");
                            continue;
                        }
                        word.Translations.Add(name);
                        break;
                    case 1:
                        words.Add(word);
                        AddWord();
                        break;
                    case 2:
                        words.Add(word);
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
            int choice;
            while(true)
            {
                try
                {
                    choice = Menu.ChooseItem(options, "Manipulate Word", 5, 5);
                    switch (choice)
                    {
                        case 0:
                            string[] q = (from w in words
                                          select w.MainWord).ToArray();

                            words.Remove(words[Array.IndexOf(q, word)]);
                            break;
                        case 1:
                            string[] q1 = (from w in words
                                           select w.MainWord).ToArray();

                            AddDefinition(Array.IndexOf(q1, word));
                            break;
                        case 2:

                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error: {ex.Message}\nPress any key to continue");
                    Console.ResetColor();
                    Console.ReadKey(true);
                }
            }
        }

        private void AddDefinition(int index)
        {
            Console.WriteLine("Add definition of a word: ");
            string name = Console.ReadLine();
            if (name == "")
                throw new Exception("Null Value!");

            if (Array.IndexOf(words[index].Translations.ToArray(), name) != -1)
            {
                throw new Exception("Definition already exists!");
            }
            words[index].Translations.Add(name);

            string[] options = { "Add another definition", "Finish adding definitions" };
            int choice;
            while (true)
            {
                choice = Menu.ChooseItem(options, "Adding Definitions...", 5, 5);
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
                        if (Array.IndexOf(words[index].Translations.ToArray(), name) != -1)
                        {
                            Console.WriteLine("Definition already exists!");
                            continue;
                        }
                        words[index].Translations.Add(name);
                        break;
                    case 2:
                        return;
                        break;
                }

            }
        }
    }
}
