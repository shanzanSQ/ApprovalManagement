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

        public ResultResponse BillPOSavetoDatabase(List<BillAprrovalPoDetails> billAprrovalPoDetails, int UserId )
        {
            try
            {
                int masterId = 0;
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                var json = new JavaScriptSerializer().Serialize(billAprrovalPoDetails);
                aParameters.Add(new SqlParameter("@billPoDetails", json));
                aParameters.Add(new SqlParameter("@UserID", UserId));


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

        public DataTable POUniqueNumber(int PONumber)
        {
            try
            {
                DataTable dt = new DataTable();
                if (conn.State == 0)
                {
                    conn.Open();
                }

                string sql = "Select PONo from Bill_POMaster where PONo = @PONumber";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                //cmd.Parameters.Add("@cheque", SqlDbType.Int).Value = supplierId;
                cmd.Parameters.Add("@PONumber", SqlDbType.Int).Value = PONumber;
                SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                adpt.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        public ResultResponse POMasterInsert(int PONumber, string Supplier, int CurrencyID, int TotalQty, 
            decimal TotalValue, DateTime POCreationDate, DateTime POApprovedDate, string Status, int UploadBy)
        {
            try
            {
                int masterId = 0;
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                //string sql = "Select PONumber from Bill_POMaster where PONumber = @PONumber";

                //SqlCommand cmd = new SqlCommand("sp_SavePOMaster", conn);
                //cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.Add("@cheque", SqlDbType.Int).Value = supplierId;
                //cmd.Parameters.Add("@PONo", SqlDbType.Int).Value = PONumber;
                //cmd.Parameters.Add("@Supplier", SqlDbType.NVarChar).Value = Supplier;
                //cmd.Parameters.Add("@CurrencyID", SqlDbType.Int).Value = CurrencyID;
                //cmd.Parameters.Add("@TotalQty", SqlDbType.Int).Value = TotalQty;
                //cmd.Parameters.Add("@TotalValue", SqlDbType.Decimal).Value = TotalValue;
                //cmd.Parameters.Add("@POCreationDate", SqlDbType.DateTime).Value = POCreationDate;
                //cmd.Parameters.Add("@POApprovedDate", SqlDbType.DateTime).Value = POApprovedDate;
                //cmd.Parameters.Add("@Status", SqlDbType.NVarChar).Value = Status;
                //cmd.Parameters.Add("@UploadBy", SqlDbType.Int).Value = UploadBy;
                aParameters.Add(new SqlParameter("@PONo", PONumber));
                aParameters.Add(new SqlParameter("@Supplier", Supplier));
                //aParameters.Add(new SqlParameter("@ExpGenralList", exceptionGenaralInfo));
                aParameters.Add(new SqlParameter("@CurrencyID", CurrencyID));
                aParameters.Add(new SqlParameter("@TotalQty", TotalQty));
                aParameters.Add(new SqlParameter("@TotalValue", TotalValue));
                aParameters.Add(new SqlParameter("@POCreationDate", POCreationDate));
                aParameters.Add(new SqlParameter("@POApprovedDate", POApprovedDate));
                aParameters.Add(new SqlParameter("@Status", Status));
                aParameters.Add(new SqlParameter("@UploadBy", UploadBy));


                masterId = accessManager.SaveDataReturnPrimaryKey("sp_SavePOMaster", aParameters);
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

        public ResultResponse PODetailsInsert(long POKey, int PONumber, string Article, string ArticleCode, string Color,
            string Size, string UOM, decimal POQty, float Rate, decimal POValue)
        {
            try
            {
                int masterId = 0;
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                //                @POKey bigint,
                //@PONo int,
                //@Article nvarchar(200),
                //	@ArticleCode nvarchar(MAX),
                //	@Color nvarchar(200),
                //	@Size nvarchar(200),
                //	@UOM nvarchar(200),
                //	@POQty decimal(18, 2),
                //	@Rate decimal(18, 2),
                //	@POValue decimal(18, 2)
                aParameters.Add(new SqlParameter("@POKey", POKey));
                aParameters.Add(new SqlParameter("@PONo", PONumber));
                aParameters.Add(new SqlParameter("@Article", Article));
                //aParameters.Add(new SqlParameter("@ExpGenralList", exceptionGenaralInfo));
                aParameters.Add(new SqlParameter("@ArticleCode", ArticleCode));
                aParameters.Add(new SqlParameter("@Color", Color));
                aParameters.Add(new SqlParameter("@Size", Size));
                aParameters.Add(new SqlParameter("@UOM", UOM));
                aParameters.Add(new SqlParameter("@POQty", POQty));
                aParameters.Add(new SqlParameter("@Rate", Rate));
                aParameters.Add(new SqlParameter("@POValue", POValue));


                masterId = accessManager.SaveDataReturnPrimaryKey("sp_SavePODetails", aParameters);
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
                    poinformation.Approverlist = JsonConvert.DeserializeObject<List<BillApproverModel>>(dr["Approverlist"].ToString());
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

        public DataTable SupplierList(int userid)
        {
            DataTable dt = new DataTable();
            if (conn.State == 0)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand("sp_supplierList", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@userId", SqlDbType.NVarChar).Value = userid;
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

        public DataTable InvoiceSubcategoryList(int InvoiceType)
        {
            DataTable dt = new DataTable();
            if (conn.State == 0)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand("sp_InvoiceSubCatList", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@InvoiceType", SqlDbType.Int).Value = InvoiceType;
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

        public DataTable SupplierWisePOSearch(int supplierId, string search)
        {
            DataTable dt = new DataTable();
            if (conn.State == 0)
            {
                conn.Open();
            }

            SqlCommand cmd = new SqlCommand("sp_SupplierWisePOList", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@supplierId", SqlDbType.Int).Value = supplierId;
            cmd.Parameters.Add("@search", SqlDbType.NVarChar).Value = search;
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            adpt.Fill(dt);
            return dt;
        }

        public DataTable InvoiceTypeWiseApproverList(int invoiceType, int subcategoryId)
        {
            DataTable dt = new DataTable();
            if (conn.State == 0)
            {
                conn.Open();
            }

            SqlCommand cmd = new SqlCommand("sp_InvoiceTypeWiseApproverList", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@invoiceType", SqlDbType.Int).Value = invoiceType;
            cmd.Parameters.Add("@subCategoryID", SqlDbType.Int).Value = subcategoryId;

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
                var poFilelist = new JavaScriptSerializer().Serialize(billMasterInfo.POFilesList);
                var approverList = new JavaScriptSerializer().Serialize(billMasterInfo.ApproverList);
                var dcFilelist = new JavaScriptSerializer().Serialize(billMasterInfo.BillDocList);
                billMasterInfo.BillInfoList = null;
                billMasterInfo.BillFilesList = null;                
                int noOfApprover = billMasterInfo.ApproverList.Count();
                billMasterInfo.ApproverList = null;
                aParameters.Add(new SqlParameter("@billMasterInfo", json));
                aParameters.Add(new SqlParameter("@billDetails", billDetails));
                //aParameters.Add(new SqlParameter("@ExpGenralList", exceptionGenaralInfo));
                aParameters.Add(new SqlParameter("@FileUploadJSon", billFilelist));
                aParameters.Add(new SqlParameter("@POFileUploadJson", poFilelist));
                aParameters.Add(new SqlParameter("@DCFileUploadJson", dcFilelist));
                aParameters.Add(new SqlParameter("@ApproverJson", approverList));
                aParameters.Add(new SqlParameter("@NoOfApprover", noOfApprover));
                aParameters.Add(new SqlParameter("@UserID", userId));


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

        public bool UpdateQuality(int qualityId, string item, string result,
    string comment, string fileName, string filePath, int created_by)
        {
            try
            {
                bool flag = false;
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@qualityId", qualityId));
                aParameters.Add(new SqlParameter("@item", item));
                aParameters.Add(new SqlParameter("@result", result));
                aParameters.Add(new SqlParameter("@comment", comment));
                aParameters.Add(new SqlParameter("@fileName", fileName));
                aParameters.Add(new SqlParameter("@filePath", filePath));
                aParameters.Add(new SqlParameter("@createdBy", created_by));

                flag = accessManager.UpdateData("sp_UpdateQuality", aParameters);
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
                    billrequest.BusinessUnitId = (int)dr["BusinessUnitId"];
                    billrequest.BusinessUnitName = dr["BusinessUnitName"].ToString();                    
                    billrequest.InvoiceDate = dr["InvoiceDate"].ToString();
                    billrequest.InvoiceTypeID = Convert.ToInt32(dr["InvoiceTypeID"]);
                    billrequest.InvoiceTypeName = dr["InvoiceType"].ToString();
                    billrequest.SubCategoryID = Convert.ToInt32(dr["SubCategoryID"]);
                    billrequest.SubCategoryName = dr["SubCategoryName"].ToString();
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
                    billrequest.DiscountPercent = Convert.ToDouble(dr["Discount_Percent"]);
                    billrequest.DiscountAmt = Convert.ToDecimal(dr["Total_DiscountAmt"]);
                    //billrequest.TotalDiscount = (decimal)dr["Total_Discount"];
                    billrequest.TotalPaid = Convert.ToDecimal(dr["Total_Paid"]);
                    billrequest.TotalAmount = Convert.ToDecimal(dr["Total_Amount"]);
                    billrequest.AdjustmentPercent = Convert.ToDouble(dr["Adv_Adjustment_Percent"]);
                    billrequest.AdjustmentAmt = Convert.ToDecimal(dr["Adv_Adjustment_Amt"]);
                    billrequest.RetaintionPercent = Convert.ToDouble(dr["Retaintion_Percent"]);
                    billrequest.RetaintionAmt = Convert.ToDecimal(dr["Retaintion_Amt"]);
                    billrequest.AdvTotal = Convert.ToDecimal(dr["Total_Adjustment_Amt"]);
                    billrequest.VATPercent = Convert.ToDouble(dr["VAT_Percent"]);
                    billrequest.VATAmt = Convert.ToDecimal(dr["VAT_Amt"]);
                    billrequest.AITPercent = Convert.ToDouble(dr["TAX_Percent"]);
                    billrequest.AITAmt = Convert.ToDecimal(dr["TAX_Amt"]);

                    billrequest.NetValue = Convert.ToDecimal(dr["Net_Value"]);

                    //billrequest.CCID = Convert.ToInt32(dr["CCID"]);
                    billrequest.CostCenter = dr["CostCenter"].ToString();
                    billrequest.CostCenterOwner = dr["CostCenterOwner"].ToString();
                    billrequest.CostCenterDesignation = dr["Designation"].ToString();

                    billrequest.CapexInfoId = Convert.ToInt32(dr["CapexInfoId"]);

                    billrequest.BillInfoList = JsonConvert.DeserializeObject<List<InvoiceInformation>>(dr["BillInfoList"].ToString());

                    billrequest.BillFilesList = JsonConvert.DeserializeObject<List<BillFileUploadDetails>>(dr["BillFilesList"].ToString());
                    billrequest.POFilesList = JsonConvert.DeserializeObject<List<POFileUploadDetails>>(dr["POFilesList"].ToString());
                    billrequest.GRNFilesList = JsonConvert.DeserializeObject<List<GRNFileUploadDetails>>(dr["GRNFilesList"].ToString());
                    billrequest.BillDocList = JsonConvert.DeserializeObject<List<DocumentCenterFileUpload>>(dr["BillDocList"].ToString());
                    billrequest.ApproverList = JsonConvert.DeserializeObject<List<BillApproverModel>>(dr["ApproverList"].ToString());

                    billrequest.ChequeInfoDetails = JsonConvert.DeserializeObject<List<ChequeInfoDetails>>(dr["ChequeInfoDetails"].ToString());

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
          int VisitorRequestId,
          List<GRNFileUploadDetails> grnFilesList,
          List<POFileUploadDetails> poFiles, 
          List<BillFileUploadDetails> billFiles,
          List<DocumentCenterFileUpload> billDocList,
          int ProcurementUserId,
          int CapexInfoId,
          int CostCenterId
          )
        {
            try
            {
                var grnFileList = new JavaScriptSerializer().Serialize(grnFilesList);
                var poFilesList = new JavaScriptSerializer().Serialize(poFiles);
                var billFilesList = new JavaScriptSerializer().Serialize(billFiles);
                var billDocFileList = new JavaScriptSerializer().Serialize(billDocList);
                this.accessManager.SqlConnectionOpen(DataBase.SQQeye);


                return this.accessManager.SaveData("sp_billApproveOrreject", new List<SqlParameter>()
                {
                  new SqlParameter("@Progress",  Progress),
                  new SqlParameter("@CommentText",  CommentText),
                  new SqlParameter("@UserID", UserID),
                  new SqlParameter("@InvoiceKey", VisitorRequestId),
                   new SqlParameter("@ProcurementUserId", ProcurementUserId),
                  new SqlParameter("@CapexInfoId",  CapexInfoId),
                  new SqlParameter("@CostCenterId", CostCenterId),
                   new SqlParameter("@POFileUploadJson", poFilesList),
                    new SqlParameter("@BillFileUploadJson", billFilesList),
                  new SqlParameter("@GRNFileUploadJson", grnFileList),
                  new SqlParameter("@DCFileUploadJson", billDocFileList)
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

        public bool UpdateInvoiceBill(int InvoiceDetailsKey, decimal checkValue, decimal checkQty,  int userId)
        {
            bool result = true;
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@InvoiceDetailsKey", InvoiceDetailsKey));
                aList.Add(new SqlParameter("@checkValue", checkValue));
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


        public bool UpdateBillDetails(int detailsId, decimal qty, decimal value, decimal total)
        {
            bool result = true;
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@detailsId", detailsId));
                aList.Add(new SqlParameter("@qty", qty));
                aList.Add(new SqlParameter("@value", value));
                aList.Add(new SqlParameter("@total", total));
                result = accessManager.UpdateData("sp_UpdateBillDetails", aList);
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

        public bool UpdateBillMaster(int invoiceKey, decimal totalQty,  decimal totalValue, decimal totalPaid, double discountPercent, 
            decimal discountAmt, decimal totalAmount, double vat, decimal vatAmt, decimal netValue, double adjustmentPercent, decimal adjustmentAmt, double retaintionPercent, decimal retaintionAmt, decimal total_adjustment_amt,
            string tax, string taxAmt, List<POFileUploadDetails> poFiles, List<BillFileUploadDetails> billFiles, int userID)
        {
            bool result = true;
            try
            {
                var poFilelist = new JavaScriptSerializer().Serialize(poFiles);
                var billFilelist = new JavaScriptSerializer().Serialize(billFiles);

                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@invoiceKey", invoiceKey));
                aList.Add(new SqlParameter("@totalQty", totalQty));
                aList.Add(new SqlParameter("@totalValue", totalValue));
                aList.Add(new SqlParameter("@totalPaid", totalPaid));
                aList.Add(new SqlParameter("@discountPercent", discountPercent));
                aList.Add(new SqlParameter("@discountAmt", discountAmt));
                aList.Add(new SqlParameter("@totalAmount", totalAmount));
                aList.Add(new SqlParameter("@vat", vat));
                aList.Add(new SqlParameter("@vatAmt", vatAmt));
                aList.Add(new SqlParameter("@netValue", netValue));
                aList.Add(new SqlParameter("@adjustmentPercent", adjustmentPercent));
                aList.Add(new SqlParameter("@adjustmentAmt", adjustmentAmt));
                aList.Add(new SqlParameter("@retaintionPercent", retaintionPercent));
                aList.Add(new SqlParameter("@retaintionAmt", retaintionAmt));
                aList.Add(new SqlParameter("@total_adjustment_amt", total_adjustment_amt));
                aList.Add(new SqlParameter("@tax", tax));
                aList.Add(new SqlParameter("@taxAmt", taxAmt));
                aList.Add(new SqlParameter("@POFileUploadJson", poFilelist));
                aList.Add(new SqlParameter("@BillFileUploadJson", billFilelist));
                aList.Add(new SqlParameter("@createdBy", userID));
                result = accessManager.UpdateData("sp_UpdateBillMaster", aList);
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

        //sp_GetAllApprovedInvoiceList

        public DataTable ApprovedBillList(string Status)
        {
            DataTable dt = new DataTable();
            if (conn.State == 0)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand("sp_GetAllApprovedBillList", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Status", SqlDbType.NVarChar).Value = Status;
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            adpt.Fill(dt);
            return dt;
        }

        public DataTable ApprovedInvoiceList(int SupplierId)
        {
            DataTable dt = new DataTable();
            if (conn.State == 0)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand("sp_GetAllApprovedInvoiceList", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SupplierId", SqlDbType.Int).Value = SupplierId;
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            adpt.Fill(dt);
            return dt;
        }

        public DataTable SupplierWiseInvoiceSearch(int supplierId, string search)
        {
            DataTable dt = new DataTable();
            if (conn.State == 0)
            {
                conn.Open();
            }

            SqlCommand cmd = new SqlCommand("sp_SupplierWiseInvoiceList", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@supplierId", SqlDbType.Int).Value = supplierId;
            cmd.Parameters.Add("@search", SqlDbType.NVarChar).Value = search;
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            adpt.Fill(dt);
            return dt;
        }

        //sp_GetApprovedPOFromInvoice

        public DataTable GetApprovedPOFromInvoice(int supplierId, string invoiceKey)
        {

            DataTable dt = new DataTable();
            if (conn.State == 0)
            {
                conn.Open();
            }
            

            SqlCommand cmd = new SqlCommand("sp_GetApprovedPOFromInvoice", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@supplierId", SqlDbType.Int).Value = supplierId;
            //var parms = valuearray.Select((s, i) => i.ToString()).ToArray();
            //var inclause = string.Join(",", valuearray);

            cmd.Parameters.Add("@invoiceKey", SqlDbType.NVarChar).Value = invoiceKey;
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            adpt.Fill(dt);
            return dt;
        }

        public DataTable GETSERILANO(string doctype)
        {

            DataTable dt = new DataTable();
            if (conn.State == 0)
            {
                conn.Open();
            }


            SqlCommand cmd = new SqlCommand("SP_GETSERILANO", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@DOCTYPE", SqlDbType.NVarChar).Value = doctype;
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            adpt.Fill(dt);
            return dt;
        }


        public ChequeInfo GetChequeInforamtion(int masterId)
        {
            ChequeInfo cheque = new ChequeInfo();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@InvoiceKey", masterId));
                // aList.Add(new SqlParameter("@UserId", userId));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_chequeInfoDetails", aList);
                while (dr.Read())
                {
                    cheque.InvoiceKey = (int)dr["InvoiceKey"];
                    cheque.InvoiceNo = dr["InvoiceNo"].ToString();
                    cheque.TotalInvoiceQty = Convert.ToDecimal(dr["Total_InvoiceQty"]);
                    cheque.TotalInvoiceValue = Convert.ToDecimal(dr["Total_InvoiceValue"]);
                    cheque.NetValue = Convert.ToDecimal(dr["Net_Value"]);
                    cheque.PaidAmount = Convert.ToDecimal(dr["PaidAmount"]);
                    cheque.BalanceAmount = Convert.ToDecimal(dr["BalanceAmount"]);
                    cheque.ApproverList = JsonConvert.DeserializeObject<List<BillApproverModel>>(dr["ApproverList"].ToString());
                    cheque.BillInfoList = JsonConvert.DeserializeObject<List<InvoiceInformation>>(dr["BillInfoList"].ToString());
                    cheque.ChequeInfoDetails = JsonConvert.DeserializeObject<List<ChequeInfoDetails>>(dr["ChequeInfoDetails"].ToString());
                }
                return cheque;
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

        public DataTable ChequeStatusList()
        {
            DataTable dt = new DataTable();
            if (conn.State == 0)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand("sp_ChequeStatusList", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            adpt.Fill(dt);
            return dt;
        }

        public bool SaveCheckInfo(int invoiceKey, string cheque, string amount, string date, string status, int created_by)
        {
            try
            {
                bool flag = false;
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@invoiceKey", invoiceKey));
                aParameters.Add(new SqlParameter("@cheque", cheque));
                aParameters.Add(new SqlParameter("@amount", amount));
                aParameters.Add(new SqlParameter("@date", date));
                aParameters.Add(new SqlParameter("@status", status));
                aParameters.Add(new SqlParameter("@createdBy", created_by));

                flag = accessManager.SaveData("sp_SaveCheckInfo", aParameters);
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

        public bool SaveAllocation(int invoiceKey, string invoiceNo, string poNo, string allocatedValue)
        {
            try
            {
                bool flag = false;
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@invoiceKey", invoiceKey));
                aParameters.Add(new SqlParameter("@invoiceNo", invoiceNo));
                aParameters.Add(new SqlParameter("@poNo", poNo));
                aParameters.Add(new SqlParameter("@allocatedValue", allocatedValue));

                flag = accessManager.SaveData("sp_SaveAllocation", aParameters);
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

        public bool UpdateChequeInfo(int chequeInfoId, string chequeNo, decimal amount, string date)
        {
            try
            {
                bool flag = false;
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@chequeInfoId", chequeInfoId));
                aParameters.Add(new SqlParameter("@chequeNo", chequeNo));
                aParameters.Add(new SqlParameter("@amount", amount));
                aParameters.Add(new SqlParameter("@date", date));

                flag = accessManager.UpdateData("sp_UpdateChequeInfo", aParameters);
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

        public DataTable ProcurementUserList()
        {
            DataTable dt = new DataTable();
            if (conn.State == 0)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand("sp_ProcurementUserList", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            adpt.Fill(dt);
            return dt;
        }

        public DataTable CostCenterList(int unitId)
        {
            DataTable dt = new DataTable();
            if (conn.State == 0)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand("sp_CostCenterList", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@unitId", SqlDbType.Int).Value = unitId;
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            adpt.Fill(dt);
            return dt;
        }

        public int DeletePOFileFromDatatbase(int FileID, int type)
        {
            int i = 0;
            if (conn.State == 0)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand("sp_DeleteUploadedFile", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FileId", SqlDbType.Int).Value = FileID;
            cmd.Parameters.Add("@Type", SqlDbType.Int).Value = type;
            i = cmd.ExecuteNonQuery();
            return i;
        }

        public ResultResponse SaveBillAllocationRequest(BillAllocationMaster billAllocationMasterInfo, int userId)
        {
            try
            {
                int masterId = 0;
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                var json = new JavaScriptSerializer().Serialize(billAllocationMasterInfo);
               
                var billDetails = new JavaScriptSerializer().Serialize(billAllocationMasterInfo.AllocationDetails);

                billAllocationMasterInfo.AllocationDetails = null;                
                
                aParameters.Add(new SqlParameter("@billAllocationMasterInfo", json));
                aParameters.Add(new SqlParameter("@billAllocationDetails", billDetails));
                aParameters.Add(new SqlParameter("@UserID", userId));


                masterId = accessManager.SaveDataReturnPrimaryKey("sp_CreateBillAllocationRequest", aParameters);
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

        public DataTable chequeUniqueNumber(string cheque)
        {
            try
            {
                DataTable dt = new DataTable();
                if (conn.State == 0)
                {
                    conn.Open();
                }

                string sql = "Select ChequeOrTTNo from Bill_AllocationMaster where ChequeOrTTNo = @cheque";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                //cmd.Parameters.Add("@cheque", SqlDbType.Int).Value = supplierId;
                cmd.Parameters.Add("@cheque", SqlDbType.NVarChar).Value = cheque;
                SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                adpt.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            } 
        }

        public DataTable ChequePaymentDetails()
        {
            try
            {
                DataTable dt = new DataTable();
                if (conn.State == 0)
                {
                    conn.Open();
                }

                SqlCommand cmd = new SqlCommand("sp_BillChequePaymentDetails", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.Add("@cheque", SqlDbType.Int).Value = supplierId;
                //cmd.Parameters.Add("@cheque", SqlDbType.NVarChar).Value = cheque;
                SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                adpt.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            
        }

        public DataTable BillDashboard(string SupplierId, string UnitId)
        {
            try
            {
                DataTable dt = new DataTable();

                if (conn.State == 0)
                {
                    conn.Open();
                }


                SqlCommand cmd = new SqlCommand("sp_BillReportDashboard", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@SupplierID", SqlDbType.NVarChar).Value = SupplierId;
                cmd.Parameters.Add("@UnitID", SqlDbType.NVarChar).Value = UnitId;
                SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                adpt.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        #endregion



    }
}