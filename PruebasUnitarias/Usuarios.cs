using ECommerceWeb.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECommerceWeb.Tests
{
    [TestClass]
    public class UsuarioTests
    {
        private Usuario _usuario;

        [TestInitialize]
        public void Setup()
        {
            // Inicialización de un usuario para pruebas
            _usuario = new Usuario
            {
                Nombre = "Juan",
                Apellido = "Pérez",
                Direccion = "Calle Falsa 123",
                CodigoPostal = "12345",
                Provincia = "Buenos Aires",
                Localidad = "La Plata",
                Rol = "Usuario"
            };
        }

        [TestMethod]
        public void Usuario_ValidarNombreYApellido_Obligatorios()
        {
            // Arrange
            _usuario.Nombre = ""; // Campo obligatorio
            _usuario.Apellido = ""; // Campo obligatorio

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(_usuario);
            var isValid = Validator.TryValidateObject(_usuario, validationContext, validationResults, true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(2, validationResults.Count); // Debe haber 2 errores de validación
            Assert.AreEqual("Este campo es obligatorio", validationResults[0].ErrorMessage);
            Assert.AreEqual("Este campo es obligatorio", validationResults[1].ErrorMessage);
        }

        [TestMethod]
        public void Usuario_ValidarCamposOpcionales_SonValidos()
        {
            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(_usuario);
            var isValid = Validator.TryValidateObject(_usuario, validationContext, validationResults, true);

            // Assert
            Assert.IsTrue(isValid); // Todos los campos obligatorios son válidos
            Assert.AreEqual(0, validationResults.Count); // No debe haber errores de validación
        }

        [TestMethod]
        public void Usuario_ActualizarInformacion_ActualizaCorrectamente()
        {
            // Arrange
            var nuevoNombre = "Carlos";
            var nuevoApellido = "González";
            var nuevaDireccion = "Avenida Siempre Viva 742";

            // Act
            _usuario.Nombre = nuevoNombre;
            _usuario.Apellido = nuevoApellido;
            _usuario.Direccion = nuevaDireccion;

            // Assert
            Assert.AreEqual(nuevoNombre, _usuario.Nombre);
            Assert.AreEqual(nuevoApellido, _usuario.Apellido);
            Assert.AreEqual(nuevaDireccion, _usuario.Direccion);
        }

        [TestMethod]
        public void Usuario_EliminarUsuario_NuloLanzaExcepcion()
        {
            // Arrange
            Usuario usuarioAEliminar = null;

            // Act & Assert
            Assert.ThrowsException<System.NullReferenceException>(() =>
            {
                // Intentar acceder a una propiedad de un usuario nulo
                var nombre = usuarioAEliminar.Nombre; // Esto debería lanzar una excepción
            });
        }
    }
}
