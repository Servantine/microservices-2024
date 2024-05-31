using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using OrderServices.DAL.interfaces;
using OrderServices.Models;
namespace OrderServices.DAL
{
    public class CustomerDAL : ICustomer
    {
        private string GetConnectionString()
        {
            return "Data Source=.\\SQLEXPRESS; Initial Catalog=OrderDB;Integrated Security=true;TrustServerCertificate=false;";
        }

        public IEnumerable<Customer> GetAll()
        {
            List<Customer> Customer = new List<Customer>();
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                var strSql = @"SELECT * FROM Customers order by CustomerName";
                SqlCommand cmd = new SqlCommand(strSql, conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Customer customer = new Customer();
                        customer.CustomerId = Convert.ToInt32(dr["CustomerID"]);
                        customer.CustomerName = dr["CustomerName"].ToString();
                        Customer.Add(customer);
                    }
                }
                dr.Close();
                cmd.Dispose();
                conn.Close();

                return Customer;
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
        public Customer GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public void Insert(Customer obj)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                var strSql = @"INSERT INTO Customers (CustomerId, CustomerName) VALUES (@CustomerId, @CustomerName)";
                SqlCommand cmd = new SqlCommand(strSql, conn);
                cmd.Parameters.AddWithValue("@CustomerId", obj.CustomerId);
                cmd.Parameters.AddWithValue("@CustomerName", obj.CustomerName);

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

        public void Update(Customer obj)
        {
            throw new NotImplementedException();
        }
    }
}