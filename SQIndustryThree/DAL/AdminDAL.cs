﻿using SQIndustryThree.DataManager;
using SQIndustryThree.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
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
                accessManager.SqlConnectionClose(true);
                throw;
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
                    capexInformation.CapexCreateDate = (DateTime)dr["CapexCreateDate"];
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

    }
}