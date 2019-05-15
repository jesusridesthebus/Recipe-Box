using System.Collections.Generic;
using System;
using MySql.Data.MySqlClient;

namespace RecipeBox.Models
{
  public class Recipe
  {
    public int Id {get;set;}
    public string Name {get;set;}
    public int Rating {get;set;}
    public string Ingredients {get;set;}
    public string Instructions {get;set;}

    public Recipe (string name, int rating, string ingredients, string instructions, int id = 0)
    {
      Name = name;
      Rating = rating;
      Ingredients = ingredients;
      Instructions = instructions;
      Id = id;
    }

    public override bool Equals(System.Object otherRecipe)
    {
      if (!(otherRecipe is Recipe))
      {
        return false;
      }
      else
      {
        Recipe newRecipe = (Recipe) otherRecipe;
        bool idEquality = this.Id == newRecipe.Id;
        bool nameEquality = this.Name == newRecipe.Name;
        bool ratingEquality = this.Rating == newRecipe.Rating;
        bool ingredientsEquality = this.Ingredients == newRecipe.Ingredients;
        bool instructionsEquality = this.Instructions == newRecipe.Instructions;
        return (idEquality && nameEquality && ratingEquality && ingredientsEquality && instructionsEquality);
      }
    }

    public override int GetHashCode()
    {
      return this.Id.GetHashCode();
    }

    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM recipes;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    // public void Save()
    // {
    //   MySqlConnection conn = DB.Connection();
    //   conn.Open();
    //   var cmd = conn.CreateCommand() as MySqlCommand;
    //   cmd.CommandText = @"INSERT INTO recipes (name, rating, ingredients, instructions) VALUES (@name, @rating, @ingredients, @instructions);";
    //   MySqlParameter recipe = new MySqlParameter();
    //   recipe.ParameterName = "@recipe";
    //   recipe.Value = this.Recipe;
    //   cmd.Parameters.Add(recipe);
    //   cmd.ExecuteNonQuery();
    //   Id = (int) cmd.LastInsertedId;
    //   conn.Close();
    //   if (conn != null)
    //   {
    //     conn.Dispose();
    //   }
    // }
    //

    // public void AddCategory(Category newCategory)
    // {
    //   MySqlConnection conn = DB.Connection();
    //   conn.Open();
    //   var cmd = conn.CreateCommand() as MySqlCommand;
    //   cmd.CommandText = @"INSERT INTO categories_recipes (category_id, recipe_id) VALUES (@CategoryId, @RecipeId);";
    //   MySqlParameter categoryId = new MySqlParameter();
    //   categoryId.ParameterName = "@CategoryId";
    //   categoryId.Value = newCategory.Id;
    //   cmd.Parameters.Add(categoryId);
    //   MySqlParameter recipeId = new MySqlParameter();
    //   recipeId.ParameterName = "@RecipeId";
    //   recipeId.Value = Id;
    //   cmd.Parameters.Add(recipeId);
    //   cmd.ExecuteNonQuery();
    //   conn.Close();
    //   if (conn != null)
    //   {
    //     conn.Dispose();
    //   }
    // }
    //
    // public List<Category> GetCategories()
    // {
    //   MySqlConnection conn = DB.Connection();
    //   conn.Open();
    //   var cmd = conn.CreateCommand() as MySqlCommand;
    //   cmd.CommandText = @"SELECT categories.* FROM recipes JOIN categories_recipes ON (recipes.id = categories_recipes.recipe_id) JOIN categories ON (categories_recipes.category_id = categories.id)
    //   WHERE recipes.id = @recipeId;";
    //   MySqlParameter recipeIdParameter = new MySqlParameter();
    //   recipeIdParameter.ParameterName = "@recipeId";
    //   recipeIdParameter.Value = Id;
    //   cmd.Parameters.Add(recipeIdParameter);
    //   var rdr = cmd.ExecuteReader() as MySqlDataReader;
    //   List<Category> categories = new List<Category> {};
    //   while(rdr.Read())
    //   {
    //     int thisCategoryId = rdr.GetInt32(0);
    //     string categoryName = rdr.GetString(1);
    //     Category foundCategory = new Category(categoryName, thisCategoryId);
    //     categories.Add(foundCategory);
    //   }
    //   conn.Close();
    //   if (conn != null)
    //   {
    //     conn.Dispose();
    //   }
    //   return categories;
    // }
  }
}
