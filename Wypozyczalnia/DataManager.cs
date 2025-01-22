using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace WypozyczalniaSprzetu
{
    public static class DataManager
    {
        private static readonly string SprzetFilePath = "sprzet.xml";
        private static readonly string KarnetyFilePath = "karnety.xml";

        // 🔹 Zapisuje listę sprzętu do pliku XML
        public static void ZapiszSprzet(List<SprzetNarciarski> sprzet)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<SprzetNarciarski>), new Type[] { typeof(Narty), typeof(Snowboard) });
                using (StreamWriter writer = new StreamWriter(SprzetFilePath))
                {
                    serializer.Serialize(writer, sprzet);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd zapisu sprzętu: {ex.Message}");
            }
        }

        // 🔹 Wczytuje listę sprzętu z pliku XML
        public static List<SprzetNarciarski> WczytajSprzet()
        {
            try
            {
                if (!File.Exists(SprzetFilePath)) return new List<SprzetNarciarski>();

                XmlSerializer serializer = new XmlSerializer(typeof(List<SprzetNarciarski>), new Type[] { typeof(Narty), typeof(Snowboard) });
                using (StreamReader reader = new StreamReader(SprzetFilePath))
                {
                    return (List<SprzetNarciarski>)serializer.Deserialize(reader);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd odczytu sprzętu: {ex.Message}");
                return new List<SprzetNarciarski>();
            }
        }

        // 🔹 Zapisuje listę karnetów do pliku XML
        public static void ZapiszKarnety(List<Karnet> karnety)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Karnet>));
                using (StreamWriter writer = new StreamWriter(KarnetyFilePath))
                {
                    serializer.Serialize(writer, karnety);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd zapisu karnetów: {ex.Message}");
            }
        }

        // 🔹 Wczytuje listę karnetów z pliku XML
        public static List<Karnet> WczytajKarnety()
        {
            try
            {
                if (!File.Exists(KarnetyFilePath)) return new List<Karnet>();

                XmlSerializer serializer = new XmlSerializer(typeof(List<Karnet>));
                using (StreamReader reader = new StreamReader(KarnetyFilePath))
                {
                    return (List<Karnet>)serializer.Deserialize(reader);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd odczytu karnetów: {ex.Message}");
                return new List<Karnet>();
            }
        }
    }
}
