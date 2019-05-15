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

    public static List<Recipe> GetAll()
    {
      List<Recipe> allRecipes = new List<Recipe> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM recipes;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int recipeId = rdr.GetInt32(0);
        string recipeName = rdr.GetString(1);
        int recipeRating = rdr.GetInt32(2);
        string recipeIngredients = rdr.GetString(3);
        string recipeInstructions = rdr.GetString(4);
        Recipe newRecipe = new Recipe(recipeName, recipeRating, recipeIngredients, recipeInstructions, recipeId);
        allRecipes.Add(newRecipe);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allRecipes;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO recipes (name, rating, ingredients, instructions) VALUES (@name, @rating, @ingredients, @instructions);";
      cmd.Parameters.AddWithValue("@name", this.Name);
      cmd.Parameters.AddWithValue("@rating", this.Rating);
      cmd.Parameters.AddWithValue("@ingredients", this.Ingredients);
      cmd.Parameters.AddWithValue("@instructions", this.Instructions);
      cmd.ExecuteNonQuery();
      Id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public void AddCategory(Category newCategory)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO recipes_categories (category_id, recipe_id) VALUES (@CategoryId, @RecipeId);";
      MySqlParameter categoryId = new MySqlParameter();
      categoryId.ParameterName = "@CategoryId";
      categoryId.Value = newCategory.Id;
      cmd.Parameters.Add(categoryId);
      MySqlParameter recipeId = new MySqlParameter();
      recipeId.ParameterName = "@RecipeId";
      recipeId.Value = Id;
      cmd.Parameters.Add(recipeId);
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public List<Category> GetCategories()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT categories.* FROM recipes JOIN recipes_categories ON (recipes.id = recipes_categories.recipe_id) JOIN categories ON (recipes_categories.category_id = categories.id)
      WHERE recipes.id = @recipeId;";
      MySqlParameter recipeIdParameter = new MySqlParameter();
      recipeIdParameter.ParameterName = "@recipeId";
      recipeIdParameter.Value = Id;
      cmd.Parameters.Add(recipeIdParameter);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      List<Category> categories = new List<Category> {};
      while(rdr.Read())
      {
        int thisCategoryId = rdr.GetInt32(0);
        string categoryName = rdr.GetString(1);
        Category foundCategory = new Category(categoryName, thisCategoryId);
        categories.Add(foundCategory);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return categories;
    }

    public static Recipe Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM recipes WHERE id = (@searchId);";
      cmd.Parameters.AddWithValue("@searchId", id);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int recipeId = 0;
      string recipeName = "";
      int recipeRating = 0;
      string recipeIngredients = "";
      string recipeInstructions = "";
      while(rdr.Read())
      {
        recipeId = rdr.GetInt32(0);
        recipeName = rdr.GetString(1);
        recipeRating = rdr.GetInt32(2);
        recipeIngredients = rdr.GetString(3);
        recipeInstructions = rdr.GetString(4);
      }
      Recipe newRecipe = new Recipe(recipeName, recipeRating, recipeIngredients, recipeInstructions, recipeId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newRecipe;
    }


    public void DeleteRecipe()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM recipes WHERE id = @RecipeId; DELETE FROM recipes_categories WHERE recipe_id = @RecipeId;";
      MySqlParameter recipeIdParameter = new MySqlParameter();
      recipeIdParameter.ParameterName = "@RecipeId";
      recipeIdParameter.Value = this.Id;
      cmd.Parameters.Add(recipeIdParameter);
      cmd.ExecuteNonQuery();
      if (conn != null)
      {
        conn.Close();
      }
    }

    public void Edit(string newName, int newRating, string newIngredients, string newInstructions)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmdName = conn.CreateCommand() as MySqlCommand;
      cmdName.CommandText = @"UPDATE recipes SET name = @newName WHERE id = @searchId;";
      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = Id;
      cmdName.Parameters.Add(searchId);

      Name = newName;
      cmdName.Parameters.AddWithValue("@newName", newName);

      Rating = newRating;
      var cmdRating = conn.CreateCommand() as MySqlCommand;
      cmdRating.CommandText = @"UPDATE recipes SET rating = @newRating WHERE id = @searchId;";
      cmdRating.Parameters.Add(searchId);
      cmdRating.Parameters.AddWithValue("@newRating", newRating);

      Ingredients = newIngredients;
      var cmdIngredients = conn.CreateCommand() as MySqlCommand;
      cmdIngredients.CommandText = @"UPDATE recipes SET ingredients = @newIngredients WHERE id = @searchId;";
      cmdIngredients.Parameters.Add(searchId);
      cmdIngredients.Parameters.AddWithValue("@newIngredients", newIngredients);

      Instructions = newInstructions;
      var cmdInstructions = conn.CreateCommand() as MySqlCommand;
      cmdInstructions.CommandText = @"UPDATE recipes SET instructions = @newInstructions WHERE id = @searchId;";
      cmdInstructions.Parameters.Add(searchId);
      cmdInstructions.Parameters.AddWithValue("@newInstructions", newInstructions);

      cmdName.ExecuteNonQuery();
      cmdRating.ExecuteNonQuery();
      cmdIngredients.ExecuteNonQuery();
      cmdInstructions.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

  }
}
