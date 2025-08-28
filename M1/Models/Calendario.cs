namespace M1.Models
{
    public class Calendario(int ano)
    {
        public int Ano { get; } = ano;
        private List<Dia> Dias { get; set; } = new();


        public void AdicionarEvento(DateTime data, Evento evento)
        {
            if (EstaDisponivel(data, evento.Hora))
            {
                var dia = Dias.FirstOrDefault(d => d.Data.Date == data.Date);
                if (dia == null)
                {
                    dia = new Dia { Data = data, Eventos = [evento] };
                    Dias.Add(dia);
                }
                else
                {
                    dia.Eventos.Add(evento);
                }
            }
        }

        public bool EstaDisponivel(DateTime data, uint hora)
        {
            var dia = Dias.FirstOrDefault(d => d.Data.Date == data.Date);
            return dia == null || dia.Eventos.All(e => e.Hora != hora);
        }

        public List<Dia> ListarDias => Dias;
    }

    public class Dia
    {
        public DateTime Data { get; set; }
        public List<Evento> Eventos { get; set; } = [];
    }

    public record Evento(string Titulo, uint Hora);
}
