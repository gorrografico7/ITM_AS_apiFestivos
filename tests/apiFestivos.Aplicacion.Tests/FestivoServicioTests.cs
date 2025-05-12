using apiFestivos.Aplicacion.Servicios;
using apiFestivos.Core.Interfaces.Repositorios;
using apiFestivos.Dominio.Entidades;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiFestivos.Test
{
    public class FestivoServicioTests
    {
        private readonly Mock<IFestivoRepositorio> _mockRepo;
        private readonly FestivoServicio _servicio;

        public FestivoServicioTests()
        {
            _mockRepo = new Mock<IFestivoRepositorio>();
            _servicio = new FestivoServicio(_mockRepo.Object);
        }

        [Fact]

        public async Task EsFestivo_FechaEsFestiva_RetornaTrue()
        {
            var fecha = new DateTime(2025, 1, 1);
            var festivos = new List<Festivo>
            {
                new Festivo { IdTipo = 1, Dia = 1, Mes = 1, Nombre = "Año nuevo" }
            };

            _mockRepo.Setup(r => r.ObtenerTodos()).ReturnsAsync(festivos);

            var resultado = await _servicio.EsFestivo(fecha);

            Assert.True(resultado);
        }

        [Fact]

        public async Task EsFestivo_FechaNoEsFestiva_RetornaFalse()
        {
            var fecha = new DateTime(2025, 2, 20);
            var festivos = new List<Festivo>();

            _mockRepo.Setup(r => r.ObtenerTodos()).ReturnsAsync(festivos);

            var resultado = await _servicio.EsFestivo(fecha);

            Assert.False(resultado);
        }
    }
}
