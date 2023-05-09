using Microsoft.Extensions.Configuration;
using SalesWebMvc.Interfaces;
using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Runtime.Serialization;

namespace SalesWebMvc.Services
{
    public class SellerServiceAdo : ISellerService
    {

        private readonly IConfiguration _configuration;
        /*private readonly string _connection;  propriedade global com apenas a conection*/

        public SellerServiceAdo(IConfiguration configuration)
        {
            _configuration = configuration;
            /*_connection = _configuration["AdoSqlConn"];*/
        }



        public List<Seller> GetSellers()
        {
            List<Seller> sellerList = new List<Seller>();

            var stringConnection = _configuration["AdoSqlConn"];
            var query = "SELECT Id, Name, Email, BirthDate, BaseSalary  FROM tb_Seller";

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
                            sellerList.Add(new Seller()
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = Convert.ToString(reader["Name"]),
                                Email = Convert.ToString(reader["Email"]),
                                BirthDate = Convert.ToDateTime(reader["BirthDate"]),
                                BaseSalary = Convert.ToDouble(reader["BaseSalary"])

                            });
                        }
                    }
                    conn.Close();
                }
            }

            if (sellerList.Count == 0)
            {
                return new List<Seller>() { new Seller() };
            }

            return sellerList;
        }



        public int CreateSeller(Seller seller)
        {
            throw new System.NotImplementedException();
        }

    }
}
