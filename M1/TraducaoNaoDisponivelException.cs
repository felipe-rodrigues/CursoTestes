namespace M1
{
    public class TraducaoNaoDisponivelException : Exception
    {
        public TraducaoNaoDisponivelException(string idioma) : base($"Tradução nao disponivel para o idioma: {idioma}")
        {
        }
    }
}
