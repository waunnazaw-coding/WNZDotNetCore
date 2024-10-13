using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WNZDotNetCore.Database.Models;
using WNZDotNetTraining.RestApi.DataModels;
using WNZDotNetTraining.RestApi.ViewModels;

namespace WNZDotNetTraining.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdoDotNetController : ControllerBase
    {

        private readonly string _connectionString = " Data Source = . ; Initial Catalog = WNZDotNet; User ID = sa; Password = waunnazaw ;TrustServerCertificate=True; ";

        [HttpGet]
        public IActionResult GetBlogs()
        {
            List<BlogViewModels> lst = new List<BlogViewModels>();

            SqlConnection connection = new SqlConnection(_connectionString);

            connection.Open();

            string query = $@"SELECT [BlogId] 
                  ,[BlogTitle]
                  ,[BlogAuthor]
                  ,[BlogContent]
                  ,[DeleteFlag]
              FROM [dbo].[Tbl_Blog] where DeleteFlag = 0 "; // @ sign is multiline supports , $ 
            SqlCommand command = new SqlCommand(query, connection);

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(reader["BlogId"]);
                Console.WriteLine(reader["BlogTitle"]);
                Console.WriteLine(reader["BlogAuthor"]);
                Console.WriteLine(reader["BlogContent"]);

                //lst.Add(new BlogViewModels
                //{
                //    Id = Convert.ToInt32(reader["BlogId"]),
                //    Title = Convert.ToString(reader["BlogTitle"]),
                //    Author = Convert.ToString(reader["BlogAuthor"]),
                //    Content = Convert.ToString(reader["BlogContent"]),
                //    DeleteFlag = Convert.ToBoolean(reader["DeleteFlag"])
                //});

                var item = new BlogViewModels
                {
                    Id = Convert.ToInt32(reader["BlogId"]),
                    Title = Convert.ToString(reader["BlogTitle"]),
                    Author = Convert.ToString(reader["BlogAuthor"]),
                    Content = Convert.ToString(reader["BlogContent"]),
                    DeleteFlag = Convert.ToBoolean(reader["DeleteFlag"])
                };

                lst.Add(item);
            }

            connection.Close();

            return Ok (lst);
        }


        [HttpPost]
        public IActionResult CreateBlogs(BlogDataModels blog)
        {

            SqlConnection sqlConnection = new SqlConnection(_connectionString);

            sqlConnection.Open();

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

            command.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle); 
            command.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor); 
            command.Parameters.AddWithValue("@BlogContent", blog.BlogContent);

            int result = command.ExecuteNonQuery();

            sqlConnection.Close();

            return result == 1 ? Ok("Creating Successfully! ") : NotFound("Creating Fail! ");

        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlogs(int id, BlogViewModels blog)
        {

            SqlConnection sqlConnection = new SqlConnection(_connectionString);

            sqlConnection.Open();

            string sqlQuery = $@"UPDATE [dbo].[Tbl_Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
      ,[DeleteFlag] = 0
 WHERE BlogId = @BlogId";
            SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);

            command.Parameters.AddWithValue("@BlogId", id);
            command.Parameters.AddWithValue("@BlogTitle", blog.Title);
            command.Parameters.AddWithValue("@BlogAuthor", blog.Author);
            command.Parameters.AddWithValue("@BlogContent", blog.Content);

            int result = command.ExecuteNonQuery();

            sqlConnection.Close();

            return result == 1 ? Ok("Updating Successfully.") : NotFound("Updating Fail");

        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlogs(int id,  BlogViewModels blog)
        {

            string conditions = "";

            if (! string.IsNullOrEmpty(blog.Title))
            {
                conditions += " [BlogTitle] = @BlogTitle, ";
            }

            if (!string.IsNullOrEmpty(blog.Author))
            {
                conditions += " [BlogAuthor] = @BlogAuthor, ";
            }

            if (!string.IsNullOrEmpty(blog.Content))
            {
                conditions += " [BlogContent] = @BlogContent, ";
            }

            if (conditions.Length == 0)
            {
                return BadRequest("Invaild Parameter");
            }

            conditions = conditions.Substring(0, conditions.Length - 2);

            SqlConnection sqlConnection = new SqlConnection(_connectionString);

            sqlConnection.Open();

            string sqlQuery = $@"UPDATE [dbo].[Tbl_Blog] SET {conditions}  WHERE BlogId = @BlogId";
            SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);

            command.Parameters.AddWithValue("@BlogId", id);
            if (!string.IsNullOrEmpty(blog.Title))
            {
                command.Parameters.AddWithValue("@BlogTitle", blog.Title);
            }

            if (!string.IsNullOrEmpty(blog.Author))
            {
                command.Parameters.AddWithValue("@BlogAuthor", blog.Author);
            }

            if (!string.IsNullOrEmpty(blog.Content))
            {
                command.Parameters.AddWithValue("@BlogContent", blog.Content);
            }

            int result = command.ExecuteNonQuery(); 

            sqlConnection.Close();

            return result == 1 ? Ok("Updating successfully!") : NotFound("Update failed");

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlogs(int id)
        {
            SqlConnection sqlConnection = new SqlConnection(_connectionString);

            sqlConnection.Open();

            string sqlQuery = @"DELETE FROM [dbo].[Tbl_Blog]
                           WHERE BlogId = @BlogId";
            SqlCommand command = new SqlCommand(sqlQuery, sqlConnection);
            command.Parameters.AddWithValue("@BlogId", id);

            int result = command.ExecuteNonQuery();
            
            sqlConnection.Close();

            return result == 1 ? Ok("Delete Successfully! ") : NotFound("Delete fail");
        }
    }
}
