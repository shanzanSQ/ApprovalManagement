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
using System.Web;
using System.Web.Script.Serialization;

namespace SQIndustryThree.DAL
{
    public class CourierDAL
    {
        private DataAccessManager accessManager = new DataAccessManager();
        private string connStr = ConfigurationManager.ConnectionStrings["SQQEYEDatabase"].ConnectionString;
     
        public List<BusinessUnit> GetBusinessUnits()
        {

            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<BusinessUnit> businessUnits = new List<BusinessUnit>();
                List<SqlParameter> aList = new List<SqlParameter>();
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetCourierAllBusinessUnit");
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
        public List<CommonModel> GetDepartmentList(int location)
        {
            try
            {
                this.accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<CommonModel> commonModelList = new List<CommonModel>();
                SqlDataReader sqlDataReader = this.accessManager.GetSqlDataReader("CourierDepartmentList", new List<SqlParameter>()
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
        public ResultResponse SaveCourierRequest(CourierRequestModel courierRequestModel, int UserId)
        {
            try
            {
                using (IDbConnection cnn = (IDbConnection)new SqlConnection(this.connStr))
                {
                    if (cnn.State == ConnectionState.Closed)
                        cnn.Open();
                    string str1 = new JavaScriptSerializer().Serialize((object)courierRequestModel.CourierApproverList);
                    int count = courierRequestModel.CourierApproverList.Count;
                    courierRequestModel.CourierApproverList = (List<CourierApproverModel>)null;
                     new JavaScriptSerializer().Serialize((object)courierRequestModel);
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@RequestorId", (object)UserId);
                    dynamicParameters.Add("@SQId", (object)courierRequestModel.LocationId);
                    dynamicParameters.Add("@SQUnitId", (object)courierRequestModel.BusinessUnitId);
                    dynamicParameters.Add("@SQDepartmentId", (object)courierRequestModel.DepartmentID);
                    dynamicParameters.Add("@CourierType", (object)courierRequestModel.CourierType);
                    dynamicParameters.Add("@Customer", (object)courierRequestModel.Customer);
                    dynamicParameters.Add("@Receiver", (object)courierRequestModel.Receiver);
                    dynamicParameters.Add("@Title", (object)courierRequestModel.Title);
                    dynamicParameters.Add("@ContactNo", (object)courierRequestModel.ContactNo);
                    dynamicParameters.Add("@Country", (object)courierRequestModel.Country);
                    dynamicParameters.Add("@PostCode", (object)courierRequestModel.PostCode);
                    dynamicParameters.Add("@Address", (object)courierRequestModel.Address);
                    dynamicParameters.Add("@DispatchDate", (object)courierRequestModel.DispatchDate);
                    dynamicParameters.Add("@DeliveryDate", (object)courierRequestModel.Deliverydate);
                    dynamicParameters.Add("@ProductDescription", (object)courierRequestModel.ProductDescription);
                    dynamicParameters.Add("@Weight", (object)courierRequestModel.Weight);
                    dynamicParameters.Add("@Volume", (object)courierRequestModel.Volume);
                    //dynamicParameters.Add("@AirwayBillno", (object)courierRequestModel.AirwayBillno);
                    dynamicParameters.Add("@Courier", (object)courierRequestModel.Courier);
                    dynamicParameters.Add("@ProposedDate", (object)courierRequestModel.ProposedDate);
                    dynamicParameters.Add("@Cost", (object)courierRequestModel.Cost);
                    dynamicParameters.Add("@GenerateCourier", (object)courierRequestModel.GenerateCourier);
                    dynamicParameters.Add("@GenerateProposedDate", (object)courierRequestModel.GenerateProposedDate);
                    dynamicParameters.Add("@GenerateCost", (object)courierRequestModel.GenerateCost);
                    dynamicParameters.Add("@Remarks", (object)courierRequestModel.Remarks);
                    dynamicParameters.Add("@ApproverJson", (object)str1);
                    //dynamicParameters.Add("@UserId", (object)UserId);

                    int num = cnn.Execute("sp_CourierRequestDetailsEntryTable", (object)dynamicParameters, commandType: new CommandType?(CommandType.StoredProcedure));
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
        public ResultResponse UpdateCourierRequest(CourierRequestModel courierRequestModel, int UserId)
        {
            try
            {
                using (IDbConnection cnn = (IDbConnection)new SqlConnection(this.connStr))
                {
                    if (cnn.State == ConnectionState.Closed)
                        cnn.Open();
                    string str1 = new JavaScriptSerializer().Serialize((object)courierRequestModel.CourierApproverList);
                    int count = courierRequestModel.CourierApproverList.Count;
                    courierRequestModel.CourierApproverList = (List<CourierApproverModel>)null;
                    new JavaScriptSerializer().Serialize((object)courierRequestModel);
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@CourierRRequestId", (object)courierRequestModel.CourierRequestId);
                    dynamicParameters.Add("@RequestorId", (object)UserId);
                    dynamicParameters.Add("@SQId", (object)courierRequestModel.LocationId);
                    dynamicParameters.Add("@SQUnitId", (object)courierRequestModel.BusinessUnitId);
                    dynamicParameters.Add("@SQDepartmentId", (object)courierRequestModel.DepartmentID);
                    dynamicParameters.Add("@CourierType", (object)courierRequestModel.CourierType);
                    dynamicParameters.Add("@Customer", (object)courierRequestModel.Customer);
                    dynamicParameters.Add("@Receiver", (object)courierRequestModel.Receiver);
                    dynamicParameters.Add("@Title", (object)courierRequestModel.Title);
                    dynamicParameters.Add("@ContactNo", (object)courierRequestModel.ContactNo);
                    dynamicParameters.Add("@Country", (object)courierRequestModel.Country);
                    dynamicParameters.Add("@PostCode", (object)courierRequestModel.PostCode);
                    dynamicParameters.Add("@Address", (object)courierRequestModel.Address);
                    dynamicParameters.Add("@DispatchDate", (object)courierRequestModel.DispatchDate);
                    dynamicParameters.Add("@DeliveryDate", (object)courierRequestModel.Deliverydate);
                    dynamicParameters.Add("@ProductDescription", (object)courierRequestModel.ProductDescription);
                    dynamicParameters.Add("@Weight", (object)courierRequestModel.Weight);
                    dynamicParameters.Add("@Volume", (object)courierRequestModel.Volume);
                    //dynamicParameters.Add("@AirwayBillno", (object)courierRequestModel.AirwayBillno);
                    dynamicParameters.Add("@Courier", (object)courierRequestModel.Courier);
                    dynamicParameters.Add("@ProposedDate", (object)courierRequestModel.ProposedDate);
                    dynamicParameters.Add("@Cost", (object)courierRequestModel.Cost);
                    dynamicParameters.Add("@GenerateCourier", (object)courierRequestModel.GenerateCourier);
                    dynamicParameters.Add("@GenerateProposedDate", (object)courierRequestModel.GenerateProposedDate);
                    dynamicParameters.Add("@GenerateCost", (object)courierRequestModel.GenerateCost);
                    dynamicParameters.Add("@Remarks", (object)courierRequestModel.Remarks);
                    dynamicParameters.Add("@ApproverJson", (object)str1);
                    //dynamicParameters.Add("@UserId", (object)UserId);

                    int num = cnn.Execute("sp_CourierRequestDetailsUpdate", (object)dynamicParameters, commandType: new CommandType?(CommandType.StoredProcedure));
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
        public List<CourierRequestModel> GetCourierProposedDate(string country, string delivery_date, string weight,string type)
        {
            try
            {
                this.accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<CourierRequestModel> CourierRequestModelList = new List<CourierRequestModel>();
                SqlDataReader sqlDataReader = this.accessManager.GetSqlDataReader("sp_CourierProposedDate", new List<SqlParameter>()
        {
            new SqlParameter("@country", (object) country),
            new SqlParameter("@weightRange", (object) weight),
            new SqlParameter("@deliverDate", (object) delivery_date),
            new SqlParameter("@type", (object) type)
        });
                while (sqlDataReader.Read())
                    CourierRequestModelList.Add(new CourierRequestModel()
                    {
                        CourierTypeId = sqlDataReader["CourierTypeId"].ToString(),
                        ServiceProvider = sqlDataReader["ServiceProvider"].ToString(),
                        Rate = sqlDataReader["Rate"].ToString(),
                        CourierProposedDate = sqlDataReader["CourierProposedDate"].ToString(),
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
        public List<CourierRequestModel> GetcourierWiseCostDate(string courier, string delivery_date, string weight, string type)
        {
            try
            {
                this.accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<CourierRequestModel> CourierRequestModelList = new List<CourierRequestModel>();
                SqlDataReader sqlDataReader = this.accessManager.GetSqlDataReader("sp_CourierWiseCostDate", new List<SqlParameter>()
        {
          new SqlParameter("@courier", (object) courier),
           new SqlParameter("@weightRange", (object) weight),
            new SqlParameter("@deliverDate", (object) delivery_date),
             new SqlParameter("@type", (object) type)
        });
                while (sqlDataReader.Read())
                    CourierRequestModelList.Add(new CourierRequestModel()
                    {
                        //CourierTypeId = sqlDataReader["CourierTypeId"].ToString(),
                        //ServiceProvider = sqlDataReader["ServiceProvider"].ToString(),
                        Rate = sqlDataReader["Rate"].ToString(),
                        CourierProposedDate = sqlDataReader["CourierProposedDate"].ToString(),
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
        public List<CourierRequestModel> GetproposedDateWisecourierCostDate(string country, string delivery_date, string weight, string type,string proposed_date)
        {
            try
            {
                this.accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<CourierRequestModel> CourierRequestModelList = new List<CourierRequestModel>();
                SqlDataReader sqlDataReader = this.accessManager.GetSqlDataReader("sp_ProposedDateCourier", new List<SqlParameter>()
        {
          new SqlParameter("@country", (object) country),
           new SqlParameter("@weightRange", (object) weight),
            new SqlParameter("@deliverDate", (object) delivery_date),
             new SqlParameter("@type", (object) type),
              new SqlParameter("@proposedDate", (object) proposed_date)
        });
                while (sqlDataReader.Read())
                    CourierRequestModelList.Add(new CourierRequestModel()
                    {
                        CourierTypeId = sqlDataReader["CourierTypeId"].ToString(),
                        ServiceProvider = sqlDataReader["ServiceProvider"].ToString(),
                        Rate = sqlDataReader["Rate"].ToString(),
                        CourierProposedDate = sqlDataReader["CourierProposedDate"].ToString(),
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
        public List<CourierRequestModel> GetcourierWiseConsolidateCostDate(string Courier, string CountryName, string Weight)
        {
            try
            {
                this.accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<CourierRequestModel> CourierRequestModelList = new List<CourierRequestModel>();
                SqlDataReader sqlDataReader = this.accessManager.GetSqlDataReader("sp_DispatchCourierConsolidateCost", new List<SqlParameter>()
        {
          new SqlParameter("@country", (object) CountryName),
          new SqlParameter("@courier", (object) Courier),
           new SqlParameter("@weightRange", (object) Weight)
            
        });
                while (sqlDataReader.Read())
                    CourierRequestModelList.Add(new CourierRequestModel()
                    {
                        
                        ServiceProvider = sqlDataReader["ServiceProvider"].ToString(),
                        Rate = sqlDataReader["Rate"].ToString(),
                        
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
        public List<CourierRequestModel> GetAllCourierRequest(
         int UserId,
         int Status,
         int Pgress)
        {
            List<CourierRequestModel> CourierRequestModelList = new List<CourierRequestModel>();
            try
            {
                this.accessManager.SqlConnectionOpen(DataBase.SQQeye);
                SqlDataReader sqlDataReader = this.accessManager.GetSqlDataReader("sp_CourierGetAllRequest", new List<SqlParameter>()
        {
          new SqlParameter("@UserId", (object) UserId),
          new SqlParameter("@Status", (object) Status),
          new SqlParameter("@Progress", (object) Pgress)
        });
                while (sqlDataReader.Read())
                    CourierRequestModelList.Add(new CourierRequestModel()
                    {
                        CourierRequestId = (int)sqlDataReader["CourierRequestId"],
                        //LocationId = (int)sqlDataReader["LocationId"],
                        //BusinessUnitId = (int)sqlDataReader["BusinessUnitId"],
                        //DepartmentID = (int)sqlDataReader["DepartmentID"],
                        Customer = sqlDataReader["Customer"].ToString(),
                        BuyerName = sqlDataReader["BuyerName"].ToString(),
                        Receiver = sqlDataReader["Receiver"].ToString(),
                        Country = sqlDataReader["Country"].ToString(),
                        CountryName = sqlDataReader["CountryName"].ToString(),
                        PostCode = sqlDataReader["PostCode"].ToString(),
                        Address = sqlDataReader["Address"].ToString(),
                        DispatchDate = sqlDataReader["DispatchDate"].ToString(),
                        Deliverydate = sqlDataReader["Deliverydate"].ToString(),
                        ProductDescription = sqlDataReader["ProductDescription"].ToString(),
                        Weight = sqlDataReader["Weight"].ToString(),
                        Volume = sqlDataReader["Volume"].ToString(),
                        AirwayBillno = sqlDataReader["AirwayBillno"].ToString(),
                        Courier = sqlDataReader["Courier"].ToString(),
                        ServiceProvider = sqlDataReader["ServiceProvider"].ToString(),
                        ProposedDate = sqlDataReader["ProposedDate"].ToString(),
                        Cost = sqlDataReader["Cost"].ToString(),
                        Remarks = sqlDataReader["Remarks"].ToString(),
                        IsApproved = (int)sqlDataReader["IsApproved"],
                        Pending = (int)sqlDataReader["Pending"],
                        DateOfRequest = sqlDataReader["DateOfRequest"].ToString(),
                        UserName = sqlDataReader["UserName"].ToString(),
                         //Status = (int)sqlDataReader["Status"]

                        //CreateDate = sqlDataReader["CreateDate"].ToString(),

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
        public List<CourierRequestModel> GetAllCourierDispatch(
        int Status
        )
        {
            List<CourierRequestModel> CourierRequestModelList = new List<CourierRequestModel>();
            try
            {
                this.accessManager.SqlConnectionOpen(DataBase.SQQeye);
                SqlDataReader sqlDataReader = this.accessManager.GetSqlDataReader("sp_GetCourierDispatchList", new List<SqlParameter>()
        {
         
          new SqlParameter("@status", (object) Status),
         
        });
                while (sqlDataReader.Read())
                    CourierRequestModelList.Add(new CourierRequestModel()
                    {
                        CourierDispatchNo = (int)sqlDataReader["CourierDispatchNo"],
                        PostCode = sqlDataReader["PostCode"].ToString(),
                        Country = sqlDataReader["Country"].ToString(),
                        Courier = sqlDataReader["Courier"].ToString(),
                        CourierNumber = sqlDataReader["CourierNumber"].ToString(),
                        DispatchDate = sqlDataReader["DispatchDate"].ToString(),
                        AirwayBillno = sqlDataReader["AirwayBillno"].ToString(),
                        ConsolidateCost = sqlDataReader["ConsolidateCost"].ToString(),
                        ConsolidateWeight = sqlDataReader["ConsolidateWeight"].ToString(),
                        ReferenceNo = sqlDataReader["ReferenceNo"].ToString(),
                        Remarks = sqlDataReader["Remarks"].ToString(),
                        
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
        public List<CourierRequestModel> GetAllCourierReceived(
       int Status
       )
        {
            List<CourierRequestModel> CourierRequestModelList = new List<CourierRequestModel>();
            try
            {
                this.accessManager.SqlConnectionOpen(DataBase.SQQeye);
                SqlDataReader sqlDataReader = this.accessManager.GetSqlDataReader("sp_GetCourierDispatchList", new List<SqlParameter>()
        {

          new SqlParameter("@status", (object) Status),

        });
                while (sqlDataReader.Read())
                    CourierRequestModelList.Add(new CourierRequestModel()
                    {
                        CourierRequestId = (int)sqlDataReader["CourierRequestId"],
                        CourierNumber = sqlDataReader["CourierNumber"].ToString(),
                        AirwayBillno = sqlDataReader["AirwayBillno"].ToString(),
                        ReceivedDate = sqlDataReader["ReceivedDate"].ToString(),
                        ReceivedWeight = sqlDataReader["ReceivedWeight"].ToString(),
                        HandOverTo = sqlDataReader["HandOverTo"].ToString(),
                        ReferenceNo = sqlDataReader["ReferenceNo"].ToString(),
                       Remarks = sqlDataReader["Remarks"].ToString(),

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
        public List<VisitorApprover> GetApprovers(
            int userId,
            int status
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
                    dynamicParameters.Add("@UserId", (object)userId);
                    dynamicParameters.Add("@Status", (object)status);
                    visitorApproverList = cnn.Query<VisitorApprover>("sp_CourierRoleWiseApproverList", (object)dynamicParameters, commandType: new CommandType?(CommandType.StoredProcedure)).ToList<VisitorApprover>();
                }
                return visitorApproverList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<VisitorApprover> GetApproverlistnew(
           int userId,
           int status
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
                    dynamicParameters.Add("@UserId", (object)userId);
                    dynamicParameters.Add("@Status", (object)status);
                    visitorApproverList = cnn.Query<VisitorApprover>("sp_CourierRoleWiseApproverList", (object)dynamicParameters, commandType: new CommandType?(CommandType.StoredProcedure)).ToList<VisitorApprover>();
                }
                return visitorApproverList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Country> GetCountry(string type)
        {
            try
            {
                this.accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<Country> countrys = new List<Country>();
                SqlDataReader sqlDataReader = this.accessManager.GetSqlDataReader("sp_GetAllCountry", new List<SqlParameter>()
        {
            new SqlParameter("@type", (object) type)

        });
                while (sqlDataReader.Read())
                    countrys.Add(new Country()
                    {
                        CountryId = (int)sqlDataReader["CountryId"],
                        CountryName = sqlDataReader["CountryName"].ToString(),

                    });
                return countrys;
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
        //public List<Country> GetCountry(string type)
        //{

        //    try
        //    {
        //        accessManager.SqlConnectionOpen(DataBase.SQQeye);
        //        List<Country> countrys = new List<Country>();
        //        List<SqlParameter> aList = new List<SqlParameter>();
        //        aList.Add(new SqlParameter("@type", type));
        //        SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetAllCountry");
        //        while (dr.Read())
        //        {
        //            Country country = new Country();
        //            country.CountryId = (int)dr["CountryId"];
        //            country.CountryName = dr["CountryName"].ToString();
        //            countrys.Add(country);
        //        }
        //        return countrys;
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
        public List<Country> GetCourier()
        {

            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<Country> countrys = new List<Country>();
                List<SqlParameter> aList = new List<SqlParameter>();
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetAllCourier");
                while (dr.Read())
                {
                    Country country = new Country();
                    country.ServiceProviderId = (int)dr["ServiceProviderId"];
                    country.ServiceProviderName = dr["ServiceProviderName"].ToString();
                    countrys.Add(country);
                }
                return countrys;
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
        public List<Country> GetCustomer()
        {

            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<Country> countrys = new List<Country>();
                List<SqlParameter> aList = new List<SqlParameter>();
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_getAllBuyer");
                while (dr.Read())
                {
                    Country country = new Country();
                    country.BuyerId = (int)dr["BuyerId"];
                    country.BuyerName = dr["BuyerName"].ToString();
                    countrys.Add(country);
                }
                return countrys;
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
        
        public CourierRequestModel CourierDetailsInformation(int masterId, int userId)
        {
            CourierRequestModel CourierRequestModel = new CourierRequestModel();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@CourierRequestorId", masterId));
                aList.Add(new SqlParameter("@UserId", userId));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_CourierDetailsInfo", aList);
                while (dr.Read())
                {
                    CourierRequestModel.CourierRequestId = (int)dr["CourierRequestId"];
                    CourierRequestModel.Customer = dr["Customer"].ToString();
                    CourierRequestModel.BuyerName = dr["BuyerName"].ToString();
                    CourierRequestModel.Receiver = dr["Receiver"].ToString();
                    CourierRequestModel.Receiver = dr["Receiver"].ToString();
                    CourierRequestModel.Title = dr["Title"].ToString();
                    CourierRequestModel.ContactNo = dr["ContactNo"].ToString();
                    CourierRequestModel.PostCode = dr["PostCode"].ToString();
                    CourierRequestModel.Address = dr["Address"].ToString();
                    CourierRequestModel.BusinessUnitName = dr["BusinessUnitName"].ToString();
                    CourierRequestModel.LocationName = dr["LocationName"].ToString();
                    CourierRequestModel.DepartmentName = dr["DepartmentName"].ToString();
                    CourierRequestModel.DepartmentID = (int)dr["DepartmentId"];
                    CourierRequestModel.BusinessUnitId = (int)dr["BusinessUnitId"];
                    CourierRequestModel.LocationId = (int)dr["LocationId"];
                    CourierRequestModel.CourierType = dr["CourierType"].ToString();
                    CourierRequestModel.DispatchDate = dr["DispatchDate"].ToString();
                    CourierRequestModel.Deliverydate = dr["Deliverydate"].ToString();
                    CourierRequestModel.ProductDescription = dr["ProductDescription"].ToString();
                    CourierRequestModel.IsApproved = (int)dr["IsApproved"];
                    CourierRequestModel.UserName = dr["UserName"].ToString();
                    CourierRequestModel.Weight = dr["Weight"].ToString();
                    CourierRequestModel.Volume = dr["Volume"].ToString();
                    CourierRequestModel.AirwayBillno = dr["AirwayBillno"].ToString();
                    CourierRequestModel.Courier = dr["Courier"].ToString();
                    CourierRequestModel.Cost = dr["Cost"].ToString();
                    CourierRequestModel.ProposedDate = dr["ProposedDate"].ToString();
                    CourierRequestModel.Remarks = dr["Remarks"].ToString();
                    CourierRequestModel.IsApproved = (int)dr["IsApproved"];
                    CourierRequestModel.Country = dr["Country"].ToString();
                    CourierRequestModel.CountryName = dr["CountryName"].ToString();
                    CourierRequestModel.ServiceProvider = dr["ServiceProvider"].ToString();
                    CourierRequestModel.GenerateCourier = dr["GenerateCourier"].ToString();
                    CourierRequestModel.GenerateCourierName = dr["GenerateCourierName"].ToString();
                    CourierRequestModel.GenerateProposedDate = dr["GenerateProposedDate"].ToString();
                    CourierRequestModel.GenerateCost = dr["GenerateCost"].ToString();
                    CourierRequestModel.DateOfRequest = dr["DateOfRequest"].ToString();
                    CourierRequestModel.CourierApproverList = JsonConvert.DeserializeObject<List<CourierApproverModel>>(dr["CourierApproverList"].ToString());
                    CourierRequestModel.CourierLogSection = JsonConvert.DeserializeObject<List<LogSection>>(dr["CourierLogSection"].ToString());
                    CourierRequestModel.CourierComments = JsonConvert.DeserializeObject<List<CommentsTable>>(dr["CourierComments"].ToString());
                }
                return CourierRequestModel;
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
        public CourierRequestModel CourierDispatchedRequestDetails(int masterId, int userId)
        {
            CourierRequestModel CourierRequestModel = new CourierRequestModel();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@CourierRequestorId", masterId));
                aList.Add(new SqlParameter("@UserId", userId));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_CourierDispatchDetailsInfo", aList);
                while (dr.Read())
                {
                    CourierRequestModel.CourierRequestId = (int)dr["CourierRequestId"];
                    CourierRequestModel.CourierDispatchNo = (int)dr["CourierDispatchNo"];
                    CourierRequestModel.CourierNumber = dr["CourierNumber"].ToString();
                    CourierRequestModel.ReferenceNo = dr["ReferenceNo"].ToString();
                    CourierRequestModel.FontDispatchDate = dr["FontDispatchDate"].ToString();
                    CourierRequestModel.FontAirwayBillno = dr["FontAirwayBillno"].ToString();
                    CourierRequestModel.FontRemarks = dr["FontRemarks"].ToString();
                    CourierRequestModel.Customer = dr["Customer"].ToString();
                    CourierRequestModel.BuyerName = dr["BuyerName"].ToString();
                    CourierRequestModel.Receiver = dr["Receiver"].ToString();
                    CourierRequestModel.Receiver = dr["Receiver"].ToString();
                    CourierRequestModel.Title = dr["Title"].ToString();
                    CourierRequestModel.ContactNo = dr["ContactNo"].ToString();
                    CourierRequestModel.PostCode = dr["PostCode"].ToString();
                    CourierRequestModel.Address = dr["Address"].ToString();
                    CourierRequestModel.BusinessUnitName = dr["BusinessUnitName"].ToString();
                    CourierRequestModel.LocationName = dr["LocationName"].ToString();
                    CourierRequestModel.DepartmentName = dr["DepartmentName"].ToString();
                    CourierRequestModel.DepartmentID = (int)dr["DepartmentId"];
                    CourierRequestModel.BusinessUnitId = (int)dr["BusinessUnitId"];
                    CourierRequestModel.LocationId = (int)dr["LocationId"];
                    CourierRequestModel.DispatchDate = dr["DispatchDate"].ToString();
                    CourierRequestModel.Deliverydate = dr["Deliverydate"].ToString();
                    CourierRequestModel.ProductDescription = dr["ProductDescription"].ToString();
                    CourierRequestModel.IsApproved = (int)dr["IsApproved"];
                    CourierRequestModel.UserName = dr["UserName"].ToString();
                    CourierRequestModel.Weight = dr["Weight"].ToString();
                    CourierRequestModel.Volume = dr["Volume"].ToString();
                    CourierRequestModel.AirwayBillno = dr["AirwayBillno"].ToString();
                    CourierRequestModel.Courier = dr["Courier"].ToString();
                    CourierRequestModel.Cost = dr["Cost"].ToString();
                    CourierRequestModel.ProposedDate = dr["ProposedDate"].ToString();
                    CourierRequestModel.Remarks = dr["Remarks"].ToString();
                    CourierRequestModel.IsApproved = (int)dr["IsApproved"];
                    CourierRequestModel.CountryName = dr["CountryName"].ToString();
                    CourierRequestModel.ServiceProvider = dr["ServiceProvider"].ToString();
                    CourierRequestModel.GenerateCourier = dr["GenerateCourier"].ToString();
                    CourierRequestModel.GenerateCourierName = dr["GenerateCourierName"].ToString();
                    CourierRequestModel.GenerateProposedDate = dr["GenerateProposedDate"].ToString();
                    CourierRequestModel.GenerateCost = dr["GenerateCost"].ToString();
                    CourierRequestModel.DateOfRequest = dr["DateOfRequest"].ToString();
                    CourierRequestModel.CourierApproverList = JsonConvert.DeserializeObject<List<CourierApproverModel>>(dr["CourierApproverList"].ToString());
                    CourierRequestModel.CourierLogSection = JsonConvert.DeserializeObject<List<LogSection>>(dr["CourierLogSection"].ToString());
                    CourierRequestModel.CourierComments = JsonConvert.DeserializeObject<List<CommentsTable>>(dr["CourierComments"].ToString());
                }
                return CourierRequestModel;
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
        public CourierRequestModel CourierReceivedRequestDetails(int masterId, int userId)
        {
            CourierRequestModel CourierRequestModel = new CourierRequestModel();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@CourierRequestorId", masterId));
                aList.Add(new SqlParameter("@UserId", userId));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_CourierReceivedDetailsInfo", aList);
                while (dr.Read())
                {
                    CourierRequestModel.CourierRequestId = (int)dr["CourierRequestId"];
                    CourierRequestModel.CourierNumber = dr["CourierNumber"].ToString();
                    CourierRequestModel.FontAirwayBillno = dr["FontAirwayBillno"].ToString();
                    CourierRequestModel.FontReceivedDate = dr["FontReceivedDate"].ToString();
                    CourierRequestModel.ReceivedWeight = dr["ReceivedWeight"].ToString();
                    CourierRequestModel.HandOverTo = dr["HandOverTo"].ToString();
                    CourierRequestModel.ReferenceNo = dr["ReferenceNo"].ToString();
                    CourierRequestModel.FontRemarks = dr["FontRemarks"].ToString();
                    CourierRequestModel.Customer = dr["Customer"].ToString();
                    CourierRequestModel.BuyerName = dr["BuyerName"].ToString();
                    CourierRequestModel.Receiver = dr["Receiver"].ToString();
                    CourierRequestModel.Title = dr["Title"].ToString();
                    CourierRequestModel.ContactNo = dr["ContactNo"].ToString();
                    CourierRequestModel.PostCode = dr["PostCode"].ToString();
                    CourierRequestModel.Address = dr["Address"].ToString();
                    CourierRequestModel.BusinessUnitName = dr["BusinessUnitName"].ToString();
                    CourierRequestModel.LocationName = dr["LocationName"].ToString();
                    CourierRequestModel.DepartmentName = dr["DepartmentName"].ToString();
                    CourierRequestModel.DepartmentID = (int)dr["DepartmentId"];
                    CourierRequestModel.BusinessUnitId = (int)dr["BusinessUnitId"];
                    CourierRequestModel.LocationId = (int)dr["LocationId"];
                    CourierRequestModel.DispatchDate = dr["DispatchDate"].ToString();
                    CourierRequestModel.Deliverydate = dr["Deliverydate"].ToString();
                    CourierRequestModel.ProductDescription = dr["ProductDescription"].ToString();
                    CourierRequestModel.IsApproved = (int)dr["IsApproved"];
                    CourierRequestModel.UserName = dr["UserName"].ToString();
                    CourierRequestModel.Weight = dr["Weight"].ToString();
                    CourierRequestModel.Volume = dr["Volume"].ToString();
                    CourierRequestModel.AirwayBillno = dr["AirwayBillno"].ToString();
                    CourierRequestModel.Courier = dr["Courier"].ToString();
                    CourierRequestModel.Cost = dr["Cost"].ToString();
                    CourierRequestModel.ProposedDate = dr["ProposedDate"].ToString();
                    CourierRequestModel.Remarks = dr["Remarks"].ToString();
                    CourierRequestModel.IsApproved = (int)dr["IsApproved"];
                    CourierRequestModel.CountryName = dr["CountryName"].ToString();
                    CourierRequestModel.ServiceProvider = dr["ServiceProvider"].ToString();
                    CourierRequestModel.GenerateCourier = dr["GenerateCourier"].ToString();
                    CourierRequestModel.GenerateCourierName = dr["GenerateCourierName"].ToString();
                    CourierRequestModel.GenerateProposedDate = dr["GenerateProposedDate"].ToString();
                    CourierRequestModel.GenerateCost = dr["GenerateCost"].ToString();
                    CourierRequestModel.DateOfRequest = dr["DateOfRequest"].ToString();
                    CourierRequestModel.CourierApproverList = JsonConvert.DeserializeObject<List<CourierApproverModel>>(dr["CourierApproverList"].ToString());
                    CourierRequestModel.CourierLogSection = JsonConvert.DeserializeObject<List<LogSection>>(dr["CourierLogSection"].ToString());
                    CourierRequestModel.CourierComments = JsonConvert.DeserializeObject<List<CommentsTable>>(dr["CourierComments"].ToString());
                }
                return CourierRequestModel;
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

        public List<CourierRequestModel> CourierDispatchDetailsInformation(
      int masterId
     )
        {
            List<CourierRequestModel> CourierRequestModelList = new List<CourierRequestModel>();
            try
            {
                this.accessManager.SqlConnectionOpen(DataBase.SQQeye);
                SqlDataReader sqlDataReader = this.accessManager.GetSqlDataReader("sp_GetCourierDispatchDetailsList", new List<SqlParameter>()
        {
          new SqlParameter("@courierDispatchNo", (object) masterId)
         
        });
                while (sqlDataReader.Read())
                    CourierRequestModelList.Add(new CourierRequestModel()
                    {
                        CourierDispatchNo = (int)sqlDataReader["CourierDispatchNo"],
                        CourierRequestId = (int)sqlDataReader["CourierRequestId"],
                        Customer = sqlDataReader["Customer"].ToString(),
                        BuyerName = sqlDataReader["BuyerName"].ToString(),
                        Receiver = sqlDataReader["Receiver"].ToString(),
                        Title = sqlDataReader["Title"].ToString(),
                        ContactNo = sqlDataReader["ContactNo"].ToString(),
                        PostCode = sqlDataReader["PostCode"].ToString(),
                        Address = sqlDataReader["Address"].ToString(),
                        BusinessUnitName = sqlDataReader["BusinessUnitName"].ToString(),
                        LocationName = sqlDataReader["LocationName"].ToString(),
                        DepartmentName = sqlDataReader["DepartmentName"].ToString(),
                        DepartmentID = (int)sqlDataReader["DepartmentID"],
                        BusinessUnitId = (int)sqlDataReader["BusinessUnitId"],
                        LocationId = (int)sqlDataReader["LocationId"],
                        DispatchDate = sqlDataReader["DispatchDate"].ToString(),
                        Deliverydate = sqlDataReader["Deliverydate"].ToString(),
                        ProductDescription = sqlDataReader["ProductDescription"].ToString(),
                        IsApproved = (int)sqlDataReader["IsApproved"],
                        UserName = sqlDataReader["UserName"].ToString(),
                        Weight = sqlDataReader["Weight"].ToString(),
                        ActualWeight = sqlDataReader["ActualWeight"].ToString(),
                        Volume = sqlDataReader["Volume"].ToString(),
                        AirwayBillno = sqlDataReader["AirwayBillno"].ToString(),
                        Courier = sqlDataReader["Courier"].ToString(),
                        Cost = sqlDataReader["Cost"].ToString(),
                        ProposedDate = sqlDataReader["ProposedDate"].ToString(),
                        Remarks = sqlDataReader["Remarks"].ToString(),
                        CountryName = sqlDataReader["CountryName"].ToString(),
                        ServiceProvider = sqlDataReader["ServiceProvider"].ToString(),
                      
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
                result = accessManager.SaveData("sp_SentCourierComment", aParameters);
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
        public bool CourierApproveOrReject(int Progress, string CommentText, int UserID, int CourierRequestId)
        {
            bool result = false;
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@Progress", Progress));
                aParameters.Add(new SqlParameter("@CommentText", CommentText));
                aParameters.Add(new SqlParameter("@UserID", UserID));
                aParameters.Add(new SqlParameter("@CourierRequestId", CourierRequestId));
                result = accessManager.SaveData("sp_CourierApproveOrreject", aParameters);
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
        public ResultResponse SaveCourierTypeDatabase(CourierType courierType, int UserId)
        {
            try
            {
                int masterId = 0;
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aParameters = new List<SqlParameter>();
               ResultResponse result = new ResultResponse();
                result.pk = masterId;
                result.isSuccess = true;
                foreach (CourierTypeDetails item in courierType.CourierTypeDetails)
                {
                    List<SqlParameter> aList = new List<SqlParameter>();
                    aList.Add(new SqlParameter("@CourierType", courierType.Type));
                    //aList.Add(new SqlParameter("@Currency", item.Currency));
                    aList.Add(new SqlParameter("@Country", item.Country));
                    aList.Add(new SqlParameter("@ServiceProvider", item.ServiceProviderName));
                    aList.Add(new SqlParameter("@WeightRange", item.WeightRange));
                    aList.Add(new SqlParameter("@LeadTimeFrom", item.LeadTimeFrom));
                    aList.Add(new SqlParameter("@LeadTimeTo", item.LeadTimeTo));
                    aList.Add(new SqlParameter("@Rate", item.Rate));
                    aList.Add(new SqlParameter("@CreateBy", UserId));
                    result.isSuccess = accessManager.SaveData("sp_CourierTypeEntry", aList);
                }
                // bool xres = FileUploadToDatabase(capexmaster.CapexFileUpload, masterId);

                return result;
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
        public List<CourierTypeDetails> GetCourierTypeInfo(int status,string type)
        {
            List<CourierTypeDetails> courierType = new List<CourierTypeDetails>();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aList = new List<SqlParameter>();
                //aList.Add(new SqlParameter("@userId", UserId));
                aList.Add(new SqlParameter("@status", status));
                aList.Add(new SqlParameter("@type", type));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetALLCourierType", aList);
                while (dr.Read())
                {
                    CourierTypeDetails courierTypeInfromation = new CourierTypeDetails();
                    courierTypeInfromation.CourierTypeId = (int)dr["CourierTypeId"];
                    courierTypeInfromation.WeightRange = dr["WeightRange"].ToString();
                    courierTypeInfromation.Country = dr["Country"].ToString();
                    courierTypeInfromation.CountryName = dr["CountryName"].ToString();
                    courierTypeInfromation.ServiceProviderId = dr["ServiceProviderId"].ToString();
                    courierTypeInfromation.ServiceProvider = dr["ServiceProvider"].ToString();
                    courierTypeInfromation.Currency = dr["Currency"].ToString();
                    courierTypeInfromation.Rate = dr["Rate"].ToString();
                    courierTypeInfromation.LeadTimeFrom = dr["LeadTimeFrom"].ToString();
                    courierTypeInfromation.LeadTimeTo = dr["LeadTimeTo"].ToString();
                    courierTypeInfromation.Type = dr["Type"].ToString();
                    courierTypeInfromation.CreateDate = dr["CreateDate"].ToString();
                    courierTypeInfromation.UserName = dr["UserName"].ToString();
                    courierTypeInfromation.Rate = dr["Rate"].ToString();
                    courierType.Add(courierTypeInfromation);
                }
                return courierType;
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
        public List<CourierTypeDetails> courierTypeCheck(CourierTypeDetails courierTypeDetails, int userID)
        {
           // DataTable dt = new DataTable();
            List<CourierTypeDetails> CheckcourierTypeDetails = new List<CourierTypeDetails>();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aList = new List<SqlParameter>();
                //aList.Add(new SqlParameter("@userId", UserId));
                aList.Add(new SqlParameter("@Country", courierTypeDetails.Country));
                aList.Add(new SqlParameter("@ServiceProvider", courierTypeDetails.ServiceProviderName));
                aList.Add(new SqlParameter("@weightRange", courierTypeDetails.WeightRange));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_CourierTypeCheck", aList);
                while (dr.Read())
                {
                    CourierTypeDetails courierTypeInfromation = new CourierTypeDetails();
                    courierTypeInfromation.LeadTimeFrom = dr["LeadTimeFrom"].ToString();
                    courierTypeInfromation.LeadTimeTo = dr["LeadTimeTo"].ToString();
                    //  dt.Load(dr);
                    CheckcourierTypeDetails.Add(courierTypeInfromation);
                }
                return CheckcourierTypeDetails;
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
        public CourierTypeDetails GetcourierType(int UserId, int primarykey)
        {
            CourierTypeDetails CourierTypeDetails = new CourierTypeDetails();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@courierTypeId", primarykey));
                //aList.Add(new SqlParameter("@UserId", userId));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetCourierTypeInfo", aList);
                while (dr.Read())
                {
                    // CourierTypeDetails courierTypeInfromation = new CourierTypeDetails();
                    CourierTypeDetails.CourierTypeId = (int)dr["CourierTypeId"];
                    CourierTypeDetails.WeightRange = dr["WeightRange"].ToString();
                    CourierTypeDetails.Country = dr["Country"].ToString();
                    CourierTypeDetails.CountryName = dr["CountryName"].ToString();
                    CourierTypeDetails.ServiceProviderId = dr["ServiceProviderId"].ToString();
                    CourierTypeDetails.ServiceProvider = dr["ServiceProvider"].ToString();
                    CourierTypeDetails.Currency = dr["Currency"].ToString();
                    CourierTypeDetails.Rate = dr["Rate"].ToString();
                    CourierTypeDetails.LeadTimeFrom = dr["LeadTimeFrom"].ToString();
                    CourierTypeDetails.LeadTimeTo = dr["LeadTimeTo"].ToString();
                    CourierTypeDetails.Type = dr["Type"].ToString();
                    CourierTypeDetails.CreateDate = dr["CreateDate"].ToString();
                    CourierTypeDetails.CreateBy = dr["CreateBy"].ToString();
                    CourierTypeDetails.UserName = dr["UserName"].ToString();
                    CourierTypeDetails.Rate = dr["Rate"].ToString();
                    //courierRequestModel.Add(courierTypeInfromation);
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
        public ResultResponse UpdateCourierType(CourierTypeDetails courierTypeDetails, int UserId)
        {
            try
            {
                using (IDbConnection cnn = (IDbConnection)new SqlConnection(this.connStr))
                {
                    if (cnn.State == ConnectionState.Closed)
                        cnn.Open();
                 
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@CourierTypeId", (object)courierTypeDetails.CourierTypeId);
                    dynamicParameters.Add("@CourierType", (object)courierTypeDetails.Type);
                    dynamicParameters.Add("@Currency", (object)courierTypeDetails.Currency);
                    dynamicParameters.Add("@Country", (object)courierTypeDetails.Country);
                    dynamicParameters.Add("@ServiceProvider", (object)courierTypeDetails.ServiceProvider);
                    dynamicParameters.Add("@WeightRange", (object)courierTypeDetails.WeightRange);
                    dynamicParameters.Add("@LeadTimeFrom", (object)courierTypeDetails.LeadTimeFrom);
                    dynamicParameters.Add("@LeadTimeTo", (object)courierTypeDetails.LeadTimeTo);
                    dynamicParameters.Add("@Rate", (object)courierTypeDetails.Rate);
                    dynamicParameters.Add("@CreateBy", (object)UserId);
                    int num = cnn.Execute("sp_UpdateCourierTypeEntry", (object)dynamicParameters, commandType: new CommandType?(CommandType.StoredProcedure));
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
        public List<CourierRequestModel> GetFrontDeskCourier(
         int status,
         int userId,
         int frontdesk)
        {
            List<CourierRequestModel> courierRequestModelList = new List<CourierRequestModel>();
            try
            {
                this.accessManager.SqlConnectionOpen(DataBase.SQQeye);
                SqlDataReader sqlDataReader = this.accessManager.GetSqlDataReader("sp_GetAllCourierInformation", new List<SqlParameter>()
        {
          new SqlParameter("@userId", (object) userId),
          new SqlParameter("@status", (object) status),
          new SqlParameter("@frontdesk", (object) frontdesk)
        });
                while (sqlDataReader.Read())
                courierRequestModelList.Add(new CourierRequestModel()
                    {
                        CourierRequestId = (int)sqlDataReader["CourierRequestId"],
                        DeartmentName = sqlDataReader["DeartmentName"].ToString(),
                        Customer = sqlDataReader["Customer"].ToString(),
                        BuyerName = sqlDataReader["BuyerName"].ToString(),
                        Receiver = sqlDataReader["Receiver"].ToString(),
                        Country = sqlDataReader["Country"].ToString(),
                        CountryName = sqlDataReader["CountryName"].ToString(),
                        PostCode = sqlDataReader["PostCode"].ToString(),
                        Address = sqlDataReader["Address"].ToString(),
                        DispatchDate = sqlDataReader["DispatchDate"].ToString(),
                        Deliverydate = sqlDataReader["Deliverydate"].ToString(),
                        ProductDescription = sqlDataReader["ProductDescription"].ToString(),
                        Weight = sqlDataReader["Weight"].ToString(),
                        Volume = sqlDataReader["Volume"].ToString(),
                        AirwayBillno = sqlDataReader["AirwayBillno"].ToString(),
                        Courier = sqlDataReader["Courier"].ToString(),
                        ServiceProvider = sqlDataReader["ServiceProvider"].ToString(),
                        ProposedDate = sqlDataReader["ProposedDate"].ToString(),
                        Cost = sqlDataReader["Cost"].ToString(),
                        Remarks = sqlDataReader["Remarks"].ToString(),
                        IsApproved = (int)sqlDataReader["IsApproved"],
                        //Pending = (int)sqlDataReader["PendingStatus"],
                        DateOfRequest = sqlDataReader["DateOfRequest"].ToString(),
                        UserName = sqlDataReader["UserName"].ToString()
                     
                    });
                return courierRequestModelList;
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
       public ResultResponse SaveCourierDispatchDatabase(CourierRequestModel courierRequestModel, int userId, long InvoiceNo)
        {
            try
            {
                int masterId = 0;
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@ConsolidateWeight", courierRequestModel.NetweightSum));
                aParameters.Add(new SqlParameter("@ConsolidateCost", courierRequestModel.ConsolidateValue));
                aParameters.Add(new SqlParameter("@CourierDispatchNo", InvoiceNo));
                aParameters.Add(new SqlParameter("@PostCode", courierRequestModel.PostCode));
                aParameters.Add(new SqlParameter("@Country", courierRequestModel.CountryName));
                aParameters.Add(new SqlParameter("@Courier", courierRequestModel.ServiceProvider));
                aParameters.Add(new SqlParameter("@CourierNumber", courierRequestModel.CourierNumber));
                aParameters.Add(new SqlParameter("@DispatchDate", courierRequestModel.FontDispatchDate));
                aParameters.Add(new SqlParameter("@AirwayBillno", courierRequestModel.FontAirwayBillno));
                aParameters.Add(new SqlParameter("@ReferenceNo", courierRequestModel.ReferenceNo));
                aParameters.Add(new SqlParameter("@Remarks", courierRequestModel.FontRemarks));
                aParameters.Add(new SqlParameter("@CreatedBy", userId));

                masterId = accessManager.SaveDataReturnPrimaryKey("sp_courierDispatchMasterPost", aParameters);
                ResultResponse result = new ResultResponse();
                result.pk = masterId;
                result.isSuccess = true;
                foreach (CourierAmmountListModel item in courierRequestModel.CourierAmmountList)
                {
                    List<SqlParameter> aList = new List<SqlParameter>();
                    aList.Add(new SqlParameter("@CourierDispatchNo", InvoiceNo));
                    aList.Add(new SqlParameter("@CourierRequestId", item.CourierRequestId));
                    aList.Add(new SqlParameter("@ActualWeight", item.ActualWeight));
                    aList.Add(new SqlParameter("@CreatedBy", userId));
                    result.isSuccess = accessManager.SaveData("sp_courierDispatchDetailsPost", aList);
                }
                foreach (CourierAmmountListModel item in courierRequestModel.CourierAmmountList)
                {
                    List<SqlParameter> aList = new List<SqlParameter>();
                    aList.Add(new SqlParameter("@CourierRequestId", item.CourierRequestId));
                    result.isSuccess = accessManager.UpdateData("sp_UpdateIsApprove", aList);
                }
                return result;
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
        public ResultResponse SaveCourierReceivedDatabase(CourierRequestModel courierRequestModel, int userId, long InvoiceNo)
        {
            try
            {
                int masterId = 0;
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@CourierRequestId", courierRequestModel.CourierRequestId));
                aParameters.Add(new SqlParameter("@CourierNumber", courierRequestModel.CourierNumber));
                aParameters.Add(new SqlParameter("@AirwayBillno", courierRequestModel.FontAirwayBillno));
                aParameters.Add(new SqlParameter("@ReceivedDate", courierRequestModel.FontReceivedDate));
                aParameters.Add(new SqlParameter("@ReceivedWeight", courierRequestModel.FontReceivedWeight));
                aParameters.Add(new SqlParameter("@HandOverTo", courierRequestModel.HandOverTo));
                aParameters.Add(new SqlParameter("@ReferenceNo", courierRequestModel.ReferenceNo));
                aParameters.Add(new SqlParameter("@Remarks", courierRequestModel.FontRemarks));
                aParameters.Add(new SqlParameter("@CreatedBy", userId));

                masterId = accessManager.SaveDataReturnPrimaryKey("sp_CourierReceivedTablePost", aParameters);
                ResultResponse result = new ResultResponse();
                result.pk = masterId;
                result.isSuccess = true;
               
                return result;
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
        public long GetInvoice_No(string _dDname)
        {
            long value = 0;
            
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@DOCTYPE", _dDname));
                SqlDataReader dr = accessManager.GetSqlDataReader("SP_GETSERILANO", aList);
                DataTable dt = new DataTable();
                dt.Load(dr);
                value = int.Parse(dt.Rows[0]["LASTSERIALNO"].ToString());

                return value;
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