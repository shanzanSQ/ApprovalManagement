
        using Dapper;
using DocSoOperation.Models;
using Newtonsoft.Json;
using SQIndustryThree.DataManager;
using SQIndustryThree.Models;
using SQIndustryThree.Models.VisitorApproval;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Script.Serialization;

namespace SQIndustryThree.DAL
    {
        public class VisitorDAL_1
        {
            private DataAccessManager accessManager = new DataAccessManager();

            private string connStr = ConfigurationManager.ConnectionStrings["SQQEYEDatabase"].ConnectionString;

            public VisitorDAL_1()
            {
            }

            public bool CommentSent(int MasterID, int ReviewTo, string ReviewMessage, int UserID)
            {
                bool flag;
                try
                {
                    try
                    {
                        this.accessManager.SqlConnectionOpen(DataBase.SQQeye);
                        List<SqlParameter> aParameters = new List<SqlParameter>()
                    {
                        new SqlParameter("@MasterID", (object)MasterID),
                        new SqlParameter("@ReviewTo", (object)ReviewTo),
                        new SqlParameter("@ReviewMessage", ReviewMessage),
                        new SqlParameter("@UserID", (object)UserID)
                    };
                        flag = this.accessManager.SaveData("sp_SentVisitorComment", aParameters, false);
                    }
                    catch (Exception exception)
                    {
                        this.accessManager.SqlConnectionClose(true);
                        throw exception;
                    }
                }
                finally
                {
                    this.accessManager.SqlConnectionClose(false);
                }
                return flag;
            }

            public List<VisitorRequestModel> GetAllRequestInformation(int status, int userId, int frontDesk)
            {
                List<VisitorRequestModel> visitorRequestModels;
                List<VisitorRequestModel> visitorList = new List<VisitorRequestModel>();
                try
                {
                    try
                    {
                        this.accessManager.SqlConnectionOpen(DataBase.SQQeye);
                        List<SqlParameter> aParameters = new List<SqlParameter>()
                    {
                        new SqlParameter("@userId", (object)userId),
                        new SqlParameter("@status", (object)status),
                        new SqlParameter("@frontdesk", (object)frontDesk)
                    };
                        SqlDataReader dr = this.accessManager.GetSqlDataReader("sp_AllVisitorInformationGet", aParameters, false);
                        while (dr.Read())
                        {
                            VisitorRequestModel visitor = new VisitorRequestModel()
                            {
                                RequestorId = (int)dr["RequestorId"],
                                RequestorName = dr["RequestorName"].ToString(),
                                RequestorDepartment = dr["RequestorDepartment"].ToString(),
                                RequestorDesignation = dr["RequestorDesignation"].ToString(),
                                VisitorName = dr["VisitorName"].ToString(),
                                VisitDate = (DateTime)dr["VisitDate"],
                                PurposeOfVisitSQ = dr["RequestorName"].ToString(),
                                BusinessUnitName = dr["BusinessUnitName"].ToString(),
                                VisitorCompany = dr["VisitorCompany"].ToString(),
                                VisitorDesignation = dr["VisitorDesignation"].ToString(),
                                IsApproved = (int)dr["IsApproved"]
                            };
                            visitorList.Add(visitor);
                        }
                        visitorRequestModels = visitorList;
                    }
                    catch (Exception exception)
                    {
                        this.accessManager.SqlConnectionClose(true);
                        throw exception;
                    }
                }
                finally
                {
                    this.accessManager.SqlConnectionClose(false);
                }
                return visitorRequestModels;
            }

            public List<VisitorRequestModel> GetAllVisitorInformation(int status, int userId, int frontdesk)
            {
                List<VisitorRequestModel> visitorRequestModels;
                List<VisitorRequestModel> visitorList = new List<VisitorRequestModel>();
                try
                {
                    try
                    {
                        this.accessManager.SqlConnectionOpen(DataBase.SQQeye);
                        List<SqlParameter> aParameters = new List<SqlParameter>()
                    {
                        new SqlParameter("@userId", (object)userId),
                        new SqlParameter("@status", (object)status),
                        new SqlParameter("@frontdesk", (object)frontdesk)
                    };
                        SqlDataReader dr = this.accessManager.GetSqlDataReader("sp_GetAllVisitorInformation", aParameters, false);
                        while (dr.Read())
                        {
                            VisitorRequestModel visitor = new VisitorRequestModel()
                            {
                                VisitorId = (int)dr["VisitorId"],
                                RequestorName = dr["RequestorName"].ToString(),
                                RequestorDepartment = dr["RequestorDepartment"].ToString(),
                                RequestorDesignation = dr["RequestorDesignation"].ToString(),
                                RequerstorMobile = dr["RequerstorMobile"].ToString(),
                                VisitorName = dr["VisitorName"].ToString(),
                                VisitDate = (DateTime)dr["VisitDate"],
                                PurposeOfVisitSQ = dr["PurposeOfVisitSQ"].ToString(),
                                Chainavisit = dr["Chainavisit"].ToString(),
                                VisitorMobile = dr["VisitorMobile"].ToString(),
                                ApprovedStatus = dr["PendingStatus"].ToString(),
                                IsApproved = (int)dr["Approved"]
                            };
                            visitorList.Add(visitor);
                        }
                        visitorRequestModels = visitorList;
                    }
                    catch (Exception exception)
                    {
                        this.accessManager.SqlConnectionClose(true);
                        throw exception;
                    }
                }
                finally
                {
                    this.accessManager.SqlConnectionClose(false);
                }
                return visitorRequestModels;
            }

            public List<VisitorRequestModel> GetAllVisitorRequest(int UserId, int Status, int Pgress)
            {
                List<VisitorRequestModel> visitorRequestModels;
                List<VisitorRequestModel> users = new List<VisitorRequestModel>();
                try
                {
                    try
                    {
                        this.accessManager.SqlConnectionOpen(DataBase.SQQeye);
                        List<SqlParameter> aList = new List<SqlParameter>()
                    {
                        new SqlParameter("@UserId", (object)UserId),
                        new SqlParameter("@Status", (object)Status),
                        new SqlParameter("@Progress", (object)Pgress)
                    };
                        SqlDataReader dr = this.accessManager.GetSqlDataReader("sp_VisitorGetAllRequest", aList, false);
                        while (dr.Read())
                        {
                            VisitorRequestModel visitorRequest = new VisitorRequestModel()
                            {
                                RequestorId = (int)dr["RequestorId"],
                                RequestorName = dr["RequestorName"].ToString(),
                                VisitDate = (DateTime)dr["VisitDate"],
                                RequestorDepartment = dr["RequestorDepartment"].ToString(),
                                BusinessUnitName = dr["BusinessUnitName"].ToString(),
                                LocationName = dr["LocationName"].ToString(),
                                CategoryName = dr["CategoryName"].ToString(),
                                SubCategroyName = dr["SubCategoryName"].ToString(),
                                TotalVisitor = (int)dr["TotalVisitor"],
                                IsApproved = (int)dr["IsApproved"],
                                Pending = (int)dr["Pending"]
                            };
                            users.Add(visitorRequest);
                        }
                        visitorRequestModels = users;
                    }
                    catch (Exception exception)
                    {
                        throw exception;
                    }
                }
                finally
                {
                    this.accessManager.SqlConnectionClose(false);
                }
                return visitorRequestModels;
            }

            public List<CommonModel> GetApprovers(int unitId)
            {
                List<CommonModel> commonModels;
                try
                {
                    try
                    {
                        this.accessManager.SqlConnectionOpen(DataBase.SQQeye);
                        List<CommonModel> bulist = new List<CommonModel>();
                        List<SqlParameter> aList = new List<SqlParameter>()
                    {
                        new SqlParameter("@bunitId", (object)unitId)
                    };
                        SqlDataReader dr = this.accessManager.GetSqlDataReader("sp_BUnitWiseApproverList", aList, false);
                        while (dr.Read())
                        {
                            CommonModel bul = new CommonModel()
                            {
                                UserId = (int)dr["UserId"],
                                UserName = dr["UserName"].ToString(),
                                BusinessUnitId = (int)dr["BusinessUnitId"],
                                BusinessUnitName = dr["BusinessUnitName"].ToString(),
                                DesignationName = dr["DesignationName"].ToString()
                            };
                            bulist.Add(bul);
                        }
                        commonModels = bulist;
                    }
                    catch (Exception exception)
                    {
                        throw exception;
                    }
                }
                finally
                {
                    this.accessManager.SqlConnectionClose(false);
                }
                return commonModels;
            }

            public List<VisitorApprover> GetApprovers(int category, int subcategory, int unit)
            {
                List<VisitorApprover> visitorApprovers;
                List<VisitorApprover> subCatList = new List<VisitorApprover>();
                try
                {
                    using (IDbConnection con = new SqlConnection(this.connStr))
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        DynamicParameters parameters = new DynamicParameters();
                        DbType? nullable = null;
                        ParameterDirection? nullable1 = null;
                        int? nullable2 = null;
                        byte? nullable3 = null;
                        byte? nullable4 = nullable3;
                        nullable3 = null;
                        parameters.Add("@CategoryID", category, nullable, nullable1, nullable2, nullable4, nullable3);
                        nullable = null;
                        nullable1 = null;
                        nullable2 = null;
                        nullable3 = null;
                        byte? nullable5 = nullable3;
                        nullable3 = null;
                        parameters.Add("@SubCategoryID", subcategory, nullable, nullable1, nullable2, nullable5, nullable3);
                        nullable = null;
                        nullable1 = null;
                        nullable2 = null;
                        nullable3 = null;
                        byte? nullable6 = nullable3;
                        nullable3 = null;
                        parameters.Add("@UnitId", unit, nullable, nullable1, nullable2, nullable6, nullable3);
                        nullable2 = null;
                        subCatList = con.Query<VisitorApprover>("sp_VisitorRoleWiseApprover", parameters, null, true, nullable2, new CommandType?(CommandType.StoredProcedure)).ToList<VisitorApprover>();
                    }
                    visitorApprovers = subCatList;
                }
                catch (Exception exception)
                {
                    throw exception;
                }
                return visitorApprovers;
            }

            public List<CommonModel> GetDepartmentList(int location)
            {
                List<CommonModel> commonModels;
                try
                {
                    try
                    {
                        this.accessManager.SqlConnectionOpen(DataBase.SQQeye);
                        List<CommonModel> bulist = new List<CommonModel>();
                        List<SqlParameter> aList = new List<SqlParameter>()
                    {
                        new SqlParameter("@Location", (object)location)
                    };
                        SqlDataReader dr = this.accessManager.GetSqlDataReader("DepartmentList", aList, false);
                        while (dr.Read())
                        {
                            CommonModel bul = new CommonModel()
                            {
                                DepartmentId = (int)dr["DepartmentId"],
                                DeartmentName = dr["DepartmentName"].ToString()
                            };
                            bulist.Add(bul);
                        }
                        commonModels = bulist;
                    }
                    catch (Exception exception)
                    {
                        throw exception;
                    }
                }
                finally
                {
                    this.accessManager.SqlConnectionClose(false);
                }
                return commonModels;
            }

            public List<CommonModel> GetDepartments()
            {
                List<CommonModel> commonModels;
                try
                {
                    try
                    {
                        this.accessManager.SqlConnectionOpen(DataBase.SQQeye);
                        List<CommonModel> bulist = new List<CommonModel>();
                        SqlDataReader dr = this.accessManager.GetSqlDataReader("sp_GetAllDepartment", false);
                        while (dr.Read())
                        {
                            CommonModel bul = new CommonModel()
                            {
                                DepartmentId = (int)dr["DepartmentId"],
                                DeartmentName = dr["DeartmentName"].ToString()
                            };
                            bulist.Add(bul);
                        }
                        commonModels = bulist;
                    }
                    catch (Exception exception)
                    {
                        throw exception;
                    }
                }
                finally
                {
                    this.accessManager.SqlConnectionClose(false);
                }
                return commonModels;
            }

            public List<VisitorRequestModel> GetFactoryVisitorInformation(int status)
            {
                List<VisitorRequestModel> visitorRequestModels;
                List<VisitorRequestModel> visitorList = new List<VisitorRequestModel>();
                try
                {
                    try
                    {
                        this.accessManager.SqlConnectionOpen(DataBase.SQQeye);
                        List<SqlParameter> aParameters = new List<SqlParameter>()
                    {
                        new SqlParameter("@status", (object)status)
                    };
                        SqlDataReader dr = this.accessManager.GetSqlDataReader("sp_GetFactoryVisitorInformation", aParameters, false);
                        while (dr.Read())
                        {
                            VisitorRequestModel visitor = new VisitorRequestModel()
                            {
                                RequestorId = (int)dr["RequestorId"],
                                VisitorId = (int)dr["VisitorId"],
                                RequestorName = dr["RequestorName"].ToString(),
                                RequestorDepartment = dr["RequestorDepartment"].ToString(),
                                RequestorDesignation = dr["RequestorDesignation"].ToString(),
                                RequerstorMobile = dr["RequerstorMobile"].ToString(),
                                VisitorName = dr["VisitorName"].ToString(),
                                VisitDate = (DateTime)dr["VisitDate"],
                                PurposeOfVisitSQ = dr["PurposeOfVisitSQ"].ToString(),
                                Chainavisit = dr["Chainavisit"].ToString(),
                                NIDorPassport = dr["NIDorPassport"].ToString(),
                                Image = dr["Image"].ToString(),
                                VisitorMobile = dr["VisitorMobile"].ToString(),
                                ApprovedStatus = dr["PendingStatus"].ToString(),
                                IsApproved = (int)dr["Approved"]
                            };
                            visitorList.Add(visitor);
                        }
                        visitorRequestModels = visitorList;
                    }
                    catch (Exception exception)
                    {
                        this.accessManager.SqlConnectionClose(true);
                        throw exception;
                    }
                }
                finally
                {
                    this.accessManager.SqlConnectionClose(false);
                }
                return visitorRequestModels;
            }

            public VisitorRequestModel IndividualRequestShow(int PrimaryKey, int userId)
            {
                VisitorRequestModel visitorRequestModel;
                VisitorRequestModel visitor = new VisitorRequestModel();
                try
                {
                    try
                    {
                        this.accessManager.SqlConnectionOpen(DataBase.SQQeye);
                        List<SqlParameter> aParameters = new List<SqlParameter>()
                    {
                        new SqlParameter("@userId", (object)userId),
                        new SqlParameter("@VisitorId", (object)PrimaryKey)
                    };
                        SqlDataReader dr = this.accessManager.GetSqlDataReader("sp_IndividualRequestView", aParameters, false);
                        while (dr.Read())
                        {
                            visitor.VisitorId = (int)dr["VisitorId"];
                            visitor.RequestorName = dr["RequestorName"].ToString();
                            visitor.RequestorDepartment = dr["RequestorDepartment"].ToString();
                            visitor.RequestorDesignation = dr["RequestorDesignation"].ToString();
                            visitor.RequestorEmail = dr["RequestorEmail"].ToString();
                            visitor.RequerstorMobile = dr["RequerstorMobile"].ToString();
                            visitor.VisitorName = dr["VisitorName"].ToString();
                            visitor.VisitorEmail = dr["VisitorEmail"].ToString();
                            visitor.VisitDate = (DateTime)dr["VisitDate"];
                            visitor.PurposeOfVisitSQ = dr["PurposeOfVisitSQ"].ToString();
                            visitor.VisitorCardNo = dr["VisitorCardNo"].ToString();
                            visitor.GateRemarks = dr["GateRemarks"].ToString();
                            visitor.CheckIn = dr["CheckIn"].ToString();
                            visitor.CheckOut = dr["CheckOut"].ToString();
                            visitor.NIDorPassport = dr["NIDorPassport"].ToString();
                            visitor.VisitorCompany = dr["VisitorCompany"].ToString();
                            visitor.VisitorDesignation = dr["VisitorDesignation"].ToString();
                            visitor.VisitorNationality = dr["VisitorNationality"].ToString();
                            visitor.Chainavisit = dr["Chainavisit"].ToString();
                            visitor.BusinessUnitName = dr["BusinessUnitName"].ToString();
                            visitor.LocationName = dr["LocationName"].ToString();
                            visitor.VisitorMobile = dr["VisitorMobile"].ToString();
                            visitor.ApprovedStatus = dr["PendingStatus"].ToString();
                            visitor.Remarks = dr["Remarks"].ToString();
                            visitor.IsApproved = (int)dr["Approved"];
                        }
                        visitorRequestModel = visitor;
                    }
                    catch (Exception exception)
                    {
                        this.accessManager.SqlConnectionClose(true);
                        throw exception;
                    }
                }
                finally
                {
                    this.accessManager.SqlConnectionClose(false);
                }
                return visitorRequestModel;
            }

            public ResultResponse SaveVisitor(RequestorModel visitor, int UserId)
            {
                ResultResponse resultResponse;
                try
                {
                    int masterId = 0;
                    using (IDbConnection con = new SqlConnection(this.connStr))
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        string visitorList = (new JavaScriptSerializer()).Serialize(visitor.VisitorList);
                        string approverList = (new JavaScriptSerializer()).Serialize(visitor.VisitorApproverList);
                        visitor.VisitorList = null;
                        int count = visitor.VisitorApproverList.Count;
                        visitor.VisitorApproverList = null;
                        (new JavaScriptSerializer()).Serialize(visitor);
                        DynamicParameters parameters = new DynamicParameters();
                        DbType? nullable = null;
                        ParameterDirection? nullable1 = null;
                        int? nullable2 = null;
                        byte? nullable3 = null;
                        byte? nullable4 = nullable3;
                        nullable3 = null;
                        parameters.Add("@BusinessUnitId", visitor.BusinessUnitId, nullable, nullable1, nullable2, nullable4, nullable3);
                        nullable = null;
                        nullable1 = null;
                        nullable2 = null;
                        nullable3 = null;
                        byte? nullable5 = nullable3;
                        nullable3 = null;
                        parameters.Add("@LocationId", visitor.LocationId, nullable, nullable1, nullable2, nullable5, nullable3);
                        nullable = null;
                        nullable1 = null;
                        nullable2 = null;
                        nullable3 = null;
                        byte? nullable6 = nullable3;
                        nullable3 = null;
                        parameters.Add("@CategoryId", visitor.CategoryId, nullable, nullable1, nullable2, nullable6, nullable3);
                        nullable = null;
                        nullable1 = null;
                        nullable2 = null;
                        nullable3 = null;
                        byte? nullable7 = nullable3;
                        nullable3 = null;
                        parameters.Add("@SubCategoryId", visitor.SubCategoryId, nullable, nullable1, nullable2, nullable7, nullable3);
                        nullable = null;
                        nullable1 = null;
                        nullable2 = null;
                        nullable3 = null;
                        byte? nullable8 = nullable3;
                        nullable3 = null;
                        parameters.Add("@RequestorDepartment", visitor.RequestorDepartment, nullable, nullable1, nullable2, nullable8, nullable3);
                        nullable = null;
                        nullable1 = null;
                        nullable2 = null;
                        nullable3 = null;
                        byte? nullable9 = nullable3;
                        nullable3 = null;
                        parameters.Add("@NIDorPassport", visitor.NIDorPassport, nullable, nullable1, nullable2, nullable9, nullable3);
                        nullable = null;
                        nullable1 = null;
                        nullable2 = null;
                        nullable3 = null;
                        byte? nullable10 = nullable3;
                        nullable3 = null;
                        parameters.Add("@ModeOfVisit", visitor.ModeOfVisit, nullable, nullable1, nullable2, nullable10, nullable3);
                        if (visitor.ModeOfVisit != 1)
                        {
                            nullable = null;
                            nullable1 = null;
                            nullable2 = null;
                            nullable3 = null;
                            byte? nullable11 = nullable3;
                            nullable3 = null;
                            parameters.Add("@visitDate", "", nullable, nullable1, nullable2, nullable11, nullable3);
                            nullable = null;
                            nullable1 = null;
                            nullable2 = null;
                            nullable3 = null;
                            byte? nullable12 = nullable3;
                            nullable3 = null;
                            parameters.Add("@StartDate", visitor.StartDate, nullable, nullable1, nullable2, nullable12, nullable3);
                            nullable = null;
                            nullable1 = null;
                            nullable2 = null;
                            nullable3 = null;
                            byte? nullable13 = nullable3;
                            nullable3 = null;
                            parameters.Add("@EndDate", visitor.EndDate, nullable, nullable1, nullable2, nullable13, nullable3);
                        }
                        else
                        {
                            nullable = null;
                            nullable1 = null;
                            nullable2 = null;
                            nullable3 = null;
                            byte? nullable14 = nullable3;
                            nullable3 = null;
                            parameters.Add("@visitDate", visitor.VisitDate, nullable, nullable1, nullable2, nullable14, nullable3);
                            nullable = null;
                            nullable1 = null;
                            nullable2 = null;
                            nullable3 = null;
                            byte? nullable15 = nullable3;
                            nullable3 = null;
                            parameters.Add("@StartDate", "", nullable, nullable1, nullable2, nullable15, nullable3);
                            nullable = null;
                            nullable1 = null;
                            nullable2 = null;
                            nullable3 = null;
                            byte? nullable16 = nullable3;
                            nullable3 = null;
                            parameters.Add("@EndDate", "", nullable, nullable1, nullable2, nullable16, nullable3);
                        }
                        nullable = null;
                        nullable1 = null;
                        nullable2 = null;
                        nullable3 = null;
                        byte? nullable17 = nullable3;
                        nullable3 = null;
                        parameters.Add("@UserId", UserId, nullable, nullable1, nullable2, nullable17, nullable3);
                        nullable = null;
                        nullable1 = null;
                        nullable2 = null;
                        nullable3 = null;
                        byte? nullable18 = nullable3;
                        nullable3 = null;
                        parameters.Add("@Image", visitor.Image, nullable, nullable1, nullable2, nullable18, nullable3);
                        nullable = null;
                        nullable1 = null;
                        nullable2 = null;
                        nullable3 = null;
                        byte? nullable19 = nullable3;
                        nullable3 = null;
                        parameters.Add("@ImagePath", visitor.ImagePath, nullable, nullable1, nullable2, nullable19, nullable3);
                        nullable = null;
                        nullable1 = null;
                        nullable2 = null;
                        nullable3 = null;
                        byte? nullable20 = nullable3;
                        nullable3 = null;
                        parameters.Add("@ApproverJson", approverList, nullable, nullable1, nullable2, nullable20, nullable3);
                        nullable = null;
                        nullable1 = null;
                        nullable2 = null;
                        nullable3 = null;
                        byte? nullable21 = nullable3;
                        nullable3 = null;
                        parameters.Add("@VisitorJosn", visitorList, nullable, nullable1, nullable2, nullable21, nullable3);
                        nullable2 = null;
                        masterId = con.Execute("SP_VisitorDataInsert", parameters, null, nullable2, new CommandType?(CommandType.StoredProcedure));
                        resultResponse = new ResultResponse()
                        {
                            pk = masterId,
                            isSuccess = true
                        };
                    }
                }
                catch (Exception exception)
                {
                    throw exception;
                }
                return resultResponse;
            }

            public bool SaveVistorRequest(RequestorModel visitor, int UserId)
            {
                bool flag;
                bool result = false;
                try
                {
                    try
                    {
                        this.accessManager.SqlConnectionOpen(DataBase.SQQeye);
                        List<SqlParameter> aParameters = new List<SqlParameter>()
                    {
                        new SqlParameter("@BusinessUnitId", (object)visitor.BusinessUnitId),
                        new SqlParameter("@LocationId", (object)visitor.LocationId),
                        new SqlParameter("@RequestorDepartment", visitor.RequestorDepartment),
                        new SqlParameter("@visitDate", (object)visitor.VisitDate),
                        new SqlParameter("@UserId", (object)UserId)
                    };
                        int PrimaryKey = this.accessManager.SaveDataReturnPrimaryKey("sp_SaveVisitorRequestAms", aParameters, false);
                        foreach (VisitorModel visitorModel in visitor.VisitorList)
                        {
                            List<SqlParameter> aprap = new List<SqlParameter>()
                        {
                            new SqlParameter("@RequestorId", (object)PrimaryKey),
                            new SqlParameter("@VisitorName", visitorModel.VisitorName),
                            new SqlParameter("@VisitorEmail", visitorModel.VisitorEmail),
                            new SqlParameter("@VisitorDesignation", visitorModel.VisitorDesignation),
                            new SqlParameter("@VisitorMobile", visitorModel.VisitorMobile),
                            new SqlParameter("@VisitorCompany", visitorModel.VisitorCompany),
                            new SqlParameter("@VisitorNationality", visitorModel.VisitorNationality),
                            new SqlParameter("@PurposeOfVisitSQ", visitorModel.PurposeOfVisitSQ),
                            new SqlParameter("@Chainavisit", visitorModel.Chainavisit),
                            new SqlParameter("@Remarks", visitorModel.Remarks)
                        };
                            result = this.accessManager.SaveData("sp_SaveVisitorDetailsTable", aprap, false);
                        }
                        flag = result;
                    }
                    catch (Exception exception)
                    {
                        this.accessManager.SqlConnectionClose(true);
                        throw exception;
                    }
                }
                finally
                {
                    this.accessManager.SqlConnectionClose(false);
                }
                return flag;
            }

            public int SUbMenuByPermission(int userId)
            {
                int num;
                int result = 0;
                try
                {
                    try
                    {
                        this.accessManager.SqlConnectionOpen(DataBase.SQQeye);
                        List<SqlParameter> aParameters = new List<SqlParameter>()
                    {
                        new SqlParameter("@userID", (object)userId)
                    };
                        SqlDataReader dr = this.accessManager.GetSqlDataReader("sp_VisitorMenuPermission", aParameters, false);
                        while (dr.Read())
                        {
                            result = (int)dr["NOPERMISSION"];
                        }
                        num = result;
                    }
                    catch (Exception exception)
                    {
                        this.accessManager.SqlConnectionClose(true);
                        throw exception;
                    }
                }
                finally
                {
                    this.accessManager.SqlConnectionClose(false);
                }
                return num;
            }

            public dynamic UpadteVisitorCheckinAndCheckOut(int visitorId, string visitorCardNo, string remarks, string checkin, string checkout)
            {
                object obj;
                object user = null;
                try
                {
                    using (IDbConnection con = new SqlConnection(this.connStr))
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        DynamicParameters parameters = new DynamicParameters();
                        DbType? nullable = null;
                        ParameterDirection? nullable1 = null;
                        int? nullable2 = null;
                        byte? nullable3 = null;
                        byte? nullable4 = nullable3;
                        nullable3 = null;
                        parameters.Add("@VisitorId", visitorId, nullable, nullable1, nullable2, nullable4, nullable3);
                        nullable = null;
                        nullable1 = null;
                        nullable2 = null;
                        nullable3 = null;
                        byte? nullable5 = nullable3;
                        nullable3 = null;
                        parameters.Add("@VisitorCardNo", visitorCardNo, nullable, nullable1, nullable2, nullable5, nullable3);
                        nullable = null;
                        nullable1 = null;
                        nullable2 = null;
                        nullable3 = null;
                        byte? nullable6 = nullable3;
                        nullable3 = null;
                        parameters.Add("@GateRemarks", remarks, nullable, nullable1, nullable2, nullable6, nullable3);
                        nullable = null;
                        nullable1 = null;
                        nullable2 = null;
                        nullable3 = null;
                        byte? nullable7 = nullable3;
                        nullable3 = null;
                        parameters.Add("@CheckIn", checkin, nullable, nullable1, nullable2, nullable7, nullable3);
                        nullable = null;
                        nullable1 = null;
                        nullable2 = null;
                        nullable3 = null;
                        byte? nullable8 = nullable3;
                        nullable3 = null;
                        parameters.Add("@CheckOut", checkout, nullable, nullable1, nullable2, nullable8, nullable3);
                        nullable2 = null;
                        user = con.Execute("UpadteVisitorCheckinAndCheckOut", parameters, null, nullable2, new CommandType?(CommandType.StoredProcedure));
                    }
                    obj = user;
                }
                catch (Exception exception)
                {
                    throw exception;
                }
                return obj;
            }

            public ResultResponse UpdateOrReject(int primarykey, int userId, int status)
            {
                ResultResponse resultResponse;
                ResultResponse result = new ResultResponse();
                try
                {
                    try
                    {
                        this.accessManager.SqlConnectionOpen(DataBase.SQQeye);
                        List<SqlParameter> aParameters = new List<SqlParameter>()
                    {
                        new SqlParameter("@userID", (object)userId),
                        new SqlParameter("@IsApproved", (object)status),
                        new SqlParameter("@primaryKey", (object)primarykey)
                    };
                        result.isSuccess = this.accessManager.UpdateData("sp_VisitorApproverOrReject", aParameters, false);
                        resultResponse = result;
                    }
                    catch (Exception exception)
                    {
                        this.accessManager.SqlConnectionClose(true);
                        throw exception;
                    }
                }
                finally
                {
                    this.accessManager.SqlConnectionClose(false);
                }
                return resultResponse;
            }

            public ResultResponse UpdateVisitorRequest(RequestorModel visitor, int UserId)
            {
                ResultResponse resultResponse;
                try
                {
                    int masterId = 0;
                    using (IDbConnection con = new SqlConnection(this.connStr))
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        string visitorList = (new JavaScriptSerializer()).Serialize(visitor.VisitorList);
                        string approverList = (new JavaScriptSerializer()).Serialize(visitor.VisitorApproverList);
                        visitor.VisitorList = null;
                        visitor.VisitorApproverList = null;
                        visitor.VisitorApproverList = null;
                        (new JavaScriptSerializer()).Serialize(visitor);
                        DynamicParameters parameters = new DynamicParameters();
                        DbType? nullable = null;
                        ParameterDirection? nullable1 = null;
                        int? nullable2 = null;
                        byte? nullable3 = null;
                        byte? nullable4 = nullable3;
                        nullable3 = null;
                        parameters.Add("@RequestorId", visitor.RequestorId, nullable, nullable1, nullable2, nullable4, nullable3);
                        nullable = null;
                        nullable1 = null;
                        nullable2 = null;
                        nullable3 = null;
                        byte? nullable5 = nullable3;
                        nullable3 = null;
                        parameters.Add("@visitDate", visitor.VisitDate, nullable, nullable1, nullable2, nullable5, nullable3);
                        nullable = null;
                        nullable1 = null;
                        nullable2 = null;
                        nullable3 = null;
                        byte? nullable6 = nullable3;
                        nullable3 = null;
                        parameters.Add("@UserId", UserId, nullable, nullable1, nullable2, nullable6, nullable3);
                        nullable = null;
                        nullable1 = null;
                        nullable2 = null;
                        nullable3 = null;
                        byte? nullable7 = nullable3;
                        nullable3 = null;
                        parameters.Add("@ApproverJson", approverList, nullable, nullable1, nullable2, nullable7, nullable3);
                        nullable = null;
                        nullable1 = null;
                        nullable2 = null;
                        nullable3 = null;
                        byte? nullable8 = nullable3;
                        nullable3 = null;
                        parameters.Add("@VisitorJosn", visitorList, nullable, nullable1, nullable2, nullable8, nullable3);
                        nullable2 = null;
                        masterId = con.Execute("sp_UpdateVisitorRequest", parameters, null, nullable2, new CommandType?(CommandType.StoredProcedure));
                        resultResponse = new ResultResponse()
                        {
                            pk = masterId,
                            isSuccess = true
                        };
                    }
                }
                catch (Exception exception)
                {
                    throw exception;
                }
                return resultResponse;
            }

            public dynamic UserDetails(int userID)
            {
                object obj;
                object user = null;
                try
                {
                    using (IDbConnection con = new SqlConnection(this.connStr))
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        DynamicParameters parameters = new DynamicParameters();
                        DbType? nullable = null;
                        ParameterDirection? nullable1 = null;
                        int? nullable2 = null;
                        byte? nullable3 = null;
                        byte? nullable4 = nullable3;
                        nullable3 = null;
                        parameters.Add("@userId", userID, nullable, nullable1, nullable2, nullable4, nullable3);
                        nullable2 = null;
                        user = con.Query("sp_RequestDetailById", parameters, null, true, nullable2, new CommandType?(CommandType.StoredProcedure)).FirstOrDefault<object>();
                    }
                    obj = user;
                }
                catch (Exception exception)
                {
                    throw exception;
                }
                return obj;
            }

            public bool VisitorApproveOrReject(int Progress, string CommentText, int UserID, int VisitorRequestId)
            {
                bool flag;
                try
                {
                    try
                    {
                        this.accessManager.SqlConnectionOpen(DataBase.SQQeye);
                        List<SqlParameter> aParameters = new List<SqlParameter>()
                    {
                        new SqlParameter("@Progress", (object)Progress),
                        new SqlParameter("@CommentText", CommentText),
                        new SqlParameter("@UserID", (object)UserID),
                        new SqlParameter("@VisitorRequestId", (object)VisitorRequestId)
                    };
                        flag = this.accessManager.SaveData("sp_visitorApproveOrreject", aParameters, false);
                    }
                    catch (Exception exception)
                    {
                        this.accessManager.SqlConnectionClose(true);
                        throw exception;
                    }
                }
                finally
                {
                    this.accessManager.SqlConnectionClose(false);
                }
                return flag;
            }

            public List<VisitorCategory> VisitorCategories(int unit)
            {
                List<VisitorCategory> visitorCategories;
                List<VisitorCategory> catList = new List<VisitorCategory>();
                try
                {
                    using (IDbConnection con = new SqlConnection(this.connStr))
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        DynamicParameters parameters = new DynamicParameters();
                        DbType? nullable = null;
                        ParameterDirection? nullable1 = null;
                        int? nullable2 = null;
                        byte? nullable3 = null;
                        byte? nullable4 = nullable3;
                        nullable3 = null;
                        parameters.Add("@unitId", unit, nullable, nullable1, nullable2, nullable4, nullable3);
                        nullable2 = null;
                        catList = con.Query<VisitorCategory>("sp_VisitorCategoryList", parameters, null, true, nullable2, new CommandType?(CommandType.StoredProcedure)).ToList<VisitorCategory>();
                    }
                    visitorCategories = catList;
                }
                catch (Exception exception)
                {
                    throw exception;
                }
                return visitorCategories;
            }

            public RequestorModel VisitorDetailsInformation(int masterId, int userId)
            {
                RequestorModel requestorModel;
                RequestorModel visitorRequest = new RequestorModel();
                try
                {
                    try
                    {
                        this.accessManager.SqlConnectionOpen(DataBase.SQQeye);
                        List<SqlParameter> aList = new List<SqlParameter>()
                    {
                        new SqlParameter("@RequestorId", (object)masterId),
                        new SqlParameter("@UserId", (object)userId)
                    };
                        SqlDataReader dr = this.accessManager.GetSqlDataReader("sp_VisitorDetailsInfo", aList, false);
                        while (dr.Read())
                        {
                            visitorRequest.RequestorId = (int)dr["RequestorId"];
                            visitorRequest.RivisionNo = (int)dr["RevisionNo"];
                            visitorRequest.BusinessUnitId = (int)dr["BusinessUnitId"];
                            visitorRequest.BusinessUnitName = dr["BusinessUnitName"].ToString();
                            visitorRequest.LocationId = (int)dr["LocationId"];
                            visitorRequest.LocationName = dr["LocationName"].ToString();
                            visitorRequest.RequestorName = dr["RequestorName"].ToString();
                            visitorRequest.RequestorEmail = dr["RequestorEmail"].ToString();
                            visitorRequest.RequestorDesignation = dr["RequestorDesignation"].ToString();
                            visitorRequest.RequestorDepartment = dr["RequestorDepartment"].ToString();
                            visitorRequest.RequerstorMobile = dr["RequerstorMobile"].ToString();
                            if (visitorRequest.ModeOfVisit != 1)
                            {
                                visitorRequest.StartDate = (DateTime)dr["StartDate"];
                                visitorRequest.EndDate = (DateTime)dr["EndDate"];
                            }
                            else
                            {
                                visitorRequest.VisitDate = (DateTime)dr["VisitDate"];
                            }
                            visitorRequest.IsApproved = (int)dr["IsApproved"];
                            visitorRequest.Created_By = (int)dr["Created_By"];
                            visitorRequest.VisitorApproverList = JsonConvert.DeserializeObject<List<IOUApproverModel>>(dr["VisitorApproverList"].ToString());
                            visitorRequest.VisitorList = JsonConvert.DeserializeObject<List<VisitorModel>>(dr["VisitorList"].ToString());
                            visitorRequest.VisitorComments = JsonConvert.DeserializeObject<List<CommentsTable>>(dr["VisitorComments"].ToString());
                            visitorRequest.VisitorLogSection = JsonConvert.DeserializeObject<List<LogSection>>(dr["VisitorLogsection"].ToString());
                        }
                        requestorModel = visitorRequest;
                    }
                    catch (Exception exception)
                    {
                        this.accessManager.SqlConnectionClose(true);
                        throw exception;
                    }
                }
                finally
                {
                    this.accessManager.SqlConnectionClose(false);
                }
                return requestorModel;
            }

            public List<VisitorSubCategory> VisitorSubCategories(int CatId)
            {
                List<VisitorSubCategory> visitorSubCategories;
                List<VisitorSubCategory> subCatList = new List<VisitorSubCategory>();
                try
                {
                    using (IDbConnection con = new SqlConnection(this.connStr))
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        DynamicParameters parameters = new DynamicParameters();
                        DbType? nullable = null;
                        ParameterDirection? nullable1 = null;
                        int? nullable2 = null;
                        byte? nullable3 = null;
                        byte? nullable4 = nullable3;
                        nullable3 = null;
                        parameters.Add("@CatId", CatId, nullable, nullable1, nullable2, nullable4, nullable3);
                        nullable2 = null;
                        subCatList = con.Query<VisitorSubCategory>("sp_VisitorSubCategoryList", parameters, null, true, nullable2, new CommandType?(CommandType.StoredProcedure)).ToList<VisitorSubCategory>();
                    }
                    visitorSubCategories = subCatList;
                }
                catch (Exception exception)
                {
                    throw exception;
                }
                return visitorSubCategories;
            }
        }
    }
