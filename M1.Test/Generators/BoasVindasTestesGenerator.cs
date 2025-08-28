using System.Collections;

namespace M1.Test.Generators
{
    internal class BoasVindasTestesGenerator : IEnumerable<object[]>
    {
        private static readonly List<object[]> TestCases = new List<object[]>
        {
            new object[] { "pt_BR", "Seja Bem Vindo" },
            new object[] { "en_US", "Welcome" }
        };
        
        public IEnumerator<object[]> GetEnumerator()
        {
            return TestCases.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static IEnumerable<object[]> GetCasosDeTesteIdiomaEncontrado()
        {
            return TestCases;
        }

        public static IEnumerable<object[]> GetCasosDeIdiomaNaoEncontrado()
        {
            yield return new object[] { "fr_CA", "Tradução de Boas Vindas para o idioma fr_CA não encontrada" };
            yield return new object[] { "ch_CA", "Tradução de Boas Vindas para o idioma ch_CA não encontrada" };
        }
    }
}
