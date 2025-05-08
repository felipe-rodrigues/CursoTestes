using ModuloBaseUnit;

namespace Modulo1.Testes
{
    public class CalculadoraTestes
    {
        [Fact]
        public void Dividir_DeveRetornarDivisaoCorreta()
        {
            var calculadora = new Calculadora();
            double resultado = calculadora.Dividir(20, 4);
            Assert.Equal(5, resultado);
        }

        [Fact]
        public void Dividir_DeveLancarExcecaoQuandoDividirPorZero()
        {
            var calculadora = new Calculadora();
            Assert.Throws<DivideByZeroException>(() => calculadora.Dividir(10, 0));
        }
    }
}