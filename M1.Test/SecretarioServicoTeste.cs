using System.Runtime.InteropServices.JavaScript;
using M1.Models;
using M1.Servicos;
using M1.Test.Generators;
using Moq;

namespace M1.Test
{
    public class SecretarioServicoTeste
    {
        private Mock<ICalendarioServico> _calendarioServicoMock;
        
        public SecretarioServicoTeste()
        {
            _calendarioServicoMock = new Mock<ICalendarioServico>();
        }
        
        #region Boas Vindas
        [Theory]
        [InlineData("pt_BR", "Seja Bem Vindo")]
        [InlineData("en_US", "Welcome")]
        public void BoasVinda_QuandoIdiomaConhecido_RetornaMensagemTraduzida(string idioma, string mensagemEsperada)
        {
            var servico = new M1.Servicos.SecretarioServico(idioma);

            var resultado = servico.BoasVindas();

            Assert.Equal(mensagemEsperada, resultado);
        }


        [Fact]
        public void BoasVindas_QuandoIdiomaNaoReconhecido_RetornaLancaExcecao()
        {
            //arrange
            var secretarioServico = new M1.Servicos.SecretarioServico("fr_CA");

            var resultado = Assert.Throws<Exception>(() => secretarioServico.BoasVindas());

            Assert.Equal("Tradução de Boas Vindas para o idioma fr_CA não encontrada", resultado.Message);
        }

        [Theory]
        [ClassData(typeof(BoasVindasTestesGenerator))]
        public void BoasVinda_QuandoIdiomaConhecido_RetornaMensagemTraduzidaComGenerator(string idioma,
            string mensagemEsperada)
        {
            var servico = new M1.Servicos.SecretarioServico(idioma);

            var resultado = servico.BoasVindas();

            Assert.Equal(mensagemEsperada, resultado);
        }

        [Theory]
        [MemberData(nameof(BoasVindasTestesGenerator.GetCasosDeTesteIdiomaEncontrado), MemberType = typeof(BoasVindasTestesGenerator))]
        public void BoasVinda_QuandoIdiomaConhecido_RetornaMensagemTraduzidaComMemberData(string idioma,
            string mensagemEsperada)
        {
            var servico = new M1.Servicos.SecretarioServico(idioma);

            var resultado = servico.BoasVindas();

            Assert.Equal(mensagemEsperada, resultado);
        }

        [Theory]
        [MemberData(nameof(BoasVindasTestesGenerator.GetCasosDeIdiomaNaoEncontrado), MemberType = typeof(BoasVindasTestesGenerator))]
        public void BoasVinda_QuandoIdiomaNaoReconhecido_RetornaLancaExcecaoComMemberData(string idioma,
            string mensagemEsperada)
        {
            //arrange
            var secretarioServico = new M1.Servicos.SecretarioServico(idioma);

            var resultado = Assert.Throws<Exception>(() => secretarioServico.BoasVindas());

            Assert.Equal(mensagemEsperada, resultado.Message);
        }

        #endregion

        #region Agendamentos

        [Fact]
        public void AgendarCompromisso_QuandoHorarioDisponivel_RetornaMensagemDeSucesso()
        {   
            var secretarioServico = new SecretarioServico("pt_BR", _calendarioServicoMock.Object);

            _calendarioServicoMock.Setup(c => c.EstaDisponivel(It.IsAny<DateTime>(), It.IsAny<uint>()))
                .Returns(true);
            
            var resultado = secretarioServico.AgendarCompromisso(DateTime.Now, 10, "Reunião");
            
            Assert.Equal("Compromisso agendado com sucesso.", resultado);
        }

        [Fact]
        public void AgendarCompromisso_QuandoHorarioIndisponivel_RetornaMensagemDeHorarioIndisponivel()
        {
            var secretarioServico = new SecretarioServico("pt_BR", _calendarioServicoMock.Object);
            _calendarioServicoMock.Setup(c => c.EstaDisponivel(It.IsAny<DateTime>(), It.IsAny<uint>()))
                .Returns(false);

            var resultado = secretarioServico.AgendarCompromisso(DateTime.Now, 10, "Reunião");

            Assert.Equal("Horário indisponível.", resultado);
        }

        [Fact]
        public void ConfirmaEventoMarcado_QuandoAgendamentoFeito_RetornaMensagemDeConfirmacao()
        {
            var secretarioServico = new SecretarioServico("pt_BR", _calendarioServicoMock.Object);
            _calendarioServicoMock.Setup(c => c.ObterAgendamentosDoDia(It.IsAny<DateTime>()))
                .Returns(new List<Evento> { new Evento("Reunião", 10) });

            var resultado = secretarioServico.ConfirmaEventoMarcado(DateTime.Now, 10, "Reunião");
            
            Assert.Equal("Evento Reunião confirmado", resultado);
        }

