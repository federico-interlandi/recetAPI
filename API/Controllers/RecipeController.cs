using API.Entities;
using API.Extensions;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
public class RecipesController(IRecipeRepository recipeRepository) : BaseApiController{

    [HttpGet("get")]
    public async Task<ActionResult<IEnumerable<Recipe>>> GetRecipes(){

        var recipes = await recipeRepository.GetRecipesByUserEmail(User.GetUserEmail());

        if(recipes != null) return Ok(recipes);

        return StatusCode(500, new { message = "Error retrieving recipes" });
    }
}