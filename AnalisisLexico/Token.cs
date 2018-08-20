using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalisisLexico
{
    class Token
    {

        //caracter de un token
        private String lexema;//
        private String idToken;//

        public Token(String lexema, String idToken)
        {
            this.lexema = lexema;
            this.idToken = idToken;
        }

        //metodos get y set
        public String getLexema()
        {
            return lexema;
        }

        public String getIdToken()
        {
            return idToken;
        }


        public void setLexema(String lexema)
        {
            this.lexema = lexema;
        }

        public void setIdToken(String idToken)
        {
            this.idToken = idToken;
        }

    }
}
