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
            Console.WriteLine("Enter dictionary name: ");
            name = Console.ReadLine();
            if (name == "")
                throw new Exception("Null Value!");
            Dictionary dictionary = new Dictionary();
            dictionary.Name = name;
            dictionaries.Add(dictionary);
        }

        public string[] GetDictionariesNames()
        {
            string[] names = dictionaries.Select(x => x.Name).ToArray();
            return names;
        }

        public void Manipulate(int index)
        {
            string[] options = { "Add a word to dictionary", "Manipulate certain word", "Return" };
            int choice;
            while(true)
            {
                choice = Menu.ChooseItem(options, "Manipulate words", 5, 5);
                switch (choice)
                {
                    case 0:
                        dictionaries[index].AddWord();
                        break;
                    case 1:
                        Console.WriteLine($"Here are all words in this array: {dictionaries[index].GetWords()}");
                        Console.WriteLine("Enter a word you want to manipulate with: ");
                        string name = Console.ReadLine();
                        if(Array.IndexOf(dictionaries[index].GetWords(), name) != -1)
                        {
                            dictionaries[index].ManipulateWord(name);
                        }
                        else
                        {
                            Console.WriteLine("This word is not in dictionary!\nDo you want to add this word to dictionary?(y/n): ");
                            char c = Console.ReadKey().KeyChar;
                            switch(c)
                            {
                                case 'y':
                                    dictionaries[index].AddWord(name);
                                    break;
                                default:

                                    break;
                            }
                            continue;
                        }
                        break; 
                    case 2:
                        return;
                        break;
                }
            }
        }
    }
}
