using apiFestivos.Aplicacion.Servicios;
using apiFestivos.Core.Interfaces.Repositorios;
using apiFestivos.Dominio.Entidades;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace apiFestivos.Test
{
    public class FestivoRipo1Tests
    {
        private readonly Mock<IFestivoRepositorio> _mockRepo;
        private readonly FestivoServicio _servicio;

        public FestivoRipo1Tests()
        {
            _mockRepo = new Mock<IFestivoRepositorio>();
            _servicio = new FestivoServicio(_mockRepo.Object);
        }

        [Fact]
        public async Task ObtenerAño_Tipo1_RetornaFechaEsperada()
        {
            var festivos = new List<Festivo>
            {
                new Festivo
                {
                    IdTipo = 1,
                    Dia = 1,
                    Mes = 1,
                    Nombre = "Año nuevo"
                }
            };

            var año = 2025;
            _mockRepo.Setup(r => r.ObtenerTodos()).ReturnsAsync(festivos);

            var resultado = await _servicio.ObtenerAño(año);

            Assert.NotNull(resultado);
            Assert.True(resultado.Any(f => f.Fecha == new DateTime(2025, 1, 1) && f.Nombre == "Año nuevo"));
        }

        [Fact]
        public async Task ObtenerAño_Tipo2_RetornaLunesSiguiente()
        {
            var festivos = new List<Festivo>
            {
                new Festivo
                {
                    IdTipo = 2,
                    Dia = 6,
                    Mes = 1,
                    Nombre = "Día de Reyes"
                }
            };

            var año = 2025;
            _mockRepo.Setup(r => r.ObtenerTodos()).ReturnsAsync(festivos);

            var resultado = await _servicio.ObtenerAño(año);

            Assert.NotNull(resultado);
            Assert.True(resultado.Any(f => f.Fecha == new DateTime(2025, 1, 6) && f.Nombre == "Día de Reyes"));
        }

        [Fact]
        public async Task ObtenerAño_Tipo4_RetornaLunesSiguienteDeSemanaSanta()
        {
            var festivos = new List<Festivo>
            {
                new Festivo
                {
                    IdTipo = 4,
                    DiasPascua = 68,
                    Nombre = "Sagrado Corazón de Jesús"
                }
            };

            var año = 2025;
            _mockRepo.Setup(r => r.ObtenerTodos()).ReturnsAsync(festivos);

            var resultado = await _servicio.ObtenerAño(año);

            Assert.NotNull(resultado);
            Assert.True(resultado.Any(f => f.Fecha == new DateTime(2025, 6, 30) && f.Nombre == "Sagrado Corazón de Jesús"));

        }
    }
}
