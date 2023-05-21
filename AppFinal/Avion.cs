using System;

namespace AppFinal
{
    public class Avion
    {
        public string Modele { get; set; }
        public string Fabricant { get; set; }
        public int CapacitePassagers { get; set; }
        public string NumeroSerie { get; set; }
        public string? Pilote { get; set; }
        public string? Copilote { get; set; }

        public Avion(string modele, string fabricant, int capacitePassagers, string numeroSerie)
        {
            Modele = modele;
            Fabricant = fabricant;
            CapacitePassagers = capacitePassagers;
            NumeroSerie = numeroSerie;
        }

        public void Decoller()
        {
            Console.WriteLine($"L'avion de modèle {Modele} décolle.");
        }

        public void Atterrir()
        {
            Console.WriteLine($"L'avion de modèle {Modele} atterrit.");
        }

        public override string ToString()
        {
            return $"Modèle : {Modele}, Fabricant : {Fabricant}, Capacité passagers : {CapacitePassagers}, Numéro de série : {NumeroSerie}";
        }

    }
}