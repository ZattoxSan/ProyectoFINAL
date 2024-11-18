using ECommerceWeb.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECommerceWeb.Tests
{
    [TestClass]
    public class CarritoTests
    {
        private CarritoCompras _carrito;

        [TestInitialize]
        public void Setup()
        {
            // Inicialización de un carrito de compras para pruebas
            _carrito = new CarritoCompras
            {
                Cantidad = 5,
                IdUsuario = "usuario123",
                IdProducto = 1,
                Usuario = new Usuario { Nombre = "Juan", Apellido = "Pérez" },
                Producto = new Producto { Nombre = "Producto Test", Precio = 10.0f }
            };
        }

        [TestMethod]
        public void CarritoCompras_ValidarCantidad_ObligatoriaYEnRango()
        {
            // Arrange
            _carrito.Cantidad = 0; // Fuera del rango permitido

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(_carrito);
            var isValid = Validator.TryValidateObject(_carrito, validationContext, validationResults, true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(1, validationResults.Count); // Debe haber 1 error de validación
            Assert.AreEqual("Ingrese un valor entre 1 y 10000", validationResults[0].ErrorMessage);
        }

        [TestMethod]
        public void CarritoCompras_ValidarCamposObligatorios_SonValidos()
        {
            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(_carrito);
            var isValid = Validator.TryValidateObject(_carrito, validationContext, validationResults, true);

            // Assert
            Assert.IsTrue(isValid); // Todos los campos obligatorios son válidos
            Assert.AreEqual(0, validationResults.Count); // No debe haber errores de validación
        }

        [TestMethod]
        public void CarritoCompras_ActualizarCantidad_ActualizaCorrectamente()
        {
            // Arrange
            var nuevaCantidad = 10;

            // Act
            _carrito.Cantidad = nuevaCantidad;

            // Assert
            Assert.AreEqual(nuevaCantidad, _carrito.Cantidad);
        }

        [TestMethod]
        public void CarritoCompras_EliminarUsuario_NuloLanzaExcepcion()
        {
            // Arrange
            CarritoCompras carritoAEliminar = null;

            // Act & Assert
            Assert.ThrowsException<System.NullReferenceException>(() =>
            {
                // Intentar acceder a la propiedad de un carrito de compras nulo
                var cantidad = carritoAEliminar.Cantidad; // Esto debería lanzar una excepción
            });
        }
    }
}
