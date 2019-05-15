using Microsoft.VisualStudio.TestTools.UnitTesting;
using RecipeBox.Models;
using System.Collections.Generic;
using System;

namespace RecipeBox.Tests
{
  [TestClass]
  public class RecipeTest : IDisposable
  {
    public void Dispose()
    {
      Recipe.ClearAll();
    }

    public RecipeTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=recipe_box_tests;";
    }

    [TestMethod]
    public void RecipeConstructor_CreatesInstanceOfRecipe_Recipe()
    {
      Recipe newRecipe = new Recipe("test", 2, "test3", "test4");
      Assert.AreEqual(typeof(Recipe), newRecipe.GetType());
    }

    [TestMethod]
    public void GetAll_ReturnsEmptyList_RecipeList()
    {
      List<Recipe> newList = new List<Recipe> { };
      List<Recipe> result = Recipe.GetAll();
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void Save_SavesToDatabase_RecipeList()
    {
      Recipe testRecipe = new Recipe("test", 2, "test3", "test4");

      testRecipe.Save();
      List<Recipe> result = Recipe.GetAll();
      List<Recipe> testList = new List<Recipe>{testRecipe};

      CollectionAssert.AreEqual(testList, result);
    }
    // 
    // [TestMethod]
    // public void Save_AssignsIdtoObject

    [TestMethod]
    public void GetAll_ReturnsRecipes_RecipeList()
    {
      Recipe newRecipeOne = new Recipe ("test", 2, "test3", "test4");
      newRecipeOne.Save();
      Recipe newRecipeTwo = new Recipe ("testTwo", 3, "testTwo3", "testTwo4");
      newRecipeTwo.Save();

      List<Recipe> newList = new List<Recipe> {newRecipeOne, newRecipeTwo};

      List<Recipe> result = Recipe.GetAll();

      CollectionAssert.AreEqual(newList, result);
    }
  }
}
