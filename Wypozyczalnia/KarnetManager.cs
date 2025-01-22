using System.Collections.Generic;

namespace WypozyczalniaSprzetu
{
    public class KarnetManager
    {
        private List<Karnet> ListaKarnetow { get; } = new List<Karnet>();

        public void DodajKarnet(Karnet karnet)
        {
            ListaKarnetow.Add(karnet);
        }

        public List<Karnet> GetKarnety()
        {
            return ListaKarnetow;
        }

        public List<Karnet> GetKarnetyPoKliencie(Klient klient)
        {
            return klient.HistoriaKarnetow;
        }
    }
}
