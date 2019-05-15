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
      // Recipe.ClearAll();
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
  }
}
