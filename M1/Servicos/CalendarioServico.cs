using M1.Models;

namespace M1.Servicos
{
    public class CalendarioServico : ICalendarioServico
    {
        private Calendario _calendario;

        public CalendarioServico()
        {
            _calendario = new Calendario(DateTime.Now.Year);
        }
        
        public void Agendar(DateTime data, uint hora, string nomeEvento)
        {
            _calendario.AdicionarEvento(data, new Evento(nomeEvento, hora));
        }

        public bool EstaDisponivel(DateTime data, uint hora)
        {
            return _calendario.EstaDisponivel(data, hora);
        }

        public List<Tuple<DateTime, Evento>> ObterAgendamentosDaSemana(uint mes, uint semana)
        {
            var dias = _calendario.ListarDias;
            return dias.SelectMany(d => d.Eventos.Select(e => Tuple.Create(d.Data, e)))
                .Where(x => x.Item1.Month == mes && ObterSemanaDoMes(x.Item1) == semana)
                .ToList();
        }

        public List<Evento> ObterAgendamentosDoDia(DateTime data)
        {
            var dia = _calendario.ListarDias.FirstOrDefault(d => d.Data.Date == data.Date);
            return dia?.Eventos.ToList() ?? new List<Evento>();
        }

        public List<uint> ObterHorariosDisponiveis(DateTime data)
        {
            var horariosDisponiveis = new List<uint>();
            for (uint hora = 0; hora < 24; hora++)
                if (EstaDisponivel(data, hora))
                    horariosDisponiveis.Add(hora);

            return horariosDisponiveis;
        }

        private uint ObterSemanaDoMes(DateTime data)
        {
            DateTime primeiroDiaMes;
            primeiroDiaMes = new DateTime(data.Year, data.Month, 1);
            int diaDaSemanaPrimeiro = (int)primeiroDiaMes.DayOfWeek;
            int diaDoMes = data.Day;
            return (uint)((diaDaSemanaPrimeiro + diaDoMes - 1) / 7 + 1);
        }
    }
}
