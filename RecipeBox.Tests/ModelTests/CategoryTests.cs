using Microsoft.VisualStudio.TestTools.UnitTesting;
using RecipeBox.Models;
using System.Collections.Generic;
using System;

namespace RecipeBox.TestTools
{
  [TestClass]
  public class CategoryTest: IDisposable
  {
    public void Dispose()
    {
      Category.ClearAll();
      Recipe.ClearAll();
    }

    public CategoryTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=recipe_box_tests;";
    }

    [TestMethod]
    public void Equals_ReturnsTrueIfNamesAreTheSame_Category()
    {
      Category firstCategory = new Category("Mer");
      Category secondCategory = new Category("Mer");
      Assert.AreEqual(firstCategory, secondCategory);
    }

    [TestMethod]
    public void GetAll_ReturnsAllCategoryObjects_CategoryList()
    {
      string name01 = "Work";
      string name02 = "School";
      Category newCategory1 = new Category(name01);
      newCategory1.Save();
      Category newCategory2 = new Category(name02);
      newCategory2.Save();
      List<Category> newList = new List<Category> { newCategory1, newCategory2 };
      List<Category> result = Category.GetAll();
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void CategoryConstructor_CreatesInstanceOfCategory_Category()
    {
      Category newCategory = new Category("test category");
      Assert.AreEqual(typeof(Category), newCategory.GetType());
    }

    [TestMethod]
    public void Save_SavesCategoryToDatabase_CategoryList()
    {
      Category testCategory = new Category("mer mer mer");
      testCategory.Save();
      List<Category> result = Category.GetAll();
      List<Category> testList = new List<Category>{testCategory};
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void GetRecipes_RetrievesAllRecipesWithCategory_RecipeList()
    {
      Category testCategory = new Category("Mer");
      testCategory.Save();
      Recipe newRecipeOne = new Recipe ("test", 2, "test3", "test4");
      newRecipeOne.Save();
      Recipe newRecipeTwo = new Recipe ("testTwo", 3, "testTwo3", "testTwo4");
      newRecipeTwo.Save();

      testCategory.AddRecipe(newRecipeOne);
      List<Recipe> savedRecipes = testCategory.GetRecipes();
      List<Recipe> testList = new List<Recipe> {newRecipeOne};

      CollectionAssert.AreEqual(testList, savedRecipes);
    }

    [TestMethod]
    public void Find_ReturnsCategoryInDatabase_Category()
    {
      Category testCategory = new Category("merrr");
      testCategory.Save();

      Category foundCategory = Category.Find(testCategory.Id);

      Assert.AreEqual(testCategory, foundCategory);
    }
    
    [TestMethod]
    public void DeleteCategory_DeletesCategoryAssociationsFromDatabase_CategoryList()
    {
      Recipe testRecipe = new Recipe ("test", 2, "test3", "test4");
      testRecipe.Save();
      string testName = "Mer";
      Category testCategory = new Category(testName);
      testCategory.Save();

      testCategory.AddRecipe(testRecipe);
      testCategory.DeleteCategory();
      List<Category> resultRecipeCategories = testRecipe.GetCategories();
      List<Category> testRecipeCategories = new List<Category> {};

      CollectionAssert.AreEqual(testRecipeCategories, resultRecipeCategories);
    }

  }
}
