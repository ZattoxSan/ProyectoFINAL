using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace PruebasUnitarias
{
    [TestClass]
    public class RegisterModelTests
    {
        private class RegisterModel
        {
            public InputModel Input { get; set; } = new InputModel();

            public class InputModel
            {
                [Required(ErrorMessage = "Nombre es requerido.")]
                public string Nombre { get; set; }

                [Required(ErrorMessage = "Apellido es requerido.")]
                public string Apellido { get; set; }

                [Required(ErrorMessage = "Email es requerido.")]
                [EmailAddress(ErrorMessage = "Email no es válido.")]
                public string Email { get; set; }

                [Required(ErrorMessage = "Contraseña es requerida.")]
                public string Password { get; set; }

                [Compare("Password", ErrorMessage = "La confirmación de la contraseña no coincide.")]
                public string ConfirmPassword { get; set; }

                public string Provincia { get; set; }
                public string Localidad { get; set; }
                public string Direccion { get; set; }
                public string CodigoPostal { get; set; }

                public string Role { get; set; } // Opcional y solo visible para admins.
            }
        }

        [TestMethod]
        public void RegisterModel_ValidInput_ShouldNotHaveValidationErrors()
        {
            var model = new RegisterModel
            {
                Input = new RegisterModel.InputModel
                {
                    Nombre = "Juan",
                    Apellido = "Pérez",
                    Email = "juan.perez@example.com",
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
        public void RegisterModel_InvalidEmail_ShouldHaveValidationError()
        {
            var model = new RegisterModel
            {
                Input = new RegisterModel.InputModel
                {
                    Nombre = "Juan",
                    Apellido = "Pérez",
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
            Assert.AreEqual("Email no es válido.", results[0].ErrorMessage);
        }

        [TestMethod]
        public void RegisterModel_EmptyPassword_ShouldHaveValidationError()
        {
            var model = new RegisterModel
            {
                Input = new RegisterModel.InputModel
                {
                    Nombre = "Juan",
                    Apellido = "Pérez",
                    Email = "juan.perez@example.com",
                    Password = "",
                    ConfirmPassword = ""
                }
            };

            var context = new ValidationContext(model.Input);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model.Input, context, results, true);

            Assert.IsFalse(isValid);
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("Contraseña es requerida.", results[0].ErrorMessage);
        }

        [TestMethod]
        public void RegisterModel_PasswordMismatch_ShouldHaveValidationError()
        {
            var model = new RegisterModel
            {
                Input = new RegisterModel.InputModel
                {
                    Nombre = "Juan",
                    Apellido = "Pérez",
                    Email = "juan.perez@example.com",
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
