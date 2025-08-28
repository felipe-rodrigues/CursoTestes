using M1.Servicos;
using M1.Shared;
using Moq;

namespace M1.Test
{
    public class CalendarioServicoTeste
    {
        [Fact]
        public void AgendarEvento_QuandoDataDisponivel_AgendaComSucesso()
        {
            var dataProvedorMock = new Mock<IDateTimeProvider>();
            dataProvedorMock.Setup(dp => dp.Agora)
                .Returns(new DateTime(2023, 1, 1));
            var calendarioServico = new CalendarioServico(dataProvedorMock.Object);
            var date = new DateTime(2023, 01, 1);
            calendarioServico.Agendar(date, 10, "Evento Teste");

            Assert.Contains(calendarioServico.ObterAgendamentosDoDia(date),
                e => e is { Titulo: "Evento Teste", Hora: 10 });
        }

        [Fact]
        public void AgendarEvento_QuandoAnoMaiorQueOAnoDaCriacaoDoCalendario_LancaExcecao()
        {
            var dataProvedorMock = new Mock<IDateTimeProvider>();
            dataProvedorMock.Setup(dp => dp.Agora)
                .Returns(new DateTime(2023, 1, 1));
            var calendarioServico = new CalendarioServico(dataProvedorMock.Object);
            var date = new DateTime(2024, 01, 1);

            Assert.Throws<ArgumentException>(() => calendarioServico.Agendar(date, 10, "Evento Teste"));
        }

        [Fact]
        public void CancelarEvento_QuandoEventoExiste_CancelaComSucesso()
        {
            var dataProvedorMock = new Mock<IDateTimeProvider>();
            dataProvedorMock.Setup(dp => dp.Agora)
                .Returns(new DateTime(2023, 1, 1));
            var calendarioServico = new CalendarioServico(dataProvedorMock.Object);
            var date = new DateTime(2023, 01, 1);
            calendarioServico.Agendar(date, 10, "Evento Teste");

            calendarioServico.Cancelar(date, 10);

            Assert.Empty(calendarioServico.ObterAgendamentosDoDia(date));
        }

        [Fact]
        public void EstaDisponivel_QuandoDataEHoraDisponiveis_RetornaVerdadeiro()
        {
            var dataProvedorMock = new Mock<IDateTimeProvider>();
            dataProvedorMock.Setup(dp => dp.Agora)
                .Returns(new DateTime(2023, 1, 1));
            var calendarioServico = new CalendarioServico(dataProvedorMock.Object);
            var date = new DateTime(2023, 01, 1);
            Assert.True(calendarioServico.EstaDisponivel(date, 10));
        }

        [Fact]
        public void EstaDisponivel_QuandoDataDisponivelEHoraIndisponivel_RetornaFalse()
        {
            var dataProvedorMock = new Mock<IDateTimeProvider>();
            dataProvedorMock.Setup(dp => dp.Agora)
                .Returns(new DateTime(2023, 1, 1));
            var calendarioServico = new CalendarioServico(dataProvedorMock.Object);
            var date = new DateTime(2023, 01, 1);
            calendarioServico.Agendar(date, 10, "Evento Teste");

            Assert.False(calendarioServico.EstaDisponivel(date, 10));
        }

        [Fact]
        public void ObterAgendamentosDaSemana_QuandoExistemEventosNaSemana_RetornaListaDeEventos()
        {
            var dataProvedorMock = new Mock<IDateTimeProvider>();
            dataProvedorMock.Setup(dp => dp.Agora)
                .Returns(new DateTime(2023, 1, 1));

            var calendarioServico = new CalendarioServico(dataProvedorMock.Object);
            var date = new DateTime(2023, 01, 1);

            calendarioServico.Agendar(date, 10, "Evento Teste");

            var agendamentos = calendarioServico.ObterAgendamentosDaSemana(1, 1);

            Assert.Single(agendamentos);
            Assert.Equal("Evento Teste", agendamentos[0].Item2.Titulo);
        }

        [Fact]
        public void ObterAgendamentosDaSemana_QuandoEventosExistentesNaSemanaPosterior_DeveRetornarVazio()
        {
            var dataProvedorMock = new Mock<IDateTimeProvider>();
            dataProvedorMock.Setup(dp => dp.Agora)
                .Returns(new DateTime(2023, 1, 1));

            var calendarioServico = new CalendarioServico(dataProvedorMock.Object);
            var date = new DateTime(2023, 01, 1);

            calendarioServico.Agendar(date, 10, "Evento Teste");
            var agendamentos = calendarioServico.ObterAgendamentosDaSemana(1, 2);

            Assert.Empty(agendamentos);
        }

        [Fact]
        public void ObterAgendamentosDoDia_QuandoEventoExistente_RetornaLista()
        {
            var dataProvedorMock = new Mock<IDateTimeProvider>();
            dataProvedorMock.Setup(dp => dp.Agora)
                .Returns(new DateTime(2023, 1, 1));
            var calendarioServico = new CalendarioServico(dataProvedorMock.Object);
            var date = new DateTime(2023, 01, 1);
            calendarioServico.Agendar(date, 10, "Evento Teste");

            var eventos = calendarioServico.ObterAgendamentosDoDia(date);

            Assert.Single(eventos);
            Assert.Equal("Evento Teste", eventos[0].Titulo);
            Assert.Equal((uint)10, eventos[0].Hora);
        }
    }
}