        [Fact]
        public void ConfirmaEventoMarcado_QuandoAgendamentoNaoFeito_RetornaMensagemDeNaoConfirmacao()
        {
            var nomeEvento = "Reunião";
            uint hora = 10;
            var data = DateTime.Now;
            var secretarioServico = new SecretarioServico("pt_BR", _calendarioServicoMock.Object);
            _calendarioServicoMock.Setup(c => c.ObterAgendamentosDoDia(It.IsAny<DateTime>()))
                .Returns(new List<Evento>());
            var resultado = secretarioServico.ConfirmaEventoMarcado(data, hora, nomeEvento);
            
            Assert.Equal("Evento Reunião não confirmado", resultado);
        }

        [Fact]
        public void
            ConfirmaEventoMarcado_QuandoAgendamentoFeitoParaHoraDiferenteDaQualEnviada_RetornaMensagemDeNaoConfirmacao()
        {
            var secretarioServico = new SecretarioServico("pt_BR", _calendarioServicoMock.Object);
            _calendarioServicoMock.Setup(c => c.ObterAgendamentosDoDia(It.IsAny<DateTime>()))
                .Returns(new List<Evento> { new Evento("Reunião", 11) });
            
            var resultado = secretarioServico.ConfirmaEventoMarcado(DateTime.Now, 10, "Reunião");

            Assert.Equal("Evento Reunião não confirmado", resultado);
        }

        [Fact]
        public void CancelarCompromisso_QuandoAgendamentoRealizado_RetornaMensagemDeSucesso()
        {
            var secretarioServico = new SecretarioServico("pt_BR", _calendarioServicoMock.Object);
            
            var resultado = secretarioServico.CancelarCompromisso(DateTime.Now, 10);

            Assert.Equal("Compromisso cancelado com sucesso.", resultado);
        }
        
        [Fact]
        public void CancelarCompromisso_QuandoAgendamentoNaoRealizado_RetornaMensagemDeErro()
        {
            var secretarioServico = new SecretarioServico("pt_BR", _calendarioServicoMock.Object);
            var date = new DateTime(2025, 08, 21);
            _calendarioServicoMock.Setup(c => c.Cancelar(It.IsAny<DateTime>(), It.IsAny<uint>()))
                .Throws(new Exception($"Evento não encontrado para a data 21/08/2025 e hora 10"));

            var resultado = Assert.Throws<Exception>(() => secretarioServico.CancelarCompromisso(date, 10));

            Assert.Equal(
                "Erro ao cancelar compromisso: Evento não encontrado para a data 21/08/2025 e hora 10",
                resultado.Message);
        }

        [Fact]
        public void ObterAgendamentosDaSemana_QuandoEventosConfirmados_RetornaMensagemComEventoEHora()
        {
            var date = new DateTime(2025, 01, 01);
            var secretarioServico = new SecretarioServico("pt_BR", _calendarioServicoMock.Object);
            _calendarioServicoMock.Setup(c => c.ObterAgendamentosDaSemana((uint)date.Month, 01))
                .Returns(new List<Tuple<DateTime, Evento>>
                {
                    new Tuple<DateTime, Evento>(date, new Evento("Reunião", 10))
                });
            var resultado = secretarioServico.ObterAgendamentosDaSemana((uint)date.Month,01);
            Assert.Equal("Reunião - 01/01/2025 ás 10", resultado[0]);
        }
        
        [Fact]
        public void ObterAgendamentosDoDia_QuandoEventosConfirmados_RetornaMensagemComEventoEHora()
        {
            var date = new DateTime(2025, 01, 01);
            var secretarioServico = new SecretarioServico("pt_BR", _calendarioServicoMock.Object);
            _calendarioServicoMock.Setup(c => c.ObterAgendamentosDoDia(date))
                .Returns(new List<Evento> { new Evento("Reunião", 10) });
            var resultado = secretarioServico.ObterAgendamentosDoDia(date);
            Assert.Equal("Reunião - ás 10 ", resultado[0]);
        }

        [Fact]
        public void ObterHorariosDisponiveis_QuandoHorariosDisponiveis_RetornaListaDeHorarios()
        {
            var date = new DateTime(2025, 01, 01);
            var secretarioServico = new SecretarioServico("pt_BR", _calendarioServicoMock.Object);
            _calendarioServicoMock.Setup(c => c.ObterHorariosDisponiveis(date)).Returns(new List<uint> { 10, 11, 12 });
            var resultado = secretarioServico.ObterHorariosDisponiveis(date);
            Assert.Equal(new List<uint> { 10, 11, 12 }, resultado);
        }

        [Fact]
        public void ObterHorariosDisponiveis_QuandoNenhumHorarioDisponivel_RetornaExcecao()
        {
            var date = new DateTime(2025, 01, 01);
            var secretarioServico = new SecretarioServico("pt_BR", _calendarioServicoMock.Object);
            _calendarioServicoMock.Setup(c => c.ObterHorariosDisponiveis(date)).Returns(new List<uint>());
            var resultado = Assert.Throws<Exception>(() => secretarioServico.ObterHorariosDisponiveis(date));
            Assert.Equal("Não há horários disponíveis para a data 01/01/2025", resultado.Message);
        }

        #endregion
    }
}
