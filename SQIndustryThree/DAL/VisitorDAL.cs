using DocSoOperation.Models;
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

namespace SQIndustryThree.DAL
{
    public class VisitorDAL
    {
        private DataAccessManager accessManager = new DataAccessManager();
        private string connStr = ConfigurationManager.ConnectionStrings["SQQEYEDatabase"].ConnectionString;
        public List<VisitorRequestModel> GetAllRequestInformation(
  int status,
  int userId,
  int frontDesk)
        {
            List<VisitorRequestModel> visitorRequestModelList = new List<VisitorRequestModel>();
            try
            {
                this.accessManager.SqlConnectionOpen(DataBase.SQQeye);
                SqlDataReader sqlDataReader = this.accessManager.GetSqlDataReader("sp_AllVisitorInformationGet", new List<SqlParameter>()
        {
          new SqlParameter("@userId", (object) userId),
          new SqlParameter("@status", (object) status),
          new SqlParameter("@frontdesk", (object) frontDesk)
        });
                while (sqlDataReader.Read())
                    visitorRequestModelList.Add(new VisitorRequestModel()
                    {
                        RequestorId = (int)sqlDataReader["RequestorId"],
                        RequestorName = sqlDataReader["RequestorName"].ToString(),
                        RequestorDepartment = sqlDataReader["RequestorDepartment"].ToString(),
                        RequestorDesignation = sqlDataReader["RequestorDesignation"].ToString(),
                        VisitorName = sqlDataReader["VisitorName"].ToString(),
                        VisitDate = (DateTime)sqlDataReader["VisitDate"],
                        PurposeOfVisitSQ = sqlDataReader["RequestorName"].ToString(),
                        BusinessUnitName = sqlDataReader["BusinessUnitName"].ToString(),
                        VisitorCompany = sqlDataReader["VisitorCompany"].ToString(),
                        VisitorDesignation = sqlDataReader["VisitorDesignation"].ToString(),
                        IsApproved = (int)sqlDataReader["IsApproved"]
                    });
                return visitorRequestModelList;
            }
            catch (Exception ex)
            {
                this.accessManager.SqlConnectionClose(true);
                throw ex;
            }
            finally
            {
                this.accessManager.SqlConnectionClose();
            }
        }

