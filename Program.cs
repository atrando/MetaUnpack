using System;
using MySql.Data.MySqlClient;
using System.Text;
namespace _32PROJECT
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");




         void getAddresses(String column, String company_adress , String country )
    {
        String command;

        MySqlConnection connection = new MySqlConnection
            {
                ConnectionString = "server=192.168.137.85;user id=User1;password=qwerty123;persistsecurityinfo=True;port=3306;database=metapackproject"
            };
            connection.Open();
        StringBuilder stringQuery = new StringBuilder();
        stringQuery.Append("SELECT " +column +  " FROM TB_EVLS_ADDRESS ");
      //  stringQuery.Append("("+ column + ") VALUES ");
      //  stringQuery.Append("(" +company_adress +")");

          // MySqlCommand command = new MySqlCommand("INSERT INTO TB_EVLS_ADDRESS (address_name1) VALUES ('podgorna')", connection);
          //MySqlCommand command = new MySqlCommand("INSERT INTO TB_EVLS_ADDRESS (address_name1) VALUES ('podgorna')", connection);
          MySqlCommand command = new MySqlCommand(stringQuery.ToString(), connection);
            using(MySqlDataReader reader = command.ExecuteReader()){
                System.Console.WriteLine("asd");
                 while (reader.Read())
                {
                string row = $"{reader["address_name1"]}";
                System.Console.WriteLine(row);
                }

            } 
            connection.Close();
    }
   void addAddresses(String column, String company_adress , String country )
    {
        StringBuilder command;
        
        MySqlConnection connection = new MySqlConnection
            {
                ConnectionString = "server=192.168.137.85;user id=User1;password=qwerty123;persistsecurityinfo=True;port=3306;database=metapackproject"
            };
            connection.Open();
        StringBuilder stringQuery = new StringBuilder();
        stringQuery.Append("SELECT " +column +  " FROM TB_EVLS_ADDRESS ");
      //  stringQuery.Append("("+ column + ") VALUES ");
      //  stringQuery.Append("(" +company_adress +")");

          // MySqlCommand command = new MySqlCommand("INSERT INTO TB_EVLS_ADDRESS (address_name1) VALUES ('podgorna')", connection);
          //MySqlCommand command = new MySqlCommand("INSERT INTO TB_EVLS_ADDRESS (address_name1) VALUES ('podgorna')", connection);
          MySqlCommand command = new MySqlCommand(stringQuery.ToString(), connection);
            using(MySqlDataReader reader = command.ExecuteReader()){
                System.Console.WriteLine("asd");
                 while (reader.Read())
                {
                string row = $"{reader["address_name1"]}";
                System.Console.WriteLine(row);
                }

            } 
            connection.Close();
    }
    getAddresses("address_name1","","");
}
        }
    
    }

