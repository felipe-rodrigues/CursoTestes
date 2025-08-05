namespace M1.Servicos
{
    public class SecretarioServico
    {
        private string _idioma;
        private readonly ICalendarioServico _calendarioServico;
        
        public SecretarioServico(string idioma)
        {
            _idioma = idioma;
        }

        public SecretarioServico(string idioma, ICalendarioServico servico)
        {
            _calendarioServico = servico;
            _idioma = idioma;
        }
        
        public string BoasVindas()
        {
            if (_idioma == "pt_BR")
                return "Seja Bem Vindo";

            else if (_idioma == "en_US")
                return "Welcome";

            throw new($"Tradução de Boas Vindas para o idioma {_idioma} não encontrada");
        }
        
        
        public void AgendarCompromisso(DateTime data, uint hora, string nomeEvento)
        {
            if(_calendarioServico.EstaDisponivel(data,hora))
                _calendarioServico.Agendar(data, hora, nomeEvento);
            
            else
                throw new($"Data e hora não disponíveis para o evento {nomeEvento}");
        }
    }
}
