namespace M1.Models
{
    public class Calendario
    {
        public int Ano { get; }
        private IEnumerable<Dia> Dias { get; set; }
        

        public Calendario(int ano)
        {
            Ano = ano;
        }

        public void AdicionarEvento(DateTime data, Evento evento)
        {
            if (EstaDisponivel(data, evento.Hora))
            {
                var dia = Dias.FirstOrDefault(d => d.Data.Date == data.Date);
                if (dia == null)
                {
                    dia = new Dia { Data = data, Eventos = new List<Evento>() { evento } };
                    Dias = Dias.Append(dia);
                }
                else
                {
                    dia.Eventos.Append(evento);
                }
            }
        }

        public bool EstaDisponivel(DateTime data, uint hora)
        {
            var dia = Dias.FirstOrDefault(d => d.Data.Date == data.Date);
            if (dia == null)
                return true;
            return !dia.Eventos.Any(e => e.Hora == hora);
        }

        public IEnumerable<Dia> ListarDias => Dias;
    }

    public class Dia
    {
        public DateTime Data { get; set; }
        public IEnumerable<Evento> Eventos { get; set; }
    }

    public record Evento(string Titulo, uint Hora);
}
