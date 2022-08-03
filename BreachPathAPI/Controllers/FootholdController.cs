using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BreachPath.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FootholdController : ControllerBase
    {

        private readonly IConfiguration _configuration;

        public FootholdController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET: api/<FootholdController>
        [HttpGet]
        public IEnumerable<Foothold> Get()
        {
            //TODO: replace with storedproc
            string query = @"
                    select FootholdId, FootholdName, FootholdDescription, FootholdConfig, FootholdFileLocation from dbo.Footholds";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("BreachPathDbConnection");
            SqlDataReader sqlReader;
            using (SqlConnection connection = new SqlConnection(sqlDataSource))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    sqlReader = command.ExecuteReader();
                    table.Load(sqlReader);

                    sqlReader.Close();
                    connection.Close();
                }
            }
            return ConvertToFoothold(table);
        }

        // GET api/<FootholdController>/5
        [HttpGet("{id}")]
        public IEnumerable<Foothold> Get(int id)
        {
            //TODO: replace with storedproc
            string query = @"
                    select FootholdId, FootholdName, FootholdDescription, FootholdOwner from dbo.Footholds where FootholdId=" + id;
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("BreachPathDbConnection");
            SqlDataReader sqlReader;
            using (SqlConnection connection = new SqlConnection(sqlDataSource))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    sqlReader = command.ExecuteReader();
                    table.Load(sqlReader);

                    sqlReader.Close();
                    connection.Close();
                }
            }
            return ConvertToFoothold(table);
        }

        // POST api/<FootholdController>
        [HttpPost]
        public JsonResult Post([FromBody] Foothold fh)
        {
            //TODO: replace with storedproc
            string query = @"insert into dbo.Footholds values('" + fh.FootholdName + "','" + fh.FootholdDescription + "','" + fh.FootholdDescription + "','" + fh.FootholdConfig + "','" + fh.FootholdFileLocation + @"')";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("BreachPathDbConnection");
            SqlDataReader sqlReader;
            using (SqlConnection connection = new SqlConnection(sqlDataSource))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    sqlReader = command.ExecuteReader();
                    table.Load(sqlReader);

                    sqlReader.Close();
                    connection.Close();
                }
            }
            return new JsonResult("Added Successfully");
        }

        // PUT api/<FootholdController>/5
        [HttpPut("{id}")]
        public JsonResult Put(Foothold fh, int id)
        {
            //TODO: replace with storedproc
            string query = @"update dbo.Footholds set
                            FootholdName = '" + fh.FootholdName + @"',
                            FootholdDescription = '" + fh.FootholdDescription + @"',
                            FootholdConfig = '" + fh.FootholdConfig + @"',
                            FootholdFileLocation = '" + fh.FootholdFileLocation + @"'
                            where FootholdId = " + id;

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("BreachPathDbConnection");
            SqlDataReader sqlReader;
            using (SqlConnection connection = new SqlConnection(sqlDataSource))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    sqlReader = command.ExecuteReader();
                    table.Load(sqlReader);

                    sqlReader.Close();
                    connection.Close();
                }
            }
            return new JsonResult("Updated Successfully");
        }

        // DELETE api/<FootholdController>/5
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            //TODO: replace with storedproc
            string query = @"delete from dbo.Repositories where FootholdId = " + id + @"";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("BreachPathDbConnection");
            SqlDataReader sqlReader;
            using (SqlConnection connection = new SqlConnection(sqlDataSource))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    sqlReader = command.ExecuteReader();
                    table.Load(sqlReader);

                    sqlReader.Close();
                    connection.Close();
                }
            }
            return new JsonResult("Deleted Successfully");
        }

        private IEnumerable<Foothold> ConvertToFoothold(DataTable dataTable)
        {
            foreach (DataRow row in dataTable.Rows)
            {
                yield return new Foothold
                {
                    FootholdId = Convert.ToInt32(row[0]),
                    FootholdName = Convert.ToString(row[1]),
                    FootholdDescription = Convert.ToString(row[2]),
                    FootholdConfig = Convert.ToString(row[3]),
                    FootholdFileLocation = Convert.ToString(row[4])
                };
            }
        }
    }
}