        public VisitorRequestModel IndividualRequestShow(int PrimaryKey, int userId)
        {
            VisitorRequestModel visitorRequestModel = new VisitorRequestModel();
            try
            {
                this.accessManager.SqlConnectionOpen(DataBase.SQQeye);
                SqlDataReader sqlDataReader = this.accessManager.GetSqlDataReader("sp_IndividualRequestView", new List<SqlParameter>()
        {
          new SqlParameter("@userId", (object) userId),
          new SqlParameter("@VisitorId", (object) PrimaryKey)
        });
                while (sqlDataReader.Read())
                {
                    visitorRequestModel.RequestorId = (int)sqlDataReader["RequestorId"];
                    visitorRequestModel.VisitorId = (int)sqlDataReader["VisitorId"];
                    visitorRequestModel.RequestorName = sqlDataReader["RequestorName"].ToString();
                    visitorRequestModel.RequestorDepartment = sqlDataReader["RequestorDepartment"].ToString();
                    visitorRequestModel.RequestorDesignation = sqlDataReader["RequestorDesignation"].ToString();
                    visitorRequestModel.RequestorEmail = sqlDataReader["RequestorEmail"].ToString();
                    visitorRequestModel.RequerstorMobile = sqlDataReader["RequerstorMobile"].ToString();
                    visitorRequestModel.VisitorName = sqlDataReader["VisitorName"].ToString();
                    visitorRequestModel.VisitorEmail = sqlDataReader["VisitorEmail"].ToString();
                    visitorRequestModel.VisitDate = (DateTime)sqlDataReader["VisitDate"];
                    visitorRequestModel.PurposeOfVisitSQ = sqlDataReader["PurposeOfVisitSQ"].ToString();
                    visitorRequestModel.VisitorCardNo = sqlDataReader["VisitorCardNo"].ToString();
                    if (sqlDataReader["VisitorCardNo"] != null )
                    {
                        visitorRequestModel.VisitorCardNo = sqlDataReader["VisitorCardNo"].ToString();
                    }

                    if (sqlDataReader["VehicleNo"] != null)
                    {
                        visitorRequestModel.VehicleNo = sqlDataReader["VehicleNo"].ToString();
                    }

                    if (sqlDataReader["GateRemarks"] != null)
                    {
                        visitorRequestModel.GateRemarks = sqlDataReader["GateRemarks"].ToString();
                    }
                    if (sqlDataReader["CheckIn"] != null)
                    {
                        visitorRequestModel.CheckIn = sqlDataReader["CheckIn"].ToString();
                    }
                    if (sqlDataReader["CheckOut"] != null)
                    {
                        visitorRequestModel.CheckOut = sqlDataReader["CheckOut"].ToString();
                    }

                    //visitorRequestModel.VehicleNo = sqlDataReader["VehicleNo"].ToString();
                    //visitorRequestModel.GateRemarks = sqlDataReader["GateRemarks"].ToString();
                    //visitorRequestModel.CheckIn = sqlDataReader["CheckIn"].ToString();
                    //visitorRequestModel.CheckOut = sqlDataReader["CheckOut"].ToString();
                    visitorRequestModel.NIDorPassport = sqlDataReader["NIDorPassport"].ToString();
                    visitorRequestModel.VisitorCompany = sqlDataReader["VisitorCompany"].ToString();
                    visitorRequestModel.VisitorDesignation = sqlDataReader["VisitorDesignation"].ToString();
                    visitorRequestModel.VisitorNationality = sqlDataReader["VisitorNationality"].ToString();
                    visitorRequestModel.Chainavisit = sqlDataReader["Chainavisit"].ToString();
                    visitorRequestModel.BusinessUnitName = sqlDataReader["BusinessUnitName"].ToString();
                    visitorRequestModel.LocationName = sqlDataReader["LocationName"].ToString();
                    visitorRequestModel.VisitorMobile = sqlDataReader["VisitorMobile"].ToString();
                    visitorRequestModel.ApprovedStatus = sqlDataReader["PendingStatus"].ToString();
                    visitorRequestModel.Remarks = sqlDataReader["Remarks"].ToString();
                    visitorRequestModel.IsApproved = (int)sqlDataReader["Approved"];
                }
                return visitorRequestModel;
            }
            catch (Exception ex)
            {
                this.accessManager.SqlConnectionClose(true);
                throw ex;
            }
            finally
            {
                this.accessManager.SqlConnectionClose();
            }
        }

        public ResultResponse UpdateOrReject(int primarykey, int userId, int status)
        {
            ResultResponse resultResponse = new ResultResponse();
            try
            {
                this.accessManager.SqlConnectionOpen(DataBase.SQQeye);
                resultResponse.isSuccess = this.accessManager.UpdateData("sp_VisitorApproverOrReject", new List<SqlParameter>()
        {
          new SqlParameter("@userID", (object) userId),
          new SqlParameter("@IsApproved", (object) status),
          new SqlParameter("@primaryKey", (object) primarykey)
        });
                return resultResponse;
            }
            catch (Exception ex)
            {
                this.accessManager.SqlConnectionClose(true);
                throw ex;
            }
            finally
            {
                this.accessManager.SqlConnectionClose();
            }
        }

        public int SUbMenuByPermission(int userId)
        {
            int num = 0;
            try
            {
                this.accessManager.SqlConnectionOpen(DataBase.SQQeye);
                SqlDataReader sqlDataReader = this.accessManager.GetSqlDataReader("sp_VisitorMenuPermission", new List<SqlParameter>()
        {
          new SqlParameter("@userID", (object) userId)
        });
                while (sqlDataReader.Read())
                    num = (int)sqlDataReader["NOPERMISSION"];
                return num;
            }
            catch (Exception ex)
            {
                this.accessManager.SqlConnectionClose(true);
                throw ex;
            }
            finally
            {
                this.accessManager.SqlConnectionClose();
            }
        }

