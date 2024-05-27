using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pryPedidoDePrograma
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        public static string usuario;
        public static string contraseña;

        int contador = 0;

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            usuario = txtUsuario.Text;
            contraseña = txtContraseña.Text;

            clsBaseDatos login = new clsBaseDatos();
            login.BuscarUsuario();

            clsBaseDatos objLogs = new clsBaseDatos();

            //Si el usuario y contraseña son correctos, ingresa
            if (clsBaseDatos.acceso == true)
            {
                objLogs.RegistroLogInicioSesionExitoso();


                this.Hide();
                frmPantallaReportes frmPantallaReportes = new frmPantallaReportes();
                frmPantallaReportes.Show();
            }
            else
            {
                //objLogs.RegistroLogInicioSesionFallido();

                contador = contador + 1;
                MessageBox.Show("Usuario o contraeña incorrecto", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (contador == 2)
                {
                    MessageBox.Show("Le queda un solo intento", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                //Si intenta ingresar 3 veces y no es correcta la cuenta se bloquea el botón de ingreso
                if (contador > 2)
                {
                    btnIngresar.Enabled = false;
                    MessageBox.Show("Ingreso bloqueado", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    contador = 0;
                }
            }
        }

        private void btnCrearCuenta_Click(object sender, EventArgs e)
        {
            frmCrearCuenta frmCrearCuenta = new frmCrearCuenta();
            frmCrearCuenta.Show();
            this.Hide();
        }
    }
}
