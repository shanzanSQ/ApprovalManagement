using Dapper;
using SQIndustryThree.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SQIndustryThree.DAL
{
    public class CameraDAL
    {
        private string connStr = ConfigurationManager.ConnectionStrings["SQQEYEDatabase"].ConnectionString;

        public int ImageInsert(ImageStore image)
        {
            int imageStore = 0;
            try
            {
                using (IDbConnection cnn = (IDbConnection)new SqlConnection(this.connStr))
                {
                    if (cnn.State == ConnectionState.Closed)
                        cnn.Open();
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@RowId", image.RowId);
                    dynamicParameters.Add("@ImageBase64String", image.ImageBase64String);
                    dynamicParameters.Add("@CreateDate", image.CreateDate);
                    dynamicParameters.Add("@ImageName", image.ImageName);
                    dynamicParameters.Add("@ImagePath", image.ImagePath);
                    imageStore = cnn.Execute("sp_CreateImageStore", (object)dynamicParameters, commandType: new CommandType?(CommandType.StoredProcedure));
                }
                return imageStore;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}