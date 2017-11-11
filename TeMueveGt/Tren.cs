using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeMueveGt
{
    class Tren
    {
        public Vagon[] ListaVagones { get; set; }
        public string EstacionDeSalida { get; set; }
        public string EstacionDeEntrada { get; set; }
        public int CantidadDeVagones { get; set; }
        public string Nombre { get; set; }
        public Tren(string estacionDeSalida, string estacionDeEntrada, int cantidadDeVagones, string correlativo)
        {
            ListaVagones = new Vagon[cantidadDeVagones];
            EstacionDeSalida = estacionDeSalida;
            EstacionDeEntrada = estacionDeEntrada;
            CantidadDeVagones = cantidadDeVagones;
            Nombre = obtenerNombreTren(estacionDeSalida, estacionDeEntrada, correlativo);
        }

        private string obtenerNombreTren(string salida, string entrada, string correlativo)
        {
            string nombre = string.Empty;
            for (int i = 0; i < 3; i++)
            {
                nombre = nombre + salida[i];
            }
            nombre = nombre + "-";
            for (int i = 0; i < 3; i++)
            {
                nombre = nombre + entrada[i];
            }
            nombre = nombre + "-";
            nombre = nombre + correlativo.ToString();
            return nombre;
        }
    }
}
