using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace RafagasCPU
{
    class Procesos
    {
        public List<int> Rafagas;
        public List<string> nprocesos;
        public int[] Retorno;
        public int[] Espera;
        public double CalcularTME(int[] retorno)
        {
            double tme = 0;
            for (int i = 0; i < Rafagas.Count; i++)
            {
                tme += retorno[i];
            }
            return Math.Round(tme / Rafagas.Count, 2);
        }
        public double CalcularTMR(int[] espera)
        {
            double tmr = 0;
            for (int i = 0; i < Rafagas.Count; i++)
            {
                tmr += espera[i];
            }
            return Math.Round(tmr / Rafagas.Count, 2);
        }
        public void InstanciarListas()
        {
            Rafagas = new List<int>();
            nprocesos = new List<string>();

        }
        public void InstanciarArrays()
        {
            Retorno = new int[Rafagas.Count];
            Espera = new int[Rafagas.Count];
        }
        public void CalcularRetorno()
        {

            for (int i = 0; i < Rafagas.Count; i++)
            {

                Retorno[i] = Rafagas[i];

            }
            for (int i = 0; i < Rafagas.Count; i++)
            {
                if (i != 0)
                {
                    Retorno[i] += Retorno[i - 1];
                }

            }

        }
        public void CalcularEspera()
        {
            for (int i = 0; i < Rafagas.Count; i++)
            {
                if (i == 0)
                {
                    Espera[0] = 0;
                }
                else
                {
                    Espera[i] = Retorno[i - 1];
                }
            }
        }


    }
    class FCFS : Procesos
    {
        ///Solamente Esta para crear la instancia para este algoritmo en el Formulario

    }

    class RR : Procesos
    {
        public int Q = 0;
        public int[] ArrayDuplicada;
        public void CalcularRetorno()
        {
            DuplicarArray();
            int contador = 0;
            bool fin = false;
            bool mayorcero = false;
            while (!fin)
            {

                for (int i = 0; i < Rafagas.Count; i++)
                {

                    foreach (var item in Rafagas)
                    {
                        if (item > 0)
                        {
                            mayorcero = true;
                            break;
                        }
                        else
                        {
                            mayorcero = false;

                        }
                    }

                    if (mayorcero == true)
                    {

                        if (Rafagas[i] > Q)
                        {
                            Rafagas[i] = Rafagas[i] - Q;
                            contador += Q;
                            Retorno[i] = contador;

                        }

                        else
                        {
                            if (Rafagas[i] != 0)
                            {
                                contador += Rafagas[i];
                                Retorno[i] = contador;
                                Rafagas[i] = 0;
                            }


                        }

                    }
                    else
                    {
                        fin = true;
                        break;
                    }

                }
            }

        }
        public void CalcularEspera()
        {
            for (int i = 0; i < Rafagas.Count; i++)
            {
                Espera[i] = Retorno[i] - ArrayDuplicada[i];
            }


        }
        private void DuplicarArray()
        {
            ArrayDuplicada = new int[Rafagas.Count];
            for (int i = 0; i < Rafagas.Count; i++)
            {

                ArrayDuplicada[i] = Rafagas[i];

            }

        }
    }
    class SFJ : Procesos
    {

        public SFJ(int Retorno, int Espera)
        {
            _Retorno = Retorno;
            _Espera = Espera;

        }
        public SFJ(int rafaga, string proceso)
        {
            _Rafaga = rafaga;
            _Proceso = proceso;

        }
        public int _Retorno;
        public int _Espera;
        public int _Rafaga;
        public string _Proceso;

    }
    class PP : Procesos
    {
        public List<int> Prioridad;
        public PP(int Retorno, int Espera)
        {
            _Retorno = Retorno;
            _Espera = Espera;

        }
        public PP(int rafaga, string proceso, int prioridad)
        {
            _Rafaga = rafaga;
            _Proceso = proceso;
            _Prioridad = prioridad;

        }
        public int _Retorno;
        public int _Espera;
        public int _Rafaga;
        public int _Prioridad;
        public string _Proceso;
        public void InstanciarListas()
        {
            Rafagas = new List<int>();
            nprocesos = new List<string>();
            Prioridad = new List<int>();
        }

    }
}



