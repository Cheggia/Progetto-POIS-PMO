using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgettoPMO
{
    /*
     * La classe gara conterrà i dati:
     * -città in cui si svolge la gara
     * -anno della gara
     */
    public class Gara
    {
        public readonly string citta;
        public readonly int data;

        public Gara(string citta, int data)
        {
            this.citta = citta.ToUpper();
            this.data = data;
        }
    }
}
