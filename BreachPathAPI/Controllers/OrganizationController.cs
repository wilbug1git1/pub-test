using BreachPathAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BreachPath.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public OrganizationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET: api/<OrganizationController>
        [HttpGet]
        public IEnumerable<Organization> Get()
        {
            //TODO: replace with storedproc
            string query = @"
                    select OrganizationId, OrganizationName, OrganizationUrl, OrganizationTokenUri from dbo.Organizations";
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
            return ConvertToOrganization(table);
        }

                // GET api/<OrganizationController>/5
        [HttpGet("{id}")]
        public IEnumerable<Organization> Get(int id)
        {
            //TODO: replace with storedproc
            string query = @"
                    select OrganizationId, OrganizationName, OrganizationUrl, OrganizationTokenUri from dbo.Organizations where OrganizationId = " + id;
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
            return ConvertToOrganization(table);
        }

        // POST api/<OrganizationController>
        [HttpPost]
        public JsonResult Post([FromBody] Organization org)
        {
            //TODO: replace with storedproc
            string query = @"insert into dbo.Organizations values('" + org.OrganizationName + "','" + org.OrganizationUrl + "','" + org.ConnectionTokenUri + "',')";

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
        
        // PUT api/<OrganizationController>/5
        [HttpPut("{id}")]
        public JsonResult Put(int id, Organization org)
        {
            //TODO: replace with storedproc
            string query = @"update dbo.Organizations set
                            OrganizationName = '" + org.OrganizationName + @"',
                            OrganizationUrl = '" + org.OrganizationUrl + @"',
                            OrganizationTokenUri = '" + org.ConnectionTokenUri + @"',
                            where OrganizationId = " + id;

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

        // DELETE api/<OrganizationController>/5
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            //TODO: replace with storedproc
            string query = @"delete from dbo.Organizations where OrganizationId = " + id + @"";

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

        private IEnumerable<Organization> ConvertToOrganization(DataTable dataTable)
        {
            foreach (DataRow row in dataTable.Rows)
            {
                yield return new Organization
                {
                    OrganizationId = Convert.ToInt32(row[0]),
                    OrganizationName = Convert.ToString(row[1]),
                    OrganizationUrl = Convert.ToString(row[2]),
                    ConnectionTokenUri = Convert.ToString(row[3])
                };
            }
        }

        // POST api/<OrganizationController>
        [HttpPost]
        public JsonResult Post(string name, string token)
        {
            Organization org = new Organization();
            org.ConnectionTokenUri = token;

            //TODO: replace with storedproc
            string query = @"insert into dbo.Organizations values('" + org.ConnectionTokenUri + "') where " + org.OrganizationName + "=" + name;

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
    }
}
