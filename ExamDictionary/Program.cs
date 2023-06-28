using System.Text;

namespace ExamDictionary
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            //Console.CursorVisible = false;
            Console.Title = "Dictionary";
            
            DictionariesCollection dictionaries = new DictionariesCollection();
            
            while(true)
            {
                try
                {
                    string[] options = { "Create new Dictionary", "See existing Dictionaries", "Exit" };
                    int choice = Menu.ChooseItem(options, "Main Menu", 5, 5);
                    switch (choice)
                    {
                        case 0:
                            dictionaries.Add();
                            break;
                        case 1:
                            choice = Menu.ChooseItem(dictionaries.GetDictionariesNames().Append("Back").ToArray(), "Dictionaries", 5, 5);
                            if (choice == dictionaries.GetDictionariesNames().Length)
                            {
                                continue;
                            }
                            dictionaries.Manipulate(choice);
                            break;
                        case 2:
                            Console.WriteLine("Goodbye!");
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
    }
}