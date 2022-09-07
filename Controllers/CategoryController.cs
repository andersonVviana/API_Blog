﻿using Blog.Data;
using Blog.Models;
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
        var category = await context
            .Categories
            .FirstOrDefaultAsync(x=>x.Id == id);

        if (category == null)
            return NotFound();
        
        return Ok(category);
    }
    
    [HttpPost("v1/categories")]
    public async Task<IActionResult> PostAsync(
        [FromBody] Category category,
        [FromServices]BlogDataContext context)
    {
        await context.Categories.AddAsync(category);
        await context.SaveChangesAsync();

        return Created($"v1/categories/{category.Id}", category);
    }
    
    [HttpPut("v1/categories/{id:int}")]
    public async Task<IActionResult> PutAsync(
        [FromRoute] int id,
        [FromBody] Category category,
        [FromServices] BlogDataContext context)
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
    
    [HttpDelete("v1/categories/{id:int}")]
    public async Task<IActionResult> DeleteAsync(
        [FromRoute] int id,
        [FromServices] BlogDataContext context)
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
    
}