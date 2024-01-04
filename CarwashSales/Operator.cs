using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarwashSales
{
    class Operator
    { 
        public void Run()
        {
            DB_Connector dbObj = new DB_Connector();
            Menus MenusObj = new Menus();
            

            MenusObj.MainMenu();
            string menuchosen = Console.ReadLine();

            if (menuchosen == "1")
            {
                MenusObj.SalesMenu();
            }
            else if (menuchosen == "2")
            {

            }
            else
            {
                Console.Clear();
                Console.WriteLine("INVALID INPUT");
                Console.ReadLine();
                MenusObj.MainMenu();
            }

        }
    }
}
    

