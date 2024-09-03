using System.Data.SqlClient;
using TODO_API.CommonUtilities;
using System.Data;

namespace TODO_API.DAL
{
    public class ToDoDAL: IToDoDAL
    {
        public int AddUpdateTODODetail(SqlParameter[] objSqlParameter)
        {
			try
			{
				return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionString, CommandType.StoredProcedure, "[dbo].[usp_Add_Update_TODO_Data]", objSqlParameter);
			}
			catch (Exception)
			{
				throw;
			}
        }

		public DataSet GetTODOList(SqlParameter[] objSqlParameter)
		{
			try
			{
				return SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, "[dbo].[usp_Get_TODO_List]", objSqlParameter);
			}
			catch (Exception)
			{
				throw;
			}
		}

        public DataSet GetTODOLisTPagination(SqlParameter[] objSqlParameter)
        {
            try
            {
                return SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, "[dbo].[usp_Get_TODO_Pagination]", objSqlParameter);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int DeleteTODOData(SqlParameter[] objSqlParameter)
		{
			try
			{

				return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionString, CommandType.StoredProcedure, "[dbo].[usp_Delete_TODO_Data]", objSqlParameter);
			}
			catch (Exception)
			{
				throw;
			}
		}
    }
}
