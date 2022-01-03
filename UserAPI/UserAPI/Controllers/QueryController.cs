using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using WebAPIReact.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace WebAPIReact.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class QueryController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public QueryController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public JsonResult Get()
        {

            string query = @"select *
                            from dbo.query";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("UserCon");
            SqlDataReader myReader;
            using (SqlConnection myconn = new SqlConnection(sqlDataSource))
            {

                myconn.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myconn))
                {

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myconn.Close();
                }

            }
            return new JsonResult(table);
        }
        [HttpPost]
        public JsonResult Post(Query qr)
        {
            string query = @"insert into dbo.query(queryname,Department,Descript) values(
                                        '" + qr.queryname + @"',
                                         '" + qr.Department + @"',
                                         '" + qr.Descript + @"'
                                          
                                         
                                            
                                                )
                                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("UserCon");
            SqlDataReader myReader;
            using (SqlConnection myconn = new SqlConnection(sqlDataSource))
            {

                myconn.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myconn))
                {

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myconn.Close();
                }

            }
            return new JsonResult("Added Successfully");


        }

        [HttpPut]
        public JsonResult Put(Query qr)
        {


            string query = @"update dbo.query set
                            queryname= '" + qr.queryname + @"',
                             Department='" + qr.Department + @"',
                            Descript= '" + qr.Descript + @"'
                  where QueryId=" + qr.QueryId + @"";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("UserCon");
            SqlDataReader myReader;
            using (SqlConnection myconn = new SqlConnection(sqlDataSource))
            {

                myconn.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myconn))
                {

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myconn.Close();
                }

            }
            return new JsonResult("Updated Successfully");


        }
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"delete from dbo.query 
                  where QueryId=" + id + @"";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("UserCon");
            SqlDataReader myReader;
            using (SqlConnection myconn = new SqlConnection(sqlDataSource))
            {

                myconn.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myconn))
                {

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myconn.Close();
                }

            }
            return new JsonResult("Deleted Successfully");


        }
        [Route("GetAllDepartmentNames")]
        public JsonResult GetAllDepartmentNames()
        {
            string query = @"select DepartmentName from dbo.Department";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("UserCon");
            SqlDataReader myReader;
            using (SqlConnection myconn = new SqlConnection(sqlDataSource))
            {

                myconn.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myconn))
                {

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myconn.Close();
                }

            }
            return new JsonResult(table);
        }
    }
}
