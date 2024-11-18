using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace PruebasUnitarias
{
    [TestClass]
    public class ResetPasswordModelTests
    {
        private class ResetPasswordModel
        {
            public InputModel Input { get; set; } = new InputModel();

            public class InputModel
            {
                [Required(ErrorMessage = "Correo es requerido.")]
                [EmailAddress(ErrorMessage = "Correo no es válido.")]
                public string Email { get; set; }

                [Required(ErrorMessage = "Nueva contraseña es requerida.")]
                public string Password { get; set; }

                [Compare("Password", ErrorMessage = "La confirmación de la contraseña no coincide.")]
                public string ConfirmPassword { get; set; }

                public string Code { get; set; }  // Este campo se recibe como `hidden`, normalmente es opcional en las pruebas.
            }
        }

        [TestMethod]
        public void ResetPasswordModel_ValidInput_ShouldNotHaveValidationErrors()
        {
            var model = new ResetPasswordModel
            {
                Input = new ResetPasswordModel.InputModel
                {
                    Email = "test@example.com",
                    Password = "Password123!",
                    ConfirmPassword = "Password123!"
                }
            };

            var context = new ValidationContext(model.Input);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model.Input, context, results, true);

            Assert.IsTrue(isValid);
            Assert.AreEqual(0, results.Count);
        }

        [TestMethod]
        public void ResetPasswordModel_InvalidEmail_ShouldHaveValidationError()
        {
            var model = new ResetPasswordModel
            {
                Input = new ResetPasswordModel.InputModel
                {
                    Email = "invalid-email",
                    Password = "Password123!",
                    ConfirmPassword = "Password123!"
                }
            };

            var context = new ValidationContext(model.Input);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model.Input, context, results, true);

            Assert.IsFalse(isValid);
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("Correo no es válido.", results[0].ErrorMessage);
        }

        [TestMethod]
        public void ResetPasswordModel_EmptyPassword_ShouldHaveValidationError()
        {
            var model = new ResetPasswordModel
            {
                Input = new ResetPasswordModel.InputModel
                {
                    Email = "test@example.com",
                    Password = "",
                    ConfirmPassword = ""
                }
            };

            var context = new ValidationContext(model.Input);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model.Input, context, results, true);

            Assert.IsFalse(isValid);
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("Nueva contraseña es requerida.", results[0].ErrorMessage);
        }

        [TestMethod]
        public void ResetPasswordModel_PasswordMismatch_ShouldHaveValidationError()
        {
            var model = new ResetPasswordModel
            {
                Input = new ResetPasswordModel.InputModel
                {
                    Email = "test@example.com",
                    Password = "Password123!",
                    ConfirmPassword = "DifferentPassword!"
                }
            };

            var context = new ValidationContext(model.Input);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model.Input, context, results, true);

            Assert.IsFalse(isValid);
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("La confirmación de la contraseña no coincide.", results[0].ErrorMessage);
        }
    }
}
