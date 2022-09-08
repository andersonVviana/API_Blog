using Blog.Data;
using Blog.Models;
using Blog.ViewsModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Controllers;

[ApiController]
public class CategoryController : ControllerBase
{
    [HttpGet("v1/categories")]
    public async Task<IActionResult> GetAsync(
        [FromServices]BlogDataContext context)
    {
        var categories = await context.Categories.ToListAsync();
        return Ok(categories);
    }
    
    [HttpGet("v1/categories/{id:int}")]
    public async Task<IActionResult> GetByIdAsync(
        [FromRoute] int id,
        [FromServices]BlogDataContext context)
    {
        try
        {
            var category = await context
                .Categories
                .FirstOrDefaultAsync(x=>x.Id == id);

            if (category == null)
                return NotFound();
            
            return Ok(category);
        }
        catch (DbUpdateException dbuex)
        {
            return StatusCode(500, "01AA001 - It was not possible to get the category");
        }
        
        catch (Exception ex)
        {
            return StatusCode(500, "01AA002 - Internal Server Fail");
        }
        
    }
    
    [HttpPost("v1/categories")]
    public async Task<IActionResult> PostAsync(
        [FromBody] EditorCategoryViewModel model,
        [FromServices]BlogDataContext context)
    {
        try
        {
            var category = new Category
            {
                Id = 0,
                Name = model.Name,
                Slug = model.Slug.ToLower(),
            };
            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();

            return Created($"v1/categories/{category.Id}", category);
        }
        
        catch (DbUpdateException dbuex)
        {
            return StatusCode(500, "01AA003 - It was not possible to post the category");
        }
        
        catch (Exception ex)
        {
            return StatusCode(500, "01AA004 - Internal Server Fail");
        }
    }
    
    [HttpPut("v1/categories/{id:int}")]
    public async Task<IActionResult> PutAsync(
        [FromRoute] int id,
        [FromBody] EditorCategoryViewModel category,
        [FromServices] BlogDataContext context)
    {
        try
        {
            var categoryContext = await context
                .Categories
                .FirstOrDefaultAsync(x=>x.Id == id);

            if (categoryContext == null)
                return NotFound();

            categoryContext.Name = category.Name;
            categoryContext.Slug = category.Slug;

            context.Categories.Update(categoryContext);
            await context.SaveChangesAsync();
        
            return Ok(category);
        }
        catch (DbUpdateException dbuex)
        {
            return StatusCode(500, "01AA005 - It was not possible to chage the category");
        }
        
        catch (Exception ex)
        {
            return StatusCode(500, "01AA006 - Internal Server Fail");
        }
    }
    
    [HttpDelete("v1/categories/{id:int}")]
    public async Task<IActionResult> DeleteAsync(
        [FromRoute] int id,
        [FromServices] BlogDataContext context)
    {
        try
        {
            var categoryContext = await context
                .Categories
                .FirstOrDefaultAsync(x=>x.Id == id);

            if (categoryContext == null)
                return NotFound();

            context.Categories.Remove(categoryContext);
            await context.SaveChangesAsync();
        
            return Ok(categoryContext);
        }
        
        catch (DbUpdateException dbuex)
        {
            return StatusCode(500, "01AA007 - It was not possible to delete the category");
        }
        
        catch (Exception ex)
        {
            return StatusCode(500, "01AA008 - Internal Server Fail");
        }
    }
    
}