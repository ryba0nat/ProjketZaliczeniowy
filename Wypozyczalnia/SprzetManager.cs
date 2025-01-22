using WypozyczalniaSprzetu;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
namespace WypozyczalniaSprzetu
{
    public class SprzetManager
    {
        private static readonly string SciezkaPliku = "sprzet.xml";// zmiana !!!
        public List<SprzetNarciarski> ListaSprzetu { get; } = new List<SprzetNarciarski>();

        public void DodajSprzet(SprzetNarciarski sprzet)
        {
            ListaSprzetu.Add(sprzet);
            //ZapiszSprzet(); // zmiana
        }

        

        public void UsunSprzet(int indeks)
        {
            if (indeks < 0 || indeks >= ListaSprzetu.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(indeks), "Nieprawidłowy indeks.");
            }

            ListaSprzetu.RemoveAt(indeks);
        }

        public List<SprzetNarciarski> FiltrujDostepny()
        {
            return ListaSprzetu.FindAll(sprzet => !sprzet.CzyWypozyczony);
        }

        public string PobierzStanSprzetu(SprzetNarciarski sprzet)
        {
            return $"Stan: {sprzet.StanTechniczny}, Wypożyczony: {(sprzet.CzyWypozyczony ? "Tak" : "Nie")}";
        }

       
        public List<SprzetNarciarski> GetSprzet()
        {
            return ListaSprzetu;
        }

        public void AktualizujStanSprzetu(SprzetNarciarski sprzet, string nowyStan)
        {
            sprzet.StanTechniczny = nowyStan;
        }

        public SprzetNarciarski ZnajdzSprzetPoID(int id)
        {
            return ListaSprzetu.Find(s => s.ID == id);
        }







        // 🔹 NOWA METODA: Zapis i odczyt sprzętu z XML


        //public void ZapiszSprzet()
        //{
        //    XmlSerializer serializer = new XmlSerializer(typeof(List<SprzetNarciarski>));
        //    using (StreamWriter writer = new StreamWriter(SciezkaPliku))
        //    {
        //        serializer.Serialize(writer, ListaSprzetu);
        //    }
        //}


        //public void WczytajSprzet()
        //{
        //    if (!File.Exists(SciezkaPliku)) return;

        //    XmlSerializer serializer = new XmlSerializer(typeof(List<SprzetNarciarski>));
        //    using (StreamReader reader = new StreamReader(SciezkaPliku))
        //    {
        //        List<SprzetNarciarski> wczytanaLista = (List<SprzetNarciarski>)serializer.Deserialize(reader);
        //        ListaSprzetu.Clear();
        //        ListaSprzetu.AddRange(wczytanaLista);
        //    }
        //}
    }
}
