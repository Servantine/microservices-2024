using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using OrderServices.DAL.Interfaces;
using OrderServices.Models;

namespace OrderServices.DAL
{
    public class OrderHeaderDAL : IOrderHeader
    {
        private string GetConnectionString()
        {
            return "Data Source=.\\SQLEXPRESS; Initial Catalog=OrderDB;Integrated Security=true;TrustServerCertificate=false;";
        }
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderHeader> GetAll()
        {
            List<OrderHeader> OrderHeader = new List<OrderHeader>();
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                var strSql = @"SELECT * FROM OrderHeaders order by OrderHeaderId";
                SqlCommand cmd = new SqlCommand(strSql, conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        OrderHeader orderHeader = new OrderHeader();
                        orderHeader.OrderHeaderId = Convert.ToInt32(dr["OrderHeaderId"]);
                        orderHeader.CustomerId = Convert.ToInt32(dr["CustomerID"]);
                        OrderHeader.Add(orderHeader);
                    }
                }
                dr.Close();
                cmd.Dispose();
                conn.Close();

                return OrderHeader;
            }
        }

        public OrderHeader GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderHeader> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public void Insert(OrderHeader obj)
        {
           using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                var strSql = @"INSERT INTO OrderHeader (CustomerId, OrderHeaderId) VALUES (@CustomerId, @OrderHeaderId)";
                SqlCommand cmd = new SqlCommand(strSql, conn);
                cmd.Parameters.AddWithValue("@CustomerId", obj.CustomerId);
                cmd.Parameters.AddWithValue("@OrderHeaderId", obj.OrderHeaderId);

                try
                {
                    conn.Open();
                    var result = cmd.ExecuteNonQuery();
                    if (result != 1)
                    {
                        throw new ArgumentException("Data gagal disimpan");
                    }
                }
                catch (SqlException sqlEx)
                {
                    throw new ArgumentException($"Error: {sqlEx.Message} - {sqlEx.Number}");
                }
                catch (Exception ex)
                {
                    throw new ArgumentException($"Error: {ex.Message}");
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();
                }
            }
        }

        public void Update(OrderHeader obj)
        {
            throw new NotImplementedException();
        }
    }
}