using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using OrderServices.DAL.interfaces;
using OrderServices.Models;

namespace OrderServices.DAL
{
    public class OrderDetailDAL : IOrderDetail
    {
        private string GetConnectionString()
        {
            return "Data Source=.\\SQLEXPRESS; Initial Catalog=OrderDB;Integrated Security=true;TrustServerCertificate=false;";
        }
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderDetail> GetAll()
        {
            List<OrderDetail> OrderDetail = new List<OrderDetail>();
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                var strSql = @"SELECT * FROM OrderDetail order by OrderDetailId";
                SqlCommand cmd = new SqlCommand(strSql, conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        OrderDetail orderDetail = new OrderDetail();
                        orderDetail.OrderHeaderId = Convert.ToInt32(dr["OrderHeaderId"]);
                        orderDetail.ProductId = Convert.ToInt32(dr["ProductId"]);
                        orderDetail.Quantity = Convert.ToInt32(dr["Quantity"]);
                        OrderDetail.Add(orderDetail);
                    }
                }
                dr.Close();
                cmd.Dispose();
                conn.Close();

                return OrderDetail;
            }
        }

        public OrderDetail GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderDetail> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public void Insert(OrderDetail obj)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                var strSql = @"INSERT INTO OrderDetails (OrderHeaderId, ProductId, Quantity, Price) VALUES (@OrderHeaderId, @ProductId, @Quantity, @Price)";
                SqlCommand cmd = new SqlCommand(strSql, conn);
                cmd.Parameters.AddWithValue("@OrderHeaderId", obj.OrderHeaderId);
                cmd.Parameters.AddWithValue("@ProductId", obj.ProductId);
                cmd.Parameters.AddWithValue("@Quantity", obj.Quantity);
                cmd.Parameters.AddWithValue("@Price", obj.Price);

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

        public void Update(OrderDetail obj)
        {
            throw new NotImplementedException();
        }
    }
}