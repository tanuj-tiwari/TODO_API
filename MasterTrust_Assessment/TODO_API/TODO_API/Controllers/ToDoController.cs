using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TODO_API.BAL;
using TODO_API.Model;

namespace TODO_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoController : ControllerBase
    {
        #region Fields
        private readonly IToDoBAL _ObjBAL;
        #endregion

        #region ctor
        public ToDoController(IToDoBAL ObjBAL)
        {
            _ObjBAL = ObjBAL;
        }
        #endregion

        [HttpGet]
        public async Task<ActionResult<List<TODOModel>>> Get()
        {
            try
            {
                List<TODOModel> lstData = await _ObjBAL.GetTODOList();
                return Ok(lstData);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TODOModel>> Get(int id)
        {
            try
            {
                if(id == 0)
                {
                    return NoContent();
                }

                TODOModel objModel = await _ObjBAL.GetTODOListById(id);
                return Ok(objModel);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] TODOModel objModel)
        {
            try
            {
                string Msg = string.Empty;
                int responseOut = 0;
                Validation(objModel);

                if (ModelState.IsValid)
                {
                    responseOut = await _ObjBAL.AddUpdateTODOItems(objModel);
                }
                else
                {
                    return BadRequest(ModelState);
                }

                if (responseOut == 1)
                {
                    Msg = "TODO item saved successfully";
                    objModel.StatusMessage = Msg;
                    return Ok(objModel);
                }
                else if (responseOut == 3)
                {
                    Msg = "TODO item already exits.";
                    objModel.StatusMessage = Msg;
                    return Ok(objModel);
                }

                Msg = "Information cannot be processed";
                return BadRequest(objModel);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] TODOModel objModel)
        {
            try
            {
                if (id == 0)
                {
                    objModel.StatusMessage = "Please enter Id";
                    return BadRequest(objModel);
                }

                string Msg = string.Empty;
                int responseOut = 0;
                Validation(objModel);

                if (ModelState.IsValid)
                {
                    responseOut = await _ObjBAL.AddUpdateTODOItems(objModel);
                }
                else
                {
                    return BadRequest(ModelState);
                }

                if (responseOut == 2)
                {
                    Msg = "TODO item updated successfully";
                    objModel.StatusMessage = Msg;
                    return Ok(objModel);
                }
                else if (responseOut == 3)
                {
                    Msg = "TODO item already exits.";
                    objModel.StatusMessage = Msg;
                    return Ok(objModel);
                }

                Msg = "Information cannot be processed";
                return BadRequest(objModel);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {         
            try
            {
                TODOModel objModel = new TODOModel();
                var data = await _ObjBAL.GetTODOListById(id);

                if (id == 0 || data.Id == 0)
                {
                    return NotFound();
                }

                if (await _ObjBAL.DeleteTODOItem(id))
                {
                    objModel.StatusMessage = "TODO item deleted successfully.";
                    return Ok(objModel);
                }
                else
                {
                    objModel.StatusMessage = "TODO item cannot be deleted.";
                    return Ok(objModel);
                }
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [HttpGet("{pageNum}/{pageSize}")]
        public async Task<ActionResult<List<TODOModel>>> Get(int pageNum, int pageSize)
        {
            try
            {
                List<TODOModel> lstData = await _ObjBAL.GetTODOListPagination(pageNum, pageSize);
                return Ok(lstData);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        private void Validation(TODOModel objModel)
        {
            if (string.IsNullOrWhiteSpace(objModel.Title))
            { 
                ModelState.AddModelError("Title", "Please enter title.");
            }

            if (string.IsNullOrWhiteSpace(objModel.Description))
            {
                ModelState.AddModelError("Description", "Please enter description");
            }

        }
    }
}
