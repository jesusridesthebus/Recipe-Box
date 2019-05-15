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
