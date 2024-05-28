using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace pryPedidoDePrograma
{
    internal class clsBaseDatos
    {
        //Instanciamos 3 objetos conexion, comando y adaptador
        private OleDbConnection conexion = new OleDbConnection();//Hace de puente con el sistema gestor de base de datos 
        private OleDbCommand comando = new OleDbCommand();//Ejecuta las acciones que le pidamos a la base de datos 
        private OleDbDataAdapter adaptador = new OleDbDataAdapter();//Adapta el tipo de datos a nuestro 

        OleDbDataReader lectorBD;

        //Delaro una variable de tipo DataSet que voy a utilizar como contenedor de datos de las tablas de la base de datos 
        DataSet objDS;

        private string varCadenaConexion2 = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=BDusuarioCasa.accdb";

        public static bool acceso;

        public string estadoConexion = "";

        public string estadoDeConexion;

        public void ConectarBD()
        {
            try
            {
                //Establezco la cadena de conexión con la base de datos 
                conexion.ConnectionString = varCadenaConexion2;
                //Abro la conexión con la base de datos 
                conexion.Open();
            }
            catch (Exception error)
            {
                MessageBox.Show(Convert.ToString(error));
            }
        }

        public void BuscarUsuario()
        {
            try
            {
                ConectarBD();

                //Creo una instancia de la clase OleDbCommand para ejecutar los comandos en la base de datos 
                comando = new OleDbCommand();

                //Establezco la conexión, para que cuando se ejecute el comando lo opere en la base de datos que debe hacerse
                comando.Connection = conexion;

                //Establezco el tipo de comando, con este comando le indico que voy a leer una tabla en específica
                comando.CommandType = System.Data.CommandType.TableDirect;

                //Le digo que tabla es la que se va a leer
                comando.CommandText = "Usuario";

                //Ejecuto el comando y leo la los resultados de la consulta
                lectorBD = comando.ExecuteReader();

                //Si tiene filas quiere decir que hay datos 
                if (lectorBD.HasRows)
                {
                    while (lectorBD.Read())
                    {
                        //Almaceno los datos del registro que estoy leyendo en dos variables
                        //Columna[1] (campo Usuario), columna[2] (campo Contraseña)
                        string usuarioBD = lectorBD[1].ToString();
                        string contraseñaBD = lectorBD[2].ToString();

                        //Si las variables del formulario inicio sesión donde esta guardado el usuario
                        //y la contraseña que ingreso el usuario son iguales entonces
                        //la variable booleana acceso va a ser verdadera y rompo el bucle
                        if (usuarioBD == frmLogin.usuario & contraseñaBD == frmLogin.contraseña)
                        {
                            acceso = true;
                            break;
                        }
                        else
                        {
                            acceso = false;
                        }
                    }
                }
            }
            catch (Exception error)
            {
                estadoConexion = error.Message;
            }
        }

        public void CrearCuenta()
        {
            try
            {
                ConectarBD();

                objDS = new DataSet();

                comando = new OleDbCommand();

                comando.Connection = conexion;

                //Establezco el tipo de comando, con este comando le indico que voy a trabajar con una tabla específica
                comando.CommandType = System.Data.CommandType.TableDirect;

                //Le digo que tabla quiero traer
                comando.CommandText = "Usuario";

                //Creo el objeto DataAdapter pasando como parámetro el objeto comando que quiero vincular
                adaptador = new OleDbDataAdapter(comando);

                //Ejecuto la lectura de la tabla y almaceno su contenido en el dataSet
                adaptador.Fill(objDS, "Usuario");

                //Obtengo una referencia a la tabla

                DataTable objTabla = objDS.Tables["Usuario"];

                //Creo el nuevo DataRow con la estructura de campos de la tabla de la cual quiero traer los datos
                DataRow nuevoRegistro = objTabla.NewRow();

                //Asigno los valores a todos los campos del DataRow
                nuevoRegistro["Nombre"] = frmCrearCuenta.usuarioCrearCuenta;
                nuevoRegistro["Contraseña"] = frmCrearCuenta.lasContraseñasSonIguales;
                nuevoRegistro["Perfil"] = frmCrearCuenta.perfilCrearCuenta;

                //Agrego el DataRow a la tabla
                objTabla.Rows.Add(nuevoRegistro);


                //Creo el objeto OledBCommandBuilder pasando como parámetro el DataAdapter
                OleDbCommandBuilder constructor = new OleDbCommandBuilder(adaptador);

                //Actualizo la base con los cambios realizados
                adaptador.Update(objDS, "Usuario");

                estadoConexion = "Cuenta creada con éxito";
            }
            catch (Exception error)
            {
                estadoConexion = error.Message;
            }
        }

        public void RegistroLogInicioSesionExitoso()
        {
            try
            {
                ConectarBD();

                objDS = new DataSet();

                comando = new OleDbCommand();

                comando.Connection = conexion;

                //Establezco el tipo de comando, con este comando le indico que voy a trabajar con una tabla específica
                comando.CommandType = System.Data.CommandType.TableDirect;

                //Le digo que tabla quiero traer
                comando.CommandText = "Logs";

                //Creo el objeto DataAdapter pasando como parámetro el objeto comando que quiero vincular
                adaptador = new OleDbDataAdapter(comando);

                //Ejecuto la lectura de la tabla y almaceno su contenido en el dataAdapter
                adaptador.Fill(objDS, "Logs");

                //Obtengo una referencia a la tabla

                DataTable objTabla = objDS.Tables["Logs"];

                //Creo el nuevo DataRow con la estructura de campos de la tabla de la cual quiero traer los datos
                DataRow nuevoRegistro = objTabla.NewRow();

                //Asigno los valores a todos los campos del DataRow
                nuevoRegistro["Categoria"] = "Inicio Sesión";
                nuevoRegistro["FechaHora"] = DateTime.Now;
                nuevoRegistro["Descripcion"] = "Inicio exitoso";

                //Agrego el DataRow a la tabla
                objTabla.Rows.Add(nuevoRegistro);


                //Creo el objeto OledBCommandBuilder pasando como parámetro el DataAdapter
                OleDbCommandBuilder constructor = new OleDbCommandBuilder(adaptador);

                //Actualizo la base con los cambios realizados
                adaptador.Update(objDS, "Logs");

                estadoDeConexion = "Registro exitoso de log";
            }
            catch (Exception error)
            {
                estadoDeConexion = error.Message;
            }
        }
    }
}
