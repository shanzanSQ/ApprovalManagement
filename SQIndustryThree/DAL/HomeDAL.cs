using DocSoOperation.Models;
using SQIndustryThree.DataManager;
using SQIndustryThree.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SQIndustryThree.DAL
{
    public class HomeDAL
    {
        private DataAccessManager accessManager = new DataAccessManager();

        public bool SAveUsersToDataBase(UserInformation users)
        {
            bool result = true;
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@userName", users.UserInformationName));
                aParameters.Add(new SqlParameter("@userEmail", users.UserInformationEmail));
                aParameters.Add(new SqlParameter("@userPassword",PasswordManager.Encrypt(users.UserInformationPassword)));
                aParameters.Add(new SqlParameter("@userPhoneNumber",users.UserInformationPhoneNumber));
                aParameters.Add(new SqlParameter("@userType", (int)users.UserTypeId));
                aParameters.Add(new SqlParameter("@createBY", (int)users.CreateBY));
                aParameters.Add(new SqlParameter("@SqIdNumber", users.UserSQNumber));
                aParameters.Add(new SqlParameter("@DesignationID", (int)users.DesignationId));
                aParameters.Add(new SqlParameter("@BusinessUnitId",(int) users.BusinessUnitId));

                result = accessManager.SaveData("sp_SaveUserInformation", aParameters);
               
                return result;
            }
            catch (Exception e)
            {
                accessManager.SqlConnectionClose(true);
                throw;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }


        public UserInformation CheckUserLogin(string UserEmail, string UserPassword)
        {
            UserInformation user = new UserInformation();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@userName", UserEmail));
                aParameters.Add(new SqlParameter("@userPassword", PasswordManager.Encrypt(UserPassword)));
                SqlDataReader dr= accessManager.GetSqlDataReader("sp_CheckUserLogin", aParameters);
                while (dr.Read())
                {
                    user.UserInformationId = (int)dr["UserId"];
                    user.UserInformationName = dr["UserName"].ToString();
                    user.UserInformationEmail = dr["UserEmail"].ToString();
                    user.DesignationId = (int)dr["DesignationID"];
                    //user.UserInformationPhoneNumber = (int)dr["UserPhone"];
                }

                return user;
            }
            catch (Exception e)
            {
                accessManager.SqlConnectionClose(true);
                throw;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

    }
}