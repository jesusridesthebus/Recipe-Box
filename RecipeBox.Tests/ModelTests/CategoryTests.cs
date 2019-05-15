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

  }
}
