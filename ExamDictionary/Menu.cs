using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamDictionary
{
    internal class Menu
    {
        private static void Show(string[] options, int highlight, int posX, int posY)
        {

            var lengthArr = from o in options
                          select o.Length;

            int longest = lengthArr.Max();

            for (int i = 0; i < options.Length; i++)
            {
                int xDiff = (longest - options[i].Length) / 2;
                Console.SetCursorPosition(posX + xDiff, posY + i);
                if (i == highlight)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.WriteLine(options[i]);
                if (i == highlight)
                {
                    Console.ResetColor();
                   
                }
            }
        }

        private static void ShowBlinking(string[] options, int highlight, int posX, int posY)
        {

            var lengthArr = from o in options
                            select o.Length;

            int longest = lengthArr.Max();

            for (int j = 0; j < 4; j++)
            {


                for (int i = 0; i < options.Length; i++)
                {
                    int xDiff = (longest - options[i].Length) / 2;
                    Console.SetCursorPosition(posX + xDiff, posY + i);
                    if (i == highlight && j % 2 == 0)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    Console.WriteLine(options[i]);
                    if (i == highlight && j % 2 == 0)
                    {
                        Console.ResetColor();
                    }
                }

                Thread.Sleep(170);
            }
        }

        public static int ChooseItem(string[] options, int posX = 0, int posY = 0)
        {
            int result = 0;
            ConsoleKeyInfo keyPress;
            while (true)
            {
                //Console.Clear();
                Show(options, result, posX, posY);
                keyPress = Console.ReadKey(true);
                if (keyPress.Key == ConsoleKey.Enter)
                {
                    ShowBlinking(options, result, posX, posY);
                    Console.Clear();
                    return result;
                }
                else if (keyPress.Key == ConsoleKey.DownArrow)
                {
                    result++;
                    if(result > options.Length - 1)
                    {
                        result = 0;
                    }
                }
                else if (keyPress.Key == ConsoleKey.UpArrow)
                {
                    result--;
                    if (result < 0)
                    {
                        result = options.Length - 1;
                    }
                }
            }
        }

        private static void Show(string[] options, int highlight, int posX, int posY, string menuName)
        {

            var lengthArr = from o in options
                            select o.Length;

            int longest = lengthArr.Max();

            int xDiff = (longest - menuName.Length) / 2;
            Console.SetCursorPosition(posX + xDiff, posY);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(menuName);
            Console.ResetColor();

            

            for (int i = 0; i < options.Length; i++)
            {
                xDiff = (longest - options[i].Length) / 2;
                Console.SetCursorPosition(posX + xDiff, posY + i + 1);
                if (i == highlight)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.WriteLine(options[i]);
                if (i == highlight)
                {
                    Console.ResetColor();

                }
            }
        }

        private static void ShowBlinking(string[] options, int highlight, int posX, int posY, string menuName)
        {

            var lengthArr = from o in options
                            select o.Length;

            int longest = lengthArr.Max();

            int xDiff;

            for (int j = 0; j < 4; j++)
            {
                xDiff = (longest - menuName.Length) / 2;
                Console.SetCursorPosition(posX + xDiff, posY);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(menuName);
                Console.ResetColor();

                for (int i = 0; i < options.Length; i++)
                {
                    xDiff = (longest - options[i].Length) / 2;
                    Console.SetCursorPosition(posX + xDiff, posY + i + 1);
                    if (i == highlight && j % 2 == 0)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    Console.WriteLine(options[i]);
                    if (i == highlight && j % 2 == 0)
                    {
                        Console.ResetColor();
                    }
                }

                Thread.Sleep(170);
            }
        }

        public static int ChooseItem(string[] options, string menuName, int posX = 0, int posY = 0)
        {
            int result = 0;
            ConsoleKeyInfo keyPress;
            while (true)
            {
                Console.Clear();
                Show(options, result, posX, posY, menuName);
                keyPress = Console.ReadKey(true);
                if (keyPress.Key == ConsoleKey.Enter)
                {
                    ShowBlinking(options, result, posX, posY, menuName);
                    Console.Clear();
                    return result;
                }
                else if (keyPress.Key == ConsoleKey.DownArrow)
                {
                    result++;
                    if (result > options.Length - 1)
                    {
                        result = 0;
                    }
                }
                else if (keyPress.Key == ConsoleKey.UpArrow)
                {
                    result--;
                    if (result < 0)
                    {
                        result = options.Length - 1;
                    }
                }
            }
        }
    }
}
