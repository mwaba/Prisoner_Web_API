using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrisonerWebAPI.Models;
using System.Data;
using System.Data.SqlClient;

namespace PrisonerWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrisonersController : ControllerBase
    {
        //Use of Dependency Injection
        private readonly IConfiguration _configuration;

        public PrisonersController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]

        public JsonResult Get()
        {
            string query = @" 
                    select PrisonerId, PrisonerName, PrisonerCrime, PrisonerCellnumber from dbo.Prisoners";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PrisonerAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpGet("{PrisonerId}")]

        public JsonResult Get(int PrisonerId)
        {
            string query = @" 
                    SELECT Prisoners.PrisonerName, Prisoners.PrisonerCrime, Prisoners.PrisonerCellnumber, Inventories.InventoryId, Inventories.ItemName, Inventories.Quantity

                    FROM dbo.Prisoners, dbo.Inventories
                    where Prisoners.PrisonerId = Inventories.PrisonerId";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PrisonerAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }




        [HttpPost]
        public JsonResult Post(Prisoners pris)
        {
            string query = @" 
                    insert into dbo.Prisoners(PrisonerId, PrisonerName, PrisonerCrime,PrisonerCellnumber ) values 
                    ('" + pris.PrisonerId+ @"',
                     '"+ pris.PrisonerName+ @"',
                      '"+ pris.PrisonerCrime+@"',
                      '"+ pris.PrisonerCellnumber+ @"')";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PrisonerAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }

        [HttpPut("{PrisonerId}")]
        public JsonResult Put(Prisoners pris)
        {
            string query = @" 
                    update dbo.Prisoners set                     
                     PrisonerName= '" + pris.PrisonerName + @"',
                      PrisonerCrime= '" + pris.PrisonerCrime + @"',
                      PrisonerCellnumber= '" + pris.PrisonerCellnumber + @"'
                      where PrisonerId= '" + pris.PrisonerId + @"'";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PrisonerAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Updated Successfully");
        }


       



        [HttpDelete("{PrisonerId}")]
        public JsonResult Delete(int PrisonerId)
        {
            string query = @" 
                    delete from  dbo.Prisoners where PrisonerId=" + PrisonerId + @"";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PrisonerAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Deleted Successfully");
        }
    }
}
