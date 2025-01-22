namespace WypozyczalniaSprzetu
{
    public class Snowboard : SprzetNarciarski
    {
        public string TypSnowboardu { get; set; }

        public Snowboard(string typSnowboardu, int rozmiar, string stanTechniczny, bool czyWypozyczony)
            : base("Snowboard", rozmiar, stanTechniczny, czyWypozyczony)
        {
            TypSnowboardu = typSnowboardu;
        }

        public override string Opis()
        {
            return base.Opis() + $", Typ: {TypSnowboardu}";
        }

        public override double CenaZaDzien()
        {
            return 60; // Przykładowa cena za dzień
        }
    }
}
