using Microsoft.AspNetCore.Mvc;
using ToDoWebApi.Context;
using ToDoWebApi.Entities;
using ToDoWebApi.Models;

namespace ToDoWebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ToDosController : Controller
	{
		private readonly ToDoContext _db;
        public ToDosController(ToDoContext db)
        {
             _db = db;
        }
		[HttpPost]
        public IActionResult AddToDo(AddToDoRequest request)
		{
			var entity = new ToDoEntity()
			{
				Title = request.Title,
				Content = request.Content
			};
			_db.ToDos.Add(entity);
			try
			{
				_db.SaveChanges();
				return Ok();
			}
			catch (Exception)
			{

				return StatusCode(500);
			}			
		}
		[HttpGet]
		public IActionResult GetAllToDos()
		{
			var entities = _db.ToDos.ToList();

			return Ok(entities);
		}

		[HttpGet("{id}")]
		public IActionResult GetToDo(int id)
		{
			var entity = _db.ToDos.Find(id);
			if (entity is null)
				return NotFound();

			return Ok(entity);
		}
		[HttpPatch("{id}")]
		public IActionResult CheckToDo(int id)
		{
			var entity = _db.ToDos.Find(id);
			if(entity is null)
				return NotFound();

			entity.IsDone = !entity.IsDone;

			_db.ToDos.Update(entity);

			try
			{
				_db.SaveChanges();
				return Ok();
			}
			catch (Exception)
			{
				return StatusCode(500);
				
			}
		}
		[HttpPut("{id}")]
		public IActionResult UpdateToDo(int id, UpdateToDoRequest request)
		{
			var entity = _db.ToDos.Find(id);
			
			if(entity is null)
				return NotFound();

			entity.Title = request.Title;
			entity.Content = request.Content;

			_db.ToDos.Update(entity);

			try
			{
				_db.SaveChanges();
				return Ok();
			}
			catch (Exception)
			{
				return StatusCode(500);
				
			}
		}
		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			var entity = _db.ToDos.Find(id);
			if (entity is null)
				return BadRequest();

			_db.ToDos.Remove(entity);
			try
			{
				_db.SaveChanges();
				return Ok();
			}
			catch (Exception)
			{

				return StatusCode(500);
			}
		}
	}
}
