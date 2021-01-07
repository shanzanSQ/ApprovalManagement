using Newtonsoft.Json;
using SQIndustryThree.DataManager;
using SQIndustryThree.Models;
using SQIndustryThree.Models.BillApproval;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SQIndustryThree.DAL
{
    public class BillApprovalDAL
    {
        private DataAccessManager accessManager = new DataAccessManager();
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
    }
}