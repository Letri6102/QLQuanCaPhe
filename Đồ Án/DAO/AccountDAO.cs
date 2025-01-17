﻿using Đồ_Án.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Đồ_Án.DAO
{
    public class AccountDAO
    {
        private static AccountDAO Instance;

        public static AccountDAO Instance1 {
            get { if (Instance == null) Instance = new AccountDAO(); return Instance; }
            private set { Instance = value; } 
        }
        private AccountDAO() { }
        public bool Login(string userName,string passWord)
        {
            
            string query = "USP_Login @userName , @passWord";
            DataTable result = DataProvider.Instance.ExecuteQuery(query,new object[] {userName, passWord});
            return result.Rows.Count > 0;
        }

        public bool UpdateAccount(string userName,string displayName,string pass,string newPass)
        {
            int result = DataProvider.Instance.ExecuteNonQuery("exec USP_UpdateAccount @userName , @displayName , @password , @newpassword ",new object[] {userName,displayName,pass,newPass});

            return result > 0;
        }

        public DataTable GetListAccount()
        {
            return DataProvider.Instance.ExecuteQuery("Select Username, DisplayName, Type from account");
        }
        public Account GetAccountByUserName(string userName)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("Select * from account where userName ='" + userName+"'");
            foreach (DataRow item in data.Rows)
            {
                return new Account(item);
            }
            return null;
        }

        public bool InsertAccount(string name, string displayName,int Type)
        {
            string query = string.Format("Insert dbo.Account(UserName,DisplayName,Type) Values (N'{0}',N'{1}',{2})", name, displayName, Type);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool UpdateAccount(string name, string displayName, int Type)
        {
            string query = string.Format("update dbo.Account set DisplayName = N'{1}',Type = {2} where UserName = N'{0}'", name, displayName, Type);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool DeleteAccount(string name)
        {
            string query = string.Format("Delete Account where UserName = N'{0}'", name);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool ResetPassword(string name)
        {
            string query = string.Format("update account set password = N'0' where UserName = N'{0}'", name);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
    }
}
