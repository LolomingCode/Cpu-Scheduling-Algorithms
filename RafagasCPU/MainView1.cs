using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RafagasCPU
{
    public partial class MainView1 : Form
    {
        public MainView1()
        {
            InitializeComponent();
        }
        
        FCFS F = new FCFS();
        RR R = new RR();
        SFJ S = new SFJ(0,null);
        PP P = new PP(0,null,0);

        private void btn_calcularFCFS_Click(object sender, EventArgs e)
        {
            F.InstanciarListas();
            LimpiarGrilla(F.nprocesos, F.Rafagas, dgv_outFCFS);
            CargarListas(dgv_inFCFS, F.Rafagas, F.nprocesos, "RafagaFCFS", "ProcesoinFCFS");
            F.InstanciarArrays();
            F.CalcularRetorno();
            F.CalcularEspera();
            LlenardgvoutFCFS("RetornoFCFS", "EsperaFCFS", "ProcesooutFCFS", F.Retorno, dgv_outFCFS, F.Espera, F.nprocesos);
            AsignarTMS(lbl_tmeFCFS, lbl_tmrFCFS, F.Retorno, F.Espera, F);


        }
        private void LlenardgvoutFCFS(string Retorno, string Espera, string Proceso, int[] _Retorno, DataGridView Grilla, int[] _Espera, List<string> _Procesos)
        {
            for (int i = 0; i < _Retorno.Length; i++)
            {
                if (Retorno.Length > Grilla.Rows.Count)
                {
                    Grilla.Rows.Add();
                }
                Grilla.Rows[i].Cells[Retorno].Value = _Retorno[i];
                Grilla.Rows[i].Cells[Espera].Value = _Espera[i];
                Grilla.Rows[i].Cells[Proceso].Value = _Procesos[i];


            }

        }
        private void CargarListas(DataGridView Grilla, List<int> ListaR, List<string> ListaP, string Rafaga, string Proceso)
        {
            
            foreach (DataGridViewRow row in Grilla.Rows)
            {
                if (!row.IsNewRow)
                {
                    try
                    {
                        ListaR.Add((Convert.ToInt32(row.Cells[Rafaga].Value)));
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Por favor ingrese valores de rafagas que sean numericos");
                    }
                    ListaP.Add((Convert.ToString(row.Cells[Proceso].Value)));
                    if (tabControl1.SelectedTab == _PP)
                    {
                        P.Prioridad.Add((Convert.ToInt32(row.Cells["_Prioridad"].Value)));
                    }
                }


            }
        }
        private void btn_calcularRR_Click(object sender, EventArgs e)
        {
            R.InstanciarListas();
            LimpiarGrilla(R.nprocesos, R.Rafagas, dgv_outRR);
            R.Q = Convert.ToInt32(nud_Q.Value);
            CargarListas(dgv_inRR, R.Rafagas, R.nprocesos, "RafagaRR", "ProcesoinRR");
            R.InstanciarArrays();
            R.CalcularRetorno();
            R.CalcularEspera();
            LlenardgvoutFCFS("RetornoRR", "EsperaRR", "ProcesooutRR", R.Retorno, dgv_outRR, R.Espera, R.nprocesos);
            AsignarTMS(lbl_tmeRR, lbl_tmrRR, R.Retorno, R.Espera, R);
        }
        private void LimpiarGrilla(List<string> proceso, List<int> rafaga, DataGridView Grilla)
        {
            Grilla.Rows.Clear();
            proceso.Clear();
            rafaga.Clear();

        }
        private void AsignarTMS(Label lbl_tme, Label lbl_tmr, int[] _Retorno, int[] _Espera, Procesos P)
        {
            lbl_tme.Text = "TME:" + Convert.ToString(P.CalcularTME(_Espera));
            lbl_tmr.Text = "TMR:" + Convert.ToString(P.CalcularTMR(_Retorno));
        }

        private void btn_calcularSJF_Click(object sender, EventArgs e)
        {
            
            S.InstanciarListas();
            LimpiarGrilla(S.nprocesos, S.Rafagas, dgv_outSJF);
            CargarListas(dvgin_SJF, S.Rafagas, S.nprocesos, "RafagaSJF", "ProcesoinSJF");
            List<SFJ> L = new List<SFJ>();
            for (int i = 0; i < S.nprocesos.Count; i++)
            {
                L.Add(new SFJ(S.Rafagas[i], S.nprocesos[i]));
            }
            List<SFJ> Lordenado = L.OrderBy(o => o._Rafaga).ToList();
            S.Rafagas.Sort();
            S.InstanciarArrays();
            S.CalcularRetorno();
            S.CalcularEspera();
            for (int i = 0; i < S.nprocesos.Count; i++)
            {
                Lordenado[i]._Retorno = S.Retorno[i];
                Lordenado[i]._Espera = S.Espera[i];
            }
            for (int i = 0; i < S.Rafagas.Count; i++)
            {
                if (S.Retorno.Length > dgv_outSJF.Rows.Count)
                {
                    dgv_outSJF.Rows.Add();
                }
                dgv_outSJF.Rows[i].Cells["ProcesooutSJF"].Value = Lordenado[i]._Proceso;
                dgv_outSJF.Rows[i].Cells["EsperaSJF"].Value = Lordenado[i]._Espera;
                dgv_outSJF.Rows[i].Cells["RetornoSJF"].Value = Lordenado[i]._Retorno;
            }
            dgv_outSJF.Sort(dgv_outSJF.Columns["ProcesooutSJF"], ListSortDirection.Ascending);
            AsignarTMS(lbl_tmeSJF, lbl_tmrSJF, S.Retorno, S.Espera, S);

        }

        private void dvgin_SJF_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            for (int i = 0; i < dvgin_SJF.Rows.Count; i++)
            {
                dvgin_SJF.Rows[i].Cells[0].Value = ("P" + "-" + (i + 1));
            }
        }

        private void MainView1_Load(object sender, EventArgs e)
        {
            dgv_inFCFS.Rows[0].Cells[0].Value = ("P-1");
            dvgin_SJF.Rows[0].Cells[0].Value = ("P-1");
            dgv_inRR.Rows[0].Cells[0].Value = ("P-1");
            dgv_inPP.Rows[0].Cells[0].Value = ("P-1");
             
        }

        private void dgv_inFCFS_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            for (int i = 0; i < dgv_inFCFS.Rows.Count; i++)
            {
                dgv_inFCFS.Rows[i].Cells[0].Value = ("P" + "-" + (i + 1));
            }
        }

        private void dgv_inPP_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            for (int i = 0; i < dgv_inPP.Rows.Count; i++)
            {
                dgv_inPP.Rows[i].Cells[0].Value = ("P" + "-" + (i + 1));
            }
        }

        private void dgv_inRR_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            for (int i = 0; i < dgv_inRR.Rows.Count; i++)
            {
                dgv_inRR.Rows[i].Cells[0].Value = ("P" + "-" + (i + 1));
            }
        }

        private void btn_calcularPP_Click(object sender, EventArgs e)
        {
            P.InstanciarListas();
            LimpiarGrilla(P.nprocesos, P.Rafagas, dgv_outPP);
            CargarListas(dgv_inPP, P.Rafagas, P.nprocesos, "RafagaPP", "ProcesoinPP");
            List<PP> L = new List<PP>();
            for (int i = 0; i < P.nprocesos.Count; i++)
            {
                L.Add(new PP(P.Rafagas[i], P.nprocesos[i],P.Prioridad[i]));
            }
            List<PP> Lordenado = L.OrderBy(o => o._Prioridad).ToList();
            //P.Prioridad.Sort();
            P.InstanciarArrays();
            for (int i = 0; i < P.nprocesos.Count; i++)
            {
                P.Rafagas[i] = Lordenado[i]._Rafaga;
            }
            P.CalcularRetorno();
            P.CalcularEspera();
            for (int i = 0; i < P.nprocesos.Count; i++)
            {
                Lordenado[i]._Retorno = P.Retorno[i];
                Lordenado[i]._Espera = P.Espera[i];
            }
            for (int i = 0; i < P.Rafagas.Count; i++)
            {
                if (P.Retorno.Length > dgv_outPP.Rows.Count)
                {
                    dgv_outPP.Rows.Add();
                }
                dgv_outPP.Rows[i].Cells["ProcesooutPP"].Value = Lordenado[i]._Proceso;
                dgv_outPP.Rows[i].Cells["EsperaPP"].Value = Lordenado[i]._Espera;
                dgv_outPP.Rows[i].Cells["RetornoPP"].Value = Lordenado[i]._Retorno;
            }
            dgv_outPP.Sort(dgv_outPP.Columns["ProcesooutPP"], ListSortDirection.Ascending);
            AsignarTMS(lbl_tmePP, lbl_tmrPP, P.Retorno, P.Espera, P);
        }

       
    }
}





