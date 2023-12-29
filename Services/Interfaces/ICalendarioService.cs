using TrabajoFinalRestaurante.Calendario;

namespace TrabajoFinalRestaurante.Services.Interfaces
{
    public interface ICalendarioService
    {
        public Task<CalendarioResponse> GetCalendarioAsync();
    }
}
