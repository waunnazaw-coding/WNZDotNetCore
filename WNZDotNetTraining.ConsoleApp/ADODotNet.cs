using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WNZDotNetTraining.ConsoleApp
{
    public class ADODotNet
    {
        private readonly string _connectionString = " Data Source = . ; Initial Catalog = WNZDotNet; User ID = sa; Password = waunnazaw "; // same  
        public void Read()
        {
            //string connectionString = " Data Source = . ; Initial Catalog = WNZDotNet; User ID = sa; Password = waunnazaw ";
            Console.WriteLine($"Connection String => {_connectionString}");
            SqlConnection connection = new SqlConnection(_connectionString);
            //Connection Open
            Console.WriteLine("Connection Openning ............");
            connection.Open();
            Console.WriteLine("Connection Opened............");

            ////DataSet=> DataTable => DataRow => DataColumn
            //foreach (DataRow dr in dt.Rows)
            //{
            //    Console.WriteLine(dr["BlogId"]);
            //    Console.WriteLine(dr["BlogTitle"]);
            //    Console.WriteLine(dr["BlogAuthor"]);
            //    Console.WriteLine(dr["BlogContent"]);
            //    //Console.WriteLine(dr["DeleteFlag"]);
            //}


            string query = $@"SELECT [BlogId] 
                  ,[BlogTitle]
                  ,[BlogAuthor]
                  ,[BlogContent]
                  ,[DeleteFlag]
              FROM [dbo].[Tbl_Blog] where DeleteFlag = 0 "; // @ sign is multiline supports , $ 
            SqlCommand command = new SqlCommand(query, connection);
            //SqlDataAdapter adapter = new SqlDataAdapter(command);
            //DataTable dt = new DataTable();
            //adapter.Fill(dt);// dt = adapter.Fill() or adapter.Excuter();

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(reader["BlogId"]);
                Console.WriteLine(reader["BlogTitle"]);
                Console.WriteLine(reader["BlogAuthor"]);
                Console.WriteLine(reader["BlogContent"]);
                //Console.WriteLine(dr["DeleteFlag"]);
            }

            //Connection Close
            Console.WriteLine("Connection Closing ............");
            connection.Close();
            Console.WriteLine("Connection Closed............");

            //DataSet=> DataTable => DataRow => DataColumn
            //foreach (DataRow dr in dt.Rows)
            //{
            //    Console.WriteLine(dr["BlogId"]);
            //    Console.WriteLine(dr["BlogTitle"]);
            //    Console.WriteLine(dr["BlogAuthor"]);
            //    Console.WriteLine(dr["BlogContent"]);
            //    //Console.WriteLine(dr["DeleteFlag"]);
            //}
        }

        public void Create()
        {
            Console.WriteLine("Blog Title => ");
            string title = Console.ReadLine();

            Console.WriteLine("Blog Author => ");
            string author = Console.ReadLine();

            Console.WriteLine("Blog Content => ");
            string content = Console.ReadLine();


            //string connectionString = "Data Source = . ; Initial Catalog = WNZDotNet ; User Id = sa; Password = waunnazaw";
            SqlConnection sqlConnection = new SqlConnection(_connectionString);

            sqlConnection.Open();

            //string sqlQuery = $@"INSERT INTO [dbo].[Tbl_Blog]
            //           ([BlogTitle]
            //           ,[BlogAuthor]
            //           ,[BlogContent]
            //           ,[DeleteFlag])
            //     VALUES
            //           ('{title}' ,
            //            '{author}' ,
            //            '{content}' ,  
            //            0)"; // no secure . have become SQL Injection
            string sqlQuery = $@"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent]
           ,[DeleteFlag])
     VALUES
           (@BlogTitle,
            @BlogAuthor,
            @BlogContent,
            0)";
            SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);

            command.Parameters.AddWithValue("@BlogTitle", title); //secure 
            command.Parameters.AddWithValue("@BlogAuthor", author); //secure
            command.Parameters.AddWithValue("@BlogContent", content); //secure

            //SqlDataAdapter adapter = new SqlDataAdapter(command); // => use in read
            //DataTable dt = new DataTable();
            //adapter.Fill(dt); 

            int result = command.ExecuteNonQuery();  //output is interger . one time run output one integer
            //if (result == 1)
            //{
            //    Console.WriteLine("Saving Successfully !");
            //}
            //else
            //{
            //    Console.WriteLine("Saving Fail!");
            //}

            Console.WriteLine(result == 1 ? "Saving successfully!" : "Saving Fail"); //tenary operator

            sqlConnection.Close(); 
        }

        public void Edit()
        {
            Console.Write("Blog Id => ");
            string id = Console.ReadLine();

            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            //Connection Open
            sqlConnection.Open();

            string sqlQuery = @"SELECT [BlogId]
                              ,[BlogTitle]
                              ,[BlogAuthor]
                              ,[BlogContent]
                              ,[DeleteFlag]
                          FROM [dbo].[Tbl_Blog] where BlogId = @BlogId";

            SqlCommand command = new SqlCommand(sqlQuery , sqlConnection);
            command.Parameters.AddWithValue("@BlogId", id);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            //Connection Close
            sqlConnection.Close();

            if (dt.Rows.Count == 0)
            {
                Console.WriteLine("No Data Found!");
                return;
            }

            DataRow dr = dt.Rows[0];
            Console.WriteLine(dr["BlogId"]);
            Console.WriteLine(dr["BlogTitle"]);
            Console.WriteLine(dr["BlogAuthor"]);
            Console.WriteLine(dr["BlogContent"]);
            //Console.WriteLine(dr["DeleteFlag"]);

        }

        public void Update()
        {
            Console.Write("Blog Id => ");
            string id = Console.ReadLine();

            Console.WriteLine("Blog Title => ");
            string title = Console.ReadLine();

            Console.WriteLine("Blog Author => ");
            string author = Console.ReadLine();

            Console.WriteLine("Blog Content => ");
            string content = Console.ReadLine();

            SqlConnection sqlConnection = new SqlConnection(_connectionString);

            sqlConnection.Open();

            string sqlQuery = $@"UPDATE [dbo].[Tbl_Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
      ,[DeleteFlag] = 0
 WHERE BlogId = @BlogId";
            SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);

            command.Parameters.AddWithValue("@BlogId" , id );
            command.Parameters.AddWithValue("@BlogTitle", title);
            command.Parameters.AddWithValue("@BlogAuthor", author); 
            command.Parameters.AddWithValue("@BlogContent", content); 

            int result = command.ExecuteNonQuery();  //output is interger . one time run output one integer

            Console.WriteLine(result == 1 ? "Updating successfully!" : "Saving Fail"); //tenary operator

            sqlConnection.Close();
        }

    }
}
