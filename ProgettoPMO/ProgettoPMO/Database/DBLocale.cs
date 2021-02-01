using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ProgettoPMO
{
    // La classe DBLocale gestirà la connesione con il database MySql creato con XAMPP e le varie operazioni ad esso collegato.

    public class DBLocale
    {
        private readonly List<Bersaglio> lBersagli = new List<Bersaglio>();
        private Bersaglio bersaglio = null;

        // Il metodo tenta la connessione con il database
        private MySqlConnection Connesso()
        {
            MySqlConnection connessione = new MySqlConnection("server=localhost;user=root;password=;database=risultati");
            try
            {
                connessione.Open();
            }
            catch (MySqlException e)
            {
                Console.WriteLine("ERRORE DI CONNESSIONE AL DATABASE \n" + e);
            }
            return connessione;
        }

        // Il metodo si connette al database per selezionare i bersagli di un noto arciere in una nota data ed una nota città
        // I 20 bersagli, se trovati, vengono inseriti in una lista
        public List<Bersaglio> QueryLetturaBersagli(string nome, int data, string citta)
        {
            using (var connessione = Connesso())
            {
                string query = $"select Numero, Freccia1, Freccia2, Freccia3, SpecialeLibero from bersagli" +
                    $" where (DataGara ='{data}' and NomeCognome ='{nome}' and LuogoGara ='{citta}') order by Numero asc;";
                
                MySqlCommand comando = new MySqlCommand(query, connessione);
                MySqlDataReader legge = comando.ExecuteReader();

                while (legge.Read())
                {
                    bersaglio = new Bersaglio(legge.GetInt32(0), legge.GetInt32(1), legge.GetInt32(2), legge.GetInt32(3), legge.GetInt32(4));
                    lBersagli.Add(bersaglio);
                }
                connessione.Close();
                return lBersagli;
            }
        }

        // Il metodo si connette al database per selezionare l'anagrafica di un noto arciere
        // I dati, se trovati, vengono inseriti in un oggetto di tipo Anagrafica
        public Anagrafica QueryAnagrafica (string nome)
        {
            using (var connessione = Connesso())
            {
                string queryAn = $"select * from anagrafica where NomeCognome ='{nome}';";

                MySqlCommand cmd = new MySqlCommand(queryAn, connessione);
                MySqlDataReader read = cmd.ExecuteReader();

                Anagrafica a = null;

                while (read.Read())
                {
                    a = new Anagrafica(read.GetString(0), read.GetString(1), read.GetString(2), read.GetString(3));
                }
                connessione.Close();
                return a;
            }
        }

        // Il metodo si connette al database per controllare l'esistenza di un noto arciere in una nota tabella
        // Se viene trovato l'arciere, il nome dell'arcierre viene rispedito alla funzione invocante
        public string QueryControlloNome (string nome, string tabella)
        {
            using (var connessione = Connesso())
            {
                string query = $"select NomeCognome from {tabella} where NomeCognome = '{nome}'; ";
                string nomeCognome = "";
                MySqlCommand comando = new MySqlCommand(query, connessione);
                MySqlDataReader lettore = comando.ExecuteReader();
                
                while (lettore.Read())
                {
                    nomeCognome = lettore.GetString(0);
                } 
                
                connessione.Close();
                return nomeCognome;
            }
        }

        // Il metodo si connette al database per memorizzare l'anagrafica di un nuovo arciere
        public void QueryInserimentoAnagrafica(Anagrafica anagrafica)
        {
            using (var connessione = Connesso())
            {
                string query = $"insert into anagrafica (NomeCognome, Compagnia, Categoria, TipoArco)" +
                    $"values ('{anagrafica.nomeCognome}', '{anagrafica.compagnia}', '{anagrafica.categoria}', '{anagrafica.tipoArco}'); ";

                MySqlCommand comando = new MySqlCommand(query, connessione);
                comando.ExecuteNonQuery();

                connessione.Close();
            }
        }

        // Il metodo si connette al database per memorizzare i nuovi dati di gara e bersagli di un noto arciere
        public void QueryInserimentoBersagli(List<Bersaglio> bersagli, Anagrafica anagrafica, Gara gara)
        {
            using (var connessione = Connesso())
            {
                int i = 0;
                Bersaglio singolo;
                foreach (var b in bersagli)
                {
                    singolo = bersagli[i];
                    string queryBersagli = $"insert into bersagli (NomeCognome, LuogoGara, DataGara, Numero, Freccia1, Freccia2, Freccia3, SpecialeLibero) values" +
                        $" ('{anagrafica.nomeCognome}','{gara.citta}','{gara.data}','{singolo.numeroBersaglio}','{singolo.punteggioFreccia1}'," +
                        $"'{singolo.punteggioFreccia2}','{singolo.punteggioFreccia3}','{singolo.specialeLibero}');";
                    MySqlCommand inserimentoBersaglio = new MySqlCommand(queryBersagli,connessione);
                    inserimentoBersaglio.ExecuteNonQuery();
                    i++;
                }
                connessione.Close();
            }
        }

        // Il metodo si connette al database per rimuovere l'anagrafica di un noto arciere
        public void QueryCancellazioneAnagrafica(string nome)
        {
            using (var connessione = Connesso())
            {
                string queryCancellazioneAnagrafica = $"delete from anagrafica where NomeCognome = '{nome}';";

                MySqlCommand cancellazioneAnagrafica = new MySqlCommand(queryCancellazioneAnagrafica, connessione);
                cancellazioneAnagrafica.ExecuteNonQuery();
        
                connessione.Close();
            }
        }

        // Il metodo si connette al database per rimuovere i dati di gara e bersagli di un noto arciere
        // righeCancellate viene restituito per notificare il cancellamento dal database
        public int QueryCancellazioneBersagli(string nome, string citta, int anno)
        {
            
            using (var connessione = Connesso())
            {
                int righeCancellate;
                string queryCancellazioneBersagli = $"delete from bersagli where NomeCognome = '{nome}' and LuogoGara = '{citta}' and DataGara = '{anno}';";

                MySqlCommand cancellazioneBersagli = new MySqlCommand(queryCancellazioneBersagli, connessione);
                righeCancellate = cancellazioneBersagli.ExecuteNonQuery();

                connessione.Close();
                return righeCancellate;
            }
        }
    }
}
