using Microsoft.Extensions.Configuration;
using SalesWebMvc.Interfaces;
using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Services
{
    public class DepartmentServiceAdo : IDepartmentService
    {
        private readonly IConfiguration _configuration;

        public DepartmentServiceAdo(IConfiguration configuration)
        {
            _configuration = configuration;
        }



        public List<Department> GetDepartments()
        {
            List<Department> departmentList = new List<Department>();

            var stringConnection = _configuration["AdoSqlConn"];
            var query = "SELECT Id, Name FROM tb_Department";

            using (SqlConnection conn = new SqlConnection(stringConnection))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = conn;
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            departmentList.Add(new Department() 
                            { 
                                Id = Convert.ToInt32(reader["Id"]), 
                                Name = Convert.ToString(reader["Name"]) 
                            
                            });
                        }
                    }
                    conn.Close();
                }
            }

            if (departmentList.Count == 0)
            {
                return new List<Department>() { new Department() };
            }

            return departmentList;
        }

        public int CreateDepartment(Department department)
        {
            var query = "Insert into tb_Department (Name) Values (@Name)";
            var stringConnection = _configuration["AdoSqlConn"];
            query += "SELECT SCOPE_IDENTITY()";

            using (SqlConnection con = new SqlConnection(stringConnection))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@Name", department.Name);
                    cmd.Connection = con;
                    con.Open();
                    department.Id = Convert.ToInt32(cmd.ExecuteScalar());

                    con.Close();
                }

                return department.Id;
            }

        }
    }
}
