using System;
using System.Data.SqlClient;
using sqlserverapp.Models;

namespace sqlserverapp.Services;


public class ProductService
{
    private static string db_source = "raghumovvasqlserver.database.windows.net";
    private static string db_username = "serveradmin";
    private static string db_password = "Password1234";
    private static string db_database = "appdb";

    private SqlConnection GetConnection()
    {
        var _builder = new SqlConnectionStringBuilder();
        _builder.DataSource = db_source;
        _builder.UserID = db_username;
        _builder.Password = db_password;
        _builder.InitialCatalog = db_database;
        return new SqlConnection(_builder.ConnectionString);
    }

    public List<Product> GetProducts()
    {
        SqlConnection conn = GetConnection();
        List<Product> products = new List<Product>();
        string statement = "select ProductId,ProductName,Quantity from products";
        conn.Open();
        SqlCommand sqlCommand = new SqlCommand(statement, conn);
        using (SqlDataReader reader = sqlCommand.ExecuteReader())
        {
            while(reader.Read())
            {
                Product product = new Product()
                {
                    ProductId = reader.GetInt32(0),
                    ProductName = reader.GetString(1),
                    Quantity = reader.GetInt32(2)

                };
                products.Add(product);


            }
        }
        conn.Close();
        return products;

           
    }

}

