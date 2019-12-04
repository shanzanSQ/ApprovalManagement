using SQIndustryThree.DataManager;
using SQIndustryThree.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SQIndustryThree.DAL
{
    public class DashboardDAL
    {
        private DataAccessManager accessManager = new DataAccessManager();
        public List<ChartModel> GetApproveStatus(int userId,int year,int catagory)
        {

            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<ChartModel> coststatus = new List<ChartModel>();
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@userId", userId));
                aList.Add(new SqlParameter("@year", year));
                aList.Add(new SqlParameter("@catagory", catagory));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_getAllApproverStatus", aList);
                while (dr.Read())
                {
                    ChartModel cmast = new ChartModel();
                    cmast.Cost = (int)dr["NumCount"];
                    int month= (int)dr["month"];
                    switch (month)
                    {
                        case 1:
                            cmast.Month = "January";
                            break;
                        case 2:
                            cmast.Month = "February";
                            break;
                        case 3:
                            cmast.Month = "March";
                            break;
                        case 4:
                            cmast.Month = "April";
                            break;
                        case 5:
                            cmast.Month = "May";
                            break;
                        case 6:
                            cmast.Month = "June";
                            break;
                        case 7:
                            cmast.Month = "July";
                            break;
                        case 8:
                            cmast.Month = "August";
                            break;
                        case 9:
                            cmast.Month = "September";
                            break;
                        case 10:
                            cmast.Month = "October";
                            break;
                        case 11:
                            cmast.Month = "November";
                            break;
                        case 12:
                            cmast.Month = "December";
                            break;
                    }
                    coststatus.Add(cmast);

                }
                return coststatus;
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