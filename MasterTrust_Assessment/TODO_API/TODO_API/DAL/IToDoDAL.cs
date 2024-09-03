using System.Data;
using System.Data.SqlClient;

namespace TODO_API.DAL
{
    public interface IToDoDAL
    {
        int AddUpdateTODODetail(SqlParameter[] objSqlParameter);
        DataSet GetTODOList(SqlParameter[] objSqlParameter);
        int DeleteTODOData(SqlParameter[] objSqlParameter);
        DataSet GetTODOLisTPagination(SqlParameter[] objSqlParameter);
    }
}
