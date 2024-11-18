using ECommerceWeb.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.ComponentModel.DataAnnotations;

namespace PruebasUnitarias
{
    [TestClass]
    public class Productos
    {
        [TestMethod]
        public void CrearProducto_ConDatosValidos_DeberiaEstablecerValoresCorrectamente()
        {
            // Arrange
            var producto = new Producto
            {
                IdProducto = 1,
                Nombre = "Producto de Prueba",
                Descripcion = "Descripción del producto",
                Marca = "Marca XYZ",
                Precio = 100.0f,
                PrecioDescuento = 90.0f,
                PorcentajeDescuento = 10.0f,
                URLImagen = "http://ejemplo.com/imagen.jpg",
                IdCategoria = 1,
                Categoria = new Categoria() // Suponiendo que tienes un modelo Categoria
            };

            // Act
            var nombre = producto.Nombre;
            var precio = producto.Precio;

            // Assert
            Assert.AreEqual("Producto de Prueba", nombre);
            Assert.AreEqual(100.0f, precio);
            Assert.IsNotNull(producto.Categoria); // Verifica que la categoría no sea nula
        }

        [TestMethod]
        public void CrearProducto_SinNombre_LanzaExcepcion()
        {
            // Arrange
            var producto = new Producto
            {
                IdProducto = 2,
                Descripcion = "Descripción del producto",
                Marca = "Marca XYZ",
                Precio = 100.0f,
                URLImagen = "http://ejemplo.com/imagen.jpg",
                IdCategoria = 1,
                Categoria = new Categoria()
            };

            // Act & Assert
            var exception = Assert.ThrowsException<ValidationException>(() => Validate(producto));
            Assert.AreEqual("Este campo es obligatorio", exception.Message);
        }

        [TestMethod]
        public void ActualizarProducto_ConDatosValidos_DeberiaActualizarValoresCorrectamente()
        {
            // Arrange
            var producto = new Producto
            {
                IdProducto = 1,
                Nombre = "Producto Original",
                Descripcion = "Descripción inicial",
                Precio = 100.0f,
                URLImagen = "http://ejemplo.com/imagen.jpg",
                IdCategoria = 1,
                Categoria = new Categoria()
            };

            // Act
            producto.Nombre = "Producto Actualizado";
            producto.Descripcion = "Descripción actualizada";
            producto.Precio = 120.0f;

            // Assert
            Assert.AreEqual("Producto Actualizado", producto.Nombre);
            Assert.AreEqual("Descripción actualizada", producto.Descripcion);
            Assert.AreEqual(120.0f, producto.Precio);
        }

        [TestMethod]
        public void EliminarProducto_SinDatos_LanzaExcepcion()
        {
            // Arrange
            Producto producto = null;

            // Act & Assert
            Assert.ThrowsException<NullReferenceException>(() =>
            {
                // Intentar acceder a una propiedad de un producto nulo
                var nombre = producto.Nombre; // Esto debería lanzar una excepción
            });
        }

        private void Validate(Producto producto)
        {
            var context = new ValidationContext(producto, serviceProvider: null, items: null);
            Validator.ValidateObject(producto, context, validateAllProperties: true);
        }
    }
}
