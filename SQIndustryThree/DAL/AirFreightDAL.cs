using Dapper;
using DocSoOperation.Models;
using Newtonsoft.Json;
using SQIndustryThree.DataManager;
using SQIndustryThree.Models;
using SQIndustryThree.Models.AirFreightTracker;
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
    public class AirFreightDAL
    {
        private DataAccessManager accessManager = new DataAccessManager();
        private string connStr = ConfigurationManager.ConnectionStrings["SQQEYEDatabase"].ConnectionString;

        public List<BuyerListModel> LoadBuyerList()
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<BuyerListModel> buyerListModels = new List<BuyerListModel>();
                List<SqlParameter> aaList = new List<SqlParameter>();
                //aList.Add(new SqlParameter("@userId", userId));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_getAllBuyerList", aaList);
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

        public List<ExceptionRequestMaster> LoadERList(int buyerId, int businessUnitId)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<ExceptionRequestMaster> ERmasters = new List<ExceptionRequestMaster>();
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@BuyerId", buyerId));
                aList.Add(new SqlParameter("@BusinessUnitId", businessUnitId));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_LoadERList", aList);
                while (dr.Read())
                {
                    ExceptionRequestMaster erMaster = new ExceptionRequestMaster();
                    erMaster.ExceptionMasterId = (int)dr["ExceptionMasterId"];
                    ERmasters.Add(erMaster);
                }
                return ERmasters;
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

        public List<Forwarder> LoadForwardersList()
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<Forwarder> forwarders = new List<Forwarder>();
                List<SqlParameter> aList = new List<SqlParameter>();
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_ForwarderList", aList);
                while (dr.Read())
                {
                    Forwarder forwarder = new Forwarder();
                    forwarder.ForwarderId = (int)dr["ForwarderId"];
                    forwarder.Name = dr["Name"].ToString();
                    forwarders.Add(forwarder);
                }
                return forwarders;
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

        public ExceptionRequestMaster LoadDataAgainstERId(int id)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                ExceptionRequestMaster ERData = new ExceptionRequestMaster();
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@Id", id));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_LoadDataForER", aList);
                if (dr.Read())
                {
                    ERData.ExceptionMasterId = (int)dr["ExceptionMasterId"];
                    ERData.CreateDate = dr["CreateDate"].ToString();
                }
                return ERData;
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

        public List<ExceptionGenaralInformation> LoadPOAgainstERId(int id)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<ExceptionGenaralInformation> POList = new List<ExceptionGenaralInformation>();
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@Id", id));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_LoadPOForERId", aList);
                while (dr.Read())
                {
                    POList.Add(new ExceptionGenaralInformation
                    {
                        ExceptionGenralId = (int)dr["ExceptionGenralId"],
                        PO = dr["PO"].ToString(),
                        //Quantity = (int)dr["Quantity"]
                    });
                }
                return POList;
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

        public ResultResponse SubmitDataForSQ(AirFreightMaster data)
        {
            ResultResponse result = new ResultResponse();
            int AirFreightMasterId = CreateAirFreightMaster(data);
            foreach (var item in data.AirFreightDetails)
                result = CreateAirFreightDetails(AirFreightMasterId, item);

            return result;
        }
        
        public ResultResponse SubmitDataForMod(AirFreightMaster data)
        {
            ResultResponse result = new ResultResponse();
            int temp = EditAirFreightMaster(data);
            foreach (var item in data.AirFreightDetails)
                result = EditAirFreightDetails(data.AirFreightMasterId, item);

            return result;
        }

        public int CreateAirFreightMaster(AirFreightMaster data)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                var aiFreightDetailsJson = new JavaScriptSerializer().Serialize(data.AirFreightDetails);
                aParameters.Add(new SqlParameter("@ExceptionMasterId", data.ExceptionMasterId));
                aParameters.Add(new SqlParameter("@BusinessUnitId", data.BusinessUnitId));
                aParameters.Add(new SqlParameter("@BuyersNameId", data.BuyersNameId));
                aParameters.Add(new SqlParameter("@ForwarderId", data.ForwarderId));
                return accessManager.SaveDataReturnPrimaryKey("sp_AddNewAirFreightMaster", aParameters);

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
        
        public int EditAirFreightMaster(AirFreightMaster data)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@AirFreightMasterId", data.AirFreightMasterId));
                aParameters.Add(new SqlParameter("@ExceptionMasterId", data.ExceptionMasterId));
                aParameters.Add(new SqlParameter("@BusinessUnitId", data.BusinessUnitId));
                aParameters.Add(new SqlParameter("@BuyersNameId", data.BuyersNameId));
                aParameters.Add(new SqlParameter("@ForwarderId", data.ForwarderId));
                return accessManager.SaveDataReturnPrimaryKey("sp_EditAirFreightMaster", aParameters);
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

        public ResultResponse CreateAirFreightDetails(int AirFreightMasterId, AirFreightDetails data)
        {
            try
            {
                using (IDbConnection cnn = (IDbConnection)new SqlConnection(this.connStr))
                {
                    if (cnn.State == ConnectionState.Closed)
                        cnn.Open();
                    var aiFreightDetailsFileJson = new JavaScriptSerializer().Serialize(data.AirFreightFiles);
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@AirFreightMasterId", (object)AirFreightMasterId);
                    dynamicParameters.Add("@ExceptionGenralId", (object)data.ExceptionGenralId);
                    dynamicParameters.Add("@PO", (object)data.PO);
                    dynamicParameters.Add("@ModeOfShipmentId", (object)data.ModeOfShipmentId);
                    dynamicParameters.Add("@PortOfDestination", (object)data.PortOfDestination);
                    dynamicParameters.Add("@CountryOfDestination", (object)data.CountryOfDestination);
                    dynamicParameters.Add("@IncotermId", (object)data.IncotermId);
                    dynamicParameters.Add("@InvoiceNo", (object)data.InvoiceNo);
                    dynamicParameters.Add("@InvoiceValueInUSD", (object)data.InvoiceValueInUSD);
                    dynamicParameters.Add("@QTYInPack", (object)data.QTYInPack);
                    dynamicParameters.Add("@QTYInPcERApproved", (object)data.QTYInPcERApproved);
                    dynamicParameters.Add("@QtyInPc", (object)data.QtyInPc);
                    dynamicParameters.Add("@QtyInCtn", (object)data.QtyInCtn);
                    dynamicParameters.Add("@GrossWeightInKg", (object)data.GrossWeightInKg);
                    dynamicParameters.Add("@HAWBLNo", (object)data.HAWBLNo);
                    dynamicParameters.Add("@HAWBLDate", (object)data.HAWBLDate);
                    dynamicParameters.Add("@ChargeableWeightInKgERApproved", (object)data.ChargeableWeightInKgERApproved);
                    dynamicParameters.Add("@FreightAmountInUSDErApproved", (object)data.FreightAmountInUSDErApproved);
                    dynamicParameters.Add("@ChargeableWeightInKg", (object)data.ChargeableWeightInKg);
                    dynamicParameters.Add("@FreightAmountInUSD", (object)data.FreightAmountInUSD);
                    dynamicParameters.Add("@FrieghtRatePerKgERApproved", (object)data.FrieghtRatePerKgERApproved);
                    dynamicParameters.Add("@FreightRatePerKg", (object)data.FreightRatePerKg);
                    dynamicParameters.Add("@FreightAmountInBDT", (object)data.FreightAmountInBDT);
                    dynamicParameters.Add("@FreightInvoiceNo", (object)data.FreightInvoiceNo);
                    dynamicParameters.Add("@FreightInvoiceReceivedDate", (object)data.FreightInvoiceReceivedDate);
                    dynamicParameters.Add("@BillSubDateForPayment", (object)data.BillSubDateForPayment);
                    dynamicParameters.Add("@PaymentDate", (object)data.PaymentDate);
                    dynamicParameters.Add("@CHQPOSubmitDateToForwarder", (object)data.CHQPOSubmitDateToForwarder);
                    dynamicParameters.Add("@AWABReleaseDate", (object)data.AWABReleaseDate);
                    dynamicParameters.Add("@Remarks", (object)data.Remarks);
                    dynamicParameters.Add("@AiFreightDetailsFileJson", (object)aiFreightDetailsFileJson);

                    int num = cnn.Execute("sp_AddNewAirFreightDetails", (object)dynamicParameters, commandType: new CommandType?(CommandType.StoredProcedure));
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
        
        public ResultResponse EditAirFreightDetails(int AirFreightMasterId, AirFreightDetails data)
        {
            try
            {
                using (IDbConnection cnn = (IDbConnection)new SqlConnection(this.connStr))
                {
                    if (cnn.State == ConnectionState.Closed)
                        cnn.Open();
                    var aiFreightDetailsFileJson = new JavaScriptSerializer().Serialize(data.AirFreightFiles);
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@AirFreightMasterId", (object)AirFreightMasterId);
                    dynamicParameters.Add("@AirFreightDetailsId", (object)data.AirFreightDetailsId);
                    dynamicParameters.Add("@ExceptionGenralId", (object)data.ExceptionGenralId);
                    dynamicParameters.Add("@PO", (object)data.PO);
                    dynamicParameters.Add("@ModeOfShipmentId", (object)data.ModeOfShipmentId);
                    dynamicParameters.Add("@PortOfDestination", (object)data.PortOfDestination);
                    dynamicParameters.Add("@CountryOfDestination", (object)data.CountryOfDestination);
                    dynamicParameters.Add("@IncotermId", (object)data.IncotermId);
                    dynamicParameters.Add("@InvoiceNo", (object)data.InvoiceNo);
                    dynamicParameters.Add("@InvoiceValueInUSD", (object)data.InvoiceValueInUSD);
                    dynamicParameters.Add("@QTYInPack", (object)data.QTYInPack);
                    dynamicParameters.Add("@QTYInPcERApproved", (object)data.QTYInPcERApproved);
                    dynamicParameters.Add("@QtyInPc", (object)data.QtyInPc);
                    dynamicParameters.Add("@QtyInCtn", (object)data.QtyInCtn);
                    dynamicParameters.Add("@GrossWeightInKg", (object)data.GrossWeightInKg);
                    dynamicParameters.Add("@HAWBLNo", (object)data.HAWBLNo);
                    dynamicParameters.Add("@HAWBLDate", (object)data.HAWBLDate);
                    dynamicParameters.Add("@ChargeableWeightInKgERApproved", (object)data.ChargeableWeightInKgERApproved);
                    dynamicParameters.Add("@FreightAmountInUSDErApproved", (object)data.FreightAmountInUSDErApproved);
                    dynamicParameters.Add("@ChargeableWeightInKg", (object)data.ChargeableWeightInKg);
                    dynamicParameters.Add("@FreightAmountInUSD", (object)data.FreightAmountInUSD);
                    dynamicParameters.Add("@FrieghtRatePerKgERApproved", (object)data.FrieghtRatePerKgERApproved);
                    dynamicParameters.Add("@FreightRatePerKg", (object)data.FreightRatePerKg);
                    dynamicParameters.Add("@FreightAmountInBDT", (object)data.FreightAmountInBDT);
                    dynamicParameters.Add("@FreightInvoiceNo", (object)data.FreightInvoiceNo);
                    dynamicParameters.Add("@FreightInvoiceReceivedDate", (object)data.FreightInvoiceReceivedDate);
                    dynamicParameters.Add("@BillSubDateForPayment", (object)data.BillSubDateForPayment);
                    dynamicParameters.Add("@PaymentDate", (object)data.PaymentDate);
                    dynamicParameters.Add("@CHQPOSubmitDateToForwarder", (object)data.CHQPOSubmitDateToForwarder);
                    dynamicParameters.Add("@AWABReleaseDate", (object)data.AWABReleaseDate);
                    dynamicParameters.Add("@Remarks", (object)data.Remarks);
                    dynamicParameters.Add("@AiFreightDetailsFileJson", (object)aiFreightDetailsFileJson);

                    int num = cnn.Execute("sp_EditAirFreightDetails", (object)dynamicParameters, commandType: new CommandType?(CommandType.StoredProcedure));
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

        public List<AirFreightMaster> GetAllAirFreightList()
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<AirFreightMaster> airFreights = new List<AirFreightMaster>();
                List<SqlParameter> aList = new List<SqlParameter>();
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_AirFreightList", aList);
                while (dr.Read())
                {
                    AirFreightMaster airFreight = new AirFreightMaster();
                    airFreight.AirFreightMasterId = (int)dr["AirFreightMasterId"];
                    airFreight.ExceptionMasterId = (int)dr["ExceptionMasterId"];
                    airFreight.ERDate = airFreight.ExceptionMasterId == 0? "---" : LoadDataAgainstERId((int)dr["ExceptionMasterId"]).CreateDate;
                    airFreight.BusinessUnit = dr["BuyerName"].ToString();
                    airFreight.BuyersName = dr["BusinessUnitName"].ToString();
                    airFreight.Forwarder = dr["Name"].ToString();
                    airFreights.Add(airFreight);
                }
                return airFreights;
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

        public List<AirFreightDetails> GetPoListForAirFreightMaster(int id)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<AirFreightDetails> airFreights = new List<AirFreightDetails>();
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@Id", id));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_AirFreightPOListForMaster", aList);
                while (dr.Read())
                {
                    AirFreightDetails airFreight = new AirFreightDetails();
                    airFreight.AirFreightDetailsId = (int)dr["AirFreightDetailsId"];
                    airFreight.PO = dr["PO"].ToString();
                    airFreight.ParentPO = dr["ParentPO"].ToString();
                    airFreight.ModeOfShipment = (int)dr["ModeOfShipmentId"] == 0? "---" : (int)dr["ModeOfShipmentId"] == 1? "Air" : "Sea & Air";
                    airFreight.ModeOfShipmentId = (int)dr["ModeOfShipmentId"];
                    airFreight.PortOfDestination = dr["PortOfDestination"].ToString();
                    airFreight.CountryOfDestination = dr["CountryOfDestination"].ToString();
                    airFreight.Incoterm = GetIncoterm(dr["IncotermId"].ToString() == ""? 0: (int)dr["IncotermId"]);
                    airFreight.IncotermId = dr["IncotermId"].ToString() == ""? 0: (int)dr["IncotermId"];
                    airFreight.InvoiceNo = dr["InvoiceNo"].ToString();
                    airFreight.InvoiceValueInUSD = (decimal)dr["InvoiceValueInUSD"];
                    airFreight.QTYInPack = (int)dr["QTYInPack"];
                    airFreight.QTYInPcERApproved = (int)dr["QTYInPcERApproved"];
                    airFreight.QtyInPc = (int)dr["QtyInPc"];
                    airFreight.QtyInCtn = (int)dr["QtyInCtn"];
                    airFreight.GrossWeightInKg = (decimal)dr["GrossWeightInKg"];
                    airFreight.HAWBLNo = dr["HAWBLNo"].ToString();
                    airFreight.HAWBLDate = dr["HAWBLDate"].ToString();
                    //airFreight.HAWBLDate = dr["HAWBLDate"].ToString() != null? (DateTime)dr["HAWBLDate"] : null;
                    airFreight.ChargeableWeightInKgERApproved = (decimal)dr["ChargeableWeightInKgERApproved"];
                    airFreight.FreightAmountInUSDErApproved = (decimal)dr["FreightAmountInUSDErApproved"];
                    airFreight.ChargeableWeightInKg = (decimal)dr["ChargeableWeightInKg"];
                    airFreight.FreightAmountInUSD = (decimal)dr["FreightAmountInUSD"];
                    airFreight.FrieghtRatePerKgERApproved = (decimal)dr["FrieghtRatePerKgERApproved"];
                    airFreight.FreightRatePerKg = (decimal)dr["FreightRatePerKg"];
                    airFreight.FreightAmountInBDT = (decimal)dr["FreightAmountInBDT"];
                    airFreight.FreightInvoiceNo = dr["FreightInvoiceNo"].ToString();
                    airFreight.FreightInvoiceReceivedDate = dr["FreightInvoiceReceivedDate"].ToString();
                    airFreight.BillSubDateForPayment = dr["BillSubDateForPayment"].ToString();
                    airFreight.PaymentDate = dr["PaymentDate"].ToString();
                    airFreight.CHQPOSubmitDateToForwarder = dr["CHQPOSubmitDateToForwarder"].ToString();
                    airFreight.AWABReleaseDate = dr["AWABReleaseDate"].ToString();
                    airFreight.Remarks = dr["Remarks"].ToString();
                    airFreight.AirFreightFiles = GetAttachments((int)dr["AirFreightDetailsId"]);
                    airFreights.Add(airFreight);
                }
                return airFreights;
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

        public string GetIncoterm(int id)
        {
            string temp = "---";
            switch (id)
            {
                case 1:
                    temp = "EXW";
                    break;
                case 2:
                    temp = "FCA";
                    break;
                case 3:
                    temp = "CPT";
                    break;
                case 4:
                    temp = "CIP";
                    break;
                case 5:
                    temp = "DAT";
                    break;
                case 6:
                    temp = "DAP";
                    break;
                case 7:
                    temp = "DDP";
                    break;
                case 8:
                    temp = "FAS";
                    break;
                case 9:
                    temp = "FOB";
                    break;
                case 10:
                    temp = "CFT";
                    break;
                case 11:
                    temp = "CIF";
                    break;
                default:
                    temp = "---";
                    break;
            }

            return temp;
        }

        public List<AirFreightFile> GetAttachments(int id)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<AirFreightFile> airFreights = new List<AirFreightFile>();
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@Id", id));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_AirFreightFileFromDId", aList);
                while (dr.Read())
                {
                    AirFreightFile airFreightFile = new AirFreightFile();
                    airFreightFile.FileName = dr["FileName"].ToString();
                    airFreights.Add(airFreightFile);
                }
                return airFreights;
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

        public AirFreightMaster GetAirFreightMasterById(int id)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                var airFreightMaster = new AirFreightMaster();
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@Id", id));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetAirFreightMasterDataById", aList);
                if(dr.Read())
                {
                    airFreightMaster.ExceptionMasterId = (int)dr["ExceptionMasterId"];
                    airFreightMaster.BusinessUnitId = (int)dr["BusinessUnitId"];
                    airFreightMaster.BuyersNameId = (int)dr["BuyersNameId"];
                    airFreightMaster.ForwarderId = (int)dr["ForwarderId"];
                    airFreightMaster.FrieghtCostOnACOf = dr["FrieghtCostOnACOf"].ToString() == "" ? 0 : (int)dr["FrieghtCostOnACOf"];
                }
                return airFreightMaster;
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