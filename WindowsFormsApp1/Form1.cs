using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Empleados Empleados = new Empleados();
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnguardar_Click(object sender, EventArgs e)
        {
            if (txtnombre.Text == "")
            {

                errorProvider1.SetError(txtnombre, "no ingreso el nombre");
                txtnombre.Focus();
                return;

            }
            else if (txtDUI.Text == "")
            {

                errorProvider1.SetError(txtDUI, "no ingreso el Dui");
                txtDUI.Focus();
                return;

            }



            SqlConnection conexion = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;Connect Timeout=30");
            conexion.Open();

            Empleados.Nombre = txtnombre.Text;
            Empleados.Dui = txtDUI.Text;
            Empleados.Salario = Convert.ToDouble(txtSalario.Text);
            Empleados.Afp = Empleados.AFP(Empleados.Salario);
            txtAFP.Text = Convert.ToString(Empleados.Afp);
            Empleados.Codigo = txtcodigo.Text;
            Empleados.Iss = Empleados.ISSS(Empleados.Salario);
            Empleados.Descuento = Empleados.DESCUENTO(Empleados.Afp,Empleados.Iss);
            Empleados.Sueldoliquido = Empleados.SUELDOLOQUIDO(Empleados.Salario,Empleados.Descuento);

            string cadena = "insert into [Empleado] (Nombre, Codigo, Dui, Salario, AFP, Iss, Descuento, Salario_Liquido) values ('" + Empleados.Nombre + "','" + Empleados.Codigo + "','" + Empleados.Dui + "','" + Empleados.Salario + "','" + Empleados.Afp + "','" + Empleados.Iss + "','" + Empleados.Descuento + "','" + Empleados.Sueldoliquido + "') ";
            SqlCommand comando = new SqlCommand(cadena, conexion);
            comando.ExecuteNonQuery();

            label6.Text = "SE GUARDO CON EXITO EL REGISTRO";
            conexion.Close();
        }

        private void btnconsultartodo_Click(object sender, EventArgs e)
        {
            SqlConnection conexion = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;Connect Timeout=30");
            conexion.Open();
            string cadena = "select id, nombre, codigo, dui,salario,afp,iss,descuento,salario_liquido from Empleado";
            SqlCommand comando = new SqlCommand(cadena, conexion);
            SqlDataReader registro = comando.ExecuteReader();
            this.dataGridView1.DataSource = null;
            this.dataGridView1.Rows.Clear();
            if (registro.HasRows)
            {
                while (registro.Read())
                {
                    int n = dataGridView1.Rows.Add();
                    dataGridView1.Rows[n].Cells[0].Value = registro.GetInt32(0);
                    dataGridView1.Rows[n].Cells[1].Value = registro.GetString(1);
                    dataGridView1.Rows[n].Cells[2].Value = registro.GetString(2);
                    dataGridView1.Rows[n].Cells[3].Value = registro.GetString(3);
                    dataGridView1.Rows[n].Cells[4].Value = registro.GetDecimal(4);
                    dataGridView1.Rows[n].Cells[5].Value = registro.GetDecimal(5);
                    dataGridView1.Rows[n].Cells[6].Value = registro.GetDecimal(6);
                    dataGridView1.Rows[n].Cells[7].Value = registro.GetDecimal(7);
                    dataGridView1.Rows[n].Cells[8].Value = registro.GetDecimal(8);

                }

            }
            else
            {

                MessageBox.Show("no hay registros");
            }
            conexion.Close();


        }

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            SqlConnection conexion = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;Connect Timeout=30");
            conexion.Open();
            Int32 cod;

            if (!Int32.TryParse(Txtconsultar.Text, out cod))

            {
                errorProvider1.SetError(Txtconsultar, "No ingresó el salario de forma correcta");
                txtSalario.Focus();
                return;
            }
            errorProvider1.SetError(Txtconsultar, "");


            string cadena = "select id, nombre, codigo, dui,salario,afp,iss,descuento,salario_liquido from Empleado where Id=" + cod;
            SqlCommand comando = new SqlCommand(cadena, conexion);
            SqlDataReader registro = comando.ExecuteReader();
            if (registro.Read())
            {
                dataGridView1.Rows[0].Cells[0].Value = registro.GetInt32(0);
                dataGridView1.Rows[0].Cells[1].Value = registro.GetString(1);
                dataGridView1.Rows[0].Cells[2].Value = registro.GetString(2);
                dataGridView1.Rows[0].Cells[3].Value = registro.GetString(3);
                dataGridView1.Rows[0].Cells[4].Value = registro.GetDecimal(4);
                dataGridView1.Rows[0].Cells[5].Value = registro.GetDecimal(5);
                dataGridView1.Rows[0].Cells[6].Value = registro.GetDecimal(6);
                dataGridView1.Rows[0].Cells[7].Value = registro.GetDecimal(7);
                dataGridView1.Rows[0].Cells[8].Value = registro.GetDecimal(8);
            }
            else
                MessageBox.Show("No existe un Empleado con el código ingresado");
            conexion.Close();

        }

        private void btneliminar_Click(object sender, EventArgs e)
        {
            SqlConnection conexion = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;Connect Timeout=30");
            conexion.Open();
            string cod = Txtconsultar.Text;

            string cadena = "delete from [Empleado] where Id=" + cod;
            SqlCommand comando = new SqlCommand(cadena, conexion);
            int cant;
            cant = comando.ExecuteNonQuery();

            if (cant == 1)
            {

                this.dataGridView1.DataSource = null;
                this.dataGridView1.Rows.Clear();
                MessageBox.Show("se borro el registro");
            }
            else
            {

                MessageBox.Show("se selecciono ningun registro");

            }
            conexion.Close();
        }

        private void btnmodificar_Click(object sender, EventArgs e)
        {
            SqlConnection conexion = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;Connect Timeout=30");
            conexion.Open();
            string cod = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            string Dnombre = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            string Dcodigo = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            string Ddui = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            string Dsalario = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            string DAFP = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            string DIss = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            string Ddescuento = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            string Dsalario_liquido = dataGridView1.CurrentRow.Cells[8].Value.ToString();

            string cadena = "update [Empleado] set nombre='" + Dnombre + "', codigo='" + Dcodigo + "', dui='" + Ddui + "', salario='" + Dsalario + "', afp='" + DAFP + "', iss='" + DIss + "', descuento='" + Ddescuento + "', salario_liquido='" + Dsalario_liquido + "' where Id=" + cod;
            SqlCommand comando = new SqlCommand(cadena, conexion);
            int cant;
            cant = comando.ExecuteNonQuery();

            if (cant == 1)
            {

                this.dataGridView1.DataSource = null;
                this.dataGridView1.Rows.Clear();
                MessageBox.Show("se modificaron los datos");
            }
            else
            {

                MessageBox.Show("se selecciono ningun registro");

            }
            conexion.Close();
        }

        private void btnconsultarco_Click(object sender, EventArgs e)
        {
            SqlConnection conexion = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;Connect Timeout=30");
            conexion.Open();
            if (Txtconsultar.Text == "")

            {
                errorProvider1.SetError(Txtconsultar, "No ingresó el codigo de forma correcta");
                txtSalario.Focus();
                return;
            }
            errorProvider1.SetError(Txtconsultar , "");

            string codigo = Txtconsultar.Text;

            string cadena = "select id, nombre, codigo,dui , salario, afp, iss, descuento, salario_liquido from Empleado where codigo = '" + Txtconsultar.Text + "'";
            SqlCommand comando = new SqlCommand(cadena, conexion);
            SqlDataReader registro = comando.ExecuteReader();
            if (registro.Read())
            {
                dataGridView1.Rows[0].Cells[0].Value = registro.GetInt32(0);
                dataGridView1.Rows[0].Cells[1].Value = registro.GetString(1);
                dataGridView1.Rows[0].Cells[2].Value = registro.GetString(2);
                dataGridView1.Rows[0].Cells[3].Value = registro.GetString(3);
                dataGridView1.Rows[0].Cells[4].Value = registro.GetDecimal(4);
                dataGridView1.Rows[0].Cells[5].Value = registro.GetDecimal(5);
                dataGridView1.Rows[0].Cells[6].Value = registro.GetDecimal(6);
                dataGridView1.Rows[0].Cells[7].Value = registro.GetDecimal(7);
                dataGridView1.Rows[0].Cells[8].Value = registro.GetDecimal(8);
            }
            else
                MessageBox.Show("No existe un Empleado con el codigo ingresado");
            conexion.Close();
        }
    }

}

    

