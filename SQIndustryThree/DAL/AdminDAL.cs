using SQIndustryThree.DataManager;
using SQIndustryThree.Models;
using SQIndustryThree.Models.BillApproval;
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

        public List<BillSupplier> GetAllBillSuppliers()
        {
            List<BillSupplier> suppliers = new List<BillSupplier>();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);

                SqlDataReader dr = accessManager.GetSqlDataReader("sp_BillSupplierList");
                while (dr.Read())
                {
                    BillSupplier supplier = new BillSupplier();
                    supplier.SupplierID = (int)dr["SupplierID"];
                    supplier.Supplier = dr["Supplier"].ToString();
                    suppliers.Add(supplier);
                }
                return suppliers;
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

        public List<BillCurrency> GetAllBillCurrency()
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                //List<SqlParameter> aList = new List<SqlParameter>();
                //aList.Add(new SqlParameter("@userId", 0));
                List<BillCurrency> currencies = new List<BillCurrency>();
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_CurrencyList");
                while (dr.Read())
                {
                    BillCurrency currency = new BillCurrency();
                    currency.CurrencyID = (int)dr["CurrencyID"];
                    currency.CurrencyCode = dr["CurrencyCode"].ToString();
                    currency.Currency = dr["Currency"].ToString();
                    currencies.Add(currency);
                }
                return currencies;
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

        public List<BillUnit> GetAllBillUnits()
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@userId", 0));
                List<BillUnit> billUnits = new List<BillUnit>();
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_UnitList");
                while (dr.Read())
                {
                    BillUnit unit = new BillUnit();
                    unit.UnitID = (int)dr["UnitID"];
                    unit.UnitName = dr["UnitName"].ToString();
                    billUnits.Add(unit);
                }
                return billUnits;
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


        public List<BillSupplier> SupplierSorting(string order, string orderDir, List<BillSupplier> data)
        {
            // Initialization.
            List<BillSupplier> lst = new List<BillSupplier>();

            try
            {
                // Sorting
                switch (order)
                {
                    case "0":
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.SupplierID).ToList()
                                                                                                 : data.OrderBy(p => p.SupplierID).ToList();
                        break;
                    case "1":
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Supplier).ToList()
                                                                                                 : data.OrderBy(p => p.Supplier).ToList();
                        break;

                    default:
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.SupplierID).ToList()
                                                                                                 : data.OrderBy(p => p.SupplierID).ToList();
                        break;
                }
            }
            catch (Exception ex)
            {
                // info.
                Console.Write(ex);
            }

            // info.
            return lst;
        }

        public List<BillCurrency> CurrencySorting(string order, string orderDir, List<BillCurrency> data)
        {
            // Initialization.
            List<BillCurrency> lst = new List<BillCurrency>();

            try
            {
                // Sorting
                switch (order)
                {
                    case "0":
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.CurrencyID).ToList()
                                                                                                 : data.OrderBy(p => p.CurrencyID).ToList();
                        break;
                    case "1":
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.CurrencyCode).ToList()
                                                                                                 : data.OrderBy(p => p.CurrencyCode).ToList();
                        break;

                    case "2":
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Currency).ToList()
                                                                                                 : data.OrderBy(p => p.Currency).ToList();
                        break;

                    default:
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.CurrencyID).ToList()
                                                                                                 : data.OrderBy(p => p.CurrencyID).ToList();
                        break;
                }
            }
            catch (Exception ex)
            {
                // info.
                Console.Write(ex);
            }

            // info.
            return lst;
        }

        public List<BillUnit> UnitSorting(string order, string orderDir, List<BillUnit> data)
        {
            // Initialization.
            List<BillUnit> lst = new List<BillUnit>();

            try
            {
                // Sorting
                switch (order)
                {
                    case "0":
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.UnitID).ToList()
                                                                                                 : data.OrderBy(p => p.UnitID).ToList();
                        break;
                    case "1":
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.UnitName).ToList()
                                                                                                 : data.OrderBy(p => p.UnitName).ToList();
                        break;

                    default:
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.UnitID).ToList()
                                                                                                 : data.OrderBy(p => p.UnitID).ToList();
                        break;
                }
            }
            catch (Exception ex)
            {
                // info.
                Console.Write(ex);
            }

            // info.
            return lst;
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