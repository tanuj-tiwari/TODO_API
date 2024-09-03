using TODO_API.Model;

namespace TODO_API.BAL
{
    public interface IToDoBAL
    {
        Task<List<TODOModel>> GetTODOList();
        Task<TODOModel> GetTODOListById(int id);
        Task<int> AddUpdateTODOItems(TODOModel objModel);
        Task<bool> DeleteTODOItem(int id);
        Task<List<TODOModel>> GetTODOListPagination(int PageNum, int PageSize);
    }
}
