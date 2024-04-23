using Programming_Exercise.Business;
using Programming_Exercise.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programming_Exercise.UserInterface
{
    public class InitialMenu : IMenu
    {
        public bool Menu()
        {
            Console.Clear();
            Console.WriteLine("Choose an option:\r\n");
            Console.WriteLine("1) Insert a new endpoint");
            Console.WriteLine("2) Edit an existing endpoint");
            Console.WriteLine("3) Delete an existing endpoint");
            Console.WriteLine("4) List all endpoints");
            Console.WriteLine("5) Find and endpoint by 'Endpoint Serial Number'");
            Console.WriteLine("6) Exit");
            Console.WriteLine("\r\nSelect an option: ");

            Operation operation = new Operation();

            switch (Console.ReadLine())
            {
                case "1":
                    operation.CreateEndPoint();
                    return true;
                case "2":
                    operation.CreateEndPoint(edit: true);
                    return true;
                case "3":
                    operation.DeleteEndPoint();
                    return true;
                case "4":
                    operation.ListAllEndPoint();
                    return true;
                case "5":
                    operation.SearchEndPoint(mainMenu: true);
                    return true;
                case "6":
                    return CloseApp();
                default:
                    return true;
            }

        }

        public bool CloseApp()
        {
            Console.WriteLine("\r\nAre you sure to close this application? Press Y or N");

            switch (Console.ReadLine().ToUpper())
            {
                case "Y":
                    return false;
                case "N":
                    return true;
                default:
                    return true;
            }
        }
              

    }
}
