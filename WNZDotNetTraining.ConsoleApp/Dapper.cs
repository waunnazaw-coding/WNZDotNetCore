using Dapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using WNZDotNetTraining.ConsoleApp.Models;

namespace WNZDotNetTraining.ConsoleApp
{
    public class DapperExample
    {
        private readonly string _connectionString = " Data Source = . ; Initial Catalog = WNZDotNet; User ID = sa; Password = waunnazaw ";
        public void Read()
        {
            //using (IDbConnection db = new SqlConnection(_connectionString))
            //{
            //    string query = "select * from tbl_blog where DeleteFlag = 0";
            //    var lst = db.Query(query).ToList();
            //    foreach (var item in lst)
            //    {
            //        Console.WriteLine(item.BlogId);
            //        Console.WriteLine(item.BlogTitle);
            //        Console.WriteLine(item.BlogAuthor);
            //        Console.WriteLine(item.BlogContent);
            //    }
            //}

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "select * from tbl_blog where DeleteFlag = 0";
                var lst = db.Query<BlogDataModel>(query).ToList();
                foreach (var item in lst)
                {
                    Console.WriteLine(item.BlogId);
                    Console.WriteLine(item.BlogTitle);
                    Console.WriteLine(item.BlogAuthor);
                    Console.WriteLine(item.BlogContent);
                }
            }

            //DTO => Data Transfer Object
        }

        public void Create(string title, string author, string content)
        {
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

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                int result = db.Execute(sqlQuery, new BlogDataModel
                {
                    BlogTitle = title,
                    BlogAuthor = author,
                    BlogContent = content
                });
                Console.WriteLine(result == 1 ? "Create Successfully! " : "Create Fail");
            }
        }

        public void Edit(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "select * from tbl_blog where DeleteFlag = 0 and BlogId = @BlogId";
                var item = db.Query<BlogDataDapperModel>(query, new BlogDataDapperModel
                {
                    BlogId = id
                }).FirstOrDefault();

                if ( item is null)
                {
                    Console.WriteLine("No data found");
                    return;
                }
                
                    Console.WriteLine(item.BlogId);
                    Console.WriteLine(item.BlogTitle);
                    Console.WriteLine(item.BlogAuthor);
                    Console.WriteLine(item.BlogContent);
                
            }
        }

        public void Update(int id , string title, string author, string content)
        {
            string sqlQuery = $@"UPDATE [dbo].[Tbl_Blog]
  SET [BlogTitle] = @BlogTitle
     ,[BlogAuthor] = @BlogAuthor
     ,[BlogContent] =@BlogContent
     ,[DeleteFlag] =0
WHERE BlogId = @BlogId;";


            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                int result = db.Execute(sqlQuery, new BlogDataDapperModel
                {
                    BlogId = id,
                    BlogTitle = title,
                    BlogAuthor = author,
                    BlogContent = content
                });
                Console.WriteLine(result == 1 ? "Updating Successfully! " : "Updating Fail");
            }
        }

        public void Delete(int id)
        {
            string sqlQuery = @"DELETE FROM [dbo].[Tbl_Blog] WHERE BlogId = @BlogId";
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                int result = db.Execute(sqlQuery, new BlogDataDapperModel
                {
                    BlogId = id,
                });
                Console.WriteLine(result == 1 ? "Deleting Successfully! " : "Deletng Fail");
            }

        }
    }
}
