using System;

namespace AppFinal;
public class Helicopter : Avion
    {
        public int NombreRotors { get; set; }

        public Helicopter(string modele, string fabricant, int capacitePassagers, string numeroSerie, int nombreRotors)
            : base(modele, fabricant, capacitePassagers, numeroSerie)
        {
            NombreRotors = nombreRotors;
        }

        public void Voler()
        {
            Console.WriteLine($"L'hélicoptère {Modele} vole avec {NombreRotors} rotors.");
        }

        public override string ToString()
        {
            return $"Modèle : {Modele}, Fabricant : {Fabricant}, Capacité : {CapacitePassagers} passagers, Numéro de série : {NumeroSerie}, Nombre de rotors : {NombreRotors}";
        }
        public void DecollerHelicopter()
        {
            Console.WriteLine($"L'hélicoptère {Modele} a décollé avec {NombreRotors} rotors.");
        }

        public void AtterrirHelicopter()
        {
            Console.WriteLine($"L'hélicoptère {Modele} a atterri avec {NombreRotors} rotors.");
        }

}
