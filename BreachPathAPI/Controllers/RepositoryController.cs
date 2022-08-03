using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BreachPath.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepositoryController : ControllerBase
    {

        private readonly IConfiguration _configuration;

        public RepositoryController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET: api/<RepositoryController>
        [HttpGet]
        public IEnumerable<Repository> Get()
        {
            //TODO: replace with storedproc
            string query = @"
                    select RepositoryId, RepositoryName, RepositoryUrl, RepositoryOwner from dbo.Repositories";
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
            return ConvertToRepository(table);
        }

        // GET api/<RepositoryController>/5
        [HttpGet("{id}")]
        public IEnumerable<Repository> Get(int id)
        {
            //TODO: replace with storedproc
            string query = @"
                    select RepositoryId, RepositoryName, RepositoryUrl, RepositoryOwner from dbo.Repositories where RepositoryId=" + id;
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
            return ConvertToRepository(table);
        }

        // POST api/<RepositoryController>
        [HttpPost]
        public JsonResult Post([FromBody] Repository repo)
        {
            //TODO: replace with storedproc
            string query = @"insert into dbo.Repositories values('" + repo.RepositoryName + "','" + repo.RepositoryOwner + "','" + repo.RepositoryUrl + "','" + repo.RepositoryOrganizationId + @"')";

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

        // PUT api/<RepositoryController>/5
        [HttpPut("{id}")]
        public JsonResult Put(Repository repo, int id)
        {
            //TODO: replace with storedproc
            string query = @"update dbo.Repositories set
                            RepositoryName = '" + repo.RepositoryName + @"',
                            RepositoryUrl = '" + repo.RepositoryOwner + @"',
                            RepositoryTokenUri = '" + repo.RepositoryUrl + @"',
                            where RepositoryId = " + id;

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

        // DELETE api/<RepositoryController>/5
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            //TODO: replace with storedproc
            string query = @"delete from dbo.Repositories where RepositoryId = " + id + @"";

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

        private IEnumerable<Repository> ConvertToRepository(DataTable dataTable)
        {
            foreach (DataRow row in dataTable.Rows)
            {
                yield return new Repository
                {
                    RepositoryId = Convert.ToInt32(row[0]),
                    RepositoryName = Convert.ToString(row[1]),
                    RepositoryUrl = Convert.ToString(row[2]),
                    RepositoryOwner = Convert.ToString(row[3])
                    //RepositoryOrganizationId = Convert.ToInt32(row[4])
                };
            }
        }
    }
}
