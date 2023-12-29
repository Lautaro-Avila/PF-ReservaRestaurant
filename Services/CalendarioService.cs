using Microsoft.EntityFrameworkCore;
using System.Globalization;
using TrabajoFinalRestaurante.Calendario;
using TrabajoFinalRestaurante.Repository;
using TrabajoFinalRestaurante.Services.Interfaces;

namespace TrabajoFinalRestaurante.Services
{
    public class CalendarioService : ICalendarioService
    {
        private readonly ReservaRestaurantContext _restaurantContext;

        public CalendarioService(ReservaRestaurantContext restaurantContext)
        {
            _restaurantContext = restaurantContext;
        }

        public async Task<CalendarioResponse> GetCalendarioAsync()
        {
            var Dias = new List<FechaCalendario>();

            var FechaInicio = DateTime.Now;
            var FechaConsulta = FechaInicio;

            while ((FechaConsulta - DateTime.Now).TotalDays <= 7)
            {
                var FechaDia = FechaConsulta.Date.ToString("dddd", new CultureInfo("es-ES"));

                var rangos = await _restaurantContext.RangoReservas.Select(rango => new RangoReservaResponse
                {
                    Rango = rango.Descripcion,
                    Reserva = new ReservaInfo
                    {
                        Ocupados = _restaurantContext.Reservas
                                            .Where(reserva =>
                                                reserva.FechaReserva.Date == FechaConsulta.Date &&
                                                reserva.IdRangoReserva == rango.IdRangoReserva &&
                                                reserva.Estado.ToUpper() == "CONFIRMADO")
                                            .Sum(reserva => reserva.CantidadPersonas),

                        Libres = rango.Cupo - _restaurantContext.Reservas
                                            .Where(reserva =>
                                                reserva.FechaReserva.Date == FechaConsulta.Date &&
                                                reserva.IdRangoReserva == rango.IdRangoReserva &&
                                                reserva.Estado.ToUpper() == "CONFIRMADO")
                                            .Sum(reserva => reserva.CantidadPersonas),

                        Total = rango.Cupo
                    }
                }).ToListAsync();

                Dias.Add(new FechaCalendario
                {
                    Fecha = FechaConsulta.Date,
                    Dia = FechaDia,
                    Rangos = rangos
                });


                FechaConsulta = FechaConsulta.AddDays(1);
            }

            var CalendarioSemanal = new CalendarioResponse();
            CalendarioSemanal.Calendario = Dias;

            return CalendarioSemanal;
        }
    }
}