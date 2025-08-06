using M1.Models;

namespace M1.Servicos
{
    public interface ICalendarioServico
    {
        void Agendar(DateTime data, uint hora, string nomeEvento);
        void Cancelar(DateTime data, uint hora);
        bool EstaDisponivel(DateTime data, uint hora);
        List<Tuple<DateTime, Evento>> ObterAgendamentosDaSemana(uint mes, uint semana);
        List<Evento> ObterAgendamentosDoDia(DateTime data);
        List<uint> ObterHorariosDisponiveis(DateTime data);
    }
}
