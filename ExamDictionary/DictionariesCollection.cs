using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ExamDictionary
{
    [DataContract]
    [Serializable]
    internal class DictionariesCollection
    {
        [DataMember]
        private List<Dictionary> dictionaries;

        public DictionariesCollection()
        {
            dictionaries = new List<Dictionary>();
        }

        public void Add()
        {
            string? name;
            Console.WriteLine("Enter dictionary name (type \\b to go back): ");
            name = Console.ReadLine();
            if (name == "")
                throw new Exception("Null Value!");
            else if (name == "\\b")
                return;

            string[] q = (from d in dictionaries
                     select d.Name.ToLower()).ToArray();

            if(Array.IndexOf(q, name.ToLower()) != -1)
            {
                throw new Exception("Dictionary already exists!");
            }
            Dictionary dictionary = new Dictionary();
            dictionary.Name = name;
            dictionaries.Add(dictionary);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Dictionary \"{name}\" succesfully added!");
            Console.ResetColor();
            Console.ReadKey(true);
        }

        public string[] GetDictionariesNames()
        {
            string[] names = dictionaries.Select(x => x.Name).ToArray();
            return names;
        }

        public void Manipulate(int index)
        {
            string[] options = { "Search a word", "Add a word to dictionary", "Manipulate certain word", "Return" };
            int choice;
            while(true)
            {
                try
                {
                    choice = Menu.ChooseItem(options, "Manipulate words", 5, 5);
                    switch (choice)
                    {
                        case 0:
                            while (SearchAWord(index)) { }
                            break;
                        case 1:
                            while (dictionaries[index].AddWord()) { }
                            break;
                        case 2:
                            ManipulateCertainWord(index);
                            break;
                        case 3:
                            return;
                            break;
                    }
                }
                catch(Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error: {ex.Message}\nPress any key to continue");
                    Console.ResetColor();
                    Console.ReadKey(true);
                }
            }
        }

        private void ManipulateCertainWord(int index)
        {
            string[] q = (from w in dictionaries[index].GetWords()
                          select w.ToLower()).ToArray();
            Console.WriteLine($"Here are all words in this array: {string.Join(", ", q)}");
            Console.WriteLine("Enter a word you want to manipulate with (type \\b to go back): ");
            string name = Console.ReadLine();

            if (name == "")
                throw new Exception("Null Value!");
            else if (name == "\\b")
                return;

            if (Array.IndexOf(q, name.ToLower()) != -1)
            {
                dictionaries[index].ManipulateWord(name);
            }
            else
            {
                Console.WriteLine("This word is not in dictionary!\nDo you want to add this word to dictionary?(y/n): ");
                char c = Console.ReadKey(true).KeyChar;
                switch (c)
                {
                    case 'y':
                        if (dictionaries[index].AddWord(name))
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"Word has been succesfully added to {dictionaries[index].Name} Dictionary!\nPress any key to continue");
                            Console.ResetColor();
                            Console.ReadKey(true);
                        }
                        break;
                    default:

                        break;
                }
                return;
            }
        }

        private bool SearchAWord(int index)
        {
            Console.WriteLine("Enter a word to search for (type \\b to go back): ");
            string word = Console.ReadLine();
            if (word == "")
                throw new Exception("Null Value!");

            if (Array.IndexOf(dictionaries[index].GetWords(), word) != -1)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Word found!");
                Console.ResetColor();
                Console.WriteLine(dictionaries[index].GetWordByIndex(Array.IndexOf(dictionaries[index].GetWords(), word)));
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
                return true;
            }
            else if(word == "\\b")
            {
                return false;
            }
            else
            {
                Console.WriteLine("This word is not in dictionary!\nDo you want to add this word to dictionary?(y/n): ");
                char c = Console.ReadKey(true).KeyChar;
                switch (c)
                {
                    case 'y':
                        if (dictionaries[index].AddWord(word))
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"Word has been succesfully added to {dictionaries[index].Name} Dictionary!\nPress any key to continue");
                            Console.ResetColor();
                            Console.ReadKey(true);
                        }
                        break;
                    default:

                        break;
                }
                Console.Clear();
                return true;
            }
        }

    }
}
