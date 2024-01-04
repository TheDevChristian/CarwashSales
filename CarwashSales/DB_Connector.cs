using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CarwashSales
{
    public class DB_Connector
    {
        MySqlConnection connection = new MySqlConnection("");



        public DB_Connector()
        {
            Initialize();
        }

        //Initializes connection to the DB
        public void Initialize()
        {
            string server   = "localhost";
            string database = "carwash";
            string username = "root";
            string password = "";

            string ConnectionString = ($"SERVER = {server}; DATABASE = {database}; UID = {username}; PASSWORD = {password};Allow User Variables=true;");

            connection = new MySqlConnection(ConnectionString);
        }




        // Opens the connection to the DB if not already open
        private bool OpenConnection()
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Number + " . " + ex.Message);
                return false;
            }
        }

        //Closes the connection to the DB
        private bool CloseConnection()
        {
            try
            {
                if (connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                }
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Number + " . " + ex.Message);
                return false;
            }
        }

        //Insert information                             
        public void Insert()
        {
            string query = "INSERT INTO packages (package_number, package_name, package_price) VALUES ( , , )";

            //this - (get current instance of the class)(with current value)
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                try
                {
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("DATA SUCCSESSFULLY INSERTED");
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine(ex.Number + " . " + ex.Message);
                }
                finally
                {
                    this.CloseConnection();
                }
            }
            //not used
        }


        //Updates information
        public void SelectPackagesArr()
        {
            string query = "SELECT * FROM carwash.packages";

            string[,] listArr = new string[100000, 3];

            int TotalNumRows = 0;

            if (this.OpenConnection() == true)
            {

                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    listArr[TotalNumRows, 0] = reader[0].ToString();
                    listArr[TotalNumRows, 1] = reader[1].ToString();
                    listArr[TotalNumRows, 2] = reader[2].ToString();

                    TotalNumRows = TotalNumRows + 1;
                }

                string[,] finalListArr = new string[TotalNumRows, 3];

                for (int i = 0; i < TotalNumRows; i++)
                {
                    finalListArr[i, 2] = listArr[i, 2];

                    string number = listArr[i, 0] + ". ";
                    string package = listArr[i, 1] + ": ";
                    string price = "R" + listArr[i, 2];
                    Console.Write("{0,-3}{1,-25}{2,-5}", number, package, price);
                    Console.WriteLine();            
                }
                   
                reader.Close();
                this.CloseConnection();            
            }
            else
            {             
                Console.WriteLine("NO ITEMS FOUND");
            }         
        }
        public void ViewSalesIncomeArr(int rowNum)
        {
            string salesQuery = "SELECT sales_income FROM carwash.sales";
            string packagePriceQuery = "SELECT package_price FROM carwash.packages WHERE package_number = @rowNum";

            
            double amountIncome = 0;
            

            if (this.OpenConnection())
            {
                // Get current sales income
                MySqlCommand salesCmd = new MySqlCommand(salesQuery, connection);
                MySqlDataReader salesReader = salesCmd.ExecuteReader();

                while (salesReader.Read())
                {
                    amountIncome = Convert.ToDouble(salesReader[0]);
                }

                salesReader.Close();

                // Get package price for the specified row
                MySqlCommand packageCmd = new MySqlCommand(packagePriceQuery, connection);
                packageCmd.Parameters.AddWithValue("@rowNum", rowNum);

                MySqlDataReader packageReader = packageCmd.ExecuteReader();

                double addedPrice = 0;

                while (packageReader.Read())
                {
                    addedPrice = Convert.ToDouble(packageReader[0]);
                }

                packageReader.Close();
                this.CloseConnection();

                // Calculate new sales income
                double newAmountIncome = amountIncome + addedPrice;

                // Update sales income
                string updateQuery = "UPDATE carwash.sales SET sales_income = @newAmountIncome WHERE ID = 1 LIMIT 1;";

                if (this.OpenConnection())
                {
                    MySqlCommand updateCmd = new MySqlCommand(updateQuery, connection);
                    updateCmd.Parameters.AddWithValue("@newAmountIncome", newAmountIncome);

                    try
                    {
                        updateCmd.ExecuteNonQuery();
                        Console.WriteLine("Sales income successfully updated.");
                    }
                    catch (MySqlException ex)
                    {
                        Console.WriteLine(ex.Number + " . " + ex.Message);
                    }
                    finally
                    {
                        CloseConnection();
                    }
                }
                else
                {
                    Console.WriteLine("Unable to open connection.");
                }
            }
            else
            {
                Console.WriteLine("Unable to open connection.");
            }
        }
        public void BuyMenu()
        {
            Header("CHOOSE A PACKAGE");
            SelectPackagesArr();
            int rowNum = Convert.ToInt32(Console.ReadLine());
            ViewSalesIncomeArr(rowNum);
            ViewAmountSalesArr();

            Header("PAYMENT SUCCESSFUL");
            Console.WriteLine("1 TO GO BACK TO MENU");
        }
        public void ViewAmountSalesArr()
        {
            string query = "SELECT sales_amount FROM carwash.sales";
            string[,] listArr = new string[1, 1];

            int amount_sales_int = 0;

            if (this.OpenConnection() == true)
            {

                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    listArr[0, 0] = reader[0].ToString();
                }

                string[,] finalListArr = new string[1, 1];

                for (int i = 0; i < 1; i++)
                {
                    finalListArr[i, 0] = listArr[i, 0];
                    string amount_sales = listArr[i, 0];
                    amount_sales_int = Convert.ToInt32(amount_sales);
                }

                
                reader.Close();
                this.CloseConnection();

                int new_amount_sales = amount_sales_int + 1;
                //Incriments the amount of sales everytime a sale is done

                string query_ = "UPDATE carwash.sales SET sales_amount = @new_amount_sales WHERE ID = 1 LIMIT 1;";

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd_ = new MySqlCommand(query_, connection);
                    cmd_.Parameters.AddWithValue("@new_amount_sales", new_amount_sales);

                    try
                    {
                        cmd_.ExecuteNonQuery();
                    }
                    catch (MySqlException ex)
                    {
                        Console.WriteLine(ex.Number + " . " + ex.Message);
                        Console.ReadLine();
                    }
                    finally
                    {
                        CloseConnection();
                    }
                    
                }         
              
            }
            else
            {            
                Console.WriteLine("NO ITEMS FOUND");
            }
        }

        public void DisplaySalesInformation()
        {
            Console.Clear();

            Header("SALES INFO");
            string salesAmountQuery = "SELECT sales_amount FROM carwash.sales";
            string salesIncomeQuery = "SELECT sales_income FROM carwash.sales";

            if (this.OpenConnection())
            {
                // Display sales_amount
                MySqlCommand salesAmountCmd = new MySqlCommand(salesAmountQuery, connection);
                object salesAmountResult = salesAmountCmd.ExecuteScalar();

                Console.WriteLine($"TOTAL SALES: {salesAmountResult ?? "N/A"}");

                // Display sales_income
                MySqlCommand salesIncomeCmd = new MySqlCommand(salesIncomeQuery, connection);
                object salesIncomeResult = salesIncomeCmd.ExecuteScalar();

                Console.WriteLine($"TOTAL INCOME: R{salesIncomeResult ?? "N/A"}");

                this.CloseConnection();
            }
            else
            {
                Console.WriteLine("Unable to open connection.");
            }
    
            Console.WriteLine("\n\n\n\n\n1 TO GO BACK TO MENU");
        }


        public void Header(string header)
        {
            Console.Clear();

            int consoleWidth = Console.WindowWidth;                             // Get the width of the console

            int spacesOnEachSide = (consoleWidth - header.Length) / 2;          // Calculate the number of spaces needed on each side

            string leftPadding = new string(' ', spacesOnEachSide);             // Create a string with the calculated spaces for left padding

            Console.WriteLine("".PadLeft(consoleWidth, '*'));                   // Create the top border   
            Console.WriteLine($"{leftPadding}{header.Replace("~", " ")}");      // Print the title with centered alignment        
            Console.WriteLine("".PadLeft(consoleWidth, '*') + "\n");            // Create the bottom border
        }
    }
}
