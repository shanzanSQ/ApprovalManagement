using DocSoOperation.Models;
using SQIndustryThree.DataManager;
using SQIndustryThree.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;

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
                aParameters.Add(new SqlParameter("@createBY", 1));
                aParameters.Add(new SqlParameter("@SqIdNumber", users.UserSQNumber));
                aParameters.Add(new SqlParameter("@DesignationID", (int)users.DesignationId));
                aParameters.Add(new SqlParameter("@BusinessUnitId",(int) users.BusinessUnitId));
                result = accessManager.SaveData("sp_SaveUserInformation", aParameters);
                return result;
            }
            catch (Exception e)
            {
                accessManager.SqlConnectionClose(true);
                throw e;
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
                    user.IsSupplier = Convert.ToInt32(dr["IsSupplier"]);
                }

                return user;
            }
            catch (Exception e)
            {
                accessManager.SqlConnectionClose(true);
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }


        public UserInformation GetUserInformation(int userId)
        {
            UserInformation user = new UserInformation();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@userId", userId));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetUserInformation", aParameters);
                while (dr.Read())
                {
                    user.UserInformationName = dr["UserName"].ToString();
                    user.UserInformationEmail = dr["UserEmail"].ToString();
                    user.UserInformationPhoneNumber = dr["UserPhone"].ToString();
                    user.UserSQNumber = dr["SqIDNumber"].ToString();
                    user.DesignationName = dr["DesignationName"].ToString();
                }

                return user;
            }
            catch (Exception e)
            {
                accessManager.SqlConnectionClose(true);
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }
        public List<ModuleModel> GetModuleByUser(int userId,int ProjectId)
        {
            List<ModuleModel> moduleList = new List<ModuleModel>();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@userId", userId));
                aParameters.Add(new SqlParameter("@prjectId", ProjectId));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetModulePermissionUser", aParameters);
                while (dr.Read())
                {
                    ModuleModel moduleModel = new ModuleModel();
                    moduleModel.ModuleKey =(int) dr["Modulekey"];
                    moduleModel.ModuleName =dr["ModuleName"].ToString();
                    moduleModel.ModuleValue =dr["ModuleValue"].ToString();
                    moduleModel.ModuleController =dr["ModuleController"].ToString();
                    moduleList.Add(moduleModel);
                }
                return moduleList;
            }
            catch (Exception e)
            {
                accessManager.SqlConnectionClose(true);
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

        public List<ModuleModel> GetProjectmenu(int userId)
        {
            List<ModuleModel> moduleList = new List<ModuleModel>();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@UserId", userId));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_ProjectPermission", aParameters);
                while (dr.Read())
                {
                    ModuleModel moduleModel = new ModuleModel();
                    moduleModel.ModuleKey = (int)dr["ProjectId"];
                    moduleModel.ModuleName = dr["ProjectName"].ToString();
                    moduleList.Add(moduleModel);
                }
                return moduleList;
            }
            catch (Exception e)
            {
                accessManager.SqlConnectionClose(true);
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }
        public bool changePassword(int userId,string newpass)
        {
            bool success = false;
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@userId", userId));
                aParameters.Add(new SqlParameter("@newPass", PasswordManager.Encrypt(newpass)));
                success = accessManager.UpdateData("sp_changePassword", aParameters);
                return success;
            }
            catch (Exception e)
            {
                accessManager.SqlConnectionClose(true);
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

        public bool RecoveryPassword(int userId)
        {
            String password = "",name="",email="";
            bool success = false;
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@userId", userId));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_PasswordRecovery", aParameters);
                while (dr.Read())
                {
                    password = dr["UserPassword"].ToString();
                    name = dr["UserName"].ToString();
                    email= dr["UserEmail"].ToString();
                }
                if(name!="" && email != "")
                {
                    try
                    {
                        MailMessage message = new MailMessage();
                        SmtpClient smtp = new SmtpClient();
                        message.From = new MailAddress("noreply@sqgc.com");
                        message.To.Add(new MailAddress(email));
                        message.Subject = "AMS Password Recovery";
                        message.IsBodyHtml = true; //to make message body as html  
                        message.Body = "Dear Mr."+name+ "<br/> You requested for Recover your password <br/> Your Password for the Approval management system is : " + PasswordManager.Decrypt(password)+ " <br/>" +
                            "Thank you For Being with Us <br/>" +
                             "<br/>Thank You<br/> <a href='http://10.12.13.163:8080/'>Approval Management System</a><br/><br/>sqgc.com";
                        smtp.Port = 587;
                        smtp.Host = "smtp.office365.com"; //for gmail host  
                        smtp.EnableSsl = true;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential("noreply@sqgc.com", "ysd9kE6&195{rcU");
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.Send(message);
                        success = true;

                    }
                    catch (Exception) { }
                }
                return success;
            }
            catch (Exception e)
            {
                accessManager.SqlConnectionClose(true);
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }
        public bool CreateDesignation(string designationName)
        {
            bool success = false;
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@desinationName", designationName));
                success = accessManager.SaveData("sp_CreateDesignation", aParameters);
                return success;
            }
            catch (Exception e)
            {
                accessManager.SqlConnectionClose(true);
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }


    }
}