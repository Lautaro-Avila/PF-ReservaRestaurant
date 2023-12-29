namespace TrabajoFinalRestaurante.Calendario
{

    public class CalendarioResponse
    {
        public List<FechaCalendario> Calendario { get; set; }
    }

    public class FechaCalendario
    {
        public DateTime Fecha { get; set; }
        public string Dia { get; set; }
        public List<RangoReservaResponse> Rangos { get; set; }
    }

    public class RangoReservaResponse
    {
        public string Rango { get; set; }
        public ReservaInfo Reserva { get; set; }
    }

    public class ReservaInfo
    {
        public int Ocupados { get; set; }
        public int Libres { get; set; }
        public int Total { get; set; }
    }
}
