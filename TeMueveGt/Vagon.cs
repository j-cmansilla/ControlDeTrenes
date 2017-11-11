using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeMueveGt
{
    class Vagon
    {
        public Vagon(int asientosEconomicos, int asientosEjecutivos, int asientosVIP, int filas, int asientosPorFila)
        {
            AsientosEconomicos = asientosEconomicos;
            AsientosEjecutivos = asientosEjecutivos;
            AsientosVIP = asientosVIP;
            FilasYAsientos = new string[filas,asientosPorFila];
            Filas = filas;
            AsientosPorFila = asientosPorFila;
        }

        public int AsientosEconomicos { get; set; }
        public int AsientosEjecutivos { get; set; }
        public int AsientosVIP { get; set; }
        public string[,] FilasYAsientos { get; set; }
        public int Filas { get; set; }
        public int AsientosPorFila { get; set; }
       
        public int obtenerCantidadDeAsientosDisponibles()
        {
            int count = 0;
            for (int k = 0; k < Filas; k++)
            {
                for (int i = 0; i < AsientosPorFila; i++)
                {
                    if (FilasYAsientos[k, i] != "XX")
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        public bool comprar(int fila, int asiento)
        {
            if (FilasYAsientos[fila, asiento] == "XX") return false;
            FilasYAsientos[fila, asiento] = "XX";
            return true;
        }
        
        public string[,] listaAsientos()
        {
            string[,] asientos = new string[Filas, AsientosPorFila];
            for (int i = 0; i < Filas; i++)
            {
                for (int j  = 0; j < AsientosPorFila; j++)
                {
                    asientos[i, j] = FilasYAsientos[i, j];
                }
            }
            return asientos;
        }
    }
}
