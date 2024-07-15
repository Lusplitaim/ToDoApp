using Microsoft.AspNetCore.Mvc;
using ToDoApp.Core.DTOs.Todo;
using ToDoApp.Core.Extensions;
using ToDoApp.Core.Models;
using ToDoApp.Core.Services;

namespace ToDoApp.API.Controllers
{
    public class TodosController : BaseController
    {
        private readonly ITodosService m_TodosService;
        public TodosController(ITodosService todosService)
        {
            m_TodosService = todosService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] TodoFilters filters)
        {
            var result = await m_TodosService.GetAsync(filters);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTodoDto model)
        {
            var result = await m_TodosService.CreateAsync(model);
            return this.ResolveResult(result, () => CreatedAtAction(nameof(Create), result.Result));
        }

        [HttpPut("{todoId}")]
        public async Task<IActionResult> Update(int todoId, UpdateTodoDto model)
        {
            var result = await m_TodosService.UpdateAsync(todoId, model);
            return this.ResolveResult(result, () => Ok(result.Result));
        }


        [HttpPut("{todoId}/status")]
        public async Task<IActionResult> Update([FromRoute] int todoId, UpdateTodoStatusDto model)
        {
            var result = await m_TodosService.UpdateStatusAsync(todoId, model);
            return this.ResolveResult(result, () => Ok(result.Result));
        }

        [HttpDelete("{todoId}")]
        public async Task<IActionResult> Delete(int todoId)
        {
            var result = await m_TodosService.DeleteAsync(todoId);
            return this.ResolveResult(result, () => Ok());
        }
    }
}
