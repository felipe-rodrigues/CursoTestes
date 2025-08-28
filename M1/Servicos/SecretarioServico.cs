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
        
        
        public string AgendarCompromisso(DateTime data, uint hora, string nomeEvento)
        {
            if (_calendarioServico.EstaDisponivel(data, hora))
            {
                _calendarioServico.Agendar(data, hora, nomeEvento);
                return _idioma switch
                {
                    "pt_BR" => "Compromisso agendado com sucesso.",
                    "en_US" => "Appointment scheduled successfully.",
                    _ => throw new TraducaoNaoDisponivelException(_idioma)
                };
            }
            
            return _idioma switch
            {
                "pt_BR" => "Horário indisponível.",
                "en_US" => "Time slot unavailable.",
                _ => throw new TraducaoNaoDisponivelException(_idioma)
            };
        }

        public string ConfirmaEventoMarcado(DateTime data, uint hora, string evento)
        {
            var eventos = _calendarioServico.ObterAgendamentosDoDia(data);
            return eventos.Any(e => e.Titulo == evento && e.Hora == hora) 
                ? _idioma switch
                {
                    "pt_BR" => $"Evento {evento} confirmado",
                    "en_US" => $"Event {evento} confirmed",
                    _ => throw new TraducaoNaoDisponivelException(_idioma)
                }
                : _idioma switch
                {
                    "pt_BR" => $"Evento {evento} não confirmado",
                    "en_US" => $"Event {evento} not confirmed",
                    _ => throw new TraducaoNaoDisponivelException(_idioma)
                };
        }

        public string CancelarCompromisso(DateTime data, uint hora)
        {
            try
            {
                _calendarioServico.Cancelar(data, hora);
                return _idioma switch
                {
                    "pt_BR" => "Compromisso cancelado com sucesso.",
                    "en_US" => "Appointment cancelled successfully.",
                    _ => throw new TraducaoNaoDisponivelException(_idioma)
                };
            }
            catch (Exception ex)
            {
                if (_idioma == "pt_BR")
                {
                    throw new Exception($"Erro ao cancelar compromisso: {ex.Message}");
                }
                else throw new Exception($"Error cancelling appointment: {ex.Message}");
            }
        }
        
        public List<string> ObterAgendamentosDaSemana(uint mes, uint semana)
        {
            var eventos = _calendarioServico.ObterAgendamentosDaSemana(mes, semana);
            return eventos.Select(e => $"{e.Item2.Titulo} - {e.Item1:dd/MM/yyyy} {(_idioma == "pt_BR" ? "ás" : "at")} {e.Item2.Hora}").ToList();
        }

        public List<string> ObterAgendamentosDoDia(DateTime data)
        {
            var eventos = _calendarioServico.ObterAgendamentosDoDia(data);
            return eventos.Select(e => $"{e.Titulo} - {(_idioma == "pt_BR" ? "ás" : "at")} {e.Hora} ").ToList();
        }

        public List<uint> ObterHorariosDisponiveis(DateTime data)
        {
            var horarios = _calendarioServico.ObterHorariosDisponiveis(data);
            
            if(horarios.Count == 0)
                throw new Exception(_idioma switch
                    {
                        "pt_BR" => $"Não há horários disponíveis para a data {data:dd/MM/yyyy}",
                        "en_US" => $"There are no available times for the date {data:dd/MM/yyyy}",
                        _ => throw new TraducaoNaoDisponivelException(_idioma)
                    }
                );

            return horarios;
        }
    }
}
