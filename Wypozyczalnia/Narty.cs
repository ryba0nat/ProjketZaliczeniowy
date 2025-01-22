namespace WypozyczalniaSprzetu
{
    public class Narty : SprzetNarciarski
    {
        public string TypNart { get; set; }

        public Narty(string typNart, int rozmiar, string stanTechniczny, bool czyWypozyczony)
            : base("Narty", rozmiar, stanTechniczny, czyWypozyczony)
        {
            TypNart = typNart;
        }

        public override string Opis()
        {
            return base.Opis() + $", Typ: {TypNart}";
        }

        public override double CenaZaDzien()
        {
            return 50; // Przykładowa cena za dzień
        }
    }
}
