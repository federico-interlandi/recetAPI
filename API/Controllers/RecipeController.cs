using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
public class RecipesController(IRecipeRepository recipeRepository, IMapper mapper) : BaseApiController{

    [HttpGet("get")]
    public async Task<ActionResult<IEnumerable<Recipe>>> GetRecipes(){

        var recipes = await recipeRepository.GetRecipesByUserEmailAsync(User.GetUserEmail());

        if(recipes != null) return Ok(recipes);

        return StatusCode(500, new { message = "Error retrieving recipes" });
    }

    [HttpPost("add")]
    public async Task<ActionResult<ResponseMessageDto>> AddRecipe(Recipe recipe){

        var userEmail = User.GetUserEmail();

        if(userEmail == null) return BadRequest("User email invalid");

        recipe.UserEmail = userEmail;

        if(await recipeRepository.AddRecipeAsync(recipe)) return Ok(new ResponseMessageDto("Store recipe ok"));

        return StatusCode(500, new { msg = "Error storing recipe" });

    }

    [HttpPut("edit/{recipeId}")]
    public async Task<ActionResult<ResponseMessageDto>> UpdateRecipe(
    string recipeId, 
    [FromBody] RecipeUpdateDto updateRecipe){

        var recipe = await recipeRepository.GetRecipeByIdAsync(recipeId);

        if(recipe == null || recipe.UserEmail != User.GetUserEmail()) return Unauthorized();

        mapper.Map(updateRecipe, recipe);

        if(await recipeRepository.SaveAllAsync()) return Ok(new ResponseMessageDto("Update recipe ok"));

        return StatusCode(500, new { msg = "Error updating recipe. Verify you send an updated recipe" });

    }

    [HttpDelete("delete/{recipeId}")]
    public async Task<ActionResult<ResponseMessageDto>> DeleteRecipe(string recipeId){
        var recipe = await recipeRepository.GetRecipeByIdAsync(recipeId);
        if(recipe == null || recipe.UserEmail != User.GetUserEmail()) return Unauthorized();

        recipeRepository.DeleteRecipe(recipe);

        if(await recipeRepository.SaveAllAsync()) return Ok(new ResponseMessageDto("Delete recipe ok"));

        return StatusCode(500, new { msg = "Error deleting recipe" });
    }
}