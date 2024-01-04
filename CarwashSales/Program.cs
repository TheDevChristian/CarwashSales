using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarwashSales
{
    class Program
    {
        static void Main(string[] args)
        {
            Operator operatorObj = new Operator();
            operatorObj.Run();

            Console.ReadLine();

            /*
            ^^^PROPOSAL

            SUMMARY
            -----------------------------------------------------------------------------------------------------------
            This app development proposal details the creation of an application that handles the 
            sales for [DalCarwash], as a solution for physical sales records that get stolen, damaged or go missing.

            The objective of this application is to have a simple program which lets the user choose which sales option
            the customer has paid for, it will then work with a local database to digitally store the amount of sales
            that have been made, aswell as the total income that the business has generated.
            -----------------------------------------------------------------------------------------------------------

            PROBLEM
            -----------------------------------------------------------------------------------------------------------
            [DalCarwash] has had sales records go missing in the past which in-turn results in inaccurate sales 
            and income figures being reflected onto the business ledger.
            -----------------------------------------------------------------------------------------------------------

            SOLUTION
            -----------------------------------------------------------------------------------------------------------
            Develop a simple C# program that uses a database to store the amount of sales that have been made, aswell
            as the amount of income the business has generated. Storing the data digitally instead of physically,
            reduces the risks of that information being stolen, damaged or lost.
            -----------------------------------------------------------------------------------------------------------

            OBJECTIVE
            -----------------------------------------------------------------------------------------------------------
            1:Have a simple yet effective program which handles the sales 
            2:Store data digitally to protect it from any harm, and reflect accurate numbers onto the business ledger.
            -----------------------------------------------------------------------------------------------------------

            BENEFITS
            -----------------------------------------------------------------------------------------------------------
            This program will keep any data being stored into the database safe. It will also save alot of time when
            choosing which packages customers have paid for by having a simple menu which handles all the data stored
            into the DB when a sale has been completed
            -----------------------------------------------------------------------------------------------------------
            */
        }
    }
}
