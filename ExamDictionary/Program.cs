using System.Text;

namespace ExamDictionary
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode; 
            Console.Title = "Dictionary";

            

            DictionariesCollection dictionaries = new DictionariesCollection();

            while (true)
            {
                try
                {
                    string[] options = { "Create new Dictionary", "See existing Dictionaries", "Save all dictionaries", "Load all dictionaries", "Load certain dictionary", "Exit" };
                    int choice = Menu.ChooseItem(options, "Main Menu", 5, 5);
                    switch (choice)
                    {
                        case 0:
                            dictionaries.Add();
                            break;
                        case 1:
                            if(dictionaries.GetDictionariesNames().Length == 0)
                            {
                                throw new Exception("There is no dictionaries yet!");
                            }
                            choice = Menu.ChooseItem(dictionaries.GetDictionariesNames().Append("Back").ToArray(), "Dictionaries", 5, 5);
                            if (choice == dictionaries.GetDictionariesNames().Length)
                            {
                                continue;
                            }
                            dictionaries.Manipulate(choice);
                            break;
                        case 2:
                            if(dictionaries.GetDictionariesNames().Length == 0)
                            {
                                throw new Exception("There is no dictionaries to save!");
                            }
                            dictionaries.SaveAll();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"All dictionaries sucesfully saved!\nPress any key to continue");
                            Console.ResetColor();
                            Console.ReadKey(true);
                            break;
                        case 3:
                            string exePath = AppDomain.CurrentDomain.BaseDirectory;
                            string[] q = Directory.GetFiles(exePath, "*SaveFile.json", SearchOption.TopDirectoryOnly);
                            string[] files = (from f in q
                                               select Path.GetFileName(f)).ToArray();
                            if (files.Length == 0)
                            {
                                throw new Exception("There is no dictionaries to load from!");
                            }
                            foreach(string f in files)
                            {
                                dictionaries.Load(f);
                            }
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"All dictionaries sucesfully loaded!\nPress any key to continue");
                            Console.ResetColor();
                            Console.ReadKey(true);
                            break;
                        case 4:
                            string exePath2 = AppDomain.CurrentDomain.BaseDirectory;
                            string[] q2 = Directory.GetFiles(exePath2, "*SaveFile.json", SearchOption.TopDirectoryOnly);
                            string[] files2 = (from f in q2
                                         select Path.GetFileName(f)).ToArray();
                            if (files2.Length == 0)
                            {
                                throw new Exception("There is no dictionaries to load from!");
                            }
                            Console.WriteLine("Here is the list of dictionaries save files you can load from : ");
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(string.Join("\n", files2));
                            Console.ResetColor();
                            Console.WriteLine("Write a name of a file you want to load in from  (type \\b to go back): ");
                            string saveName = Console.ReadLine();
                            if (saveName == "")
                                throw new Exception("Null Value!");
                            else if (saveName == "\\b")
                                continue;
                            else if (Array.IndexOf(files2, saveName) == -1)
                                throw new Exception("There is no such save file in the list!");

                            dictionaries.Load(saveName);

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"Dictionary {saveName} sucesfully loaded!\nPress any key to continue");
                            Console.ResetColor();
                            Console.ReadKey(true);

                            break;
                        case 5:
                            Console.WriteLine("Goodbye!");
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
    }
}