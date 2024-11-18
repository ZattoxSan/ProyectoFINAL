using ECommerceWeb.Models;
using System;
using System.ComponentModel.DataAnnotations; // Necesario para ValidationException
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PruebasUnitarias
{
    [TestClass]
    public class PedidoTests
    {
        [TestMethod]
        public void CrearPedido_ConDatosValidos_DeberiaEstablecerValoresCorrectamente()
        {
            // Arrange
            var pedido = new Pedido
            {
                IdPedido = 1,
                IdPagoStripe = "stripe_payment_id",
                IdStripe = "stripe_id",
                FechaPedido = DateTime.Now,
                FechaPago = DateTime.Now,
                FechaEnvio = DateTime.Now.AddDays(5),
                EstadoPedido = "Pendiente",
                EstadoPago = "Completado",
                Nombre = "Juan",
                Apellido = "Pérez",
                Direccion = "Calle Falsa 123",
                CodigoPostal = 12345,
                Provincia = "Buenos Aires",
                Localidad = "CABA",
                Telefono = "1234567890",
                TotalPedido = 99.99f,
                IdUsuario = "user123",
                Usuario = new Usuario() // Suponiendo que tienes un modelo Usuario
            };

            // Act
            var estadoPedido = pedido.EstadoPedido;
            var totalPedido = pedido.TotalPedido;

            // Assert
            Assert.AreEqual("Pendiente", estadoPedido);
            Assert.AreEqual(99.99f, totalPedido);
            Assert.IsNotNull(pedido.Usuario); // Verifica que el usuario no sea nulo
            Assert.IsTrue(pedido.FechaPedido <= pedido.FechaEnvio); // Verifica que la fecha de pedido sea menor o igual que la fecha de envío
        }

        [TestMethod]
       
        private void Validate(Pedido pedido)
        {
            var context = new ValidationContext(pedido, serviceProvider: null, items: null);
            Validator.ValidateObject(pedido, context, validateAllProperties: true);
        }
    }
}
