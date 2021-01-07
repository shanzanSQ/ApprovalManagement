using DocSoOperation.Models;
using SQIndustryThree.DataManager;
using SQIndustryThree.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace SQIndustryThree.DAL
{
    public class CapexApprovalDAL
    {
        private DataAccessManager accessManager = new DataAccessManager();


        public int ModulePermission(int module, int userId)
        {
            int result = 0;
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@moduleId", module));
                aList.Add(new SqlParameter("@userId", userId));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetModulePermission",aList);
                while (dr.Read())
                {
                    result = (int)dr["Permission"];
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


        //get business unit
        public List<BusinessUnit> GetBusinessUnits(int userId)
        {

            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<BusinessUnit> businessUnits = new List<BusinessUnit>();
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@userId", userId));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_UserWiseBusinessUnit",aList);
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
        public List<LocationModel> GetLocation(int userId)
        {

            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<LocationModel> location = new List<LocationModel>();
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@userId", userId));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetLocation", aList);
                while (dr.Read())
                {
                    LocationModel lc = new LocationModel();
                    lc.LocationId = (int)dr["LocationId"];
                    lc.LocationName= dr["LocationName"].ToString();
                    location.Add(lc);
                }
                return location;
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

        public List<CapexInformationDetails> GetAssetCatagoryById(int UserId,int CatagoryID)
        {

            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<CapexInformationDetails> assetList = new List<CapexInformationDetails>();
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@CatagoryId", CatagoryID));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetAssetByCatagory", aList);
                while (dr.Read())
                {
                    CapexInformationDetails cpx= new CapexInformationDetails();
                    cpx.CapexInfoDetailsId= (int)dr["AssetCatagoryId"];
                    cpx.CapexAssetCatagory= dr["AssetCatagoryName"].ToString();
                    
                    assetList.Add(cpx);
                }
                return assetList;
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

        public List<CurrencyTable> LoadCurrency()
        {

            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<CurrencyTable> currencyTables = new List<CurrencyTable>();
                List<SqlParameter> aList = new List<SqlParameter>();
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_LoadCurrency");
                while (dr.Read())
                {
                        CurrencyTable currency = new CurrencyTable();
                        currency.CurrencyId = (int)dr["CurrencyId"];
                        currency.CurrencyName = dr["CurrencyName"].ToString();
                        currencyTables.Add(currency);
                }
                return currencyTables;
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


        //get capex catagory
        public List<CapexCatagory> GetCapexCatagory(int userId)
        {

            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<CapexCatagory> capexcatagorylist = new List<CapexCatagory>();
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@userId", userId));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetCapexCatagory",aList);
                while (dr.Read())
                {
                    CapexCatagory capexcatagory = new CapexCatagory();
                    capexcatagory.CapexCatagoryID = (int)dr["CapexCatagoryID"];
                    capexcatagory.CapexCatagoryName = dr["CapexCatagoryName"].ToString();

                    capexcatagorylist.Add(capexcatagory);
                }
                return capexcatagorylist;
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

        public UserInformation GetApproverList(int catgoryId, int userId)
        {
            UserInformation user = new UserInformation();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@catagoryId", catgoryId));
                aList.Add(new SqlParameter("@userId", userId));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetDepartmentHeadBYCatagory", aList);
                while (dr.Read())
                {
                    user.UserInformationId = (int)dr["UserId"];
                    user.UserInformationName = dr["UserName"].ToString();
                    user.DesignationName = dr["DesignationName"].ToString();
                }
                return user;
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

        public List<UserInformation> GetBFoORCFo(int BusinessUnit,int CatagoryId)
        {
            List<UserInformation> users = new List<UserInformation>();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@businessUnitId", BusinessUnit));
                aList.Add(new SqlParameter("@catagoryID", CatagoryId));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetBFCORCFOBybusinessUnit", aList);
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


        public ResultResponse SaveCapexInformationDatabase(CapexInformationMaster capexmaster, int userId)
        {
            try
            {
                int masterId = 0;
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@CapexName", capexmaster.CapexName));
                aParameters.Add(new SqlParameter("@BusinessUnitId", capexmaster.BusinessUnitId));
                aParameters.Add(new SqlParameter("@CapexCatagoryID", capexmaster.CapexCatagoryID));
                aParameters.Add(new SqlParameter("@CapexAssetType", capexmaster.CapexAssetType));
                aParameters.Add(new SqlParameter("@CapexCreateDate", capexmaster.CapexCreateDate));
                aParameters.Add(new SqlParameter("@CapexDescription", capexmaster.CapexDescription));
                aParameters.Add(new SqlParameter("@capexLocationId", capexmaster.LocationId));
                aParameters.Add(new SqlParameter("@Currency",capexmaster.CurrencyID));
                aParameters.Add(new SqlParameter("@UserID", userId));

                masterId = accessManager.SaveDataReturnPrimaryKey("sp_SaveCapexMasterInfo", aParameters);
                ResultResponse result = new ResultResponse();
                result.pk = masterId;
                result.isSuccess = true;
                foreach (CapexInformationDetails item in capexmaster.CapexInformationDetails)
                {
                    List<SqlParameter> aList = new List<SqlParameter>();
                    aList.Add(new SqlParameter("@CapexInfoDetailsId", '0'));
                    aList.Add(new SqlParameter("@CapexAssetCatagory", item.CapexAssetCatagory));
                    aList.Add(new SqlParameter("@CapexAssetDescription", item.CapexAssetDescription));
                    aList.Add(new SqlParameter("@CapexDetailsQty", item.CapexDetailsQty));
                    aList.Add(new SqlParameter("@CapexUnitPrice", item.CapexUnitPrice));
                    aList.Add(new SqlParameter("@CapexEstimatedCost", item.CapexEstimatedCost));
                    aList.Add(new SqlParameter("@CurrencyID", capexmaster.CurrencyID));
                    aList.Add(new SqlParameter("@CapexInfoId", masterId));
                    result.isSuccess = accessManager.SaveData("sp_SaveCapexDetails", aList);
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

        public List<CapexInformationMaster> GetCapexInfo(int UserId,int status)
        {
            List<CapexInformationMaster> capexInformationMaster = new List<CapexInformationMaster>();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@userId", UserId));
                aList.Add(new SqlParameter("@status", status));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetALLPendingCapex", aList);
                while (dr.Read())
                {
                    CapexInformationMaster capexInformation = new CapexInformationMaster();
                    capexInformation.CapexInfoId = (int)dr["CapexInfoId"];
                    capexInformation.CapexName = dr["CapexName"].ToString();
                    capexInformation.CapexAssetType = dr["CapexAssetType"].ToString();
                    capexInformation.CapexCreateDate = (DateTime)dr["CapexCreateDate"];
                    capexInformation.BusinessUnitName = dr["BusinessUnitName"].ToString();
                    capexInformation.CapexCatagoryName = dr["CapexCatagoryName"].ToString();
                    capexInformation.LocationName = dr["LocationName"].ToString();
                    capexInformation.UserName = dr["UserName"].ToString();
                    capexInformation.IsApproved = (int)dr["IsApproved"];
                    capexInformationMaster.Add(capexInformation);
                }
                return capexInformationMaster;
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


        public CapexInformationMaster GetSavedCapex(int userId,int capexInfoId)
        {
            CapexInformationMaster capexInformationMaster = new CapexInformationMaster();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@capexId", capexInfoId));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetSavedCapex", aList);
                while (dr.Read())
                {
                    capexInformationMaster.CapexInfoId = (int)dr["CapexInfoId"];
                    capexInformationMaster.CapexName = dr["CapexName"].ToString();
                    capexInformationMaster.CapexCatagoryID = (int)dr["CapexCatagoryID"];
                    capexInformationMaster.CapexAssetType = dr["CapexAssetType"].ToString();
                    capexInformationMaster.CapexCreateDate = (DateTime)dr["CapexCreateDate"];
                    capexInformationMaster.CapexDescription = dr["CapexDescription"].ToString();
                    capexInformationMaster.BusinessUnitName = dr["BusinessUnitName"].ToString();
                    capexInformationMaster.CapexCatagoryName = dr["CapexCatagoryName"].ToString();
                    capexInformationMaster.Currency = dr["CurrencyName"].ToString();
                    capexInformationMaster.UserName = dr["UserName"].ToString();
                    capexInformationMaster.LocationName = dr["LocationName"].ToString();
                    capexInformationMaster.UserId = (int)dr["UserId"];
                    capexInformationMaster.Revision = (int)dr["Revision"];
                }
                dr.Close();
                capexInformationMaster.CapexInformationDetails = new List<CapexInformationDetails>();
                dr = accessManager.GetSqlDataReader("sp_GetDetailsSavedCapex", aList);
                while (dr.Read())
                {
                    CapexInformationDetails capexInformation = new CapexInformationDetails();
                    capexInformation.CapexInfoDetailsId = (int)dr["CapexInfoDetailsId"];
                    capexInformation.CapexAssetCatagory = dr["CapexAssetCatagory"].ToString();
                    capexInformation.CapexAssetDescription = dr["CapexAssetDescription"].ToString();
                    capexInformation.CapexDetailsQty =float.Parse(dr["CapexDetailsQty"].ToString());
                    capexInformation.CapexUnitPrice =float.Parse(dr["CapexUnitPrice"].ToString());
                    capexInformation.CapexEstimatedCost =float.Parse(dr["CapexEstimatedCost"].ToString());
                    capexInformationMaster.CapexInformationDetails.Add(capexInformation);
                }
                capexInformationMaster.ApproverQueryModelList = new List<QueryModel>();
                dr.Close();
                dr = accessManager.GetSqlDataReader("sp_GetApproverReview", aList);
                while (dr.Read())
                {
                    QueryModel query = new QueryModel();
                    query.ApprovalId =(int)dr["CapApproverId"];
                    query.IsApproved =(int)dr["IsApproved"];
                    query.ApproverUserId =(int)dr["UserId"];
                    query.UpdateDate =dr["UpdateDate"].ToString();
                    query.ReplyMessage =dr["ReplyMessComment"].ToString();
                    query.ReviewComment =dr["ReviewComment"].ToString();
                    query.DesignationName =dr["DesignationName"].ToString();
                    query.ApproverName = dr["UserName"].ToString();
                    
                    capexInformationMaster.ApproverQueryModelList.Add(query);
                }
                capexInformationMaster.CommentsTables = new List<CommentsTable>();
                dr.Close();
                dr = accessManager.GetSqlDataReader("sp_GetAllCommentsByID", aList);
                while (dr.Read())
                {
                    CommentsTable comment = new CommentsTable();
                    comment.ReviewerByName = dr["ReviwerBY"].ToString();
                    comment.ReviewerToName = dr["ReviwerTo"].ToString();
                    comment.ReviewMessage = dr["ReviewMessage"].ToString();
                    comment.UpdatedBY= (DateTime)dr["UpdatedBY"];
                    capexInformationMaster.CommentsTables.Add(comment);
                }
                capexInformationMaster.LogSections = new List<LogSection>();
                dr.Close();
                dr = accessManager.GetSqlDataReader("sp_viewLogTable", aList);
                while (dr.Read())
                {
                    LogSection logSection = new LogSection();
                    logSection.ActionDate = dr["CreateDate"].ToString();
                    logSection.ActionBy = dr["UserName"].ToString();
                    logSection.ActionStatus = dr["Status"].ToString();
                    logSection.Comments = dr["Comments"].ToString();
                    capexInformationMaster.LogSections.Add(logSection);
                }
                capexInformationMaster.CapexFileUpload = new List<CapexFileUploadDetails>();
                capexInformationMaster.CapexFileUpload = GetUploadedFilesByID(capexInfoId, userId);
                return capexInformationMaster;
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


        public ResultResponse UpdateOrApproveCapex(CapexInformationMaster capexmaster, int userId)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@capexInfoId", capexmaster.CapexInfoId));
                aParameters.Add(new SqlParameter("@userId", userId));
                aParameters.Add(new SqlParameter("@isApproved", capexmaster.IsApproved));
                aParameters.Add(new SqlParameter("@reviewComment", capexmaster.CapexReview));

                ResultResponse result = new ResultResponse();
                result.isSuccess = accessManager.UpdateData("sp_UpdateApproveRequest", aParameters);
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

        public ResultResponse ReviewCapexComment(CommentsTable commets, int userId)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@CapexInfoId", commets.CapexInfoId));
                aParameters.Add(new SqlParameter("@userId", userId));
                aParameters.Add(new SqlParameter("@ReviewTo", commets.ReviewerTo));
                aParameters.Add(new SqlParameter("@ReviewMessage", commets.ReviewMessage));

                ResultResponse result = new ResultResponse();
                result.isSuccess = accessManager.UpdateData("sp_ReviewUpdateComment", aParameters);
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
        public bool FileUploadToDatabase(CapexFileUploadDetails capexFile)
        {
            bool result = true;
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@CapexFileName", capexFile.CapexFileName));
                aList.Add(new SqlParameter("@CapexFilePath", capexFile.CapexFilePath));
                aList.Add(new SqlParameter("@CapexInfoId", capexFile.CapexInfoId));
                aList.Add(new SqlParameter("@UserId", capexFile.userId));
                result = accessManager.SaveData("sp_SaveUploadedFiles", aList);
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

        public bool DeleteFileFromDatabase(int capexInfo,int userId)
        {
            bool result = true;
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@capinfo", capexInfo));
                aList.Add(new SqlParameter("@userId", userId));
                result = accessManager.DeleteData("sp_deleteFilesFromTables", aList);
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
        public ResultResponse RevisedCapexInformation(CapexInformationMaster capexmaster, int userId)
        {
            ResultResponse result = new ResultResponse();
            try
            {
               // int masterId = 0;
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@CapexDescription", capexmaster.CapexDescription));
                aParameters.Add(new SqlParameter("@capexInfo", capexmaster.CapexInfoId));
                aParameters.Add(new SqlParameter("@UserID", userId));

                result.isSuccess = accessManager.UpdateData("sp_updateCapexInfoMaster", aParameters);
                foreach (CapexInformationDetails item in capexmaster.CapexInformationDetails)
                {
                    List<SqlParameter> aList = new List<SqlParameter>();
                    aList.Add(new SqlParameter("@CapexInfoDetailsId", item.CapexInfoDetailsId));
                    aList.Add(new SqlParameter("@CapexAssetCatagory", item.CapexAssetCatagory));
                    aList.Add(new SqlParameter("@CapexAssetDescription", item.CapexAssetDescription));
                    aList.Add(new SqlParameter("@CapexDetailsQty", item.CapexDetailsQty));
                    aList.Add(new SqlParameter("@CapexUnitPrice", item.CapexUnitPrice));
                    aList.Add(new SqlParameter("@CapexEstimatedCost", item.CapexEstimatedCost));
                    aList.Add(new SqlParameter("@CurrencyID", 1));
                    aList.Add(new SqlParameter("@CapexInfoId", capexmaster.CapexInfoId));
                    result.isSuccess = accessManager.SaveData("sp_SaveCapexDetails", aList);
                }
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



        //get files 
        public List<CapexFileUploadDetails> GetUploadedFilesByID(int pmkey,int userid)
        {
            List<CapexFileUploadDetails> filedetails = new List<CapexFileUploadDetails>();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.SQQeye);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@capexId",pmkey));
                aList.Add(new SqlParameter("@userId", userid));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetFileDetails", aList);
                while (dr.Read())
                {
                    CapexFileUploadDetails cpfileup = new CapexFileUploadDetails();
                    cpfileup.CapexFileUploadId = (int)dr["CapexInfoId"];
                    cpfileup.CapexFileName = dr["CapexFileName"].ToString();
                    cpfileup.CapexFilePath = dr["CapexFilePath"].ToString();
                    filedetails.Add(cpfileup);
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
    }
}