using Datenbank;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text.Json;
using SQL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
using Newtonsoft.Json;
namespace WEB_API.Controllers
{
    [Route("api/[controller]")] 
    [ApiController]
    public class RecipeController : ControllerBase
    {
        // GET: api/<RecipeController>
        [HttpGet("Get all recipes")]
        public string Get()
        {
            var builder = new SqlConnectionStringBuilder();
            builder.ConnectionString = "Server=(localDB)\\MSSQLLocaldb;Database=recipes;Integrated Security=True;TrustServerCertificate=true";

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();

                var dt = Commands.recipeGet("select * from recipe",connection);

                var json = JsonConvert.SerializeObject(dt);

                return json;
            }

            
        }

        [HttpGet("Get single recipe")]
        public string Get(int id)
        {
            var builder = new SqlConnectionStringBuilder();
            builder.ConnectionString = "Server=(localDB)\\MSSQLLocaldb;Database=recipes;Integrated Security=True;TrustServerCertificate=true";

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();

                var dt = Commands.recipeGet($"select * from recipe where Dish_ID = {id}", connection);

                var json = JsonConvert.SerializeObject(dt);

                return json;
            }


        }
    }
}
