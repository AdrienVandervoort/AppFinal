using System;

namespace AppFinal
{
    public class Pilote : Personne
    {
        public string NumeroLicence { get; set; }

        public Pilote(string nom, int age, string numeroLicence) : base(nom, age)
        {
            NumeroLicence = numeroLicence;
        }

        public override string ToString()
        {
            return $"Nom : {Nom}, Age : {Age}, Numéro de licence : {NumeroLicence}";
        }
    }
}