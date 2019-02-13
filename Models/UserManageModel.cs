using System;
using System.Collections.Generic;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace FetchtoApp.Models
{
    public class UserManageModel
    {
        private string strConnectionString = ConfigurationManager.ConnectionStrings["fetchtoconnection"].ConnectionString;

        public List<UserInfo> fnUserModel(String username = "", String password = "", String email = "", String phoneno = "", String action = "Select", String status = "Active", String profilepic = "avatar.pic")
        {
            UserInfo user = null;
            List<UserInfo> userlist = new List<UserInfo>();
            using (SqlConnection sqlConn = new SqlConnection(strConnectionString))
            {
                try
                {
                    sqlConn.Open();
                    SqlCommand cmd = new SqlCommand("[loginModification]", sqlConn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@email", SqlDbType.NVarChar, 80).Value = email;
                    cmd.Parameters.Add("@phone", SqlDbType.NVarChar, 15).Value = phoneno;
                    cmd.Parameters.Add("@username", SqlDbType.NVarChar, 80).Value = username;
                    cmd.Parameters.Add("@password", SqlDbType.NVarChar, 100).Value = password;
                    cmd.Parameters.Add("@action", SqlDbType.NVarChar, 100).Value = action;
                    cmd.Parameters.Add("@status", SqlDbType.NVarChar, 50).Value = status;
                    cmd.Parameters.Add("@profilepic", SqlDbType.VarChar, 4000).Value = profilepic;
                    SqlDataReader sqlDataReader = cmd.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        user = new UserInfo();
                        user.userid = sqlDataReader.GetValue(0).ToString();
                        user.username = sqlDataReader.GetValue(1).ToString();
                        user.password = sqlDataReader.GetValue(2).ToString();
                        user.phoneno = sqlDataReader.GetValue(3).ToString();
                        user.emailid = sqlDataReader.GetValue(4).ToString();
                        user.status = sqlDataReader.GetValue(5).ToString();
                        user.profilepic = sqlDataReader.GetValue(6).ToString();
                        user.role = sqlDataReader.GetValue(7).ToString();
                        user.Companyname = sqlDataReader.GetValue(8).ToString();
                        user.GST = sqlDataReader.GetValue(9).ToString();
                        user.BankAccountname = sqlDataReader.GetValue(10).ToString();
                        user.BankAccountno = sqlDataReader.GetValue(11).ToString();
                        user.IFSC = sqlDataReader.GetValue(12).ToString();
                        user.Branch = sqlDataReader.GetValue(13).ToString();
                        user.Logo = sqlDataReader.GetValue(14).ToString();
                        user.Warehousename = sqlDataReader.GetValue(15).ToString();
                        user.Address = sqlDataReader.GetValue(16).ToString();
                        user.Landmark = sqlDataReader.GetValue(17).ToString();
                        user.Pincode = sqlDataReader.GetValue(18).ToString();
                        user.City = sqlDataReader.GetValue(19).ToString();
                        user.State = sqlDataReader.GetValue(20).ToString();
                        user.Contactno = sqlDataReader.GetValue(21).ToString();
                        userlist.Add(user);
                    }
                    sqlConn.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return userlist;
        }
    }
    public class UserInfo
    {
        public string userid;
        public string emailid;
        public string phoneno;
        public string username;
        public string password;
        public string status;
        public string role;
        public string profilepic;
        public string Companyname;
        public string GST;
        public string BankAccountname;
        public string BankAccountno;
        public string IFSC;
        public string Branch;
        public string Logo;
        public string Warehousename;
        public string Address;
        public string Landmark;
        public string Pincode;
        public string City;
        public string State;
        public string Contactno;
    }
}