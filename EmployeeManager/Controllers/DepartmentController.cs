using EmployeeManager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace EmployeeManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly DataContext dataContext;
        public DepartmentController(DataContext context)
        {
            this.dataContext = context;
        }



        [HttpGet]
        public async Task<JsonResult> Get()
        {
            /*
            string query = @"
                            select DepartmentId, DepartmentName from
                            dbo.Department
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = this.configuration.GetConnectionString("EmployeeAppCon");
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
            */
            
            return new JsonResult(await this.dataContext.Departments.ToListAsync());

        }



        [HttpPost]
        public async Task<JsonResult> Post(Department dep)
        {
            /*
            string query = @"
                            insert into dbo.Department values (@DepartmentName)
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = this.configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using(SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using(SqlCommand myCommand =new  SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@DepartmentName", dep.DepartmentName);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Added Successfully");   
            */
            this.dataContext.Departments.Add(dep);
            await this.dataContext.SaveChangesAsync();
            return new JsonResult("success");
        }


       

        [HttpPut]
        public async Task<JsonResult> Put(Department dep)
        {/*
            string query = @"
                            update  dbo.Department 
                            set DepartmentName =@DepartmentName
                            where DepartmentId=@DepartmentId
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = this.configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@DepartmentId", dep.DepartmentId);
                    myCommand.Parameters.AddWithValue("@DepartmentName", dep.DepartmentName);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            */
            var department=await this.dataContext.Departments.FindAsync(dep.DepartmentId);
            if(department==null)
            {
                return new JsonResult("Department not found");
            }  
            department.DepartmentName= dep.DepartmentName;
            await this.dataContext.SaveChangesAsync();

            return new JsonResult("ypdated");
        }


        
        [HttpDelete("{id}")]
        public async Task<JsonResult> Delete(int id)
        {
            /*
            string query = @"
                            delete from dbo.Department 
                            
                            where DepartmentId=@DepartmentId
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = this.configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@DepartmentId", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            */

            var department = await this.dataContext.Departments.FindAsync(id);
            if (department == null)
            {
                return new JsonResult("Department not found");
            }

            dataContext.Departments.Remove(department);
            await this.dataContext.SaveChangesAsync();

            return new JsonResult("Deleted");
        }

    
       
    }
}
