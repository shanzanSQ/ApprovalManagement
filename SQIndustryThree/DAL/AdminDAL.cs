using SQIndustryThree.DataManager;
using SQIndustryThree.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace SQIndustryThree.DAL
{
    public class AdminDAL
    {
        private DataAccessManager accessManager = new DataAccessManager();
        public UserInformation AdminUserLogin(string useremail,string password)
        {
            UserInformation user = new UserInformation();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@UserEmail", useremail));
                aParameters.Add(new SqlParameter("@Userpassword", password));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_AdminUserLogin", aParameters);
                while (dr.Read())
                {
                    user.UserInformationId = (int)dr["AdminId"];
                    user.UserInformationName = dr["AdminName"].ToString();
                    user.UserInformationEmail = dr["AdminMail"].ToString();
                }
                return user;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }
        public List<CapexInformationMaster> GetALLCapexInfo(int bunit,int catid,string startdate,string enddate)
        {
            List<CapexInformationMaster> capexInformationMaster = new List<CapexInformationMaster>();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@businessId", bunit));
                aList.Add(new SqlParameter("@catagoryId", catid));
                aList.Add(new SqlParameter("@startDate", startdate));
                aList.Add(new SqlParameter("@enddate", enddate));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetCatagorizedCapexInformation", aList);
                while (dr.Read())
                {
                    CapexInformationMaster capexInformation = new CapexInformationMaster();
                    capexInformation.CapexInfoId = (int)dr["CapexInfoId"];
                    capexInformation.CapexName = dr["CapexName"].ToString();
                    capexInformation.CapexCreateDate = (DateTime)dr["UpdateDate"];
                    capexInformation.BusinessUnitName = dr["BusinessUnitName"].ToString();
                    capexInformation.CapexCatagoryName = dr["CapexCatagoryName"].ToString();
                    capexInformation.UserName = dr["UserName"].ToString();
                    capexInformation.Cost = (double)dr["TotalCost"];
                    capexInformationMaster.Add(capexInformation);
                }
                return capexInformationMaster;
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
        //get business unit
        public List<BusinessUnit> GetBusinessUnits()
        {

            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<BusinessUnit> businessUnits = new List<BusinessUnit>();
                List<SqlParameter> aList = new List<SqlParameter>();
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetAllBusinessUnit");
                while (dr.Read())
                {
                        BusinessUnit businessUnit = new BusinessUnit();
                        businessUnit.BusinessUnitId = (int)dr["BusinessUnitId"];
                        businessUnit.BusinessUnitName = dr["BusinessUnitName"].ToString();
                        businessUnits.Add(businessUnit);
                }
                return businessUnits;
            }
            catch (Exception exception)
            {

                throw exception;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }
        public List<UserInformation> GetAllDesignation()
        {

            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<UserInformation> designation = new List<UserInformation>();
                List<SqlParameter> aList = new List<SqlParameter>();
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetAllDesignation");
                while (dr.Read())
                {
                    UserInformation userDesignation = new UserInformation();
                    userDesignation.DesignationId = (int)dr["DesignationId"];
                    userDesignation.DesignationName = dr["DesignationName"].ToString();
                    designation.Add(userDesignation);
                }
                return designation;
            }
            catch (Exception exception)
            {

                throw exception;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }
        public List<UserInformation> GetAllUsers()
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<UserInformation> userInformation = new List<UserInformation>();
                List<SqlParameter> aList = new List<SqlParameter>();
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetALlUsers");
                while (dr.Read())
                {
                    UserInformation user = new UserInformation();
                    user.UserInformationId =(int) dr["UserId"];
                    user.UserInformationName = dr["UserName"].ToString();
                    user.UserSQNumber = dr["SqIDNumber"].ToString();
                    userInformation.Add(user);
                }
                return userInformation;
            }
            catch (Exception exception)
            {

                throw exception;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }
        public List<UserInformation> ShowApproverListByBU(int buid,int catid)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<UserInformation> userInformation = new List<UserInformation>();
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@businessUnitId", buid));
                aList.Add(new SqlParameter("@catagoryId",catid));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_ApproverListAdmin",aList);
                while (dr.Read())
                {
                    UserInformation user = new UserInformation();
                    user.UserInformationId = (int)dr["UserId"];
                    user.UserInformationName = dr["UserName"].ToString();
                    user.DesignationName = dr["DesignationName"].ToString();
                    user.UserTypeId =(int) dr["IsActive"];
                    user.ApproverNo = (int)dr["ApproverNo"];
                    userInformation.Add(user);
                }
                return userInformation;
            }
            catch (Exception exception)
            {

                throw exception;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }
        public bool SaveApproverList(ApproverModelClass approverModelClass)
        {
            try
            {
                bool result = false;
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                int counter = 1;
                foreach (UserInformation item in approverModelClass.UserInformationList)
                {
                    List<SqlParameter> aList = new List<SqlParameter>();
                    aList.Add(new SqlParameter("@userId", item.UserInformationId));
                    aList.Add(new SqlParameter("@businessUnitId", approverModelClass.BusinessUnitId));
                    aList.Add(new SqlParameter("@catagoryId", approverModelClass.CatagoryId));
                    aList.Add(new SqlParameter("@approverNo",counter));
                    aList.Add(new SqlParameter("@IsActive", approverModelClass.Status));
                    result= accessManager.SaveData("sp_saveApproverToList", aList);
                    result = true;
                    counter++;
                }
                return result;
            }
            catch (Exception exception)
            {

                throw exception;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }
        public bool RecoveryPassword(string semail)
        {
            String password = "", name = "",email="";
            bool success = false;
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@userEmail", semail));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_ForgotPassowrd", aParameters);
                while (dr.Read())
                {
                    password = dr["UserPassword"].ToString();
                    name = dr["UserName"].ToString();
                    email = dr["UserEmail"].ToString();
                }
                if (name != "" && email != "")
                {
                    try
                    {
                        MailMessage message = new MailMessage();
                        SmtpClient smtp = new SmtpClient();
                        message.From = new MailAddress("noreply.mail1@sqgc.com");
                        message.To.Add(new MailAddress(email));
                        message.Subject = "AMS Password Recovery";
                        message.IsBodyHtml = true; //to make message body as html  
                        message.Body = "Dear Mr." + name + "<br/> You requested for Recover your password <br/> Your Password for the Approval management system is : " + PasswordManager.Decrypt(password) + " <br/>" +
                            "Thank you For Being with Us <br/>" +
                            "<br/>Thank You<br/> <a href='http://10.12.13.163:8080/'>Approval Management System</a><br/><br/>sqgc.com";
                        smtp.Port = 587;
                        smtp.Host = "smtp.office365.com"; //for gmail host  
                        smtp.EnableSsl = true;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential("noreply.mail1@sqgc.com", "ysd9kE6&195{rcU");
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.Send(message);
                        success = true;

                    }
                    catch (Exception e) {
                        throw e;
                    }
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
    }
}