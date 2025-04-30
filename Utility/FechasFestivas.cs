using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using VialambreAppTest1.Data;
using System.Globalization;


namespace VialambreAppTest1.Utility
{

    public class FechasFestivas
    {
        private readonly List<Tuple<string, string, int>> diasFestivos; // Arreglo de días festivos

        // Constructor
        public FechasFestivas()
        {
            // Inicializa el arreglo de días festivos
            diasFestivos = new List<Tuple<string, string, int>>()
        {
            new("Año Nuevo", "2024-01-01", 2024),
            new("Día de los Reyes Magos", "2024-01-08", 2024),
            new("Día de San José", "2024-03-19", 2024),
            new("Jueves Santo", "2024-03-28", 2024),
            new("Viernes Santo", "2024-03-29", 2024),
            new("Día del Trabajo", "2024-05-01", 2024),
            new("Ascensión del Señor", "2024-05-13", 2024),
            new("Corpus Christi", "2024-06-03", 2024),
            new("Sagrado Corazón", "2024-06-10", 2024),
            new("San Pedro y San Pablo", "2024-07-01", 2024),
            new("Día de la Independencia", "2024-07-20", 2024),
            new("Batalla de Boyacá", "2024-08-07", 2024),
            new("La Asunción de la Virgen", "2024-08-19", 2024),
            new("Día de la Raza", "2024-10-14", 2024),
            new("Día de Todos los Santos", "2024-11-04", 2024),
            new("Independencia de Cartagena", "2024-11-11", 2024),
            new("Inmaculada Concepción", "2024-12-08", 2024),
            new("Navidad", "2024-12-25", 2024)
            
            // ... actualizar  festivos para las fechas requeridas
        };
        }

        // Método para verificar si una fecha es un día festivo
        private bool IsFeriado(DateTime fecha)
        {
            // Recorre el arreglo de días festivos
            foreach (var diaFestivo in diasFestivos)
            {
                // Convierte la fecha del día festivo en formato DateTime
                DateTime fechaFestivo = DateTime.ParseExact(diaFestivo.Item2, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                // Compara la fecha actual con la fecha del día festivo
                if (fecha == fechaFestivo)
                {
                    return true; // Es un día festivo
                }
            }

            return false; // No es un día festivo
        }

        // Método para calcular la diferencia de días hábiles entre dos fechas
        public int CalculateBusinessDaysDifference(DateTime createDate, DateTime requiredDate)
        {
            // Ajustar hora de creación si es después de las 5:30 PM
            createDate = createDate.TimeOfDay > new TimeSpan(17, 30, 0)
                ? createDate.AddDays(1).Date.AddHours(7).AddMinutes(30)
                : createDate.Date.AddHours(7).AddMinutes(30);

            int businessDays = 0;
            DateTime currentDate = createDate;

            while (currentDate < requiredDate)
            {
                // Verificar si es día hábil
                if (!IsFeriado(currentDate) &&
                    currentDate.DayOfWeek != DayOfWeek.Saturday &&
                    currentDate.DayOfWeek != DayOfWeek.Sunday)
                {
                    // Incrementar días hábiles si la hora actual es >= 7:30 AM
                    if (currentDate.TimeOfDay >= new TimeSpan(7, 30, 0))
                    {
                        businessDays++;
                    }
                }

                // Avanzar al siguiente día a las 7:30 AM
                currentDate = currentDate.AddDays(1).Date.AddHours(7).AddMinutes(30);
            }

            return businessDays;
        }

    }
}






































