using DocSoOperation.Models;
using Newtonsoft.Json;
using SQIndustryThree.DataManager;
using SQIndustryThree.Models;
using SQIndustryThree.Models.BillApproval;
using SQIndustryThree.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace SQIndustryThree.DAL
{
    public class BillApprovalDAL
    {
        private DataAccessManager accessManager = new DataAccessManager();

        public SqlConnection conn = new SqlConnection(DBConnection.GetConnectionString());

        public bool BillApprovalDatabase(List<BillAprrovalPoDetails> billAprrovalPoDetails,int UserId)
        {
            bool result = false;
            foreach (var billApproval in billAprrovalPoDetails)
            {
                try
                {

                    accessManager.SqlConnectionOpen(DataBase.SQQeye);
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@PONumber", billApproval.PONumber));
                    aParameters.Add(new SqlParameter("@SupllierName", billApproval.SupllierName));
                    aParameters.Add(new SqlParameter("@ArticleName", billApproval.ArticleName));
                    aParameters.Add(new SqlParameter("@ColorName", billApproval.ColorName));
                    aParameters.Add(new SqlParameter("@SizeName", billApproval.SizeName));
                    aParameters.Add(new SqlParameter("@POQty", billApproval.POQty));
                    aParameters.Add(new SqlParameter("@Rate", billApproval.Rate));
                    aParameters.Add(new SqlParameter("@PoValue", billApproval.PoValue));
                    aParameters.Add(new SqlParameter("@UserId", UserId));
                    result = accessManager.SaveData("sp_InsertIntoPoTable", aParameters);
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
            return result;
        }

        public List<BillApprovalPOMasterTable> GetAllBillAPpprovalPo(int Status)
        {
            List<BillApprovalPOMasterTable> polist = new List<BillApprovalPOMasterTable>();
            try
            {

                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@Status", Status));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_BillApprovalPoNUmber", aParameters);
                while(dr.Read()){
                    BillApprovalPOMasterTable bill = new BillApprovalPOMasterTable();
                    bill.MasterKey = (int)dr["MasterKey"];
                    bill.PurchaseOrderNo =dr["PurchaseOrderNo"].ToString();
                    polist.Add(bill);
                }
                return polist;
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

        public BillApprovalPOMasterTable IndividualPOetailS(int POMasterKey,int Bunit,int Catkey)
        {
            BillApprovalPOMasterTable poinformation = new BillApprovalPOMasterTable();
            try
            {

                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@PoMasterKey", POMasterKey ));
                aParameters.Add(new SqlParameter("@BusinessUnit", Bunit));
                aParameters.Add(new SqlParameter("@CategoryKey", Catkey));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GEtPOInformationBill", aParameters);
                while (dr.Read())
                {
                    poinformation.MasterKey = POMasterKey;
                    poinformation.POQty = float.Parse(dr["POQty"].ToString());
                    poinformation.AdvancedPayment = float.Parse(dr["AdvancedPayment"].ToString()); 
                    poinformation.SupplierName = dr["SupplierName"].ToString();
                    poinformation.Polist= JsonConvert.DeserializeObject<List<BillAprrovalPoDetails>>(dr["PoDetailsList"].ToString());
                    poinformation.Approverlist = JsonConvert.DeserializeObject<List<IOUApproverModel>>(dr["Approverlist"].ToString());
                }
                return poinformation;
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

        public bool SubmitForBillApprovalRequest(List<BillAprrovalPoDetails> billAprrovalPoDetails, int UserId,int categoryId,int Buid)
        {
            bool result = false;
            foreach (var billApproval in billAprrovalPoDetails)
            {
                try
                {

                    accessManager.SqlConnectionOpen(DataBase.SQQeye);
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@BillPoDetailskey", billApproval.BillPoDetailskey));
                    aParameters.Add(new SqlParameter("@PIQty", billApproval.PIQty));
                    aParameters.Add(new SqlParameter("@PIValue", billApproval.PIValue));
                    aParameters.Add(new SqlParameter("@Discount", billApproval.Discount));
                    aParameters.Add(new SqlParameter("@Total", billApproval.TotalPayment));
                    aParameters.Add(new SqlParameter("@UserId", UserId));
                    aParameters.Add(new SqlParameter("@BillMasterKey", billApproval.MasterKey));
                    aParameters.Add(new SqlParameter("@CategoryId", categoryId));
                    aParameters.Add(new SqlParameter("@BusinessUnitId", Buid));
                    result = accessManager.SaveData("sp_BillSubmitApproval", aParameters);
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
            return result;
        }

        public List<BillSubmitModel> GetBillRequestList(int Status, int UserId)
        {
            List<BillSubmitModel> polist = new List<BillSubmitModel>();
            try
            {

                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@Status", Status));
                aParameters.Add(new SqlParameter("@UserId", UserId));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_BillGetAllRequest", aParameters);
                while (dr.Read())
                {
                    BillSubmitModel bill = new BillSubmitModel();
                    bill.MasterKey = (int)dr["MasterKey"];
                    bill.PurchaseOrderNo = dr["PurchaseOrderNo"].ToString();
                    bill.SupplierName = dr["SupplierName"].ToString();
                    bill.BusinessUnitName = dr["BusinessUnitName"].ToString();
                    bill.UserName = dr["UserName"].ToString();
                    bill.POQty = float.Parse(dr["POQty"].ToString());
                    bill.PoValue = float.Parse(dr["PoValue"].ToString());
                    bill.PIQty = float.Parse(dr["PIQty"].ToString());
                    bill.PIValue = float.Parse(dr["PIValue"].ToString());
                    bill.Discount = float.Parse(dr["Discount"].ToString());
                    bill.TotalPayment = float.Parse(dr["TotalPayment"].ToString());
                    bill.CreateDate = dr["CreateDate"].ToString();
                    polist.Add(bill);
                }
                return polist;
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


        #region New Visitor

        public DataTable SupplierList()
        {
            DataTable dt = new DataTable();
            if (conn.State == 0)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand("sp_supplierList", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            adpt.Fill(dt);
            return dt;
        }

        public DataTable InvoiceTypeList()
        {
            DataTable dt = new DataTable();
            if (conn.State == 0)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand("sp_InvoiceTypeList", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            adpt.Fill(dt);
            return dt;
        }

        public DataTable QualityResultList()
        {
            DataTable dt = new DataTable();
            if (conn.State == 0)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand("sp_QualityResultList", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            adpt.Fill(dt);
            return dt;
        }

        public DataTable POList(int supplierId)
        {
            DataTable dt = new DataTable();
            if (conn.State == 0)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand("sp_POList", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@supplierId", SqlDbType.NVarChar).Value = supplierId;
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            adpt.Fill(dt);
            return dt;
        }

        public DataTable SupplierWisePOSearch(int supplierId, string serach)
        {
            DataTable dt = new DataTable();
            if (conn.State == 0)
            {
                conn.Open();
            }

            SqlCommand cmd = new SqlCommand("sp_POList", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@supplierId", SqlDbType.Int).Value = supplierId;
            cmd.Parameters.Add("@serach", SqlDbType.NVarChar).Value = serach;
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            adpt.Fill(dt);
            return dt;
        }

        public DataTable InvoiceTypeWiseApproverList(int invoiceType)
        {
            DataTable dt = new DataTable();
            if (conn.State == 0)
            {
                conn.Open();
            }

            SqlCommand cmd = new SqlCommand("sp_InvoiceTypeWiseApproverList", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@invoiceType", SqlDbType.Int).Value = invoiceType;
          
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            adpt.Fill(dt);
            return dt;
        }

        public ResultResponse SaveBillRequest(BillRequestMaster billMasterInfo, int userId)
        {
            try
            {
                int masterId = 0;
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                var json = new JavaScriptSerializer().Serialize(billMasterInfo);
                //var exceptionDetails = new JavaScriptSerializer().Serialize(billMasterInfo.);
                var billDetails = new JavaScriptSerializer().Serialize(billMasterInfo.BillInfoList);
                var billFilelist = new JavaScriptSerializer().Serialize(billMasterInfo.BillFilesList);
                var approverList = new JavaScriptSerializer().Serialize(billMasterInfo.ApproverList);
                billMasterInfo.BillInfoList = null;
                billMasterInfo.BillFilesList = null;                
                int noOfApprover = billMasterInfo.ApproverList.Count();
                billMasterInfo.ApproverList = null;
                aParameters.Add(new SqlParameter("@billMasterInfo", json));
                aParameters.Add(new SqlParameter("@billDetails", billDetails));
                //aParameters.Add(new SqlParameter("@ExpGenralList", exceptionGenaralInfo));
                aParameters.Add(new SqlParameter("@FileUploadJSon", billFilelist));
                aParameters.Add(new SqlParameter("@ApproverJson", approverList));
                aParameters.Add(new SqlParameter("@NoOfApprover", noOfApprover));
                aParameters.Add(new SqlParameter("@UserID", userId));

                //aParameters.Add(new SqlParameter("@InvoiceNo", billMasterInfo.InvoiceNo));
                //aParameters.Add(new SqlParameter("@InvoiceDate", billMasterInfo.InvoiceDate));
                //aParameters.Add(new SqlParameter("@InvoiceType", billMasterInfo.InvoiceTypeID));
                //aParameters.Add(new SqlParameter("@SupplierId", billMasterInfo.SupplierID));
                //aParameters.Add(new SqlParameter("@CreatedBy", userId));
                //aParameters.Add(new SqlParameter("@IsFinalInvoice", billMasterInfo.FinalInvoice));
                //aParameters.Add(new SqlParameter("@Remarks", billMasterInfo.Remarks));
                //aParameters.Add(new SqlParameter("@Notes", billMasterInfo.Notes));
                //aParameters.Add(new SqlParameter("@POKey", billMasterInfo.POKey));
                //aParameters.Add(new SqlParameter("@Total_InvoiceQty", billMasterInfo.TotalInvoiceQty));
                //aParameters.Add(new SqlParameter("@Total_InvoiceValue", billMasterInfo.TotalInvoiceValue));
                //aParameters.Add(new SqlParameter("@Total_Discount", billMasterInfo.TotalDiscount));
                //aParameters.Add(new SqlParameter("@Total_Paid", billMasterInfo.TotalPaid));

                masterId = accessManager.SaveDataReturnPrimaryKey("sp_CreateBillRequest", aParameters);
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

        public bool SaveQuality(int invoiceKey, string invoiceNo, string item, string result, 
            string comment, string fileName, string filePath, int created_by)
        {
            try
            {
                bool flag = false;
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@invoiceKey", invoiceKey));
                aParameters.Add(new SqlParameter("@invoiceNo", invoiceNo));
                aParameters.Add(new SqlParameter("@item", item));
                aParameters.Add(new SqlParameter("@result", result));
                aParameters.Add(new SqlParameter("@comment", comment));
                aParameters.Add(new SqlParameter("@fileName", fileName));
                aParameters.Add(new SqlParameter("@filePath", filePath));
                aParameters.Add(new SqlParameter("@createdBy", created_by));

                flag = accessManager.SaveData("sp_CreateQuality", aParameters);
                return flag;
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

        public DataTable QualityList(int InvoiceKey)
        {
            DataTable dt = new DataTable();
            if (conn.State == 0)
            {
                conn.Open();
            }

            SqlCommand cmd = new SqlCommand("sp_QualityList", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@InvoiceKey", SqlDbType.Int).Value = InvoiceKey;            
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            adpt.Fill(dt);
            return dt;
        }


        public bool UpdatePODetails(double InitialQty, double InvoiceBalance, long PODetailsKey)
        {
            try
            {
                bool flag = false;
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@InvoiceQty", InitialQty));
                //aParameters.Add(new SqlParameter("@InvoiceBalance", InvoiceBalance));
                aParameters.Add(new SqlParameter("@PODetailsKey", PODetailsKey));

                flag = accessManager.UpdateData("sp_POUpdate", aParameters);
                return flag;
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

        public DataTable GetAllBillRequest(int UserId, int Status, int Pgress)
        {
            DataTable dt = new DataTable();
            if (conn.State == 0)
            {
                conn.Open();
            }

            SqlCommand cmd = new SqlCommand("sp_GetAllBillRequest", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;
            cmd.Parameters.Add("@Status", SqlDbType.Int).Value = Status;
            cmd.Parameters.Add("@Progress", SqlDbType.Int).Value = Pgress;
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            adpt.Fill(dt);
            return dt;
        }

        public DataTable GetApproverListByInvoiceKey(int invoicekey)
        {
            DataTable dt = new DataTable();
            if (conn.State == 0)
            {
                conn.Open();
            }

            SqlCommand cmd = new SqlCommand("sp_GetApproverListByInvoiceKey", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@invoicekey", SqlDbType.Int).Value = invoicekey;
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            adpt.Fill(dt);
            return dt;
        }


        public List<BillRequestModel> GetAllBillRequestLast(int UserId, int Status, int Pgress)
        {
            List<BillRequestModel> users = new List<BillRequestModel>();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@UserId", UserId));
                aList.Add(new SqlParameter("@Status", Status));
                aList.Add(new SqlParameter("@Progress", Pgress));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetAllBillRequest", aList);
                while (dr.Read())
                {
                    BillRequestModel billRequest = new BillRequestModel();
                    billRequest.InvoiceKey = (int)dr["InvoiceKey"];
                    billRequest.InvoiceNo = dr["InvoiceNo"].ToString();
                    billRequest.PONo = dr["PONo"].ToString();
                    billRequest.InvoiceDate = dr["InvoiceDate"].ToString();
                    billRequest.InvoiceType = dr["InvoiceType"].ToString();
                    billRequest.IsApproved = (int)dr["IsApproved"];
                    billRequest.Supplier = dr["Supplier"].ToString();
                    billRequest.IsFinalInvoice = (bool)dr["IsFinalInvoice"];
                    billRequest.TotalInvoiceQty = (decimal)dr["Total_InvoiceQty"];
                    billRequest.TotalInvoiceValue = (decimal)dr["Total_InvoiceValue"];
                    billRequest.TotalDiscount = (decimal)dr["Total_Discount"];
                    billRequest.TotalPaid = (decimal)dr["Total_Paid"];
                    billRequest.Remarks = dr["Remarks"].ToString();

                    users.Add(billRequest);
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

        public BillRequestMaster GetBillInforamtion(int masterId, int userId)
        {
            BillRequestMaster billrequest = new BillRequestMaster();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@InvoiceKey", masterId));
                aList.Add(new SqlParameter("@UserId", userId));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_BillDetailsInfo", aList);
                while (dr.Read())
                {
                    billrequest.InvoiceKey = (int)dr["InvoiceKey"];
                    billrequest.Requestor = dr["Requestor"].ToString();
                    billrequest.InvoiceNo = dr["InvoiceNo"].ToString();
                    billrequest.InvoiceDate = dr["InvoiceDate"].ToString();
                    billrequest.InvoiceTypeName = dr["InvoiceType"].ToString();
                    billrequest.SupplierName = dr["Supplier"].ToString();
                    billrequest.CreatedBy = (int)dr["CreatedBy"];
                    billrequest.CreateDate = dr["CreatedDate"].ToString();
                    billrequest.UpdateDate = dr["UpdatedDate"].ToString();
                    billrequest.FinalInvoice = (bool)dr["IsFinalInvoice"] == true ? 1 : 0;
                    billrequest.Remarks = dr["Remarks"].ToString();
                    billrequest.Notes = dr["Notes"].ToString();
                    billrequest.POKey = (long)dr["POKey"];
                    billrequest.TotalInvoiceQty = (decimal)dr["Total_InvoiceQty"];
                    billrequest.TotalInvoiceValue = (decimal)dr["Total_InvoiceValue"];
                    billrequest.IsApproved = (int)dr["IsApproved"];
                    billrequest.TotalDiscount = (decimal)dr["Total_Discount"];
                    billrequest.TotalPaid = (decimal)dr["Total_Paid"];

                    billrequest.BillInfoList = JsonConvert.DeserializeObject<List<InvoiceInformation>>(dr["BillInfoList"].ToString());

                    billrequest.BillFilesList = JsonConvert.DeserializeObject<List<BillFileUploadDetails>>(dr["BillFilesList"].ToString());                                    
                    
                    billrequest.ApproverList = JsonConvert.DeserializeObject<List<IOUApproverModel>>(dr["ApproverList"].ToString());
                    
                    billrequest.BillComments = JsonConvert.DeserializeObject<List<BillComments>>(dr["BillComments"].ToString());
                    billrequest.BillLogSection = JsonConvert.DeserializeObject<List<LogSection>>(dr["BillLogSection"].ToString());
                }
                return billrequest;
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

        public bool BillApproveOrReject(
          int Progress,
          string CommentText,
          int UserID,
          int VisitorRequestId)
        {
            try
         {
                this.accessManager.SqlConnectionOpen(DataBase.SQQeye);
                return this.accessManager.SaveData("sp_billApproveOrreject", new List<SqlParameter>()
        {
          new SqlParameter("@Progress", (object) Progress),
          new SqlParameter("@CommentText", (object) CommentText),
          new SqlParameter("@UserID", (object) UserID),
          new SqlParameter("@InvoiceKey", (object) VisitorRequestId)
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

        public bool BillCommentSent(int MasterID, int ReviewTo, string ReviewMessage, int UserID)
        {
            try
            {
                this.accessManager.SqlConnectionOpen(DataBase.SQQeye);
                return this.accessManager.SaveData("sp_SentBillComment", new List<SqlParameter>()
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

        public int CheckQuality(int InvoiceKey)
        {

            //DataTable dt = new DataTable();

            int result = 0;

            if (conn.State == 0)
                {
                    conn.Open();
                }

                SqlCommand cmd = new SqlCommand("sp_CheckQuality", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@invoiceKey", SqlDbType.Int).Value = InvoiceKey;
                 result = (int)cmd.ExecuteScalar();
                //SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                //adpt.Fill(dt);
                return result;
            
        }

        public List<BillQuality> GetQualityInforamtion(int invoiceKey)
        {
            List<BillQuality> billQualityList = new List<BillQuality>();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@InvoiceKey", invoiceKey));
                //aList.Add(new SqlParameter("@userId", userID));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_QualityInfo", aList);
                while (dr.Read())
                {
                    BillQuality billQuality = new BillQuality();
                    billQuality.InvoiceKey = (int)dr["InvoiceKey"];
                    billQuality.QualityID = (int)dr["QualityID"];
                    billQuality.InvoiceNo = dr["InvoiceNo"].ToString();
                    billQuality.QualityParam = dr["QualityParam"].ToString();
                    billQuality.QualityResult = dr["QualityResult"].ToString();
                    billQuality.QualityComment = dr["QualityComment"].ToString();
                    billQuality.Rate = (int)dr["Rate"];
                    billQuality.RateName = dr["RateName"].ToString();
                    billQuality.FileName = dr["FileName"].ToString();
                    billQuality.FilPath = dr["FilePath"].ToString();
                    billQuality.UserName = dr["UserName"].ToString();
                    billQualityList.Add(billQuality);
                }
                return billQualityList;
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

        public List<BillQuality> GetUploadedFilesByID(int pmkey, int userid)
        {
            List<BillQuality> filedetails = new List<BillQuality>();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@invoiceKey", pmkey));
                aList.Add(new SqlParameter("@userId", userid));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetQualityFilesByID", aList);
                while (dr.Read())
                {
                    BillQuality bfileup = new BillQuality();
                    bfileup.InvoiceKey = (int)dr["InvoiceKey"];
                    bfileup.FileName = dr["FileName"].ToString();
                    bfileup.FilPath = dr["FilePath"].ToString();
                    filedetails.Add(bfileup);
                }
                return filedetails;
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

        public bool DeleteFileFromDatabase(int capexInfo, string FileName, string FilePath,  int userId)
        {
            bool result = true;
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@invoicekey", capexInfo));
                aList.Add(new SqlParameter("@filename", FileName));
                aList.Add(new SqlParameter("@filepath", FilePath));
                aList.Add(new SqlParameter("@userId", userId));
                result = accessManager.UpdateData("sp_updateFilesFromQualityTables", aList);
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

        public bool DeleteQualityFromDatabase(int invoicekey, int userID)
        {
            bool result = true;
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@invoicekey", invoicekey));
                //aList.Add(new SqlParameter("@userId", userID));
                result = accessManager.DeleteData("sp_deleteQualityData", aList);
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

        public bool UpdateBillQuality(int qualityId, int rate, string rate_name, int userId)
        {
            bool result = true;
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@qualityId", qualityId));
                aList.Add(new SqlParameter("@rate", rate));
                aList.Add(new SqlParameter("@rate_name", rate_name));
                aList.Add(new SqlParameter("@userId", userId));
                result = accessManager.UpdateData("sp_UpdateBillQuality", aList);
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

        public bool UpdateInvoiceBill(int InvoiceDetailsKey, decimal checkQty,  int userId)
        {
            bool result = true;
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@InvoiceDetailsKey", InvoiceDetailsKey));
                aList.Add(new SqlParameter("@checkQty", checkQty));
                aList.Add(new SqlParameter("@userId", userId));
                result = accessManager.UpdateData("sp_UpdateInvoiceBill", aList);
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

        #endregion



    }
}