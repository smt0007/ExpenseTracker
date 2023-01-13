using ExpenseTracker.Interfaces;
using ExpenseTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace ExpenseTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
         
        [HttpGet]
        public IActionResult GetCategories() 
        {
            var categories = _categoryRepository.GetCategories();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
                

            return Ok(categories);  
        }

        [HttpGet("{id}")]
        public IActionResult GetCategory(int id)
        {
            if (!_categoryRepository.CategoryExists(id))
            {
                return NotFound();
            }
               

            var category = _categoryRepository.GetCategory(id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
                

            return Ok(category);
            
        }
        [HttpPost("add")]
        public IActionResult CreateCategory([FromForm, Bind("TransactionId,CategoryName,Limit")] Category categoryCreate)
        {
            if(CreateCategory == null)
            {
                return BadRequest(ModelState);
            }
               

            var categories = _categoryRepository.GetCategories()
                .Where(c => c.CategoryName.Trim().ToUpper() == categoryCreate.CategoryName.TrimEnd().ToUpper()).FirstOrDefault();

            if (categories != null)
            {
                ModelState.AddModelError("", "Category already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(!_categoryRepository.CreateCategory(categoryCreate))
            {
                ModelState.AddModelError("", "Somthing went wrong while saving");
                return StatusCode(500,ModelState);
            }

            return Ok("Sucessfully Created");

        }

        [HttpPut("update/{id}")]
        public IActionResult UpdateCategory(int id,[FromForm, Bind("CategoryId,CategoryName, Limit")] Category updatedCategory) 
        {
            if (UpdateCategory == null)
            {
                return BadRequest(ModelState);
            }
               
            if(id != updatedCategory.CategoryId)
            {
                return BadRequest(ModelState);
            }
               
            
            if (!_categoryRepository.CategoryExists(id))
            {
                return NotFound();
            }
                
            
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
                

            if (!_categoryRepository.UpdateCategory(updatedCategory))
            {
                ModelState.AddModelError("", "Somthing went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteCategory(int id)
        {
            if(!_categoryRepository.CategoryExists(id))
            {
                return NotFound();
            }

            var categoryToDelete = _categoryRepository.GetCategory(id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
                

            if(!_categoryRepository.DeleteCategory(categoryToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting category");
            }

            return NoContent();
        }
    }
}
