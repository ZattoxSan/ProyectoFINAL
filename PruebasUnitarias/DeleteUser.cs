using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace PruebasUnitarias
{
    [TestClass]
    public class DeletePersonalDataModelTests
    {
        private class DeletePersonalDataModel
        {
            public InputModel Input { get; set; } = new InputModel();

            public bool RequirePassword { get; set; } = true; // Por defecto, requerimos la contraseña

            public class InputModel
            {
                [Required(ErrorMessage = "La contraseña es requerida para eliminar la cuenta.")]
                public string Password { get; set; }
            }
        }

        [TestMethod]
        public void DeletePersonalDataModel_WithPassword_ShouldNotHaveValidationErrors()
        {
            // Arrange
            var model = new DeletePersonalDataModel
            {
                Input = new DeletePersonalDataModel.InputModel
                {
                    Password = "Password123!"
                },
                RequirePassword = true
            };

            // Act
            var context = new ValidationContext(model.Input);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model.Input, context, results, true);

            // Assert
            Assert.IsTrue(isValid);
            Assert.AreEqual(0, results.Count);
        }

        [TestMethod]
        public void DeletePersonalDataModel_WithoutPassword_ShouldHaveValidationError()
        {
            // Arrange
            var model = new DeletePersonalDataModel
            {
                Input = new DeletePersonalDataModel.InputModel
                {
                    Password = ""
                },
                RequirePassword = true
            };

            // Act
            var context = new ValidationContext(model.Input);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model.Input, context, results, true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("La contraseña es requerida para eliminar la cuenta.", results[0].ErrorMessage);
        }

       
    }
}
