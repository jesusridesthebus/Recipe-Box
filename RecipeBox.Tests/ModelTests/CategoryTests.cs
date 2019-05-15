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
      // Category.ClearAll();
      // Item.ClearAll();
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
  }
}
