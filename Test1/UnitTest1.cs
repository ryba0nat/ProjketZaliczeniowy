using WypozyczalniaSprzetu;
using WypozyczalniaSprzetu.Tests;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WypozyczalniaSprzetu;

using System.Collections.Generic;

namespace WypozyczalniaSprzetu.Tests
{
    public class TestowySprzetNarciarski : SprzetNarciarski
    {
        public TestowySprzetNarciarski(string rodzaj, int rozmiar, string stanTechniczny, bool czyWypozyczony)
            : base(rodzaj, rozmiar, stanTechniczny, czyWypozyczony)
        {
        }

        public override double CenaZaDzien()
        {
            return 50.0; // Testowa cena za dzieñ
        }
    }
}





namespace WypozyczenieTests
{
    [TestClass]
    public class WypozyczenieTests
    {
        private TestowySprzetNarciarski _testowySprzet;
        private Klient _testowyKlient;

        [TestInitialize]
        public void TestInitialize()
        {
            // Inicjalizacja testowego sprzêtu
            _testowySprzet = new TestowySprzetNarciarski(
                rodzaj: "Narty",
                rozmiar: 42,
                stanTechniczny: "Dobry",
                czyWypozyczony: false
            );

            // Inicjalizacja testowego klienta
            _testowyKlient = new Klient(
                imie: "Jan",
                nazwisko: "Kowalski",
                pesel: "12345678901"
            );
        }

        

        [TestMethod]
        public void ObliczKoszt_Test()
        {
            // Arrange
            var dataWypozyczenia = DateTime.Now.AddDays(-5); // 5 dni temu
            var dataZwrotu = DateTime.Now;
            var wypozyczenie = new Wypozyczenie(_testowySprzet, _testowyKlient, dataWypozyczenia)
            {
                DataZwrotu = dataZwrotu
            };

            // Act
            var koszt = wypozyczenie.ObliczKoszt();

            // Assert
            Assert.AreEqual(250.0, koszt); // 5 dni * 50 z³ = 250 z³
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ObliczKoszt_PowinnoRzucicWyjatekJesliBrakDatyZwrotu()
        {
            // Arrange
            var wypozyczenie = new Wypozyczenie(_testowySprzet, _testowyKlient, DateTime.Now);

            // Act
            wypozyczenie.ObliczKoszt(); // Powinien rzuciæ wyj¹tek Jesli Brak Daty Zwrotu
        }

        [TestMethod]
        public void KlientPowinienMiecDodaneWypozyczenieDoHistorii()
        {
            // Arrange
            var dataWypozyczenia = DateTime.Now;
            var wypozyczenie = new Wypozyczenie(_testowySprzet, _testowyKlient, dataWypozyczenia);

            // Act
            _testowyKlient.DodajWypozyczenie(wypozyczenie);

            // Assert
            Assert.AreEqual(1, _testowyKlient.HistoriaWypozyczen.Count);
            Assert.AreEqual(wypozyczenie, _testowyKlient.HistoriaWypozyczen[0]);
        }
    }



    namespace WypozyczalniaSprzetu.Tests
    {
        [TestClass]
        public class SprzetManagerTests
        {
            private SprzetManager sprzetManager;

            [TestInitialize]
            public void SetUp()
            {
                sprzetManager = new SprzetManager();
                sprzetManager.DodajSprzet(new Narty("Narty", 170, "Dobra", false));
                sprzetManager.DodajSprzet(new Narty("Narty", 180, "Dobra", false));
            }

            [TestMethod]
            [ExpectedException(typeof(NieprawidlowyIndeksException))]
            public void UsunSprzet_PodanoNieprawidlowyIndeks_RzucaWyjatek()
            {
                // Act
                sprzetManager.UsunSprzet(-1);
            }

            [TestMethod]
            public void UsunSprzet_UsuwaElement_ZmniejszaLiczbeElementow()
            {
                // Arrange
                int poczatkowaLiczbaElementow = sprzetManager.ListaSprzetu.Count;

                // Act
                sprzetManager.UsunSprzet(0);

                // Assert
                Assert.AreEqual(poczatkowaLiczbaElementow - 1, sprzetManager.ListaSprzetu.Count);
            }

            [TestMethod]
            public void UsunSprzet_PoprawnyIndeks_UsuwaWlasciwyElement()
            {
                // Arrange
                var sprzetDoUsuniecia = sprzetManager.ListaSprzetu[1];

                // Act
                sprzetManager.UsunSprzet(1);

                // Assert
                Assert.IsFalse(sprzetManager.ListaSprzetu.Contains(sprzetDoUsuniecia));
            }
        }
    }
}
