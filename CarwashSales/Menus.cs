using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace CarwashSales
{
    class Menus: DB_Connector
    {
        public void SalesMenu()
        {          
            BuyMenu();
         
            string input = Console.ReadLine();
            if (input == "5")
            {
                
                MainMenu();

                
            }
            else
            {
                
                MainMenu();
            }
            
        }

        public void MainMenu()
        {
            Header("MAIN MENU");

            Console.WriteLine("1 FOR SALES MENU");
            Console.WriteLine("2 FOR SALES INFO");

            string input = Console.ReadLine();

            if (input == "1")
            {
                SalesMenu();      
            }
            else if(input == "2")
            {
                
                DisplaySalesInformation();

                string input_ = Console.ReadLine();
                if (input_ == "1")
                {
                    MainMenu();
                }
                else
                {
                    MainMenu();
                }
            }
            else
            {
                MainMenu();
            }

        }

    }
}
