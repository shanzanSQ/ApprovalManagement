using DocSoOperation.Models;
using Newtonsoft.Json;
using SQIndustryThree.DataManager;
using SQIndustryThree.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Script.Serialization;

namespace SQIndustryThree.DAL
{
    public class IOUDAL
    {
        private DataAccessManager accessManager = new DataAccessManager();
        public List<UserInformation> GetDepartmentID(int Status,int DepartmentHeadID)
        {
            List<UserInformation> userslist = new List<UserInformation>();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@Status", Status));
                aList.Add(new SqlParameter("@DepartmentHeadID", DepartmentHeadID));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetIOuDepartmentHead", aList);
                while (dr.Read())
                {
                    UserInformation users = new UserInformation();
                    users.DepartmentId = (int)dr["DepartmentId"];
                    users.DepartmentName = dr["DeartmentName"].ToString();
                    users.UserInformationId = (int)dr["UserId"];
                    users.UserInformationName = dr["UserName"].ToString();
                    userslist.Add(users);
                }
                return userslist;
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

        public List<IOUApproverModel> GETIOUApproverList(int BusinessUnit, int LocationId, int DepartmentId, int Ammount,int UserId)
        {
            List<IOUApproverModel> users = new List<IOUApproverModel>();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@BusinessUnitId", BusinessUnit));
                aList.Add(new SqlParameter("@LocationId", LocationId));
                aList.Add(new SqlParameter("@DepartmentId", DepartmentId));
                aList.Add(new SqlParameter("@Ammount", Ammount));
                aList.Add(new SqlParameter("@UserId", UserId));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetIouApproverlist", aList);
                while (dr.Read())
                {
                    IOUApproverModel user = new IOUApproverModel();
                    user.UserID = (int)dr["UserId"];
                    user.UserName = dr["UserName"].ToString();
                    user.DesignationName = dr["DesignationName"].ToString();
                    user.ApproverStatusName = dr["ApproverStatusName"].ToString();
                    user.ApproverNo = (int)dr["ApproverNo"];
                    user.ApproverStatus = (int)dr["ApproverStatus"];
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

        public ResultResponse SaveIOuRequest(IOURequestModel iOURequestModel, int userId)
        {
            try
            {
                int masterId = 0;
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                
                var iouamountList = new JavaScriptSerializer().Serialize(iOURequestModel.AmmountList);
                var ioufilelist = new JavaScriptSerializer().Serialize(iOURequestModel.IOurequestfiles);
                var approverList = new JavaScriptSerializer().Serialize(iOURequestModel.IOUApproverList);
                iOURequestModel.AmmountList = null;
                iOURequestModel.IOurequestfiles = null;
                int noOfApprover = iOURequestModel.IOUApproverList.Count;
                iOURequestModel.IOUApproverList = null;
                var json = new JavaScriptSerializer().Serialize(iOURequestModel);
                aParameters.Add(new SqlParameter("@IouMasterRequest", json));
                aParameters.Add(new SqlParameter("@IouAmmountList", iouamountList));
                aParameters.Add(new SqlParameter("@IouFileList", ioufilelist));
                aParameters.Add(new SqlParameter("@ApproverJson", approverList));
                aParameters.Add(new SqlParameter("@NoOfApprover", noOfApprover));
                aParameters.Add(new SqlParameter("@UserID", userId));

                masterId = accessManager.SaveDataReturnPrimaryKey("sp_CreateIOURequest", aParameters);
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

        public ResultResponse UpdateIouRequest(IOURequestModel iOURequestModel, int userId)
        {
            try
            {
                int masterId = 0;
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aParameters = new List<SqlParameter>();

                var iouamountList = new JavaScriptSerializer().Serialize(iOURequestModel.AmmountList);
                var ioufilelist = new JavaScriptSerializer().Serialize(iOURequestModel.IOurequestfiles);
                var approverList = new JavaScriptSerializer().Serialize(iOURequestModel.IOUApproverList);
                int noOfApprover = iOURequestModel.IOUApproverList.Count;
                aParameters.Add(new SqlParameter("@IouAmmountList", iouamountList));
                aParameters.Add(new SqlParameter("@IouFileList", ioufilelist));
                aParameters.Add(new SqlParameter("@ApproverJson", approverList));
                aParameters.Add(new SqlParameter("@NoOfApprover", noOfApprover));
                aParameters.Add(new SqlParameter("@UserID", userId));
                aParameters.Add(new SqlParameter("@IouRequestId", iOURequestModel.IouRequestId));
                aParameters.Add(new SqlParameter("@LocationId", iOURequestModel.LocationId));
                aParameters.Add(new SqlParameter("@BusinessUnitId", iOURequestModel.BusinessUnitId));
                aParameters.Add(new SqlParameter("@DepartmentId", iOURequestModel.DepartmentID));
                aParameters.Add(new SqlParameter("@Purpose", iOURequestModel.Purpose));
                aParameters.Add(new SqlParameter("@RequiredDate", iOURequestModel.RequiredDate));
                aParameters.Add(new SqlParameter("@Amount", iOURequestModel.Ammount));

                masterId = accessManager.SaveDataReturnPrimaryKey("sp_UpdateIouRequest", aParameters);
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
        public ResultResponse UpdateItemAmount(IOURequestModel iOURequestModel, int userId)
        {
            try
            {
                int masterId = 0;
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                
                var ioufilelist = new JavaScriptSerializer().Serialize(iOURequestModel.IOurequestfiles);
                var json = new JavaScriptSerializer().Serialize(iOURequestModel);




                foreach(var amount in iOURequestModel.AmmountList)
                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@IouRequestId", iOURequestModel.IouRequestId));
                    aParameters.Add(new SqlParameter("@IouAmountId", amount.IouAmmountId));
                    aParameters.Add(new SqlParameter("@ItemName", amount.ItemName));
                    aParameters.Add(new SqlParameter("@ItemDescription", amount.Purpose));
                    aParameters.Add(new SqlParameter("@ItemAmount", amount.Ammount));
                    masterId += accessManager.SaveDataReturnPrimaryKey("sp_UpdateIouAmountTable", aParameters);
                }
                
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

        public ResultResponse SubmitForSettlement(IOURequestModel iOURequestModel, int userId)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                var ioufilelist = new JavaScriptSerializer().Serialize(iOURequestModel.IOurequestfiles);
                iOURequestModel.IOurequestfiles = null;
                aParameters.Add(new SqlParameter("@IouRequestId", iOURequestModel.IouRequestId));
                aParameters.Add(new SqlParameter("@DisburseAmount", iOURequestModel.TotalDisburseAmmount));
                aParameters.Add(new SqlParameter("@ExpenceAmount", iOURequestModel.TotalExpenceAmmount));
                aParameters.Add(new SqlParameter("@settlementRemarks", iOURequestModel.RemarksSettlement));
                aParameters.Add(new SqlParameter("@IouFileList", ioufilelist));
                aParameters.Add(new SqlParameter("@UserID", userId));
                ResultResponse result = new ResultResponse();
                result.isSuccess = accessManager.SaveData("sp_CreateIouSettlement", aParameters);
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

        public List<IOURequestModel> GetAllIouRequest(int UserId, int Status, int Pgress)
        {
            List<IOURequestModel> users = new List<IOURequestModel>();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@UserId", UserId));
                aList.Add(new SqlParameter("@Status", Status));
                aList.Add(new SqlParameter("@Progress", Pgress));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_IOUGetAllRequest", aList);
                while (dr.Read())
                {
                    IOURequestModel iOURequest = new IOURequestModel();
                    iOURequest.IouRequestId = (int)dr["IOURequestId"];
                    iOURequest.DepartmentName = dr["DeartmentName"].ToString();
                    iOURequest.BusinessUnitName = dr["BusinessUnitName"].ToString();
                    iOURequest.LocationName = dr["LocationName"].ToString();
                    iOURequest.IsApproved = (int)dr["IsApproved"];
                    iOURequest.Purpose = dr["Purpose"].ToString();
                    iOURequest.Ammount = (int)dr["Ammount"];
                    iOURequest.IsSettled = (int)dr["IsSettled"];
                    iOURequest.TotalDisburseAmmount = (int)dr["TotalDisburseAmmount"];
                    iOURequest.SettlementDate = dr["SettlementDate"].ToString();
                    iOURequest.Pending = (int)dr["Pending"];
                    iOURequest.IsSettledApprove = (int)dr["IsSettleApprove"];
                    iOURequest.UserName = dr["UserName"].ToString();
                    iOURequest.DateOfRequest = dr["DateOfRequest"].ToString();
                    iOURequest.RequiredDate = dr["RequiredDate"].ToString();
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

        public IOURequestModel IouDetailsInformation(int masterId,int userId)
        {
            IOURequestModel iourequest = new IOURequestModel();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@IouRequestId", masterId));
                aList.Add(new SqlParameter("@UserId", userId));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_IouDetailsInfo", aList);
                while (dr.Read())
                {
                    iourequest.IouRequestId = (int)dr["IOURequestId"];
                    iourequest.DateOfRequest = dr["DateOfRequest"].ToString();
                    iourequest.RequiredDate = dr["RequiredDate"].ToString();
                    iourequest.RivisionNo = (int)dr["RivisionNo"];
                    iourequest.Purpose = dr["Purpose"].ToString();
                    iourequest.Ammount = (int)dr["Ammount"];
                    iourequest.UserId = (int)dr["UserId"];
                    iourequest.IsSettled = (int)dr["IsSettled"];
                    iourequest.DepartmentName = dr["DeartmentName"].ToString();
                    iourequest.DepartmentID =(int) dr["DepartmentId"];
                    iourequest.BusinessUnitId = (int)dr["BusinessUnitId"];
                    iourequest.LocationId = (int)dr["LocationId"];
                    iourequest.BusinessUnitName = dr["BusinessUnitName"].ToString();
                    iourequest.LocationName = dr["LocationName"].ToString();
                    iourequest.IsApproved = (int)dr["IsApproved"];
                    iourequest.UserName = dr["UserName"].ToString();
                    iourequest.SettlementDate = dr["SettlementDate"].ToString();
                    iourequest.UserId = (int)dr["UserId"];
                    iourequest.IsSettledApprove = (int)dr["IsSettleApprove"];
                    iourequest.TotalDisburseAmmount = (int)dr["TotalDisburseAmmount"];
                    iourequest.TotalExpenceAmmount = (int)dr["TotalExpenceAmmount"];
                    iourequest.RemarksSettlement = dr["RemarksSettlement"].ToString();
                    iourequest.SettlementCreateDate = dr["SettlementCreateDate"].ToString();

                    iourequest.IOurequestfiles = JsonConvert.DeserializeObject<List<CapexFileUploadDetails>>(dr["IOurequestfiles"].ToString());
                    iourequest.IouSettlementFiles = JsonConvert.DeserializeObject<List<CapexFileUploadDetails>>(dr["IouSettlementFiles"].ToString());
                    iourequest.DisburseList = JsonConvert.DeserializeObject<List<IOURequestModel>>(dr["DisburseAmountList"].ToString());
                    iourequest.AmmountList = JsonConvert.DeserializeObject<List<IOURequestModel>>(dr["AmmountList"].ToString());
                    iourequest.IOUApproverList = JsonConvert.DeserializeObject<List<IOUApproverModel>>(dr["IOUApproverList"].ToString());
                    iourequest.SettlementApproverList = JsonConvert.DeserializeObject<List<IOUApproverModel>>(dr["IOUSettlementApproverList"].ToString());
                    iourequest.IouComments = JsonConvert.DeserializeObject<List<CommentsTable>>(dr["IouComments"].ToString());
                    iourequest.IouLogSection = JsonConvert.DeserializeObject<List<LogSection>>(dr["IouLogsection"].ToString());
                }
                return iourequest;
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

        public bool IOUApproveOrReject(int Progress, string CommentText, int UserID, int IouRequestId,string SettlementDate)
        {
            bool result = false;
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@Progress", Progress));
                aParameters.Add(new SqlParameter("@CommentText", CommentText));
                aParameters.Add(new SqlParameter("@UserID", UserID));
                aParameters.Add(new SqlParameter("@IouRequestId", IouRequestId));
                aParameters.Add(new SqlParameter("@SettlementDate",SettlementDate));
                result = accessManager.SaveData("sp_IouApproveOrreject", aParameters);
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
        public bool IouSettlementApprove(int Progress, string CommentText, int UserID, int IouRequestId)
        {
            bool result = false;
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@Progress", Progress));
                aParameters.Add(new SqlParameter("@CommentText", CommentText));
                aParameters.Add(new SqlParameter("@UserID", UserID));
                aParameters.Add(new SqlParameter("@IouRequestId", IouRequestId));
                result = accessManager.SaveData("sp_IOUSettlementApprove", aParameters);
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
                result = accessManager.SaveData("sp_SentIOUComment", aParameters);
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

        public bool SaveDisburseAmount(int IOuRequestId, int Amount, string Remarks, int UserID)
        {
            bool result = false;
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@IouRequestId", IOuRequestId));
                aParameters.Add(new SqlParameter("@Remarks", Remarks));
                aParameters.Add(new SqlParameter("@Amount", Amount));
                aParameters.Add(new SqlParameter("@UserID", UserID));
                result = accessManager.SaveData("sp_DisbursementInsertIOU", aParameters);
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

        public bool DeleteAmmountFrom(int RowNo)
        {
            bool result = false;
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@RowNo", RowNo));
                result = accessManager.DeleteData("sp_DeleteIouItemDetails", aParameters);
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
    }
}