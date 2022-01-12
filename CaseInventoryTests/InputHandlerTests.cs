using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;
using Moq;
using case_inventory;
using BusinessLayer;

namespace InputHandlerTests;

[TestClass]
public class InputHandlerTests
{
    const char INCREASE = 'I';
    const char DECREASE = 'S';
    const char GET = 'L';
    const char UNKNOWN_ACTION = 'X';
    const int NO_ARGUMENT = -1;

    [TestMethod]
    public void HandleInventory_WithValidArgumentsThatShouldCallIncrease_CallsIncreaseOnce()
    {
        // Arrange
        var mockInventory = new Mock<IInventory>();

        // Act
        InputHandler inputHandler = new InputHandler();
        inputHandler.HandleInventory(mockInventory.Object, INCREASE, 1);

        // Assert
        mockInventory.Verify(i => i.Increase(It.IsAny<int>()), Times.Once);
    }

    [TestMethod]
    public void HandleInventory_WithValidArgumentsThatShouldCallDecrease_CallsDecreaseOnce()
    {
        // Arrange
        var mockInventory = new Mock<IInventory>();

        // Act
        InputHandler inputHandler = new InputHandler();
        inputHandler.HandleInventory(mockInventory.Object, DECREASE, 1);

        // Assert
        mockInventory.Verify(i => i.Decrease(It.IsAny<int>()), Times.Once);
    }

    [TestMethod]
    public void HandleInventory_WithArgumentThatShouldCallGet_CallsGetOnce()
    {
        // Arrange
        var mockInventory = new Mock<IInventory>();

        // Act
        InputHandler inputHandler = new InputHandler();
        inputHandler.HandleInventory(mockInventory.Object, GET, NO_ARGUMENT);

        // Assert
        mockInventory.Verify(i => i.Get(), Times.Once);
    }

    [TestMethod]
    [DataRow('K', 1)]
    [DataRow('R', NO_ARGUMENT)]
    [DataRow('f', 1)]
    public void HandleInventory_WithArgumentThatShouldNotCallAnyMethod_CallsNoMethod(char action, int argument)
    {
        // Arrange
        var mockInventory = new Mock<IInventory>();

        // Act
        InputHandler inputHandler = new InputHandler();
        inputHandler.HandleInventory(mockInventory.Object, action, argument);

        // Assert
        mockInventory.Verify(i => i.Increase(It.IsAny<int>()), Times.Never);
        mockInventory.Verify(i => i.Decrease(It.IsAny<int>()), Times.Never);
        mockInventory.Verify(i => i.Get(), Times.Never);
    }

    [TestMethod]
    public void ParseInput_WithValidArgumentsWhereFirstCharacterMatchesActionDecrease_ReturnsCorrectArguments()
    {
        // Arrange
        (char expectedAction, int expectedArgument) = (DECREASE, 1);
        InputHandler inputHandler = new InputHandler();

        // Act
        (char actualAction, int actualArgument) = inputHandler.ParseInput("S1");

        // Assert
        Assert.AreEqual((expectedAction, expectedArgument), (actualAction, actualArgument));
    }

    [TestMethod]
    public void ParseInput_WithValidArgumentsWhereFirstCharacterMatchesActionIncrease_ReturnsCorrectArguments()
    {
        // Arrange
        (char expectedAction, int expectedArgument) = (INCREASE, 1);
        InputHandler inputHandler = new InputHandler();

        // Act
        (char actualAction, int actualArgument) = inputHandler.ParseInput("I1");

        // Assert
        Assert.AreEqual((expectedAction, expectedArgument), (actualAction, actualArgument));
    }

    [TestMethod]
    public void ParseInput_WhereFirstCharacterMatchesActionGet_ReturnsCorrectArguments()
    {
        // Arrange
        (char expectedAction, int expectedArgument) = (GET, NO_ARGUMENT);
        InputHandler inputHandler = new InputHandler();

        // Act
        (char actualAction, int actualArgument) = inputHandler.ParseInput("L");

        // Assert
        Assert.AreEqual((expectedAction, expectedArgument), (actualAction, actualArgument));
    }

    [TestMethod]
    public void ParseInput_WhereFirstCharacterMatchesNoAction_ReturnsCorrectArguments()
    {
        // Arrange
        (char expectedAction, int expectedArgument) = (UNKNOWN_ACTION, NO_ARGUMENT);
        InputHandler inputHandler = new InputHandler();

        // Act
        (char actualAction, int actualArgument) = inputHandler.ParseInput("D");

        // Assert
        Assert.AreEqual((expectedAction, expectedArgument), (actualAction, actualArgument));
    }

    [TestMethod]
    [DataRow("S1.5")]
    [DataRow("I3x24")]
    public void ParseInput_WithInvalidArgumentsWhereFirstCharacterMatchesActionIncreaseOrDecrease_ReturnsCorrectArguments(string input)
    {
        // Arrange
        (char expectedAction, int expectedArgument) = (UNKNOWN_ACTION, NO_ARGUMENT);
        InputHandler inputHandler = new InputHandler();

        // Act
        (char actualAction, int actualArgument) = inputHandler.ParseInput(input);

        // Assert
        Assert.AreEqual((expectedAction, expectedArgument), (actualAction, actualArgument));
    }
}
