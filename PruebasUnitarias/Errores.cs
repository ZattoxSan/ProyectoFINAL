using ECommerceWeb.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PruebasUnitarias
{
    [TestClass]
    public class ErrorViewModelTest
    {
        [TestMethod]
        public void ShowRequestId_ReturnsFalse_WhenRequestIdIsNull()
        {
            // Arrange
            var errorViewModel = new ErrorViewModel { RequestId = null };

            // Act
            var result = errorViewModel.ShowRequestId;

            // Assert
            Assert.IsFalse(result); // Se espera que sea falso cuando RequestId es null
        }

        [TestMethod]
        public void ShowRequestId_ReturnsFalse_WhenRequestIdIsEmpty()
        {
            // Arrange
            var errorViewModel = new ErrorViewModel { RequestId = string.Empty };

            // Act
            var result = errorViewModel.ShowRequestId;

            // Assert
            Assert.IsFalse(result); // Se espera que sea falso cuando RequestId es una cadena vacía
        }

        [TestMethod]
        public void ShowRequestId_ReturnsTrue_WhenRequestIdIsNotEmpty()
        {
            // Arrange
            var errorViewModel = new ErrorViewModel { RequestId = "12345" };

            // Act
            var result = errorViewModel.ShowRequestId;

            // Assert
            Assert.IsTrue(result); // Se espera que sea verdadero cuando RequestId tiene un valor
        }
    }
}
