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
        [HttpGet("Get entire recipe")]
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


        // POST api/<RecipeController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<RecipeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RecipeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
