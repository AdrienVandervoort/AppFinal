using System;

namespace AppFinal;

public class Personne
{
    public string Nom { get; set; }
    public int Age { get; set; }

    public Personne(string nom, int age)
    {
        Nom = nom;
        Age = age;
    }

    public Personne(string nom) : this(nom, 0)
    {
    }

    public void AfficherNom()
    {
        Console.WriteLine("Nom : " + Nom);
    }

    public override string ToString()
    {
        return $"Nom : {Nom}, Age : {Age}";
    }
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
