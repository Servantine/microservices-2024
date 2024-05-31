using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using WalletServices.DAL.interfaces;
using WalletServices.Models;

namespace WalletServices.DAL
{
    public class WalletDAL : IWallet
    {
        private string GetConnectionString()
        {
            return "Data Source=.\\SQLEXPRESS; Initial Catalog=walletservice;Integrated Security=true;TrustServerCertificate=true;";
        }
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Wallet> GetAll()
        {
            List<Wallet> wallets = new List<Wallet>();
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                var strSql = @"SELECT * FROM Wallets";
                SqlCommand cmd = new SqlCommand(strSql, conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Wallet wallet = new Wallet();
                        wallet.WalletId = Convert.ToInt32(dr["WalletId"]);
                        wallet.CustomerId = Convert.ToInt32(dr["CustomerId"]);
                        wallet.Saldo = Convert.ToInt32(dr["Saldo"]);
                        Wallet.Add(wallet);
                    }
                }
                dr.Close();
                cmd.Dispose();
                conn.Close();

                return wallets;
            }
        }

        public Wallet GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Wallet> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public void Insert(Wallet obj)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                var strSql = @"INSERT INTO Wallets (WalletId, CustomerId, Saldo) VALUES (@WalletId, @CustomerId, @Saldo);";
                SqlCommand cmd = new SqlCommand(strSql, conn);
                cmd.Parameters.AddWithValue("@WalletId", obj.WalletId);
                cmd.Parameters.AddWithValue("@CustomerId", obj.CustomerId);
                cmd.Parameters.AddWithValue("@Saldo", obj.Saldo);

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

        public void Update(Wallet obj)
        {
            throw new NotImplementedException();
        }

        List<Wallet> IWallet.GetAll()
        {
            List<Wallet> wallets = new List<Wallet>();
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                var strSql = @"SELECT * FROM Wallets";
                SqlCommand cmd = new SqlCommand(strSql, conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Wallet wallet = new Wallet();
                        wallet.WalletId = Convert.ToInt32(dr["WalletId"]);
                        wallet.CustomerId = Convert.ToInt32(dr["CustomerId"]);
                        wallet.Saldo = Convert.ToInt32(dr["Saldo"]);
                        Wallet.Add(wallet);
                    }
                }
                dr.Close();
                cmd.Dispose();
                conn.Close();

                return wallets;
            }
        }
    }
}