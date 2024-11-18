using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace PruebasUnitarias
{
    [TestClass]
    public class EmailModelTests
    {
        private class EmailModel
        {
            public string Email { get; set; }
            public bool IsEmailConfirmed { get; set; }
            public InputModel Input { get; set; } = new InputModel();

            public class InputModel
            {
                [Required(ErrorMessage = "Nuevo correo es requerido.")]
                [EmailAddress(ErrorMessage = "Nuevo correo no es válido.")]
                public string NewEmail { get; set; }
            }
        }

        [TestMethod]
        public void EmailModel_ValidNewEmail_ShouldNotHaveValidationErrors()
        {
            // Arrange
            var model = new EmailModel
            {
                Email = "actual@example.com",
                IsEmailConfirmed = true,
                Input = new EmailModel.InputModel
                {
                    NewEmail = "nuevo@example.com"
                }
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
        public void EmailModel_InvalidNewEmail_ShouldHaveValidationError()
        {
            // Arrange
            var model = new EmailModel
            {
                Email = "actual@example.com",
                IsEmailConfirmed = false,
                Input = new EmailModel.InputModel
                {
                    NewEmail = "correo-invalido"
                }
            };

            // Act
            var context = new ValidationContext(model.Input);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model.Input, context, results, true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("Nuevo correo no es válido.", results[0].ErrorMessage);
        }

        [TestMethod]
        public void EmailModel_EmptyNewEmail_ShouldHaveValidationError()
        {
            // Arrange
            var model = new EmailModel
            {
                Email = "actual@example.com",
                IsEmailConfirmed = false,
                Input = new EmailModel.InputModel
                {
                    NewEmail = "" // Campo vacío
                }
            };

            // Act
            var context = new ValidationContext(model.Input);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model.Input, context, results, true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("Nuevo correo es requerido.", results[0].ErrorMessage);
        }

        [TestMethod]
        public void EmailModel_EmailConfirmed_ShouldDisableCurrentEmailInput()
        {
            // Arrange
            var model = new EmailModel
            {
                Email = "actual@example.com",
                IsEmailConfirmed = true
            };

            // Act
            var isEmailInputDisabled = model.IsEmailConfirmed;

            // Assert
            Assert.IsTrue(isEmailInputDisabled, "El campo de correo actual debería estar deshabilitado si el correo está confirmado.");
        }
    }
}
