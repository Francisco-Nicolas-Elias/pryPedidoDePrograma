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
    public partial class frmCrearCuenta : Form
    {
        public frmCrearCuenta()
        {
            InitializeComponent();
        }

        public static string usuarioCrearCuenta;
        public static string contraseñaCrearCuenta;
        public static string repitaContraseñaCrearCuenta;
        public static string lasContraseñasSonIguales;
        public static string perfilCrearCuenta;

        private void btnCuentaCreada_Click(object sender, EventArgs e)
        {
            usuarioCrearCuenta = txtUsuarioCrearCuenta.Text;
            contraseñaCrearCuenta = txtContraseñaCrearCuenta.Text;
            repitaContraseñaCrearCuenta = txtConfirmarContraseñaCrearCuenta.Text;
            perfilCrearCuenta = txtPerfil.Text;


            if (contraseñaCrearCuenta == repitaContraseñaCrearCuenta)
            {
                lasContraseñasSonIguales = contraseñaCrearCuenta;

                MessageBox.Show("Cuenta creada con éxito", "", MessageBoxButtons.OK, MessageBoxIcon.None);

                clsBaseDatos objLogin = new clsBaseDatos();

                objLogin.CrearCuenta();

                frmLogin frmLogin = new frmLogin();
                frmLogin.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Las contraseñas ingresadas no son iguales.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
