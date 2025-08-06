using M1.Servicos;
using M1.Test.Generators;

namespace M1.Test
{
    public class SecretarioServicoTeste
    {
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

    }
}
