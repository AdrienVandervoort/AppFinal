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
}
   