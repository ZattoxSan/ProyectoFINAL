using ECommerceWeb.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECommerceWeb.Tests
{
    [TestClass]
    public class CarouselTests
    {
        private Carousel _carousel;

        [TestInitialize]
        public void Setup()
        {
            // Inicialización de un objeto Carousel para pruebas
            _carousel = new Carousel
            {
                Nombre = "Carousel Test",
                Titulo = "Título de Test",
                Descripcion = "Descripción del carousel que proporciona información sobre el producto o servicio."
            };
        }

        [TestMethod]
        public void Carousel_ValidarCamposObligatorios_SonValidos()
        {
            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(_carousel);
            var isValid = Validator.TryValidateObject(_carousel, validationContext, validationResults, true);

            // Assert
            Assert.IsTrue(isValid); // Todos los campos obligatorios son válidos
            Assert.AreEqual(0, validationResults.Count); // No debe haber errores de validación
        }

        [TestMethod]
        public void Carousel_ValidarTitulo_MaxLengthExcedido()
        {
            // Arrange
            _carousel.Titulo = new string('A', 31); // 31 caracteres, excede el máximo permitido

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(_carousel);
            var isValid = Validator.TryValidateObject(_carousel, validationContext, validationResults, true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(1, validationResults.Count); // Debe haber 1 error de validación
            Assert.AreEqual("The field Titulo must be a string or array type with a maximum length of '30'.", validationResults[0].ErrorMessage);
        }

        [TestMethod]
        public void Carousel_ValidarDescripcion_MaxLengthExcedido()
        {
            // Arrange
            _carousel.Descripcion = new string('B', 101); // 101 caracteres, excede el máximo permitido

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(_carousel);
            var isValid = Validator.TryValidateObject(_carousel, validationContext, validationResults, true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(1, validationResults.Count); // Debe haber 1 error de validación
            Assert.AreEqual("The field Descripcion must be a string or array type with a maximum length of '100'.", validationResults[0].ErrorMessage);
        }
       
    }
}
