using Programming_Exercise.Model;
using Programming_Exercise.UserInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Programming_Exercise
{
    internal class Program
    {
        static void Main(string[] args)
        {
            InitialMenu consoleOptions = new InitialMenu();
            bool menu = true;

            while (menu)
            {
                menu = consoleOptions.Menu();
            }
        }
    }
}
