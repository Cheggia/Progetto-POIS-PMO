using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgettoPMO
{
    /* La classe anagrafica conterrà i dati relativi all'arciere:
     * -Nome e Cognome (memorizzati su unica variabile)
     * -Compagnia di appartenenza (squadra)
     * -Categoria in cui gareggia (suddivisa in MESSERE, MADONNA, JUVENIS, PUERI)
     * -Tipo di arco con cui gareggia (suddiviso in STORICO, TRADIZIONALE, FOGGIA STORICA)
     */

    public class Anagrafica
    {
        public readonly string nomeCognome, compagnia, categoria, tipoArco;

        public Anagrafica(string nomeCognome, string compagnia, string categoria, string tipoArco)
        {
            this.nomeCognome = nomeCognome.ToUpper();
            this.compagnia = compagnia.ToUpper();
            this.categoria = categoria;
            this.tipoArco = tipoArco;
        }

    }
}
