using DocSoOperation.Models;
using Newtonsoft.Json;
using SQIndustryThree.DataManager;
using SQIndustryThree.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace SQIndustryThree.DAL
{
    public class ExceptionDAL
    {
        private DataAccessManager accessManager = new DataAccessManager();
        public List<BuyerListModel> LoadBuyerList(int userId)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.DataEntryTracker);
                List<BuyerListModel> buyerListModels = new List<BuyerListModel>();
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@userId", userId));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_LoadBuyer", aList);
                while (dr.Read())
                {
                    BuyerListModel buyerList = new BuyerListModel();
                    buyerList.BuyerId = (int)dr["BuyerId"];
                    buyerList.BuyerName = dr["BuyerName"].ToString();
                    buyerListModels.Add(buyerList);
                }
                return buyerListModels;
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

        public List<ExceptionRequestMaster> LoadExcpCategory(int userId)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<ExceptionRequestMaster> expresons = new List<ExceptionRequestMaster>();
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@userId", userId));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_LoadExpReasonCatagory", aList);
                while (dr.Read())
                {
                    ExceptionRequestMaster expcategory = new ExceptionRequestMaster();
                    expcategory.ExceptioReasonsId = (int)dr["ExceptioReasonsId"];
                    expcategory.ExceptionReasonName = dr["ExceptionReasonName"].ToString();
                    expresons.Add(expcategory);
                }
                return expresons;
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
        public ResultResponse SaveExceptionRequest(ExceptionRequestMaster exceptionMasterInfo,int userId)
        {
            try
            {
                int masterId = 0;
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aParameters = new List<SqlParameter>(); 
                var json = new JavaScriptSerializer().Serialize(exceptionMasterInfo);
                var exceptionDetails = new JavaScriptSerializer().Serialize(exceptionMasterInfo.ExceptionDetails);
                var exceptionGenaralInfo = new JavaScriptSerializer().Serialize(exceptionMasterInfo.ExpgenaralInfoList);
                var exceptionFilelist = new JavaScriptSerializer().Serialize(exceptionMasterInfo.ExceptionFilesList);
                var approverList = new JavaScriptSerializer().Serialize(exceptionMasterInfo.ApproverList);
                exceptionMasterInfo.ExceptionDetails = null;
                exceptionMasterInfo.ExpgenaralInfoList = null;
                exceptionMasterInfo.ExceptionFilesList = null;
                int noOfApprover = exceptionMasterInfo.ApproverList.Count();
                exceptionMasterInfo.ApproverList= null;
                aParameters.Add(new SqlParameter("@ExceptionRequestMaster", json));
                aParameters.Add(new SqlParameter("@ExceptionDetails", exceptionDetails));
                aParameters.Add(new SqlParameter("@ExpGenralList", exceptionGenaralInfo));
                aParameters.Add(new SqlParameter("@FileUploadJSon", exceptionFilelist));
                aParameters.Add(new SqlParameter("@ApproverJson", approverList));
                aParameters.Add(new SqlParameter("@NoOfApprover", noOfApprover));
                aParameters.Add(new SqlParameter("@UserID", userId));

                masterId = accessManager.SaveDataReturnPrimaryKey("sp_CreateExceptionRequest", aParameters);
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
        public List<UserInformation> GetApproverList(int BusinessUnit, int BuyerId,int SupplyChain)
        {
            List<UserInformation> users = new List<UserInformation>();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@BusinessUnitId", BusinessUnit));
                aList.Add(new SqlParameter("@BuyerID", BuyerId));
                aList.Add(new SqlParameter("@SupplyChain", SupplyChain));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetExceptionApproverList", aList);
                while (dr.Read())
                {
                    UserInformation user = new UserInformation();
                    user.UserInformationId = (int)dr["UserId"];
                    user.UserInformationName = dr["UserName"].ToString();
                    user.DesignationName = dr["DesignationName"].ToString();
                    user.ApproverNo = (int)dr["ApproverNo"];
                    users.Add(user);
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
        public List<ExceptionRequestMaster> GetAllexceptionRequest(int UserId, int Status,int Pgress)
        {
            List<ExceptionRequestMaster> users = new List<ExceptionRequestMaster>();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@UserId", UserId));
                aList.Add(new SqlParameter("@Status", Status));
                aList.Add(new SqlParameter("@Progress", Pgress));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_MasterExceptionRequestList", aList);
                while (dr.Read())
                {
                    ExceptionRequestMaster exceptionList = new ExceptionRequestMaster();
                    exceptionList.ExceptionMasterId = (int)dr["ExceptionMasterId"];
                    exceptionList.BuyerName =dr["BuyerName"].ToString();
                    exceptionList.BusinessUnitName =dr["BusinessUnitName"].ToString();
                    exceptionList.ExceptionTypeName =dr["ExceptionTypeName"].ToString();
                    exceptionList.IsApproved =(int)dr["IsApproved"];
                    exceptionList.ResponsiblePerson =dr["ResponsiblePerson"].ToString();
                    exceptionList.Reasons =dr["Reasons"].ToString();
                    exceptionList.PendingComments = (int)dr["Pending"];
                    exceptionList.IsHrInteraction = (int)dr["IsHrInteraction"];
                    exceptionList.ExceptionReasonName = dr["ExceptionReasonName"].ToString();
                    exceptionList.RequestBy =dr["UserName"].ToString();
                    exceptionList.CreateDate =dr["CreateDate"].ToString();
                
                    users.Add(exceptionList);
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
        public List<ExceptionRequestMaster> AllExceprionReportList(int UserId, string formDate, string todate, int Bun,int byr,int extype,int resonTyp )
        {
            List<ExceptionRequestMaster> users = new List<ExceptionRequestMaster>();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@UserId", UserId));
                aList.Add(new SqlParameter("@FromDate", formDate));
                aList.Add(new SqlParameter("@Todate", todate));
                aList.Add(new SqlParameter("@BusinessUnitId", Bun));
                aList.Add(new SqlParameter("@BuyerId", byr));
                aList.Add(new SqlParameter("@ExceptionType", extype));
                aList.Add(new SqlParameter("@ReasonCatagory", resonTyp));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetExceptionReport", aList);
                while (dr.Read())
                {
                    ExceptionRequestMaster exceptionList = new ExceptionRequestMaster();
                    exceptionList.ExceptionMasterId = (int)dr["ExceptionMasterId"];
                    exceptionList.BuyerName = dr["BuyerName"].ToString();
                    exceptionList.BusinessUnitName = dr["BusinessUnitName"].ToString();
                    exceptionList.ExceptionTypeName = dr["ExceptionTypeName"].ToString();
                    exceptionList.IsApproved = (int)dr["IsApproved"];
                    exceptionList.ResponsiblePerson = dr["ResponsiblePerson"].ToString();
                    exceptionList.Reasons = dr["Reasons"].ToString();
                    exceptionList.IsHrInteraction = (int)dr["IsHrInteraction"];
                    exceptionList.ExceptionReasonName = dr["ExceptionReasonName"].ToString();
                    exceptionList.RequestBy = dr["UserName"].ToString();
                    exceptionList.CreateDate = dr["CreateDate"].ToString();
                    exceptionList.UpdateDate = dr["UpdateDate"].ToString();

                    users.Add(exceptionList);
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

        public ExceptionRequestMaster IndividualRequestInfo(int masterId)
        {
            ExceptionRequestMaster exceptionRequest = new ExceptionRequestMaster();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@ExceptionMasterId", masterId));
                SqlDataReader dr = accessManager.GetSqlDataReader("Sp_IndividualExceptionRequest", aList);
                while (dr.Read())
                {
                    exceptionRequest.ExceptionMasterId = (int)dr["ExceptionMasterId"];
                    exceptionRequest.ExceptionTypeId = (int)dr["ExceptionTypeId"];
                    exceptionRequest.BuyerName = dr["BuyerName"].ToString();
                    exceptionRequest.BusinessUnitName = dr["BusinessUnitName"].ToString();
                    exceptionRequest.ExceptionTypeName = dr["ExceptionTypeName"].ToString();
                    exceptionRequest.IsApproved = (int)dr["IsApproved"];
                    exceptionRequest.ResponsiblePerson = dr["ResponsiblePerson"].ToString();
                    exceptionRequest.Reasons = dr["Reasons"].ToString();
                    exceptionRequest.RequestBy = dr["UserName"].ToString();
                    exceptionRequest.RequestorID =(int) dr["UserId"];
                    exceptionRequest.IsHrInteraction =(int) dr["IsHrInteraction"];
                    exceptionRequest.CreateDate = dr["CreateDate"].ToString();
                    exceptionRequest.HrActionRemarks = dr["HrActionRemarks"].ToString();
                    //exceptionRequest.Odd = dr["Odd"].ToString();
                    //exceptionRequest.Rdd = dr["Rdd"].ToString();
                    exceptionRequest.RivisionNo = (int)dr["RivisionNo"];
                    exceptionRequest.ExceptionReasonName = dr["ExceptionReasonName"].ToString();
                    exceptionRequest.NecessaryAction = dr["NecessaryAction"].ToString();
                    exceptionRequest.ExceptionDetails = JsonConvert.DeserializeObject<ExceptionDetailsTable>(dr["ExceptionDetails"].ToString());
                    exceptionRequest.ExceptionFilesList = JsonConvert.DeserializeObject<List<CapexFileUploadDetails>>(dr["ExceptionFilesList"].ToString());
                    exceptionRequest.ExpgenaralInfoList = JsonConvert.DeserializeObject<List<ExceptionGenaralInformation>>(dr["ExpgenaralInfoList"].ToString());
                    exceptionRequest.ApproverList = JsonConvert.DeserializeObject<List<QueryModel>>(dr["ApproverList"].ToString());
                    exceptionRequest.ExceptionComments = JsonConvert.DeserializeObject<List<CommentsTable>>(dr["ExceptionComments"].ToString());
                }
                return exceptionRequest;
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

        public bool CommentSent(int MasterID, int ReviewTo, string ReviewMessage,int UserID)
        {
            bool result =false;
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@MasterID", MasterID));
                aParameters.Add(new SqlParameter("@ReviewTo", ReviewTo));
                aParameters.Add(new SqlParameter("@ReviewMessage", ReviewMessage));
                aParameters.Add(new SqlParameter("@UserID", UserID));
                result = accessManager.SaveData("sp_SentExceptionComment", aParameters);
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
        public bool ApproveOrReject(int Progress, string CommentText, int UserID,int ExceptionMasterId)
        {
            bool result = false;
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@Progress", Progress));
                aParameters.Add(new SqlParameter("@CommentText", CommentText));
                aParameters.Add(new SqlParameter("@UserID", UserID));
                aParameters.Add(new SqlParameter("@ExceptionMasterId", ExceptionMasterId));
                result = accessManager.SaveData("sp_ApproveOrRejectException", aParameters);
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

        public ResultResponse UpdateExceptionRequest(ExceptionRequestMaster exceptionMasterInfo, int userId)
        {
            try
            {
                foreach (ExceptionGenaralInformation exgen in exceptionMasterInfo.ExpgenaralInfoList)
                {
                    bool res = ExceptionGenaralInformation(exgen, exceptionMasterInfo.ExceptionMasterId);
                }
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                var json = new JavaScriptSerializer().Serialize(exceptionMasterInfo);
                var exceptionDetails = new JavaScriptSerializer().Serialize(exceptionMasterInfo.ExceptionDetails);
                var exceptionFilelist = new JavaScriptSerializer().Serialize(exceptionMasterInfo.ExceptionFilesList);
                exceptionMasterInfo.ExceptionDetails = null;
                exceptionMasterInfo.ExceptionFilesList = null;
                aParameters.Add(new SqlParameter("@ExceptionRequestMaster", json));
                aParameters.Add(new SqlParameter("@ExceptionDetails", exceptionDetails));
                aParameters.Add(new SqlParameter("@FileUploadJSon", exceptionFilelist));
                aParameters.Add(new SqlParameter("@UserID", userId));
                ResultResponse result = new ResultResponse();
                result.isSuccess = accessManager.UpdateData("sp_UpdateExceptionRequest", aParameters);
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
        public bool ExceptionGenaralInformation(ExceptionGenaralInformation exceptionGnInfo,int MasterId)
        {
            bool result = false;
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@ExceptionGnId", exceptionGnInfo.ExceptionGenralId));
                aParameters.Add(new SqlParameter("@PONumber", exceptionGnInfo.PO));
                aParameters.Add(new SqlParameter("@Color", exceptionGnInfo.Color));
                aParameters.Add(new SqlParameter("@StyleNumber", exceptionGnInfo.StyleNo));
                aParameters.Add(new SqlParameter("@quantity", exceptionGnInfo.Quantity));
                aParameters.Add(new SqlParameter("@fob", exceptionGnInfo.FOB));
                aParameters.Add(new SqlParameter("@expMasterId", MasterId));
                aParameters.Add(new SqlParameter("@odd",exceptionGnInfo.OriginalDD));
                aParameters.Add(new SqlParameter("@rdd",exceptionGnInfo.RevisedDD));
                aParameters.Add(new SqlParameter("@discount", exceptionGnInfo.Discount));
                aParameters.Add(new SqlParameter("@claim", exceptionGnInfo.Claim));
                aParameters.Add(new SqlParameter("@materialLi", exceptionGnInfo.MaterialLiability));
                aParameters.Add(new SqlParameter("@garmentsLi", exceptionGnInfo.GarmentsLiability));
                result = accessManager.SaveData("sp_ExceptionGenaralUpdate", aParameters);
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
    }
}