        public List<VisitorRequestModel> GetAllVisitorInformation(
          int status,
          int userId,
          int frontdesk)
        {
            List<VisitorRequestModel> visitorRequestModelList = new List<VisitorRequestModel>();
            try
            {
                this.accessManager.SqlConnectionOpen(DataBase.SQQeye);
                SqlDataReader sqlDataReader = this.accessManager.GetSqlDataReader("sp_GetAllVisitorInformation", new List<SqlParameter>()
        {
          new SqlParameter("@userId", (object) userId),
          new SqlParameter("@status", (object) status),
          new SqlParameter("@frontdesk", (object) frontdesk)
        });
                while (sqlDataReader.Read())
                    visitorRequestModelList.Add(new VisitorRequestModel()
                    {
                        VisitorId = (int)sqlDataReader["VisitorId"],
                        RequestorId = (int)sqlDataReader["RequestorId"],
                        BusinessUnitName = sqlDataReader["BusinessUnitName"].ToString(),
                        SQUnitName = sqlDataReader["SQUnitName"].ToString(),
                        SQDepartmentId = sqlDataReader["SQDepartmentId"].ToString(),
                        SQID = sqlDataReader["SQID"].ToString(),
                        RequestorName = sqlDataReader["RequestorName"].ToString(),
                        RequestorDepartment = sqlDataReader["RequestorDepartment"].ToString(),
                        RequestorDesignation = sqlDataReader["RequestorDesignation"].ToString(),
                        RequerstorMobile = sqlDataReader["RequerstorMobile"].ToString(),
                        VisitorName = sqlDataReader["VisitorName"].ToString(),
                        VisitDate = (DateTime)sqlDataReader["VisitDate"],
                        VehicleNo = sqlDataReader["VisitorVehicleNo"].ToString(),
                        PurposeOfVisitSQ = sqlDataReader["PurposeOfVisitSQ"].ToString(),
                        Chainavisit = sqlDataReader["Chainavisit"].ToString(),
                        VisitorMobile = sqlDataReader["VisitorMobile"].ToString(),
                        VisitorCompany = sqlDataReader["VisitorCompany"].ToString(),
                        ApprovedStatus = sqlDataReader["PendingStatus"].ToString(),
                        IsApproved = (int)sqlDataReader["Approved"],
                        Image = sqlDataReader["Image"].ToString(),
                        //CheckIn = sqlDataReader["CheckIn"].ToString(),
                        //CheckOut = sqlDataReader["CheckOut"].ToString()
                        Checked = sqlDataReader["Checked"].ToString()

                    }) ;
                return visitorRequestModelList;
            }
            catch (Exception ex)
            {
                this.accessManager.SqlConnectionClose(true);
                throw ex;
            }
            finally
            {
                this.accessManager.SqlConnectionClose();
            }
        }

        public List<VisitorRequestModel> GetFactoryVisitorInformation(int status)
        {
            List<VisitorRequestModel> visitorRequestModelList = new List<VisitorRequestModel>();
            try
            {
                this.accessManager.SqlConnectionOpen(DataBase.SQQeye);
                SqlDataReader sqlDataReader = this.accessManager.GetSqlDataReader("sp_GetFactoryVisitorInformation", new List<SqlParameter>()
        {
          new SqlParameter("@status", (object) status)
        });
                while (sqlDataReader.Read())
                    visitorRequestModelList.Add(new VisitorRequestModel()
                    {
                        RequestorId = (int)sqlDataReader["RequestorId"],
                        VisitorId = (int)sqlDataReader["VisitorId"],
                        SQUnitName = sqlDataReader["SQUnitName"].ToString(),
                        SQDepartmentId = sqlDataReader["SQDepartmentId"].ToString(),
                        SQID = sqlDataReader["SQID"].ToString(),
                        RequestorName = sqlDataReader["RequestorName"].ToString(),
                        RequestorDepartment = sqlDataReader["RequestorDepartment"].ToString(),
                        RequestorDesignation = sqlDataReader["RequestorDesignation"].ToString(),
                        RequerstorMobile = sqlDataReader["RequerstorMobile"].ToString(),
                        BusinessUnitName = sqlDataReader["BusinessUnitName"].ToString(),
                        VisitorName = sqlDataReader["VisitorName"].ToString(),
                        VisitorCompany = sqlDataReader["VisitorCompany"].ToString(),
                        VisitDate = (DateTime)sqlDataReader["VisitDate"],
                        PurposeOfVisitSQ = sqlDataReader["PurposeOfVisitSQ"].ToString(),
                        VehicleNo = sqlDataReader["VisitorVehicleNo"].ToString(),
                        Chainavisit = sqlDataReader["Chainavisit"].ToString(),
                        NIDorPassport = sqlDataReader["NIDorPassport"].ToString(),
                        Image = sqlDataReader["Image"].ToString(),
                        VisitorMobile = sqlDataReader["VisitorMobile"].ToString(),
                        ApprovedStatus = sqlDataReader["PendingStatus"].ToString(),
                        IsApproved = (int)sqlDataReader["Approved"],
                        Checked = sqlDataReader["Checked"].ToString()
                        //CheckIn = sqlDataReader["CheckIn"].ToString(),
                        //CheckOut = sqlDataReader["CheckOut"].ToString()
                    });
                return visitorRequestModelList;
            }
            catch (Exception ex)
            {
                this.accessManager.SqlConnectionClose(true);
                throw ex;
            }
            finally
            {
                this.accessManager.SqlConnectionClose();
            }
        }

