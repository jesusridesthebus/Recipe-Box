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

    [TestMethod]
    public void Save_AssignsIdtoObject_Id()
    {
      Recipe testRecipe = new Recipe("test", 2, "test3", "test4");

      testRecipe.Save();
      Recipe savedRecipe = Recipe.GetAll()[0];

      int result = savedRecipe.Id;
      int testId = testRecipe.Id;

      Assert.AreEqual(testId, result);
    }

    [TestMethod]
    public void Test_AddRecipe_AddsRecipeToCategory()
    {
      Category testCategory = new Category("Mer");
      testCategory.Save();
      Recipe testRecipe = new Recipe("test", 2, "test3", "test4");
      testRecipe.Save();
      Recipe testRecipe2 = new Recipe("testTwo", 2, "testTwo3", "testTwo4");
      testRecipe2.Save();

      testCategory.AddRecipe(testRecipe);
      testCategory.AddRecipe(testRecipe2);
      List<Recipe> result = testCategory.GetRecipes();
      List<Recipe> testList = new List<Recipe>{testRecipe, testRecipe2};

      CollectionAssert.AreEqual(testList, result);
    }


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
