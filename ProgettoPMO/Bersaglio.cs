using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgettoPMO
{
    /* La classe Bersaglio conterrà i dati relativi ad un bersaglio presente sul form.
     * Gli attributi sono pubblici in quanto occorrerà accedere singolarmente ai valori per poter eseguire le query di inserimento e ricerca su database.
     * I metodi consentono di calcolare in automatico il punteggio totale relativo al bersaglio ed il numero totale di spot del bersaglio.
     * 
     * Ogni bersaglio è composto da: 
     * -punteggio delle tre frecce scagliate ed il punteggio totale
     * -numero di spot
     * -denominazione (speciale, libero o tradizionale
     * -numero bersaglio in gara (da 1 a 20)
     * 
     * I punteggi assegnabili ad ogni singola freccia sono: 
     * ---bersagli tradizionali---
     * -8 punti (spot)
     * -5 punti
     * -2 punti
     * -0 punti
     * 
     * ---bersagli speciali o liberi---
     * -qualsiasi punteggio purchè il totale non superi 24 punti
     * -speciali e liberi non assegnano spot. 
     */
    public class Bersaglio
    {
        public int punteggioFreccia1 = 0, punteggioFreccia2 = 0, punteggioFreccia3 = 0;
        public int puntiTotali = 0, spot = 0, numeroBersaglio = 0;
        public int specialeLibero = 0;

        public Bersaglio(int numeroBersaglioInGara, int freccia1, int freccia2, int freccia3, int specialeOrLibero)
        {
            punteggioFreccia1 = freccia1;
            punteggioFreccia2 = freccia2;
            punteggioFreccia3 = freccia3;
            specialeLibero = specialeOrLibero;
            numeroBersaglio = numeroBersaglioInGara;
            puntiTotali = PunteggioTotaleBersaglio();
            spot = SpotTotaliBersaglio();
        }

        private int PunteggioTotaleBersaglio()
        {
            return punteggioFreccia1 + punteggioFreccia2 + punteggioFreccia3;
        }

        private int SpotTotaliBersaglio()
        {
            int somma = 0;
            if (specialeLibero == 0)
            {
                if (punteggioFreccia1 == 8)
                    somma++;
                if (punteggioFreccia2 == 8)
                    somma++;
                if (punteggioFreccia3 == 8)
                    somma++;
            }
            return somma;
        }
    }
}
