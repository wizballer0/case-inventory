using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLayer;

namespace InventoryTests;

[TestClass]
public class InventoryTests
{
    [TestMethod]
    public void Buy_WithValidQuantity_UpdatesInventory()
    {
        // Arrange
        int buyQuantity = 1;
        int expectedQuantity = 1;
        Inventory inventory = new Inventory();

        // Act
        inventory.Increase(buyQuantity);

        // Assert
        int actualQuantity = inventory.Get();
        Assert.AreEqual(expectedQuantity, actualQuantity, 0, "Inventory not changed properly with Buy");
    }

    [TestMethod]
    public void Buy_WithNegativeQuantity_ShouldThrowArgumentOutOfRange()
    {
        // Arrange
        int buyQuantity = -1;
        Inventory inventory = new Inventory();

        // Act and Assert
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => inventory.Increase(buyQuantity));
    }

    [TestMethod]
    public void Buy_WithQuantityThatWillExceedInt32MaxValue_ShouldThrowArgumentOutOfRange()
    {
        // Arrange
        int buyQuantity = 2;
        Inventory inventory = new Inventory();

        // Act
        inventory.Increase(Int32.MaxValue - 1);

        // Act and Assert
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => inventory.Increase(buyQuantity));
    }

    [TestMethod]
    public void Sell_WithValidQuantity_UpdatesInventory()
    {
        // Arrange
        int expectedQuantity = 1;
        Inventory inventory = new Inventory();

        // Act
        inventory.Increase(2);
        inventory.Decrease(1);

        // Assert
        int actualQuantity = inventory.Get();
        Assert.AreEqual(expectedQuantity, actualQuantity, 0, "Inventory not changed properly with Sell");
    }

    [TestMethod]
    public void Sell_NegativeQuantity_ShouldShouldThrowArgumentOutOfRange()
    {
        // Arrange
        Inventory inventory = new Inventory();

        // Act and Assert
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => inventory.Decrease(-1));
    }

    [TestMethod]
    public void Sell_WhenQuantityExceedsItemsInInventory_ShouldThrowArgumentOutOfRange()
    {
        // Arrange
        Inventory inventory = new Inventory();

        // Act
        inventory.Increase(1);

        // Act and Assert
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => inventory.Decrease(2));
    }
}
