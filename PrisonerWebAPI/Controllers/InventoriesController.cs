using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrisonerWebAPI.Models;
using System.Data;
using System.Data.SqlClient;

namespace PrisonerWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoriesController : ControllerBase
    {
        //Use of Dependency Injection
        private readonly IConfiguration _configuration;

        public InventoriesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]

        public JsonResult Get()
        {
            string query = @" 
                    select InventoryId, PrisonerId, ItemName, Quantity from dbo.Inventories";
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

        [HttpPost("{PrisonerId}")]
        public JsonResult Post(Inventories inv)
        {
            string query = @" 
                    insert into dbo.Inventories (InventoryId,ItemName, Quantity) values 
                    ('" + inv.InventoryId + @"',
                     
                      '" + inv.ItemName + @"',
                      '" + inv.Quantity + @"')";
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

        [HttpPut]
        public JsonResult Put(Inventories inv)
        {
            string query = @" 
                    update dbo.Inventories set                     
                     ItemName = '" + inv.ItemName + @"',
                      Quantity= '" + inv.Quantity + @"'                    
                      where PrisonerId= '"+ inv.PrisonerId+ @"'";
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

        [HttpDelete("{InventoryId}")]
        public JsonResult Delete(int InventoryId)
        {
            string query = @" 
                    delete from  dbo.Inventories where InventoryId=" + InventoryId + @"";
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