        public bool SaveVistorRequest(RequestorModel visitor, int UserId)
        {
            bool flag = false;
            try
            {
                this.accessManager.SqlConnectionOpen(DataBase.SQQeye);
                int num = this.accessManager.SaveDataReturnPrimaryKey("sp_SaveVisitorRequestAms", new List<SqlParameter>()
        {
          new SqlParameter("@BusinessUnitId", (object) visitor.BusinessUnitId),
          new SqlParameter("@LocationId", (object) visitor.LocationId),
          new SqlParameter("@RequestorDepartment", (object) visitor.RequestorDepartment),
          new SqlParameter("@visitDate", (object) visitor.VisitDate),
          new SqlParameter("@UserId", (object) UserId)
        });
                foreach (VisitorModel visitor1 in visitor.VisitorList)
                    flag = this.accessManager.SaveData("sp_SaveVisitorDetailsTable", new List<SqlParameter>()
          {
            new SqlParameter("@RequestorId", (object) num),
            new SqlParameter("@VisitorName", (object) visitor1.VisitorName),
            new SqlParameter("@VisitorEmail", (object) visitor1.VisitorEmail),
            new SqlParameter("@VisitorDesignation", (object) visitor1.VisitorDesignation),
            new SqlParameter("@VisitorMobile", (object) visitor1.VisitorMobile),
            new SqlParameter("@VisitorCompany", (object) visitor1.VisitorCompany),
            new SqlParameter("@VisitorNationality", (object) visitor1.VisitorNationality),
            new SqlParameter("@PurposeOfVisitSQ", (object) visitor1.PurposeOfVisitSQ),
            new SqlParameter("@Chainavisit", (object) visitor1.Chainavisit),
            new SqlParameter("@Remarks", (object) visitor1.Remarks)
          });
                return flag;
            }
            catch (Exception ex)
            {
                this.accessManager.SqlConnectionClose(true);
                throw ex;
            }
            finally
            {
                this.accessManager.SqlConnectionClose();
            }
        }

