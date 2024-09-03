using System.Data;
using System.Data.SqlClient;
using TODO_API.DAL;
using TODO_API.Model;

namespace TODO_API.BAL
{
    public class ToDoBAL : IToDoBAL
    {
        #region Fields
        private readonly IToDoDAL _ObjDAL;
        #endregion

        #region ctor
        public ToDoBAL(IToDoDAL ObjDAL)
        {
            _ObjDAL = ObjDAL;
        }
        #endregion

        public Task<List<TODOModel>> GetTODOList()
        {
            try
            {
                List<TODOModel> lstData = new List<TODOModel>();

                DataSet ds = GetTODOData(0);

                foreach (DataRow drItem in ds.Tables[0].Rows)
                {
                    TODOModel objModel = new TODOModel();
                    objModel.Id = Convert.ToInt32(drItem["fld_Id"]);
                    objModel.Title = Convert.ToString(drItem["fld_Title"]);
                    objModel.Description = Convert.ToString(drItem["fld_Description"]);
                    objModel.Status = Convert.ToBoolean(drItem["fld_Status"]) ? "Completed" : "Not Completed";
                    lstData.Add(objModel);
                }

                return Task.FromResult(lstData);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public Task<TODOModel> GetTODOListById(int id)
        {
            try
            {
                TODOModel objModel = new TODOModel();
                DataSet ds = GetTODOData(id);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow drItem = ds.Tables[0].Rows[0];
                    objModel.Id = Convert.ToInt32(drItem["fld_Id"]);
                    objModel.Title = Convert.ToString(drItem["fld_Title"]);
                    objModel.Description = Convert.ToString(drItem["fld_Description"]);
                    objModel.IsCompleted = Convert.ToBoolean(drItem["fld_Status"]);
                    objModel.Status = objModel.IsCompleted ? "Completed" : "Not Completed";
                }

                return Task.FromResult(objModel);
            }
            catch (Exception)
            {
                throw;
            }
        }



        public Task<int> AddUpdateTODOItems(TODOModel objModel)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[6];
                sqlParams[0] = new SqlParameter("@Id", objModel.Id);
                sqlParams[1] = new SqlParameter("@Title", objModel.Title);
                sqlParams[2] = new SqlParameter("@Description", objModel.Description);
                sqlParams[3] = new SqlParameter("@Status", objModel.IsCompleted);
                sqlParams[4] = new SqlParameter("@CreatedBy", objModel.CreatedBy == null ? DBNull.Value : objModel.CreatedBy);
                sqlParams[5] = new SqlParameter("@ResponseOut", SqlDbType.Int);
                sqlParams[5].Direction = ParameterDirection.Output;

                int retVal = _ObjDAL.AddUpdateTODODetail(sqlParams);

                int responseOut = Convert.ToInt32(sqlParams[5].Value == DBNull.Value ? 0 : sqlParams[5].Value);

                return Task.FromResult(responseOut);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<bool> DeleteTODOItem(int id)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@Id",id);

                int retVal = _ObjDAL.DeleteTODOData(sqlParams);

                if(retVal > 0)
                {
                    return Task.FromResult(true);
                }
                else
                {
                    return Task.FromResult(false);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<List<TODOModel>> GetTODOListPagination(int PageNum, int PageSize)
        {
            try
            {
                List<TODOModel> lstData = new List<TODOModel>();

                SqlParameter[] sqlParams = new SqlParameter[2];

                sqlParams[0] = new SqlParameter("@PageNum", PageNum);
                sqlParams[1] = new SqlParameter("@PageSize", PageSize);

                DataSet ds = _ObjDAL.GetTODOLisTPagination(sqlParams);

                foreach (DataRow drItem in ds.Tables[0].Rows)
                {
                    TODOModel objModel = new TODOModel();
                    objModel.Id = Convert.ToInt32(drItem["fld_Id"]);
                    objModel.Title = Convert.ToString(drItem["fld_Title"]);
                    objModel.Description = Convert.ToString(drItem["fld_Description"]);
                    objModel.Status = Convert.ToBoolean(drItem["fld_Status"]) ? "Completed" : "Not Completed";
                    lstData.Add(objModel);
                }

                return Task.FromResult(lstData);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private DataSet GetTODOData(int Id)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];

            sqlParams[0] = new SqlParameter("@Id", Id);

            return _ObjDAL.GetTODOList(sqlParams);
        }
    }
}
