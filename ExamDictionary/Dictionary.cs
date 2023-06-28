using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
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

        public bool AddWord()
        {
            Console.WriteLine("Enter a word to add definition for (type \\b to go back): ");
            string? name = Console.ReadLine().ToLower();
            if (name == "")
                throw new Exception("Null Value!");
            else if (name == "\\b")
                return false;

            string[] q = (from w in words
                          select w.MainWord).ToArray();

            if(Array.IndexOf(q, name) != -1)
            {
                throw new Exception("Word already in dictionary!");
            }

            Word word = new Word(name);
            Console.WriteLine("Write first definition of a word (type \\b to go back): ");
            name = Console.ReadLine().ToLower();
            if (name == "")
                throw new Exception("Null Value!");
            else if (name == "\\b")
                return false;

            word.Translations.Add(name);
            string[] options = { "Add another definition", "Add another word", "Finish adding words" };
            int choice;
            while(true)
            {
                choice = Menu.ChooseItem(options, "Adding Word...", 5, 5);
                switch (choice)
                {
                    case 0:
                        Console.WriteLine("Write new definition of a word (type \\b to go back): ");
                        name = Console.ReadLine().ToLower();
                        if (name == "")
                        {
                            Console.WriteLine("Null value!");
                            continue;
                        }
                        if (name == "\\b")
                            continue;
                        if (Array.IndexOf(word.Translations.ToArray(), name) != -1)
                        {
                            Console.WriteLine("Definition already exists!");
                            continue;
                        }
                        word.Translations.Add(name);
                        break;
                    case 1:
                        words.Add(word);
                        return true;
                        break;
                    case 2:
                        words.Add(word);
                        return false;
                        break;
                }
            }
        }

        public bool AddWord(string wordUser)
        {
            Word word = new Word(wordUser);
            Console.WriteLine("Write first definition of a word (type \\b to go back): ");
            string name = Console.ReadLine().ToLower();
            if (name == "")
                throw new Exception("Null Value!");
            else if (name == "\\b")
                return false;
            word.Translations.Add(name);
            words.Add(word);
            return true;
        }

        public string[] GetWords()
        {
            string[] q = (from w in words
                    select w.MainWord).ToArray();

            return q;
        }

        public void ManipulateWord(string word)
        {
            word = word.ToLower();

            string[] q = (from w in words
                          select w.MainWord.ToLower()).ToArray();

            int index = Array.IndexOf(q, word);

            string[] options = {"Delete word", "Change Word Name", "Show all definitions for the word" , "Add Definiton", "Delete Definition", "Change Definition Name", "Export word to a .txt file", "Back" };
            int choice;
            while(true)
            {
                try
                {
                    choice = Menu.ChooseItem(options, $"Manipulate Word ({word})", 5, 5);
                    switch (choice)
                    {
                        case 0:
                            RemoveWord(word, index);
                            return;
                        case 1:
                            ChangeWordName(ref word, index);
                            break;
                        case 2:
                            ShowAllDefinitions(word, index);
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            break;
                        case 3:
                            AddDefinition(index);
                            break;
                        case 4:
                            Console.WriteLine("Here are all definitions: ");
                            ShowAllDefinitions(word, index);
                            DeleteDefinition(word, index);
                            break;
                        case 5:
                            Console.WriteLine("Here are all definitions: ");
                            ShowAllDefinitions(word, index);
                            ChangeDefinitionName(word, index);
                            break;
                        case 6:
                            words[index].ExportToATxtFile();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"Word \"{word}\" succesfully exported to a txt file: \"{words[index].MainWord}.txt\"\nPress any key to continue...");
                            Console.ResetColor();
                            Console.ReadKey();
                            break;
                        case 7:
                            return;
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

        private void RemoveWord(string word, int index)
        {
            words.Remove(words[index]);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Word: \"{word}\" succesfully deleted!\nPress any key to continue");
            Console.ResetColor();
            Console.ReadKey(true);
        }

        private void ChangeWordName(ref string word, int index)
        {
            Console.WriteLine("Write new name for word (type \\b to go back): ");
            string newName = Console.ReadLine().ToLower();
            if (newName == "")
                throw new Exception("Null Value!");
            else if (newName == "\\b")
                return;
            Console.WriteLine($"Word: \"{word}\" succesfully changed name to: \"{newName}\"");
            word = newName;
            words[index].MainWord = newName;
            
        }

        private void ShowAllDefinitions(string word, int index)
        {
            Console.WriteLine($"Word: {word}");
            string[] q2 = (words[index].Translations.Select(t => t.ToLower())).ToArray();



            Console.WriteLine($"Definitions ({q2.Length}): {string.Join(", ", q2)}");
            
        }

        private void AddDefinition(int index)
        {
            Console.WriteLine("Add definition of a word (type \\b to go back): ");
            string name = Console.ReadLine().ToLower();
            if (name == "")
                throw new Exception("Null Value!");
            else if (name == "\\b")
                return;

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
                        Console.WriteLine("Write new definition of a word (type \\b to go back): ");
                        name = Console.ReadLine().ToLower();
                        if (name == "")
                        {
                            Console.WriteLine("Null value!");
                            continue;
                        }
                        if (name == "\\b")
                            continue;
                        if (Array.IndexOf(words[index].Translations.ToArray(), name) != -1)
                        {
                            Console.WriteLine("Definition already exists!");
                            continue;
                        }
                        words[index].Translations.Add(name);
                        break;
                    case 1:
                        return;
                        break;
                }

            }
        }

        private void DeleteDefinition(string word, int index)
        {
            if(words[index].Translations.Count == 1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Can't delete the last definition for the word: \"{word}\"!\nPress any key to continue");
                Console.ResetColor();
                Console.ReadKey(true);
                return;
            }
            Console.WriteLine("Write a definition you want to delete (type \\b to go back): ");
            string name = Console.ReadLine().ToLower();
            if (name == "")
                throw new Exception("Null Value!");
            else if (name == "\\b")
                return;

            if (Array.IndexOf(words[index].Translations.ToArray(), name) != -1)
            {
                words[index].Translations.Remove(name);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Definition succesfully deleted for word: \"{word}\"!\nPress any key to continue");
                Console.ResetColor();
                Console.ReadKey(true);
                return;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"This definition does not exist for the word: \"{word}\"!\nPress any key to continue");
                Console.ResetColor();
                Console.ReadKey(true);
                return;
            }

        }

        private void ChangeDefinitionName(string word, int index)
        {
            Console.WriteLine("Write definition name you want to change (type \\b to go back): ");
            string def = Console.ReadLine().ToLower();
            if (def == "")
                throw new Exception("Null Value!");
            else if (def == "\\b")
                return;
            if (Array.IndexOf(words[index].Translations.ToArray(), def) == -1)
            {
                throw new Exception($"No definition {def} found!");
            }
            Console.WriteLine($"Write new name for definition ({def}) (type \\b to go back): ");
            string newName = Console.ReadLine().ToLower();
            if (newName == "")
                throw new Exception("Null Value!");
            else if (newName == "\\b")
                return;

            if (Array.IndexOf(words[index].Translations.ToArray(), newName) != -1)
            {
                throw new Exception("Definition already exists!");
            }

            Console.WriteLine($"Definition: \"{def}\" succesfully changed name to: \"{newName}\"");
            words[index].Translations[Array.IndexOf(words[index].Translations.ToArray(), def)] = newName;
        }

        public Word GetWordByIndex(int index)
        {
            return words[index];
        }

        public void Save()
        {
            DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(typeof(Dictionary));
            using (var file = File.Create($"{Name}SaveFile.json"))
            {
                dataContractJsonSerializer.WriteObject(file, this);
            }

        }

        

    }
}
