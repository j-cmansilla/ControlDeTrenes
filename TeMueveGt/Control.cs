using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeMueveGt
{
    static class Control
    {
        public static int CantidadDeTrenes { get; set; }
        public static double PrecioEconomica { get; set; }
        public static double PrecioEjecutiva { get; set; }
        public static string[] estacionesDeEntradaYSalida = { "Guatemala", "Antigua Guatemala", "Peten", "Izabal", "Escuintla" };
        public static double PrecioVIP { get; set; }
        public static int AsientosPorCompra { get; set; }
        public static int Correlativo { get; set; }
        public static int VagonesPorTren { get; set; }
        public static int Filas { get; set; }
        public static int Asientos { get; set; }
        public static Tren[] ListaTrenes { get; set; }
        public static int TrenesCreados { get; set; }
    }
}
