using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TeMueveGt
{
    public partial class Form1 : Form
    {
        int trenes;
        int vagonesPorTren;
        int filasPorVagon;
        int asientosPorFila;
        Tren[] listaTrenes;
        int trenesCreados;
        public Form1()
        {
            InitializeComponent();
            trenes = 0;
            vagonesPorTren = 0;
            filasPorVagon = 0;
            elegirFecha.MinDate = DateTime.Now;
            asientosPorFila = 0;
            trenesCreados = 0;
            Control.TrenesCreados = 0;
            Control.Correlativo = 0;
            Asientos.RowHeadersVisible = false;
            Asientos.ColumnHeadersVisible = false;
        }

        private void txtCantidadTrenes_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtAsientos_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtCantidadTrenes.Text.Equals("") || txtVagones.Text.Equals("") || txtFilas.Text.Equals("") || txtAsientos.Text.Equals("") || txtPEconomica.Text.Equals("") || txtPEjecutiva.Text.Equals("") || txtCVIP.Text.Equals(""))
            {
                MessageBox.Show("Ingrese toda la informacion requerida!");
                return;
            }
            if (int.Parse(txtCantidadTrenes.Text) < 1 || int.Parse(txtVagones.Text) < 1 || int.Parse(txtFilas.Text) < 1 || int.Parse(txtAsientos.Text) < 1)
            {
                MessageBox.Show("Asegurese que todas las cantidades sean mayores a 0");
                return;
            }
            Control.PrecioEconomica = double.Parse(txtPEconomica.Text);
            Control.PrecioEjecutiva = double.Parse(txtPEjecutiva.Text);
            Control.PrecioVIP = double.Parse(txtCVIP.Text);
            grupoCreacionDeTrenes.Enabled = true;
            grupoConfiguracion.Enabled = false;
            trenes = int.Parse(txtCantidadTrenes.Text);
            vagonesPorTren = int.Parse(txtVagones.Text);
            filasPorVagon = int.Parse(txtFilas.Text);
            asientosPorFila = int.Parse(txtAsientos.Text);
            Control.ListaTrenes = new Tren[trenes];
            Control.Filas = filasPorVagon;
            Control.Asientos = asientosPorFila;
            MessageBox.Show("Configuración finalizada!");
        }

        private void textBox2_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int asientosCVIP = 0;
            int asientosEconomicos = 0;
            int asientosEjecutivos = 0;
            
            if (comboEntrada.SelectedIndex == -1 || comboSalida.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione la estacion de entrada y de salida!");
                return;
            }
            if (comboEntrada.SelectedIndex == comboSalida.SelectedIndex)
            {
                MessageBox.Show("La entrada y la salida no pueden ser iguales!");
                return;
            }
            if (txtAsientosVIP.Enabled)
            {
                if (txtAsientosClaseEconomica.Text.Equals("") || txtAsientosClaseEjecutiva.Text.Equals("") || txtAsientosVIP.Text.Equals(""))
                {
                    MessageBox.Show("Llene todos los campos!");
                    return;
                }
            }
            else
            {

                if (txtAsientosClaseEconomica.Text.Equals("") || txtAsientosClaseEjecutiva.Text.Equals(""))
                {
                    MessageBox.Show("Llene todos los campos!");
                    return;
                }
            }
            asientosEconomicos = int.Parse(txtAsientosClaseEjecutiva.Text);
            asientosEjecutivos = int.Parse(txtAsientosVIP.Text);
            if (radioSi.Checked)
            {
                asientosCVIP = int.Parse(txtAsientosClaseEconomica.Text);
            }
            if ((asientosEconomicos+asientosEjecutivos+asientosCVIP) > (Control.Filas*Control.Asientos))
            {
                MessageBox.Show("La suma de los asientos no puede ser mayor a la capacidad del vagon. Capacidad: "+ (Control.Filas * Control.Asientos).ToString());
                return;
            }
            if ((asientosEconomicos + asientosEjecutivos + asientosCVIP) - (Control.Filas * Control.Asientos) != 0)
            {
                MessageBox.Show("Debe distribuir bien los asientos!");
                return;
            }
            Tren nuevoTren = new Tren(Control.estacionesDeEntradaYSalida[comboSalida.SelectedIndex],Control.estacionesDeEntradaYSalida[comboEntrada.SelectedIndex],int.Parse(txtVagones.Text),(Control.Correlativo++).ToString());
            for (int i = 0; i < nuevoTren.ListaVagones.Length; i++)
            {
                nuevoTren.ListaVagones[i] = new Vagon(asientosEconomicos,asientosEjecutivos,asientosCVIP,Control.Filas,Control.Asientos);
            }
            for (int k = 0; k < nuevoTren.ListaVagones.Length; k++)
            {
                for (int i = 0; i < Control.Filas; i++)
                {
                    for (int j = 0; j < Control.Asientos; j++)
                    {
                        nuevoTren.ListaVagones[k].FilasYAsientos[i, j] = (i.ToString()+j.ToString());
                    }
                }
            }
            Control.ListaTrenes[Control.TrenesCreados] = nuevoTren;
            Control.TrenesCreados = Control.TrenesCreados + 1;
            comboBox1.Items.Add(nuevoTren.Nombre);
            if (trenes == Control.TrenesCreados)
            {
                grupoCreacionDeTrenes.Enabled = false;
            }
            radioNo.Checked = false;
            txtAsientosClaseEconomica.Text = "";
            txtAsientosClaseEjecutiva.Text = "";
            txtAsientosVIP.Text = "";
            radioSi.Checked = false;
            MessageBox.Show("Tren creado exitosamente!");
            grupoReservarComprar.Enabled = true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                btnDisponiblidad.Enabled = true;
                if (comboVagon.Items.Count == 0)
                {
                    for (int i = 0; i < Control.ListaTrenes[comboBox1.SelectedIndex].ListaVagones.Length; i++)
                    {
                        comboVagon.Items.Add("Vagon "+(i+1).ToString());
                    }
                }
            }
            if (comboBox1.SelectedIndex == -1)
            {
                btnDisponiblidad.Enabled = false;
                return;
            }
        }

        private void btnDisponiblidad_Click(object sender, EventArgs e)
        {
            if (comboVagon.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un vagon para ver la informacion!");
                return;
            }
            txtNombreTrenReporte.Text = Control.ListaTrenes[comboBox1.SelectedIndex].Nombre;
            txtAsientosDisponiblesReporte.Text = Control.ListaTrenes[comboBox1.SelectedIndex].ListaVagones[comboVagon.SelectedIndex].obtenerCantidadDeAsientosDisponibles().ToString();
        }

        private void comboBox1_Leave(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
            {
                btnDisponiblidad.Enabled = false;
                return;
            }
        }

        private void textBox1_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox2_KeyPress_2(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void llenarAsientos()
        {
            for (int i = 0; i < Control.Asientos; i++)
            {
                //Agregar Dinamicamente las columnas
                Asientos.Columns.Add(i.ToString(),i.ToString());
            }
            for (int i = 0; i < Control.Filas; i++)
            {
                Asientos.Rows.Add();
            }
            string[,] asientos = Control.ListaTrenes[comboBox1.SelectedIndex].ListaVagones[comboVagon.SelectedIndex].listaAsientos();
            for (int i = 0; i < Control.Filas; i++)
            {
                for (int j = 0; j < Control.Asientos; j++)
                {
                    Asientos.Rows[i].Cells[j].Value = asientos[i,j];
                }
            }
        }

        private void btnContinuar_Click(object sender, EventArgs e)
        {
            TimeSpan lapso = elegirFecha.Value - DateTime.Now;
            if (lapso.TotalHours>48)
            {
                MessageBox.Show("No es posible reservar/comprar con una anticipacion a 48 horas!");
                return;
            }
            if (Control.ListaTrenes[comboBox1.SelectedIndex].ListaVagones[comboVagon.SelectedIndex].obtenerCantidadDeAsientosDisponibles() > 0)
            {
                if (txtNombreCliente.Text.Equals("") || txtDPI.Text.Equals("") || txtTelefono.Text.Equals("") || txtCorreo.Text.Equals("") || txtCantidadDeAsientosAComprar.Text.Equals(""))
                {
                    MessageBox.Show("Llene todos los campos del formulario para continuar!");
                    return;
                }
                if (!radioComprar.Checked && !radioReservar.Checked)
                {
                    MessageBox.Show("Seleccione si desea comprar o reservar!");
                    return;
                }
                if (comboBox1.SelectedIndex == -1 || comboVagon.SelectedIndex == -1)
                {
                    MessageBox.Show("Seleccione un tren y un vagon para continuar!");
                    return;
                }
                if (int.Parse(txtCantidadDeAsientosAComprar.Text)>5)
                {
                    MessageBox.Show("No se permite la compra de mas de 5 asientos!");
                    return;
                }
                Control.AsientosPorCompra = int.Parse(txtCantidadDeAsientosAComprar.Text);
                if (Control.AsientosPorCompra > Control.ListaTrenes[comboBox1.SelectedIndex].ListaVagones[comboVagon.SelectedIndex].obtenerCantidadDeAsientosDisponibles())
                {
                    MessageBox.Show("La cantidad de asientos a comprar supera la cantidad de asientos disponibles!");
                    return;
                }
                seleccionDeAsiento.Enabled = true;
                grupoReservarComprar.Enabled = false;
                llenarAsientos();
                return;
        }
            else
            {
                MessageBox.Show("Este vagon ya no tiene asientos disponibles!");
            }
        }

        private void Asientos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Asientos.SelectedCells[0].Value.ToString().Equals("XX"))
            {
                //MessageBox.Show("Este asiento ya fue reservado!");
                int fila = Asientos.SelectedCells[0].RowIndex;
                int columna = Asientos.SelectedCells[0].ColumnIndex;
                btnConfirmar.Enabled = false;
            }
            else
            {
                btnConfirmar.Enabled = true;
            }
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            Control.AsientosPorCompra--;
            if (Control.AsientosPorCompra >= 0)
            {
                string fila = Asientos.SelectedCells[0].Value.ToString()[0].ToString();
                string columna = Asientos.SelectedCells[0].Value.ToString()[1].ToString();
                if (int.Parse(fila + columna) <= (Control.ListaTrenes[comboBox1.SelectedIndex].ListaVagones[comboVagon.SelectedIndex].AsientosVIP - 1))
                {
                    txtTotal.Text = (int.Parse(txtTotal.Text) + int.Parse(txtCVIP.Text.ToString())).ToString();
                }
                else if (int.Parse(fila + columna) <= (Control.ListaTrenes[comboBox1.SelectedIndex].ListaVagones[comboVagon.SelectedIndex].AsientosEjecutivos - 1)+(Control.ListaTrenes[comboBox1.SelectedIndex].ListaVagones[comboVagon.SelectedIndex].AsientosVIP))
                {
                    txtTotal.Text = (int.Parse(txtTotal.Text.ToString()) + int.Parse(txtPEjecutiva.Text.ToString())).ToString();
                }
                else
                {
                    txtTotal.Text = (int.Parse(txtTotal.Text.ToString()) + int.Parse(txtPEconomica.Text.ToString())).ToString();
                }
                Control.ListaTrenes[comboBox1.SelectedIndex].ListaVagones[comboVagon.SelectedIndex].comprar(int.Parse(fila), int.Parse(columna));
                Asientos.Rows.Clear();
                Asientos.Columns.Clear();
                llenarAsientos();
                MessageBox.Show("Asiento reservado correctamente!");
                return;
            }
            txtTotal.Text = "0";
            Asientos.Rows.Clear();
            Asientos.Columns.Clear();
            btnConfirmar.Enabled = false;
            seleccionDeAsiento.Enabled = false;
            grupoReservarComprar.Enabled = true;
        }

        private void btnCancelarCompra_Click(object sender, EventArgs e)
        {
            Asientos.Columns.Clear();
            btnConfirmar.Enabled = false;
            seleccionDeAsiento.Enabled = false;
            grupoReservarComprar.Enabled = true;
        }

        private void txtCantidadDeAsientosAComprar_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox1_KeyPress_2(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox2_KeyPress_3(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtAsientosClaseEconomica_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtAsientosClaseEjecutiva_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtAsientosVIP_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void radioSi_CheckedChanged(object sender, EventArgs e)
        {
            if (radioSi.Checked)
            {
                txtAsientosVIP.Enabled = true;
                return;
            }
            txtAsientosVIP.Enabled = false;
        }
    }
}