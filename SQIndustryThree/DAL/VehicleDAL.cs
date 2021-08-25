using SQIndustryThree.DataManager;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using SQIndustryThree.Models;
using System.Configuration;
using System.Web.Script.Serialization;
using Dapper;
using System.Data;
using SQIndustryThree.Models.VisitorApproval;
using Newtonsoft.Json;
using DocSoOperation.Models;

namespace SQIndustryThree.DAL
{
    public class VehicleDAL
    {
        private DataAccessManager accessManager = new DataAccessManager();
        private string connStr = ConfigurationManager.ConnectionStrings["SQQEYEDatabase"].ConnectionString;
        public List<CourierApproverModel> GetApproverCategoryBased(

  int unit,
  int DepartmentHeadId
)
        {
            List<CourierApproverModel> visitorApproverList = new List<CourierApproverModel>();
            try
            {
                using (IDbConnection cnn = (IDbConnection)new SqlConnection(this.connStr))
                {
                    if (cnn.State == ConnectionState.Closed)
                        cnn.Open();
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@UnitId", (object)unit);
                    dynamicParameters.Add("@DepartmentHeadId", (object)DepartmentHeadId);
                    visitorApproverList = cnn.Query<CourierApproverModel>("sp_VehicleRoleWiseApproverList", (object)dynamicParameters, commandType: new CommandType?(CommandType.StoredProcedure)).ToList<CourierApproverModel>();
                }
                return visitorApproverList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CourierApproverModel> GetApproverUnitBased(

int unit

)
        {
            List<CourierApproverModel> visitorApproverList = new List<CourierApproverModel>();
            try
            {
                using (IDbConnection cnn = (IDbConnection)new SqlConnection(this.connStr))
                {
                    if (cnn.State == ConnectionState.Closed)
                        cnn.Open();
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@UnitId", (object)unit);
                    //dynamicParameters.Add("@DepartmentHeadId", (object)DepartmentHeadId);
                    visitorApproverList = cnn.Query<CourierApproverModel>("sp_VehicleUnitRoleWiseApproverList", (object)dynamicParameters, commandType: new CommandType?(CommandType.StoredProcedure)).ToList<CourierApproverModel>();
                }
                return visitorApproverList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CourierApproverModel> GetApproverDeoartment_headBased(

int DepartmentHeadId

)
        {
            List<CourierApproverModel> visitorApproverList = new List<CourierApproverModel>();
            try
            {
                using (IDbConnection cnn = (IDbConnection)new SqlConnection(this.connStr))
                {
                    if (cnn.State == ConnectionState.Closed)
                        cnn.Open();
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    //dynamicParameters.Add("@UnitId", (object)unit);
                    dynamicParameters.Add("@DepartmentHeadId", (object)DepartmentHeadId);
                    visitorApproverList = cnn.Query<CourierApproverModel>("sp_VehicleDHWiseApproverList", (object)dynamicParameters, commandType: new CommandType?(CommandType.StoredProcedure)).ToList<CourierApproverModel>();
                }
                return visitorApproverList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ResultResponse SaveVehicleRequest(VehicleRequestModel visitor, int UserId)
        {
            try
            {
                using (IDbConnection cnn = (IDbConnection)new SqlConnection(this.connStr))
                {
                    if (cnn.State == ConnectionState.Closed)
                        cnn.Open();
                    string str1 = new JavaScriptSerializer().Serialize((object)visitor.UserDetailsList);
                    string str2 = new JavaScriptSerializer().Serialize((object)visitor.VehicleApproverList);
                    visitor.UserDetailsList = (List<VehicleRequestModel>)null;
                    int count = visitor.VehicleApproverList.Count;
                    visitor.VehicleApproverList = (List<CourierApproverModel>)null;
                    new JavaScriptSerializer().Serialize((object)visitor);
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@RequestorId", (object)UserId);
                    dynamicParameters.Add("@LocationId", (object)visitor.LocationId);
                    dynamicParameters.Add("@BusinessUnitId", (object)visitor.BusinessUnitId);
                    dynamicParameters.Add("@DepartmentHeadId", (object)visitor.DepartmentHeadId);
                    dynamicParameters.Add("@TravelStartDate", (object)visitor.TravelStratDate);
                    dynamicParameters.Add("@TravelEndtDate", (object)visitor.TravelEndDate);
                    dynamicParameters.Add("@StartTime", (object)visitor.StartTime);
                    dynamicParameters.Add("@EndTime", (object)visitor.EndTime);
                    dynamicParameters.Add("@StartingPoint", (object)visitor.StartingPoint);
                    dynamicParameters.Add("@RouteType", (object)visitor.RouteType);
                    dynamicParameters.Add("@PurposeofTravel", (object)visitor.PurposeofTravel);
                    dynamicParameters.Add("@TripType", (object)visitor.TripType);
                    dynamicParameters.Add("@PreferredVehicle", (object)visitor.PreferredVehicle);
                    dynamicParameters.Add("@NoofUser", (object)visitor.NoofUser);
                    dynamicParameters.Add("@NoofDays", (object)visitor.NoofDays);
                    dynamicParameters.Add("@Remarks", (object)visitor.Remarks);
                    dynamicParameters.Add("@ApproverJson", (object)str2);
                    dynamicParameters.Add("@UserJosn", (object)str1);
                    int num = cnn.Execute("SP_VehicleDataInsert", (object)dynamicParameters, commandType: new CommandType?(CommandType.StoredProcedure));
                    return new ResultResponse()
                    {
                        pk = num,
                        isSuccess = true
                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<VehicleRequestModel> GetAllVehicleRequest(int UserId, int Status, int Pgress)
        {
            List<VehicleRequestModel> users = new List<VehicleRequestModel>();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@UserId", UserId));
                aList.Add(new SqlParameter("@Status", Status));
                aList.Add(new SqlParameter("@Progress", Pgress));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_VehicleGetAllRequest", aList);
                while (dr.Read())
                {
                    VehicleRequestModel iOURequest = new VehicleRequestModel();
                    iOURequest.VehicleRequesMastertId = (int)dr["VehicleRequesMastertId"];
                    iOURequest.DateOfRequest = dr["DateOfRequest"].ToString();
                    iOURequest.RequestorId = (int)dr["RequestorId"];
                    iOURequest.TravelStratDate = dr["TravelStratDate"].ToString();
                    iOURequest.TravelEndDate = dr["TravelEndDate"].ToString();
                    iOURequest.StartTime = dr["StartTime"].ToString();
                    iOURequest.EndTime = dr["EndTime"].ToString();
                    iOURequest.StartingPoint = dr["StartingPoint"].ToString();
                    iOURequest.RouteType = dr["RouteType"].ToString();
                    iOURequest.PurposeofTravel = dr["PurposeofTravel"].ToString();
                    iOURequest.TripType = dr["TripType"].ToString();
                    iOURequest.PreferredVehicle = dr["PreferredVehicle"].ToString();
                    iOURequest.NoofUser = (int)dr["NoofUser"];
                    iOURequest.Remarks = dr["Remarks"].ToString();
                    iOURequest.Pending = (int)dr["Pending"];
                    iOURequest.IsApproved = (int)dr["IsApproved"];
                    iOURequest.UserName = dr["UserName"].ToString();
                    iOURequest.BusinessUnitName = dr["BusinessUnitName"].ToString();
                    iOURequest.UserName = dr["UserName"].ToString();
                    iOURequest.Status = Status;

                    users.Add(iOURequest);
                }
                return users;
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
        public VehicleRequestModel VehicleDetailsInformation(int masterId, int userId)
        {
            VehicleRequestModel vehicleRequestModel = new VehicleRequestModel();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@VehicleRequestMasterId", masterId));
                aList.Add(new SqlParameter("@UserId", userId));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_VehicleDetailsInfo", aList);
                while (dr.Read())
                {
                    vehicleRequestModel.VehicleRequesMastertId = (int)dr["VehicleRequesMastertId"];
                    vehicleRequestModel.DateOfRequest = dr["DateOfRequest"].ToString();
                    vehicleRequestModel.RequestorId = (int)dr["RequestorId"];
                    vehicleRequestModel.TravelStratDate = dr["TravelStratDate"].ToString();
                    vehicleRequestModel.TravelEndDate = dr["TravelEndDate"].ToString();
                    vehicleRequestModel.StartTime = dr["StartTime"].ToString();
                    vehicleRequestModel.EndTime = dr["EndTime"].ToString();
                    vehicleRequestModel.StartingPoint = dr["StartingPoint"].ToString();
                    vehicleRequestModel.StartingPointName = dr["StartingPointName"].ToString();
                    vehicleRequestModel.RouteType = dr["RouteType"].ToString();
                    vehicleRequestModel.RouteTypeName = dr["RouteTypeName"].ToString();
                    vehicleRequestModel.PurposeofTravel = dr["PurposeofTravel"].ToString();
                    vehicleRequestModel.PurposeofTravelName = dr["PurposeofTravelName"].ToString();
                    vehicleRequestModel.TripType = dr["TripType"].ToString();
                    vehicleRequestModel.TripTypeName = dr["TripTypeName"].ToString();
                    vehicleRequestModel.PreferredVehicle = dr["PreferredVehicle"].ToString();
                    vehicleRequestModel.PreferredVehicleName = dr["PreferredVehicleName"].ToString();
                    vehicleRequestModel.NoofUser = (int)dr["NoofUser"];
                    vehicleRequestModel.NoofDays = (int)dr["NoofUser"];
                    vehicleRequestModel.Remarks = dr["Remarks"].ToString();
                    //vehicleRequestModel.DeartmentName = dr["DeartmentName"].ToString();
                    //vehicleRequestModel.DesignationName = dr["DesignationName"].ToString();
                    //vehicleRequestModel.Pending = (int)dr["Pending"];
                    vehicleRequestModel.IsApproved = (int)dr["IsApproved"];
                    vehicleRequestModel.UserName = dr["UserName"].ToString();
                    vehicleRequestModel.BusinessUnitId = (int)dr["BusinessUnitId"];
                    vehicleRequestModel.DepartmentHeadId = (int)dr["DepartmentHeadId"];
                    vehicleRequestModel.BusinessUnitName = dr["BusinessUnitName"].ToString();
                    vehicleRequestModel.LocationId = (int)dr["LocationId"];
                    vehicleRequestModel.LocationName = dr["LocationName"].ToString();
                    vehicleRequestModel.RequestorName = dr["UserName"].ToString();
                    //vehicleRequestModel.Budget_MTD = (int)dr["Budget_MTD"];
                    //vehicleRequestModel.MTDcost = (int)dr["MTDcost"];
                    vehicleRequestModel.Budget_MTD = dr["Budget_MTD"].ToString();
                    vehicleRequestModel.MTDcost = dr["MTDcost"].ToString();
                    vehicleRequestModel.ReaminsCost = dr["ReaminsCost"].ToString();
                    vehicleRequestModel.Central_Budget_MTD = dr["Central_Budget_MTD"].ToString();
                    vehicleRequestModel.Central_MTDcost = dr["Central_MTDcost"].ToString();
                    vehicleRequestModel.Central_ReaminsCost = dr["Central_ReaminsCost"].ToString();
                    vehicleRequestModel.VehicleRate = dr["VehicleRate"].ToString();
                    vehicleRequestModel.AllocatedCost = dr["AllocatedCost"].ToString();
                    vehicleRequestModel.UserDetailsList = JsonConvert.DeserializeObject<List<VehicleRequestModel>>(dr["UserDetailsList"].ToString());
                    vehicleRequestModel.VehicleDeligationList = JsonConvert.DeserializeObject<List<VehicleRequestModel>>(dr["VehicleDeligationList"].ToString());
                    vehicleRequestModel.VehicleApproverList = JsonConvert.DeserializeObject<List<CourierApproverModel>>(dr["VehicleApproverList"].ToString());
                    vehicleRequestModel.VehicleComments = JsonConvert.DeserializeObject<List<CommentsTable>>(dr["VehicleComments"].ToString());
                    vehicleRequestModel.VehicleLogSection = JsonConvert.DeserializeObject<List<LogSection>>(dr["VehicleLogSection"].ToString());
                }
                return vehicleRequestModel;
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
        public bool VehicleApproveOrReject(int Progress, string CommentText, int UserID, int VehicleRequestId, int DeligationUserId)
        {
            bool result = false;
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@Progress", Progress));
                aParameters.Add(new SqlParameter("@CommentText", CommentText));
                aParameters.Add(new SqlParameter("@UserID", UserID));
                aParameters.Add(new SqlParameter("@VehicleMasterRequestId", VehicleRequestId));
                aParameters.Add(new SqlParameter("@DeligationUserId", DeligationUserId));
                result = accessManager.SaveData("sp_VehicleApproveOrreject", aParameters);
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
        public bool VehicleDCApproveOrReject(int Progress, string CommentText, int UserID, int VehicleRequestId)
        {
            bool result = false;
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@Progress", Progress));
                aParameters.Add(new SqlParameter("@CommentText", CommentText));
                aParameters.Add(new SqlParameter("@UserID", UserID));
                aParameters.Add(new SqlParameter("@VehicleMasterRequestId", VehicleRequestId));
                //aParameters.Add(new SqlParameter("@DeligationUserId", DeligationUserId));
                result = accessManager.SaveData("sp_VehicleDCApproveOrreject", aParameters);
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
        public object UserDetails(int userID)
        {
            object obj = (object)null;
            try
            {
                using (IDbConnection cnn = (IDbConnection)new SqlConnection(this.connStr))
                {
                    if (cnn.State == ConnectionState.Closed)
                        cnn.Open();
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@userId", (object)userID);
                    obj = cnn.Query("sp_RequestDetailById", (object)dynamicParameters, commandType: new CommandType?(CommandType.StoredProcedure)).FirstOrDefault<object>();
                }
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool CommentSent(int MasterID, int ReviewTo, string ReviewMessage, int UserID)
        {
            bool result = false;
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@MasterID", MasterID));
                aParameters.Add(new SqlParameter("@ReviewTo", ReviewTo));
                aParameters.Add(new SqlParameter("@ReviewMessage", ReviewMessage));
                aParameters.Add(new SqlParameter("@UserID", UserID));
                result = accessManager.SaveData("sp_SentVehicleComment", aParameters);
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
        public ResultResponse UpdateVehicleRequest(VehicleRequestModel iOURequestModel, int userId)
        {
            try
            {
                int masterId = 0;
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aParameters = new List<SqlParameter>();

                var UserDetailsList = new JavaScriptSerializer().Serialize(iOURequestModel.UserDetailsList);
                var approverList = new JavaScriptSerializer().Serialize(iOURequestModel.VehicleApproverList);
                int noOfApprover = iOURequestModel.VehicleApproverList.Count;
                aParameters.Add(new SqlParameter("@VehicleUserList", UserDetailsList));
                aParameters.Add(new SqlParameter("@ApproverJson", approverList));
                aParameters.Add(new SqlParameter("@NoOfApprover", noOfApprover));
                aParameters.Add(new SqlParameter("@UserID", userId));
                aParameters.Add(new SqlParameter("@VehicleRequesMastertId", iOURequestModel.VehicleRequesMastertId));
                aParameters.Add(new SqlParameter("@LocationId", iOURequestModel.LocationId));
                aParameters.Add(new SqlParameter("@BusinessUnitId", iOURequestModel.BusinessUnitId));
                aParameters.Add(new SqlParameter("@TravelStratDate", iOURequestModel.TravelStratDate));
                aParameters.Add(new SqlParameter("@TravelEndDate", iOURequestModel.TravelEndDate));
                aParameters.Add(new SqlParameter("@StartTime", iOURequestModel.StartTime));
                aParameters.Add(new SqlParameter("@EndTime", iOURequestModel.EndTime));

                masterId = accessManager.SaveDataReturnPrimaryKey("sp_UpdateVehicleRequest", aParameters);
                ResultResponse result = new ResultResponse();
                result.pk = masterId;
                result.isSuccess = true;
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
        public ResultResponse SaveVehicleBudget(VehicleRequestModel courierRequestModel, int UserId)
        {
            try
            {
                using (IDbConnection cnn = (IDbConnection)new SqlConnection(this.connStr))
                {
                    if (cnn.State == ConnectionState.Closed)
                        cnn.Open();

                    new JavaScriptSerializer().Serialize((object)courierRequestModel);
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@BudgetHeadrId", (object)courierRequestModel.BudgetHeadrId);
                    dynamicParameters.Add("@BusinessUnitId", (object)courierRequestModel.BusinessUnitId);
                    dynamicParameters.Add("@DepartmentID", (object)courierRequestModel.DepartmentID);
                    dynamicParameters.Add("@BudgetYear", (object)courierRequestModel.BudgetYear);
                    dynamicParameters.Add("@MonthOfYear", (object)courierRequestModel.MonthOfYear);
                    dynamicParameters.Add("@InialAmount", (object)courierRequestModel.InialAmount);
                    dynamicParameters.Add("@Amount", (object)courierRequestModel.Amount);
                    dynamicParameters.Add("@FromDate", (object)courierRequestModel.FromDate);
                    dynamicParameters.Add("@ToDate", (object)courierRequestModel.ToDate);
                    dynamicParameters.Add("@CreateBy", (object)UserId);

                    int num = cnn.Execute("sp_VehicleBudgetEntry", (object)dynamicParameters, commandType: new CommandType?(CommandType.StoredProcedure));
                    return new ResultResponse()
                    {
                        pk = num,
                        isSuccess = true
                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<PreferredVehicle> LoadPreferredVehicle()
        {

            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<PreferredVehicle> businessUnits = new List<PreferredVehicle>();
                List<SqlParameter> aList = new List<SqlParameter>();
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetAllVehicleList");
                while (dr.Read())
                {
                    PreferredVehicle businessUnit = new PreferredVehicle();
                    businessUnit.VehicleId = (int)dr["VehicleId"];
                    businessUnit.VehicleName = dr["VehicleName"].ToString();
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
        public List<PreferredVehicle> LoadBudgetHeader()
        {

            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<PreferredVehicle> businessUnits = new List<PreferredVehicle>();
                List<SqlParameter> aList = new List<SqlParameter>();
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetAllBudgetHeader");
                while (dr.Read())
                {
                    PreferredVehicle businessUnit = new PreferredVehicle();
                    businessUnit.BudgetHeaderId = (int)dr["BudgetHeaderId"];
                    businessUnit.BudgetHeaderName = dr["BudgetHeaderName"].ToString();
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
        public List<PreferredVehicle> LoadPurposeofVisit(int LocationId)
        {
            try
            {
                this.accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<PreferredVehicle> commonModelList = new List<PreferredVehicle>();
                SqlDataReader sqlDataReader = this.accessManager.GetSqlDataReader("sp_GetAllPurposeofVisit", new List<SqlParameter>()
        {
          new SqlParameter("@LocationId", (object) LocationId)
        });
                while (sqlDataReader.Read())
                    commonModelList.Add(new PreferredVehicle()
                    {
                        PurposeofVisitId = (int)sqlDataReader["PurposeofVisitId"],
                        PurposeofTravelName = sqlDataReader["PurposeofTravelName"].ToString()
                    });
                return commonModelList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.accessManager.SqlConnectionClose();
            }
        }
        public List<PreferredVehicle> LoadStartingPoint(int LocationId)
        {
            try
            {
                this.accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<PreferredVehicle> commonModelList = new List<PreferredVehicle>();
                SqlDataReader sqlDataReader = this.accessManager.GetSqlDataReader("sp_GetLocationWiseStaingPoint", new List<SqlParameter>()
        {
          new SqlParameter("@LocationId", (object) LocationId)
        });
                while (sqlDataReader.Read())
                    commonModelList.Add(new PreferredVehicle()
                    {
                        StartingPointId = (int)sqlDataReader["LWStartPointId"],
                        StartingPointName = sqlDataReader["StartingPointName"].ToString()
                    });
                return commonModelList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.accessManager.SqlConnectionClose();
            }
        }
        public List<PreferredVehicle> LoadRoute(int starting_point)
        {
            try
            {
                this.accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<PreferredVehicle> commonModelList = new List<PreferredVehicle>();
                SqlDataReader sqlDataReader = this.accessManager.GetSqlDataReader("sp_GetStartPointWiseRoute", new List<SqlParameter>()
        {
          new SqlParameter("@StartPointId", (object) starting_point)
        });
                while (sqlDataReader.Read())
                    commonModelList.Add(new PreferredVehicle()
                    {
                        RouteId = (int)sqlDataReader["RouteId"],
                        RouteName = sqlDataReader["RouteName"].ToString()
                    });
                return commonModelList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.accessManager.SqlConnectionClose();
            }
        }
        public List<BusinessUnit> LoadBusinessUnit(int starting_point)
        {
            try
            {
                this.accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<BusinessUnit> commonModelList = new List<BusinessUnit>();
                SqlDataReader sqlDataReader = this.accessManager.GetSqlDataReader("sp_GetStartPointWiseBusinessUnit", new List<SqlParameter>()
        {
          new SqlParameter("@StartPointId", (object) starting_point)
        });
                while (sqlDataReader.Read())
                    commonModelList.Add(new BusinessUnit()
                    {
                        BusinessUnitId = (int)sqlDataReader["SWBusinessUnitId"],
                        BusinessUnitName = sqlDataReader["BusinessUnitName"].ToString()
                    });
                return commonModelList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.accessManager.SqlConnectionClose();
            }
        }
        //public List<PreferredVehicle> LoadPurposeofVisit(int LocationId)
        //{

        //    try
        //    {
        //        accessManager.SqlConnectionOpen(DataBase.SQQeye);
        //        List<PreferredVehicle> businessUnits = new List<PreferredVehicle>();
        //        List<SqlParameter> aList = new List<SqlParameter>();
        //        SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetAllPurposeofVisit");
        //        while (dr.Read())
        //        {
        //            PreferredVehicle businessUnit = new PreferredVehicle();
        //            businessUnit.PurposeofVisitId = (int)dr["LcWPurposeofVisitId"];
        //            businessUnit.PurposeofTravelName = dr["PurposeofTravelName"].ToString();
        //            businessUnits.Add(businessUnit);
        //        }
        //        return businessUnits;
        //    }
        //    catch (Exception exception)
        //    {

        //        throw exception;
        //    }
        //    finally
        //    {
        //        accessManager.SqlConnectionClose();
        //    }
        //}
        public List<VehicleRequestModel> GetAllVehicleBudget(
 int Status,
 int Pgress)
        {
            List<VehicleRequestModel> CourierRequestModelList = new List<VehicleRequestModel>();
            try
            {
                this.accessManager.SqlConnectionOpen(DataBase.SQQeye);
                SqlDataReader sqlDataReader = this.accessManager.GetSqlDataReader("sp_VehicleGetAllBudget", new List<SqlParameter>()
        {
          new SqlParameter("@Status", (object) Status),
          new SqlParameter("@Progress", (object) Pgress)
        });
                while (sqlDataReader.Read())
                    CourierRequestModelList.Add(new VehicleRequestModel()
                    {
                        VehicleBudgetEntryId = (int)sqlDataReader["VehicleBudgetEntryId"],
                        BusinessUnitId = (int)sqlDataReader["BusinessUnitId"],
                        BusinessUnitName = sqlDataReader["BusinessUnitName"].ToString(),
                        DepartmentID = (int)sqlDataReader["DepartmentID"],
                        //  DepartmentName = sqlDataReader["DepartmentName"].ToString(),
                        MonthOfYear = sqlDataReader["MonthOfYear"].ToString(),
                        BudgetYear = sqlDataReader["BudgetYear"].ToString(),
                        Amount = sqlDataReader["Amount"].ToString(),
                        UserName = sqlDataReader["UserName"].ToString(),

                    });
                return CourierRequestModelList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.accessManager.SqlConnectionClose();
            }
        }
        public List<VehicleRequestMaster> GetAllVehicleRequestsForAllocation(int uid)
        {
            var visitorRequestModelList = new List<VehicleRequestMaster>();

            try
            {
                this.accessManager.SqlConnectionOpen(DataBase.SQQeye);
                SqlDataReader sqlDataReader = this.accessManager.GetSqlDataReader("sp_GetAllVehicleRequests", new List<SqlParameter>());

                while (sqlDataReader.Read())
                    if ((int)sqlDataReader["IsApproved"] == 1 && !IsVehicleAssinged((int)sqlDataReader["VehicleRequesMastertId"]) && (int)sqlDataReader["DeligationUserId"] == uid)
                        visitorRequestModelList.Add(new VehicleRequestMaster()
                        {
                            VehicleRequestMasterId = (int)sqlDataReader["VehicleRequesMastertId"],
                            DateOfRequest = sqlDataReader["DateOfRequest"].ToString(),
                            RequestorId = (int)sqlDataReader["RequestorId"],
                            BusinessUnit = sqlDataReader["BusinessUnitName"].ToString(),
                            TravelStratDate = sqlDataReader["TravelDate"].ToString(),
                            TravelEndDate = sqlDataReader["TravelEndDate"].ToString(),
                            StartTime = sqlDataReader["StartTime"].ToString(),
                            EndTime = sqlDataReader["EndTime"].ToString(),
                            StartingPoint = sqlDataReader["StartingPointName"].ToString(),
                            //Destination = sqlDataReader["Destination"].ToString(),
                            //DepartmentName = sqlDataReader["DeartmentName"].ToString(),
                            DepartmentName = GetDepartmentName((int)sqlDataReader["DepartmentHeadId"]),
                            Route = sqlDataReader["RouteName"].ToString(),
                            PurposeofTravel = sqlDataReader["PurposeofTravelName"].ToString(),
                            TripType = sqlDataReader["TripType"].ToString(),
                            PreferredVehicle = sqlDataReader["PreferredVehicle"].ToString(),
                            NoofUser = (int)sqlDataReader["NoofUser"],
                            Remarks = sqlDataReader["Remarks"].ToString(),
                            CreateDate = sqlDataReader["CreateDate"].ToString(),
                            UpdateDate = sqlDataReader["UpdateDate"].ToString(),
                            IsApproved = (int)sqlDataReader["IsApproved"],
                            UserInformation = new UserInformation
                            {
                                UserInformationName = sqlDataReader["UserName"].ToString()
                            },
                            CommentFromCOO = GetCommentFromCOO((int)sqlDataReader["VehicleRequesMastertId"]),
                            CommentFromStationMaster = GetCommentFromStationMaster((int)sqlDataReader["VehicleRequesMastertId"])
                        });
                return visitorRequestModelList;
            }
            catch (Exception ex)
            {
                accessManager.SqlConnectionClose(true);
                throw ex;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

        public ResultResponse AllocateVehicleForUserFromMaster(VehicleAllocationMaster allocationDetails)
        {
            try
            {
                using (IDbConnection cnn = (IDbConnection)new SqlConnection(this.connStr))
                {
                    if (cnn.State == ConnectionState.Closed)
                        cnn.Open();

                    //new JavaScriptSerializer().Serialize((object)allocationDetails);
                    //DynamicParameters dynamicParameters = new DynamicParameters();
                    //dynamicParameters.Add("@VehicleNo", (object)allocationDetails.VehicleNo);
                    //dynamicParameters.Add("@DriverName", (object)allocationDetails.DriverName);
                    //dynamicParameters.Add("@DriverPhone", (object)allocationDetails.DriverPhone);
                    //dynamicParameters.Add("@VehicleTypeId", (object)allocationDetails.VehicleTypeId);
                    //dynamicParameters.Add("@StartPointId", (object)allocationDetails.StartPointId);
                    //dynamicParameters.Add("@TripTypeId", (object)allocationDetails.TripTypeId);
                    //dynamicParameters.Add("@RouteId", (object)allocationDetails.RouteId);

                    //int VehicleAllocationMasterId = cnn.Execute("SP_AllocateNewVehicle", (object)dynamicParameters, commandType: new CommandType?(CommandType.StoredProcedure));


                    List<SqlParameter> paramList = new List<SqlParameter>();
                    paramList.Add(new SqlParameter("@VehicleNo", allocationDetails.VehicleNo));
                    paramList.Add(new SqlParameter("@DriverName", allocationDetails.DriverName));
                    paramList.Add(new SqlParameter("@DriverPhone", allocationDetails.DriverPhone));
                    paramList.Add(new SqlParameter("@VehicleTypeId", allocationDetails.VehicleTypeId));
                    paramList.Add(new SqlParameter("@StartPointId", allocationDetails.StartPointId));
                    paramList.Add(new SqlParameter("@TripTypeId", allocationDetails.TripTypeId));
                    paramList.Add(new SqlParameter("@RouteId", allocationDetails.RouteId));
                    paramList.Add(new SqlParameter("@TripCost", allocationDetails.TripCost));
                    paramList.Add(new SqlParameter("@TransactionBy", allocationDetails.TransactionBy));

                    this.accessManager.SqlConnectionOpen(DataBase.SQQeye);
                    SqlDataReader sqlDataReader = this.accessManager.GetSqlDataReader("SP_AllocateNewVehicle", paramList);
                    int VehicleAllocationMasterId = 0;
                    while (sqlDataReader.Read())
                        VehicleAllocationMasterId = (int)sqlDataReader["VehicleAllocationMasterId"];

                    foreach (var item in allocationDetails.VehicleAllocationDetailsList)
                    {
                        DynamicParameters sqlParams = new DynamicParameters();
                        sqlParams.Add("@VehicleAllocationMasterId", (object)VehicleAllocationMasterId);
                        sqlParams.Add("@VehicleRequestMasterId", (object)item.VehicleRequestMasterId);
                        sqlParams.Add("@AllocatedCost", (object)item.AllocatedCost);
                        int num = cnn.Execute("SP_AssingVehicleAllocationDetails", (object)sqlParams, commandType: new CommandType?(CommandType.StoredProcedure));
                    }

                    return new ResultResponse
                    {
                        isSuccess = true
                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

        public bool IsVehicleAssinged(int vehicleRequestMasterId)
        {
            try
            {
                this.accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> paramList = new List<SqlParameter>();
                paramList.Add(new SqlParameter("@VehicleRequestMasterId", vehicleRequestMasterId));
                SqlDataReader sqlDataReader = this.accessManager.GetSqlDataReader("SP_GetVehicleAllocationDetailsByRID", paramList);
                if (sqlDataReader.Read())
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                accessManager.SqlConnectionClose(true);
                throw ex;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

        public List<VehicleAllocationMaster> GetAllVehicleAllocationList(int id)
        {
            var vehicleAllocationModelList = new List<VehicleAllocationMaster>();

            try
            {
                this.accessManager.SqlConnectionOpen(DataBase.SQQeye);
                SqlDataReader sqlDataReader = this.accessManager.GetSqlDataReader("sp_GetAllVehicleAllocationList", new List<SqlParameter>());

                while (sqlDataReader.Read())
                    if ((int)sqlDataReader["TransactionBy"] == id)
                        vehicleAllocationModelList.Add(new VehicleAllocationMaster()
                        {
                            VehicleAllocationMasterId = (int)sqlDataReader["VehicleAllocationMasterId"],
                            VehicleNo = sqlDataReader["VehicleNo"].ToString(),
                            DriverName = sqlDataReader["DriverName"].ToString(),
                            DriverPhone = sqlDataReader["DriverPhone"].ToString(),
                            VehicleType = GetVehicleType((int)sqlDataReader["VehicleTypeId"]),
                            StartPoint = GetStartPoint((int)sqlDataReader["StartPointId"]),
                            TripType = GetTripType((int)sqlDataReader["TripTypeId"]),
                            Route = GetRoute((int)sqlDataReader["RouteId"]),
                            TripCost = (int)sqlDataReader["TripCost"],
                            TransactionDateString = sqlDataReader["TransactionDate"].ToString()
                        });
                return vehicleAllocationModelList;
            }
            catch (Exception ex)
            {
                accessManager.SqlConnectionClose(true);
                throw ex;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

        public List<VehicleRequestMaster> GetAllPassengers(int id)
        {
            var passengerBridgeList = new List<VehicleAllocationDetails>();

            try
            {
                this.accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> paramList = new List<SqlParameter>();
                paramList.Add(new SqlParameter("@VehicleAllocationMasterId", id));
                SqlDataReader sqlDataReader = this.accessManager.GetSqlDataReader("sp_GetAllocatedPassenger", paramList);

                var vehiclePassengerList = new List<VehicleRequestMaster>();
                while (sqlDataReader.Read())
                    vehiclePassengerList.Add(new VehicleRequestMaster
                    {
                        VehicleRequestMasterId = (int)sqlDataReader["VehicleRequesMastertId"],
                        DateOfRequest = sqlDataReader["DateOfRequest"].ToString(),
                        RequestorId = (int)sqlDataReader["RequestorId"],
                        BusinessUnitName = sqlDataReader["BusinessUnitName"].ToString(),
                        TravelStratDate = sqlDataReader["TravelStratDateCasted"].ToString(),
                        TravelEndDate = sqlDataReader["TravelEndDateCasted"].ToString(),
                        StartTime = sqlDataReader["StartTime"].ToString(),
                        EndTime = sqlDataReader["EndTime"].ToString(),
                        StartingPoint = sqlDataReader["StartingPoint"].ToString(),
                        Destination = sqlDataReader["Destination"].ToString(),
                        TripType = sqlDataReader["TripType"].ToString(),
                        PreferredVehicle = sqlDataReader["PreferredVehicle"].ToString(),
                        NoofUser = (int)sqlDataReader["NoofUser"],
                        Remarks = sqlDataReader["Remarks"].ToString(),
                        CreateDate = sqlDataReader["CreateDate"].ToString(),
                        UpdateDate = sqlDataReader["UpdateDate"].ToString(),
                        IsApproved = (int)sqlDataReader["IsApproved"],
                        AllocatedCost = (decimal)sqlDataReader["AllocatedCost"],
                        Route = sqlDataReader["RouteName"].ToString(),
                        UserInformation = new UserInformation
                        {
                            UserInformationName = sqlDataReader["UserName"].ToString()
                        }
                    });
                return vehiclePassengerList;
            }
            catch (Exception ex)
            {
                accessManager.SqlConnectionClose(true);
                throw ex;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

        public string GetVehicleType(int id)
        {
            string name = "4 wheeler";
            switch (id)
            {
                case 1:
                    name = "Car";
                    break;
                case 2:
                    name = "Mini-Bus";
                    break;
                default:
                    break;
            }
            return name;
        }

        public string GetStartPoint(int id)
        {
            string name = "Gulshan-2";
            switch (id)
            {
                case 1:
                    name = "Station";
                    break;
                case 2:
                    name = "Celcius 1";
                    break;
                case 3:
                    name = "Central";
                    break;
                default:
                    break;
            }
            return name;
        }

        public string GetTripType(int id)
        {
            string name = "One-Way";
            switch (id)


            {
                case 2:
                    name = "One-Way";
                    break;
                case 1:
                    name = "Round-Trip";
                    break;
                default:
                    break;
            }
            return name;
        }

        public string GetRoute(int id)
        {
            string name = "Station-Uttara-Gulshan";
            switch (id)
            {
                case 1:
                    name = "Station-Uttara-Gulshan";
                    break;
                case 2:
                    name = "Station-Gulshan";
                    break;
                default:
                    break;
            }
            return name;
        }

        public string GetCommentFromCOO(int requstId)
        {
            try
            {
                this.accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> paramList = new List<SqlParameter>();
                paramList.Add(new SqlParameter("@VehicleRequestMasterId", requstId));
                SqlDataReader sqlDataReader = this.accessManager.GetSqlDataReader("sp_GetCommentFromCOO", paramList);

                string temp = "---";
                if (sqlDataReader.Read())
                    temp = sqlDataReader["ReviewComment"].ToString();
                return temp;
            }
            catch (Exception ex)
            {
                accessManager.SqlConnectionClose(true);
                throw ex;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

        public string GetCommentFromStationMaster(int requstId)
        {
            try
            {
                this.accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> paramList = new List<SqlParameter>();
                paramList.Add(new SqlParameter("@VehicleRequestMasterId", requstId));
                SqlDataReader sqlDataReader = this.accessManager.GetSqlDataReader("sp_GetCommentFromStationMaster", paramList);

                string temp = "---";
                if (sqlDataReader.Read())
                    temp = sqlDataReader["ReviewComment"].ToString();
                return temp;
            }
            catch (Exception ex)
            {
                accessManager.SqlConnectionClose(true);
                throw ex;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

        public List<DropDownModel> GetVehicleVendorDropDownOptions(int routeId, int tripTypeId, int vehicleTypeId)
        {
            try
            {
                this.accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> paramList = new List<SqlParameter>();
                paramList.Add(new SqlParameter("@RouteId", routeId));
                paramList.Add(new SqlParameter("@TripTypeId", tripTypeId));
                paramList.Add(new SqlParameter("@VehicleTypeId", vehicleTypeId));
                SqlDataReader sqlDataReader = this.accessManager.GetSqlDataReader("sp_GetAllVehicleVendor", paramList);

                List<DropDownModel> dropDowns = new List<DropDownModel>();
                while (sqlDataReader.Read())
                    dropDowns.Add(new DropDownModel
                    {
                        Value = (int)sqlDataReader["VendorId"],
                        Text = sqlDataReader["VendorName"].ToString()
                    });
                return dropDowns;
            }
            catch (Exception ex)
            {
                accessManager.SqlConnectionClose(true);
                throw ex;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

        public List<DropDownModel> GetVehicleTypeDropDownOptions(int routeId, int tripTypeId)
        {
            try
            {
                this.accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> paramList = new List<SqlParameter>();
                paramList.Add(new SqlParameter("@RouteId", routeId));
                paramList.Add(new SqlParameter("@TripTypeId", tripTypeId));
                SqlDataReader sqlDataReader = this.accessManager.GetSqlDataReader("sp_GetAllVehicle", paramList);

                List<DropDownModel> dropDowns = new List<DropDownModel>();
                while (sqlDataReader.Read())
                    dropDowns.Add(new DropDownModel
                    {
                        Value = (int)sqlDataReader["VehicleId"],
                        Text = sqlDataReader["VehicleName"].ToString()
                    });
                return dropDowns;
            }
            catch (Exception ex)
            {
                accessManager.SqlConnectionClose(true);
                throw ex;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

        //public List<DropDownModel> GetRouteDropDownOptions(int vendorId, int vehicleTypeId, int tripeTypeId)
        //{
        //    try
        //    {
        //        this.accessManager.SqlConnectionOpen(DataBase.SQQeye);
        //        List<SqlParameter> paramList = new List<SqlParameter>();
        //        paramList.Add(new SqlParameter("@VendorId", vendorId));
        //        paramList.Add(new SqlParameter("@VehicleTypeId", vehicleTypeId));
        //        paramList.Add(new SqlParameter("@TripeTypeId", tripeTypeId));
        //        SqlDataReader sqlDataReader = this.accessManager.GetSqlDataReader("sp_GetAllRoute", paramList);

        //        List<DropDownModel> dropDowns = new List<DropDownModel>();
        //        while (sqlDataReader.Read())
        //            dropDowns.Add(new DropDownModel
        //            {
        //                Value = (int)sqlDataReader["RouteId"],
        //                Text = sqlDataReader["RouteName"].ToString()
        //            });
        //        return dropDowns;
        //    }
        //    catch (Exception ex)
        //    {
        //        accessManager.SqlConnectionClose(true);
        //        throw ex;
        //    }
        //    finally
        //    {
        //        accessManager.SqlConnectionClose();
        //    }
        //}

        public dynamic GetVehicleInfo(int id)
        {
            try
            {
                this.accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> paramList = new List<SqlParameter>();
                paramList.Add(new SqlParameter("@Id", id));
                SqlDataReader sqlDataReader = this.accessManager.GetSqlDataReader("sp_GetVehicleInfo", paramList);

                int noOfSeats = 0;
                if (sqlDataReader.Read())
                    noOfSeats = (int)sqlDataReader["NoOfSeats"];

                return new
                {
                    NoOfSeats = noOfSeats
                };
            }
            catch (Exception ex)
            {
                accessManager.SqlConnectionClose(true);
                throw ex;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

        public dynamic GetVehicleCost(RateMatrix data)
        {
            try
            {
                this.accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> paramList = new List<SqlParameter>();
                paramList.Add(new SqlParameter("@VendorId", data.VendorId));
                paramList.Add(new SqlParameter("@VehicleTypeId", data.VehicleTypeId));
                paramList.Add(new SqlParameter("@RouteId", data.RouteId));
                paramList.Add(new SqlParameter("@TripTypeId", data.TripTypeId));
                SqlDataReader sqlDataReader = this.accessManager.GetSqlDataReader("sp_GetVehicleCost", paramList);

                decimal cost = -1;
                if (sqlDataReader.Read())
                    cost = (decimal)sqlDataReader["Rate"];

                return new
                {
                    Rate = cost
                };
            }
            catch (Exception ex)
            {
                accessManager.SqlConnectionClose(true);
                throw ex;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }
        public string GetDepartmentName(int deptHeadId)
        {
            try
            {
                this.accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> paramList = new List<SqlParameter>();
                paramList.Add(new SqlParameter("@DepartmentHeadId", deptHeadId));
                SqlDataReader sqlDataReader = this.accessManager.GetSqlDataReader("[sp_GetDepartment]", paramList);

                string temp = "---";
                if (sqlDataReader.Read())
                    temp = sqlDataReader["DeartmentName"].ToString();
                return temp;
            }
            catch (Exception ex)
            {
                accessManager.SqlConnectionClose(true);
                throw ex;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }
        public VehicleRequestModel GetcourierBudget(int UserId, int primarykey)
        {
            VehicleRequestModel CourierTypeDetails = new VehicleRequestModel();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@courierBudgetEntryId", primarykey));
                //aList.Add(new SqlParameter("@UserId", userId));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetVehicleBudgetInfo", aList);
                while (dr.Read())
                {
                    // CourierTypeDetails courierTypeInfromation = new CourierTypeDetails();
                    CourierTypeDetails.VehicleBudgetEntryId = (int)dr["VehicleBudgetEntryId"];
                    CourierTypeDetails.BusinessUnitId = (int)dr["BusinessUnitId"];
                    CourierTypeDetails.BusinessUnitName = dr["BusinessUnitName"].ToString();
                    CourierTypeDetails.DepartmentID = (int)dr["DepartmentID"];
                    //  CourierTypeDetails.DepartmentName = dr["DepartmentName"].ToString();
                    CourierTypeDetails.BudgetYear = dr["BudgetYear"].ToString();
                    CourierTypeDetails.MonthOfYear = dr["MonthOfYear"].ToString();
                    CourierTypeDetails.Amount = dr["Amount"].ToString();
                    CourierTypeDetails.InialAmount = dr["InialAmount"].ToString();
                    CourierTypeDetails.UserName = dr["UserName"].ToString();

                }
                return CourierTypeDetails;
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
        public ResultResponse UpdateCourierBudget(VehicleRequestModel courierRequestModel, int UserId)
        {
            try
            {
                using (IDbConnection cnn = (IDbConnection)new SqlConnection(this.connStr))
                {
                    if (cnn.State == ConnectionState.Closed)
                        cnn.Open();

                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@CourierBudgetEntryId", (object)courierRequestModel.VehicleBudgetEntryId);
                    dynamicParameters.Add("@BusinessUnitId", (object)courierRequestModel.BusinessUnitId);
                    dynamicParameters.Add("@DepartmentID", (object)courierRequestModel.DepartmentID);
                    dynamicParameters.Add("@BudgetYear", (object)courierRequestModel.BudgetYear);
                    dynamicParameters.Add("@MonthOfYear", (object)courierRequestModel.MonthOfYear);
                    dynamicParameters.Add("@InitialAmount", (object)courierRequestModel.InialAmount);
                    dynamicParameters.Add("@Amount", (object)courierRequestModel.Amount);
                    dynamicParameters.Add("@CreateBy", (object)UserId);
                    int num = cnn.Execute("sp_UpdateVehicleBudgetEntry", (object)dynamicParameters, commandType: new CommandType?(CommandType.StoredProcedure));
                    return new ResultResponse()
                    {
                        pk = num,
                        isSuccess = true
                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public VehicleRequestMaster GetVehicleRequestById(int id)
        {
            VehicleRequestMaster data = new VehicleRequestMaster();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@id", id));
                SqlDataReader sqlDataReader = accessManager.GetSqlDataReader("sp_GetVehicleRequestById", aList);
                if (sqlDataReader.Read())
                {
                    data.VehicleRequestMasterId = (int)sqlDataReader["VehicleRequesMastertId"];
                    data.DateOfRequest = sqlDataReader["DateOfRequest"].ToString();
                    data.RequestorId = (int)sqlDataReader["RequestorId"];
                    data.BusinessUnit = sqlDataReader["BusinessUnitName"].ToString();
                    data.TravelStratDate = sqlDataReader["TravelDate"].ToString();
                    data.TravelEndDate = sqlDataReader["TravelEndDate"].ToString();
                    data.StartTime = sqlDataReader["StartTime"].ToString();
                    data.EndTime = sqlDataReader["EndTime"].ToString();
                    data.StartingPoint = sqlDataReader["StartingPointName"].ToString();
                    data.DepartmentName = GetDepartmentName((int)sqlDataReader["DepartmentHeadId"]);
                    data.Route = sqlDataReader["RouteName"].ToString();
                    data.PurposeofTravel = sqlDataReader["PurposeofTravelName"].ToString();
                    data.TripType = sqlDataReader["TripType"].ToString();
                    data.PreferredVehicle = sqlDataReader["PreferredVehicle"].ToString();
                    data.NoofUser = (int)sqlDataReader["NoofUser"];
                    data.Remarks = sqlDataReader["Remarks"].ToString();
                    data.CreateDate = sqlDataReader["CreateDate"].ToString();
                    data.UpdateDate = sqlDataReader["UpdateDate"].ToString();
                    data.IsApproved = (int)sqlDataReader["IsApproved"];
                    data.UserInformation = new UserInformation
                    {
                        UserInformationName = sqlDataReader["UserName"].ToString()
                    };
                    data.CommentFromCOO = GetCommentFromCOO((int)sqlDataReader["VehicleRequesMastertId"]);
                    data.CommentFromStationMaster = GetCommentFromStationMaster((int)sqlDataReader["VehicleRequesMastertId"]);

                }
                return data;
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

        public List<VehicleRequestMaster> AdjustCost(int tripId, int adjustmentValue)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@TripId", tripId));
                aList.Add(new SqlParameter("@AdjustmentValue", adjustmentValue));
                SqlDataReader sqlDataReader = accessManager.GetSqlDataReader("sp_AdjustCost", aList);
                List<VehicleRequestMaster> passengerList = new List<VehicleRequestMaster>();
                while (sqlDataReader.Read())
                {
                    passengerList.Add(new VehicleRequestMaster
                    {
                        VehicleRequestMasterId = (int)sqlDataReader["VehicleRequesMastertId"],
                        NoofUser = (int)sqlDataReader["NoofUser"]
                    });
                }

                return passengerList;
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

        public int GetTripCost(int tripId)
        {
            try
            {
                this.accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aaList = new List<SqlParameter>();
                aaList.Add(new SqlParameter("@TripId", tripId));
                SqlDataReader sqlDataReader = this.accessManager.GetSqlDataReader("sp_GetTripCost", aaList);
                int tripCost = 0;
                if (sqlDataReader.Read())
                {
                    tripCost = (int)sqlDataReader["TripCost"];
                }

                return tripCost;
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

        public ResultResponse allocateCostForRequest(List<VehicleRequestMaster> passengerList, int tripCost)
        {
            int totalUserCount = 0;

            foreach (var item in passengerList)
            {
                totalUserCount += item.NoofUser;
            }
            foreach (var item in passengerList)
            {
                int Adjustment = item.NoofUser * (tripCost / totalUserCount);

                try
                {
                    using (IDbConnection cnn = (IDbConnection)new SqlConnection(this.connStr))
                    {
                        if (cnn.State == ConnectionState.Closed)
                            cnn.Open();

                        DynamicParameters dynamicParameters = new DynamicParameters();
                        dynamicParameters.Add("@VehicleRequestMasterId", (object)item.VehicleRequestMasterId);
                        dynamicParameters.Add("@Adjustment", (object)Adjustment);
                        int num = cnn.Execute("SP_UpdatePassengerAllocatedCost", (object)dynamicParameters, commandType: new CommandType?(CommandType.StoredProcedure));
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return new ResultResponse
            {
                isSuccess = true
            };
        }
        public dynamic GetAllRateMatrix()
        {
            try
            {
                this.accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> paramList = new List<SqlParameter>();
                SqlDataReader sqlDataReader = this.accessManager.GetSqlDataReader("sp_GetRateMatrix", paramList);

                List<RateMatrix> rateMatrix = new List<RateMatrix>();
                while (sqlDataReader.Read())
                    rateMatrix.Add(new RateMatrix
                    {
                        RateMatrixId = (int)sqlDataReader["RateMatrixId"],
                        Vendor = sqlDataReader["VendorName"].ToString(),
                        VehicleType = sqlDataReader["VehicleName"].ToString(),
                        Route = sqlDataReader["RouteName"].ToString(),
                        TripType = GetTripType((int)sqlDataReader["TripTypeId"]),
                        Rate = (decimal)sqlDataReader["Rate"]
                    });
                return rateMatrix;
            }
            catch (Exception ex)
            {
                accessManager.SqlConnectionClose(true);
                throw ex;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

        public List<DropDownModel> GetVehicleVendorDropDownOptions()
        {
            try
            {
                this.accessManager.SqlConnectionOpen(DataBase.SQQeye);
                SqlDataReader sqlDataReader = this.accessManager.GetSqlDataReader("sp_GetAllVehicleVendorForRate");

                List<DropDownModel> dropDowns = new List<DropDownModel>();
                while (sqlDataReader.Read())
                    dropDowns.Add(new DropDownModel
                    {
                        Value = (int)sqlDataReader["VendorId"],
                        Text = sqlDataReader["VendorName"].ToString()
                    });
                return dropDowns;
            }
            catch (Exception ex)
            {
                accessManager.SqlConnectionClose(true);
                throw ex;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

        public List<DropDownModel> GetVehicleTypeDropDownOptions()
        {
            try
            {
                this.accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> paramList = new List<SqlParameter>();
                SqlDataReader sqlDataReader = this.accessManager.GetSqlDataReader("sp_GetAllVehicleList", paramList);

                List<DropDownModel> dropDowns = new List<DropDownModel>();
                while (sqlDataReader.Read())
                    dropDowns.Add(new DropDownModel
                    {
                        Value = (int)sqlDataReader["VehicleId"],
                        Text = sqlDataReader["VehicleName"].ToString()
                    });
                return dropDowns;
            }
            catch (Exception ex)
            {
                accessManager.SqlConnectionClose(true);
                throw ex;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

        public List<DropDownModel> GetRouteDropDownOptions()
        {
            try
            {
                this.accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> paramList = new List<SqlParameter>();
                SqlDataReader sqlDataReader = this.accessManager.GetSqlDataReader("sp_GetAllRouteList", paramList);

                List<DropDownModel> dropDowns = new List<DropDownModel>();
                while (sqlDataReader.Read())
                    dropDowns.Add(new DropDownModel
                    {
                        Value = (int)sqlDataReader["RouteId"],
                        Text = sqlDataReader["RouteName"].ToString()
                    });
                return dropDowns;
            }
            catch (Exception ex)
            {
                accessManager.SqlConnectionClose(true);
                throw ex;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

        public ResultResponse AddRateMatrix(RateMatrix rateMatrix)
        {
            try
            {
                using (IDbConnection cnn = (IDbConnection)new SqlConnection(this.connStr))
                {
                    if (cnn.State == ConnectionState.Closed)
                        cnn.Open();

                    new JavaScriptSerializer().Serialize((object)rateMatrix);
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@VendorId", rateMatrix.VendorId);
                    dynamicParameters.Add("@VehicleTypeId", rateMatrix.VehicleTypeId);
                    dynamicParameters.Add("@RouteId", rateMatrix.RouteId);
                    dynamicParameters.Add("@TripTypeId", rateMatrix.TripTypeId);
                    dynamicParameters.Add("@Rate", rateMatrix.Rate);

                    int num = cnn.Execute("SP_AddRateMatrix", (object)dynamicParameters, commandType: new CommandType?(CommandType.StoredProcedure));

                    //List<SqlParameter> paramList = new List<SqlParameter>();
                    //paramList.Add(new SqlParameter("@VendorId", rateMatrix.VendorId));
                    //paramList.Add(new SqlParameter("@VehicleTypeId", rateMatrix.VehicleTypeId));
                    //paramList.Add(new SqlParameter("@RouteId", rateMatrix.RouteId));
                    //paramList.Add(new SqlParameter("@TripTypeId", rateMatrix.TripTypeId));
                    //paramList.Add(new SqlParameter("@Rate", rateMatrix.Rate));

                    //this.accessManager.SqlConnectionOpen(DataBase.SQQeye);
                    //SqlDataReader sqlDataReader = this.accessManager.GetSqlDataReader("SP_AllocateNewVehicle", paramList);

                    return new ResultResponse
                    {
                        isSuccess = true
                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}