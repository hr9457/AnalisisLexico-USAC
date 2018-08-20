using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalisisLexico
{
    class analizadorLexico
    {

        private String token;//variable de concatenacion de token 
        private int puntero;//puntero en la cadena
        private int estado = 0;//estado acutal del automata
        private int largoCadena;//tamaño de la cadena
        private String cadena;//cadena de entrada
        private char caracter;//caracter de la cadena
        private List<Token> listadoToken;//listado general de los token econtrados

        //contructor
        public analizadorLexico()
        {
            this.listadoToken = new List<Token>();//inicializacion de listado de token
        }

        //automata
        public void automata(String cadena)
        {
            this.cadena = cadena;
            largoCadena = cadena.Length - 1;//obtener el alargo de lacadena
            puntero = 0;//puntero señala la poscion en la cadena

            //ciclo para recorer la cadena 
            while (puntero <= largoCadena)
            {
                caracter = cadena[puntero];//guradar el caracter donde esta el puntero
                switch (estado)
                {
                    //estado de signos
                    case 0:
                        if (caracter.Equals(':'))
                        {
                            addToken(""+caracter, "tk_DosPuntos");
                            puntero++;
                        }
                        else if (caracter.Equals('"'))
                        {
                            addToken("" + caracter, "tk_Comillas");
                            puntero++;
                        }
                        else if (caracter.Equals('{'))
                        {
                            addToken("" + caracter, "tk_LLaveApertura");
                            puntero++;
                        }
                        else if (caracter.Equals('}'))
                        {
                            addToken("" + caracter, "tk_LLaveCierre");
                            puntero++;
                        }
                        else if (caracter.Equals(';'))
                        {
                            addToken("" + caracter, "tk_PuntoComa");
                            puntero++;
                        }
                        else if (caracter.Equals(','))
                        {
                            addToken("" + caracter, "tk_Coma");
                            puntero++;
                        }
                        else if (char.IsLetter(caracter))
                        {
                            estado = 1;//cambio de estado
                        }
                        else if (char.IsDigit(caracter))
                        {
                            estado = 2;//cambio de estado
                        }
                        else if(caracter.Equals(' ') || caracter.Equals('\n') || caracter.Equals('\t') || caracter.Equals('\r'))
                        {
                            puntero++;
                        }
                        else
                        {
                            addToken("" + caracter, "Error Lexico");
                            puntero++;
                        }
                        break;

                    //estado de letras
                    case 1:
                        if (char.IsLetter(caracter))
                        {
                            token += caracter;//concatenacion
                            estado = 1;
                            puntero++;
                        }
                        else if(caracter.Equals(' '))
                        {
                            token += caracter;
                            puntero++;
                            estado = 1;
                        }
                        else if(caracter.Equals('\n') || caracter.Equals('\t'))
                        {
                            puntero++;
                        }
                        else if (caracter.Equals('"'))//reconocer si es una cadena de texto
                        {
                            addToken("" + token, "CADENA");
                            token = "";
                            estado = 0;
                        }
                        else if(caracter.Equals(':')|| caracter.Equals('{') || caracter.Equals('}')
                            || caracter.Equals(';') || caracter.Equals(','))
                        {
                            libreriaPalabrasReservadas(token);
                            estado = 0;
                            token = "";
                        }
                            break;

                    //estado de digitos
                    case 2:
                        if (char.IsDigit(caracter))
                        {
                            token += caracter;
                            estado = 2;
                            puntero++;
                        }
                        else if (caracter.Equals(' '))
                        {
                            token += caracter;
                            puntero++;
                        }
                        else if (caracter.Equals('\n') || caracter.Equals('\t'))
                        {
                            puntero++;
                        }
                        else if (caracter.Equals(':') || caracter.Equals('"') || caracter.Equals('{') || caracter.Equals('}')
                            || caracter.Equals(';') || caracter.Equals(','))
                        {
                            addToken("" + token, "Numero");
                            token = "";
                            estado = 0;
                        }
                        break;

                }//fin de los estados

            }//fin del cilco while

        }//fin del metodo para el automata


        //metodo para encontrar mis palabras reservadas
        public void libreriaPalabrasReservadas(String token)
        {
            switch (token)
            {
                case "organigrama":
                    addToken(token, "PALABRA RESERVADA");
                    break;

                case "trabajador":
                    addToken(token, "PALABRA RESERVADA");
                    break;

                case "codigo":
                    addToken(token, "PALABRA RESERVADA");
                    break;

                case "nombre":
                    addToken(token, "PALABRA RESERVADA");
                    break;

                case "superiores":
                    addToken(token, "PALABRA RESERVADA");
                    break;

                default:
                    addToken(token, "Error Lexico");
                    break;
            }
        }

        //metodo para agregar al listado de los token
        public void addToken(String lexema, String idToken)
        {
            Token newToken = new Token(lexema,idToken);//creacion del token
            listadoToken.Add(newToken);//agreacion del token a listado
        }


        //metodo para Generar Reportes
        public void reporteToken()
        {
            TextWriter archivo;
            archivo = new StreamWriter("Reportetoken.html");
            archivo.WriteLine("<html>");
            archivo.WriteLine("<head><title>Reporte</title></head>");
            archivo.WriteLine("<body>");
            //archivo.WriteLine("<center>");
            archivo.WriteLine("<h1 ALIGN=LEFT >TOKENS</h1>");
            //archivo.WriteLine("</center>");
            //archivo.WriteLine("<center>");
            archivo.WriteLine("<table border=\"6\" bordercolor=\"blue\" ALIGN=LEFT >");
            archivo.WriteLine("<tr>");
            archivo.WriteLine("<td><strong>LEXEMA</strong></td>");
            archivo.WriteLine("<td><strong>IDTOKEN</strong></td>");
            archivo.WriteLine("<td><strong>FILA</strong></td>");
            archivo.WriteLine("<td><strong>COLUMNA</strong></td>");
            archivo.WriteLine("</tr>");
            for (int i = 0; i < listadoToken.Count; i++)
            {
                Token actual = listadoToken.ElementAt(i);
                archivo.WriteLine("<tr>");
                archivo.WriteLine("<td><strong>" + actual.getLexema() + "</strong></td>");
                archivo.WriteLine("<td><strong>" + actual.getIdToken() + "</strong></td>");
                archivo.WriteLine("</tr>");
            }
            archivo.WriteLine("</table>");
            archivo.Close();
        }

    }
}
