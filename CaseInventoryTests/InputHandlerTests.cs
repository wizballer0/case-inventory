using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using case_inventory;
using BusinessLayer;

namespace InputHandlerTests;

[TestClass]
public class InputHandlerTests
{
    [TestMethod]
    public void HandleInventory_WithValidArgumentsThatShouldCallBuy_CallsBuyOnce()
    {
        // Arrange
        var mockInventory = new Mock<IInventory>();

        // Act
        InputHandler inputHandler = new InputHandler();
        inputHandler.HandleInventory(mockInventory.Object, 'I', 1);

        // Assert
        mockInventory.Verify(i => i.Increase(It.IsAny<int>()), Times.Once);
    }

    [TestMethod]
    public void ParseInput_WithValidInputWhereFirstCharacterIsS_ReturnsCorrectArguments()
    {
        // Arrange
        const char DECREASE = 'S';
        (char expectedAction, int expectedArgument) = (DECREASE, 1);
        InputHandler inputHandler = new InputHandler();

        // Act
        (char actualAction, int actualArgument) = inputHandler.ParseInput("S1");

        // Assert
        Assert.AreEqual((expectedAction, expectedArgument), (actualAction, actualArgument));
    }
}
