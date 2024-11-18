using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace PruebasUnitarias
{
    [TestClass]
    public class LoginModelTests
    {
        private class LoginModel
        {
            public InputModel Input { get; set; } = new InputModel();

            public class InputModel
            {
                [Required(ErrorMessage = "Email es requerido.")]
                [EmailAddress(ErrorMessage = "Email no es válido.")]
                public string Email { get; set; }

                [Required(ErrorMessage = "Contraseña es requerida.")]
                public string Password { get; set; }

                public bool RememberMe { get; set; }
            }
        }

        [TestMethod]
        public void LoginModel_ValidInput_ShouldNotHaveValidationErrors()
        {
            // Arrange
            var model = new LoginModel
            {
                Input = new LoginModel.InputModel
                {
                    Email = "test@example.com",
                    Password = "Password123",
                    RememberMe = true
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
        public void LoginModel_InvalidEmail_ShouldHaveValidationErrors()
        {
            // Arrange
            var model = new LoginModel
            {
                Input = new LoginModel.InputModel
                {
                    Email = "invalid-email",
                    Password = "Password123",
                    RememberMe = true
                }
            };

            // Act
            var context = new ValidationContext(model.Input);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model.Input, context, results, true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("Email no es válido.", results[0].ErrorMessage);
        }

        [TestMethod]
        public void LoginModel_EmptyPassword_ShouldHaveValidationErrors()
        {
            // Arrange
            var model = new LoginModel
            {
                Input = new LoginModel.InputModel
                {
                    Email = "test@example.com",
                    Password = "",
                    RememberMe = true
                }
            };

            // Act
            var context = new ValidationContext(model.Input);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model.Input, context, results, true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("Contraseña es requerida.", results[0].ErrorMessage);
        }
    }
}
