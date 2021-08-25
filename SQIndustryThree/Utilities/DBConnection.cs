using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SQIndustryThree.Utilities
{
    public class DBConnection
    {
        public static string _connectionString;

        public static string GetConnectionString()
        {


            if (string.IsNullOrEmpty(_connectionString))
            {
                _connectionString = ConfigurationManager.ConnectionStrings["SQQEYEDatabase"].ConnectionString;
            }

            return _connectionString;
        }
    }
}