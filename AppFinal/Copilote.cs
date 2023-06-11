using System;

namespace AppFinal
{
    public class Copilote : Personne
    {
        public string NumeroBadge { get; set; }

        public Copilote(string nom, int age, string numeroBadge) : base(nom, age)
        {
            NumeroBadge = numeroBadge;
        }

        public override string ToString()
        {
            return $"Nom : {Nom}, Age : {Age}, Numéro de badge : {NumeroBadge}";
        }
    }
}