        public ResultResponse SaveVisitor(RequestorModel visitor, int UserId)
        {
            try
            {
                using (IDbConnection cnn = (IDbConnection)new SqlConnection(this.connStr))
                {
                    if (cnn.State == ConnectionState.Closed)
                        cnn.Open();
                    string str1 = new JavaScriptSerializer().Serialize((object)visitor.VisitorList);
                    string str2 = new JavaScriptSerializer().Serialize((object)visitor.VisitorApproverList);
                    visitor.VisitorList = (List<VisitorModel>)null;
                    int count = visitor.VisitorApproverList.Count;
                    visitor.VisitorApproverList = (List<IOUApproverModel>)null;
                    new JavaScriptSerializer().Serialize((object)visitor);
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@BusinessUnitId", (object)visitor.BusinessUnitId);
                    dynamicParameters.Add("@VisitMode", (object)visitor.VisitMode);
                    dynamicParameters.Add("@LocationId", (object)visitor.LocationId);
                    dynamicParameters.Add("@CategoryId", (object)visitor.CategoryId);
                    dynamicParameters.Add("@SubCategoryId", (object)visitor.SubCategoryId);
                    dynamicParameters.Add("@DepartmentHeadId", (object)visitor.DepartmentHeadId);
                    dynamicParameters.Add("@RequestorDepartment", (object)visitor.RequestorDepartment);
                    dynamicParameters.Add("@NIDorPassport", (object)visitor.NIDorPassport);
                    dynamicParameters.Add("@ModeOfVisit", (object)visitor.ModeOfVisit);
                    if (visitor.ModeOfVisit == 1)
                    {
                        dynamicParameters.Add("@visitDate", (object)visitor.VisitDate);
                        dynamicParameters.Add("@StartDate", (object)visitor.VisitDate);
                        dynamicParameters.Add("@EndDate", (object)visitor.VisitDate);
                    }
                    else
                    {
                        dynamicParameters.Add("@visitDate", (object)visitor.StartDate);
                        dynamicParameters.Add("@StartDate", (object)visitor.StartDate);
                        dynamicParameters.Add("@EndDate", (object)visitor.EndDate);
                    }
                    dynamicParameters.Add("@UserId", (object)UserId);
                    dynamicParameters.Add("@Image", (object)visitor.Image);
                    dynamicParameters.Add("@ImagePath", (object)visitor.ImagePath);
                    dynamicParameters.Add("@ApproverJson", (object)str2);
                    dynamicParameters.Add("@VisitorJosn", (object)str1);
                    int num = cnn.Execute("SP_VisitorDataInsert", (object)dynamicParameters, commandType: new CommandType?(CommandType.StoredProcedure));
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

        public ResultResponse UpdateVisitorRequest(RequestorModel visitor, int UserId)
        {
            try
            {
                using (IDbConnection cnn = (IDbConnection)new SqlConnection(this.connStr))
                {
                    if (cnn.State == ConnectionState.Closed)
                        cnn.Open();
                    string str1 = new JavaScriptSerializer().Serialize((object)visitor.VisitorList);
                    string str2 = new JavaScriptSerializer().Serialize((object)visitor.VisitorApproverList);
                    visitor.VisitorList = (List<VisitorModel>)null;
                    visitor.VisitorApproverList = (List<IOUApproverModel>)null;
                    visitor.VisitorApproverList = (List<IOUApproverModel>)null;
                    new JavaScriptSerializer().Serialize((object)visitor);
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@RequestorId", (object)visitor.RequestorId);
                    dynamicParameters.Add("@visitDate", (object)visitor.VisitDate);
                    dynamicParameters.Add("@UserId", (object)UserId);
                    dynamicParameters.Add("@ApproverJson", (object)str2);
                    dynamicParameters.Add("@VisitorJosn", (object)str1);
                    int num = cnn.Execute("sp_UpdateVisitorRequest", (object)dynamicParameters, commandType: new CommandType?(CommandType.StoredProcedure));
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

        public List<CommonModel> GetApprovers(int unitId)
        {
            try
            {
                this.accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<CommonModel> commonModelList = new List<CommonModel>();
                SqlDataReader sqlDataReader = this.accessManager.GetSqlDataReader("sp_BUnitWiseApproverList", new List<SqlParameter>()
        {
          new SqlParameter("@bunitId", (object) unitId)
        });
                while (sqlDataReader.Read())
                    commonModelList.Add(new CommonModel()
                    {
                        UserId = (int)sqlDataReader["UserId"],
                        UserName = sqlDataReader["UserName"].ToString(),
                        BusinessUnitId = (int)sqlDataReader["BusinessUnitId"],
                        BusinessUnitName = sqlDataReader["BusinessUnitName"].ToString(),
                        DesignationName = sqlDataReader["DesignationName"].ToString()
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

        public List<CommonModel> GetDepartments()
        {
            try
            {
                this.accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<CommonModel> commonModelList = new List<CommonModel>();
                SqlDataReader sqlDataReader = this.accessManager.GetSqlDataReader("sp_GetAllDepartment");
                while (sqlDataReader.Read())
                    commonModelList.Add(new CommonModel()
                    {
                        DepartmentId = (int)sqlDataReader["DepartmentId"],
                        DeartmentName = sqlDataReader["DeartmentName"].ToString()
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

        public List<CommonModel> GetDepartmentList(int location)
        {
            try
            {
                this.accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<CommonModel> commonModelList = new List<CommonModel>();
                SqlDataReader sqlDataReader = this.accessManager.GetSqlDataReader("DepartmentList", new List<SqlParameter>()
        {
          new SqlParameter("@Location", (object) location)
        });
                while (sqlDataReader.Read())
                    commonModelList.Add(new CommonModel()
                    {
                        DepartmentId = (int)sqlDataReader["DepartmentId"],
                        DeartmentName = sqlDataReader["DepartmentName"].ToString()
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

        public object UpadteVisitorCheckinAndCheckOut(
          string requestorId,
          string visitorId,
          string rowId,
          string visitorCardNo,
          string imageName,
          string imagePath,
          string vehicleNo,
          string remarks,
          string checkin,
          string checkout)
        {
            object obj = (object)null;
            try
            {
                using (IDbConnection cnn = (IDbConnection)new SqlConnection(this.connStr))
                {
                    if (cnn.State == ConnectionState.Closed)
                        cnn.Open();
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@RequestorId", (object)requestorId);
                    dynamicParameters.Add("@VisitorId", (object)visitorId);
                    dynamicParameters.Add("@RowId", (object)rowId);
                    dynamicParameters.Add("@VisitorCardNo", (object)visitorCardNo);
                    dynamicParameters.Add("@ImageName", (object)imageName);
                    dynamicParameters.Add("@ImagePath", (object)imagePath);
                    dynamicParameters.Add("@vehicleNo", (object)vehicleNo);
                    dynamicParameters.Add("@GateRemarks", (object)remarks);
                    dynamicParameters.Add("@CheckIn", (object)checkin);
                    dynamicParameters.Add("@CheckOut", (object)checkout);
                    obj = (object)cnn.Execute(nameof(UpadteVisitorCheckinAndCheckOut), (object)dynamicParameters, commandType: new CommandType?(CommandType.StoredProcedure));
                }
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<VisitorRequestModel> GetAllVisitorRequest(
          int UserId,
          int Status,
          int Pgress)
        {
            List<VisitorRequestModel> visitorRequestModelList = new List<VisitorRequestModel>();
            try
            {
                this.accessManager.SqlConnectionOpen(DataBase.SQQeye);
                SqlDataReader sqlDataReader = this.accessManager.GetSqlDataReader("sp_VisitorGetAllRequest", new List<SqlParameter>()
        {
          new SqlParameter("@UserId", (object) UserId),
          new SqlParameter("@Status", (object) Status),
          new SqlParameter("@Progress", (object) Pgress)
        });
                while (sqlDataReader.Read())
                    visitorRequestModelList.Add(new VisitorRequestModel()
                    {
                        RequestorId = (int)sqlDataReader["RequestorId"],
                        RequestorName = sqlDataReader["RequestorName"].ToString(),
                        VisitDate = (DateTime)sqlDataReader["VisitDate"],
                        RequestorDepartment = sqlDataReader["RequestorDepartment"].ToString(),
                        BusinessUnitName = sqlDataReader["BusinessUnitName"].ToString(),
                        LocationName = sqlDataReader["LocationName"].ToString(),
                        CategoryName = sqlDataReader["CategoryName"].ToString(),
                        SubCategroyName = sqlDataReader["SubCategoryName"].ToString(),
                        MeetingWith = sqlDataReader["MeetingWith"].ToString(),
                        VehicleNo = sqlDataReader["VisitorVehicleNo"].ToString(),
                        TotalVisitor = (int)sqlDataReader["TotalVisitor"],
                        IsApproved = (int)sqlDataReader["IsApproved"],
                        Pending = (int)sqlDataReader["Pending"]
                    }); 
                return visitorRequestModelList;
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

        public RequestorModel VisitorDetailsInformation(int masterId, int userId)
        {
            RequestorModel requestorModel = new RequestorModel();
            try
            {
                this.accessManager.SqlConnectionOpen(DataBase.SQQeye);
                SqlDataReader sqlDataReader = this.accessManager.GetSqlDataReader("sp_VisitorDetailsInfo", new List<SqlParameter>()
        {
          new SqlParameter("@RequestorId", (object) masterId),
          new SqlParameter("@UserId", (object) userId)
        });
                while (sqlDataReader.Read())
                {
                    requestorModel.RequestorId = (int)sqlDataReader["RequestorId"];
                    requestorModel.VisitMode = (int)sqlDataReader["VisitMode"];
                    requestorModel.RivisionNo = (int)sqlDataReader["RevisionNo"];
                    requestorModel.BusinessUnitId = (int)sqlDataReader["BusinessUnitId"];
                    requestorModel.BusinessUnitName = sqlDataReader["BusinessUnitName"].ToString();
                    requestorModel.LocationId = (int)sqlDataReader["LocationId"];
                    requestorModel.LocationName = sqlDataReader["LocationName"].ToString();
                    requestorModel.RequestorName = sqlDataReader["RequestorName"].ToString();
                    requestorModel.RequestorEmail = sqlDataReader["RequestorEmail"].ToString();
                    requestorModel.RequestorDesignation = sqlDataReader["RequestorDesignation"].ToString();
                    requestorModel.RequestorDepartment = sqlDataReader["RequestorDepartment"].ToString();
                    requestorModel.RequerstorMobile = sqlDataReader["RequerstorMobile"].ToString();
                    if (requestorModel.ModeOfVisit == 1)
                    {
                        requestorModel.VisitDate = (DateTime)sqlDataReader["VisitDate"];
                    }
                    else
                    {
                        requestorModel.StartDate = (DateTime)sqlDataReader["StartDate"];
                        requestorModel.EndDate = (DateTime)sqlDataReader["EndDate"];
                    }
                    requestorModel.IsApproved = (int)sqlDataReader["IsApproved"];
                    requestorModel.Created_By = (int)sqlDataReader["Created_By"];
                    requestorModel.VisitorApproverList = JsonConvert.DeserializeObject<List<IOUApproverModel>>(sqlDataReader["VisitorApproverList"].ToString());
                    requestorModel.VisitorList = JsonConvert.DeserializeObject<List<VisitorModel>>(sqlDataReader["VisitorList"].ToString());
                    requestorModel.VisitorComments = JsonConvert.DeserializeObject<List<CommentsTable>>(sqlDataReader["VisitorComments"].ToString());
                    requestorModel.VisitorLogSection = JsonConvert.DeserializeObject<List<LogSection>>(sqlDataReader["VisitorLogsection"].ToString());
                }
                return requestorModel;
            }
            catch (Exception ex)
            {
                this.accessManager.SqlConnectionClose(true);
                throw ex;
            }
            finally
            {
                this.accessManager.SqlConnectionClose();
            }
        }

        public List<VisitorCategory> VisitorCategories(int unit)
        {
            List<VisitorCategory> visitorCategoryList = new List<VisitorCategory>();
            try
            {
                using (IDbConnection cnn = (IDbConnection)new SqlConnection(this.connStr))
                {
                    if (cnn.State == ConnectionState.Closed)
                        cnn.Open();
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@unitId", (object)unit);
                    visitorCategoryList = cnn.Query<VisitorCategory>("sp_VisitorCategoryList", (object)dynamicParameters, commandType: new CommandType?(CommandType.StoredProcedure)).ToList<VisitorCategory>();
                }
                return visitorCategoryList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<VisitorSubCategory> VisitorSubCategories(int CatId)
        {
            List<VisitorSubCategory> visitorSubCategoryList = new List<VisitorSubCategory>();
            try
            {
                using (IDbConnection cnn = (IDbConnection)new SqlConnection(this.connStr))
                {
                    if (cnn.State == ConnectionState.Closed)
                        cnn.Open();
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@CatId", (object)CatId);
                    visitorSubCategoryList = cnn.Query<VisitorSubCategory>("sp_VisitorSubCategoryList", (object)dynamicParameters, commandType: new CommandType?(CommandType.StoredProcedure)).ToList<VisitorSubCategory>();
                }
                return visitorSubCategoryList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<VisitorApprover> GetApprovers(
          int category,
          int subcategory,
          int unit)
        {
            List<VisitorApprover> visitorApproverList = new List<VisitorApprover>();
            try
            {
                using (IDbConnection cnn = (IDbConnection)new SqlConnection(this.connStr))
                {
                    if (cnn.State == ConnectionState.Closed)
                        cnn.Open();
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@CategoryID", (object)category);
                    dynamicParameters.Add("@SubCategoryID", (object)subcategory);
                    dynamicParameters.Add("@UnitId", (object)unit);
                    visitorApproverList = cnn.Query<VisitorApprover>("sp_VisitorRoleWiseApprover", (object)dynamicParameters, commandType: new CommandType?(CommandType.StoredProcedure)).ToList<VisitorApprover>();
                }
                return visitorApproverList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<VisitorApprover> GetApproverCategoryBased(
          int category,
          int subcategory,
          int unit,
          int DepartmentHeadId
  )
        {
            List<VisitorApprover> visitorApproverList = new List<VisitorApprover>();
            try
            {
                using (IDbConnection cnn = (IDbConnection)new SqlConnection(this.connStr))
                {
                    if (cnn.State == ConnectionState.Closed)
                        cnn.Open();
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@CategoryID", (object)category);
                    dynamicParameters.Add("@SubCategoryID", (object)subcategory);
                    dynamicParameters.Add("@UnitId", (object)unit);
                    dynamicParameters.Add("@DepartmentHeadId", (object)DepartmentHeadId);
                    visitorApproverList = cnn.Query<VisitorApprover>("sp_VisitorRoleWiseApproverLast", (object)dynamicParameters, commandType: new CommandType?(CommandType.StoredProcedure)).ToList<VisitorApprover>();
                }
                return visitorApproverList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool VisitorApproveOrReject(
          int Progress,
          string CommentText,
          int UserID,
          int VisitorRequestId)
        {
            try
            {
                this.accessManager.SqlConnectionOpen(DataBase.SQQeye);
                return this.accessManager.SaveData("sp_visitorApproveOrreject", new List<SqlParameter>()
        {
          new SqlParameter("@Progress", (object) Progress),
          new SqlParameter("@CommentText", (object) CommentText),
          new SqlParameter("@UserID", (object) UserID),
          new SqlParameter("@VisitorRequestId", (object) VisitorRequestId)
        });
            }
            catch (Exception ex)
            {
                this.accessManager.SqlConnectionClose(true);
                throw ex;
            }
            finally
            {
                this.accessManager.SqlConnectionClose();
            }
        }

        public bool CommentSent(int MasterID, int ReviewTo, string ReviewMessage, int UserID)
        {
            try
            {
                this.accessManager.SqlConnectionOpen(DataBase.SQQeye);
                return this.accessManager.SaveData("sp_SentVisitorComment", new List<SqlParameter>()
        {
          new SqlParameter("@MasterID", (object) MasterID),
          new SqlParameter("@ReviewTo", (object) ReviewTo),
          new SqlParameter("@ReviewMessage", (object) ReviewMessage),
          new SqlParameter("@UserID", (object) UserID)
        });
            }
            catch (Exception ex)
            {
                this.accessManager.SqlConnectionClose(true);
                throw ex;
            }
            finally
            {
                this.accessManager.SqlConnectionClose();
            }
        }

        public List<VisitorRequestModel> AllApproveReport(
              string FromDate,
              string ToDate
           )
        {
            List<VisitorRequestModel> visitorApproverList = new List<VisitorRequestModel>();
            try
            {
                using (IDbConnection cnn = (IDbConnection)new SqlConnection(this.connStr))
                {
                    if (cnn.State == ConnectionState.Closed)
                        cnn.Open();
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@FromDate", (object)FromDate);
                    dynamicParameters.Add("@Todate", (object)ToDate);              
                    visitorApproverList = cnn.Query<VisitorRequestModel>("sp_AllApproveReport", (object)dynamicParameters, commandType: new CommandType?(CommandType.StoredProcedure)).ToList<VisitorRequestModel>();
                }
                return visitorApproverList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ArrivedVisitor> ArrivedVisitorList(int VisitorID)
        {
            List<ArrivedVisitor> arrivedVisitorList = new List<ArrivedVisitor>();
            try
            {
                using (IDbConnection cnn = (IDbConnection)new SqlConnection(this.connStr))
                {
                    if (cnn.State == ConnectionState.Closed)
                        cnn.Open();
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@VisitorId", (object)VisitorID);
                    arrivedVisitorList = cnn.Query<ArrivedVisitor>("sp_ArrivedVisitorList", (object)dynamicParameters, commandType: new CommandType?(CommandType.StoredProcedure)).ToList();
                }
                return arrivedVisitorList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}