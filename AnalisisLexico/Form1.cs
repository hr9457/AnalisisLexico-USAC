using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnalisisLexico
{
    public partial class Form1 : Form
    {

        
        public Form1()
        {
            InitializeComponent();
            //activacion de barras para moverse sobre el texto del archivo de entrada
            txtTexto.ScrollBars = ScrollBars.Both;
            txtTexto.WordWrap = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }


        //apertura de archivos
        OpenFileDialog buscardor = new OpenFileDialog();//buscador archivo en windows
        String ruta;//variable para guardar la ruta del archivo de entrada
        private void btnAbrir_Click(object sender, EventArgs e)
        {
            if(buscardor.ShowDialog() == DialogResult.OK)//buscador.ShowDialog(); dialogo para la busqueda del archivo
            {
                ruta = buscardor.FileName;//guardando la ruta del archivo
            }
            //******************//
            //lectura del archivo
            try
            {
                if(ruta!= null)//verificacion de que exista una ruta de archivo
                {
                    StreamReader leer = new StreamReader(ruta);
                    while (!leer.EndOfStream)//leer hasta encuentre el ultimo elemento del archivo
                    {
                        String lectura = leer.ReadToEnd();//lectura del archivo
                        txtTexto.Text = lectura;//impresion en el cuadro de texto principal
                    }
                    leer.Close();//cierre del archivo de entrada
                }
                else
                {
                    MessageBox.Show("Error en la apertura del archivo");//mensaje de advertencia
                }
            }catch(IOException ioex)
            {
                throw new IOException("ha ocurrdio un error con el archivo", ioex);//mensaje de advertencia en la lectura del archivo
            }
        }


        //evento para la salida  del programa
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();//cierre de la aplicacion
        }


        analizadorLexico analisis;
        String cadena;
        //evento para el analisis lexico del archivo de entrada 
        private void btnAnalizar_Click(object sender, EventArgs e)
        {
            analisis = new analizadorLexico();
            cadena = txtTexto.Text;
            analisis.automata(cadena);
        }

        //evento para generar reporte de token
        private void btnReporte_Click(object sender, EventArgs e)
        {
            analisis.reporteToken();
            System.Diagnostics.Process.Start("Reportetoken.html");
        }


        

    }
}
