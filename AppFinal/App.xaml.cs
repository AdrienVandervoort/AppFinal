using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using static AppFinal.Personne;

namespace AppFinal
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Test de la classe Personne
            Personne personne = new Personne("John Doe", 30);
            Console.WriteLine(personne.ToString());

            // Test de la classe Pilote
            Copilote copilote = new Copilote("Jane Smith", 35, "789012");
            Console.WriteLine(copilote.ToString());

            // Test de la classe Copilote
            Pilote pilote = new Pilote("John Doe", 40, "123456");
            Console.WriteLine(pilote.ToString());

            Avion avion = new Avion("Boeing 747", "Boeing", 300, "123456");
            Console.WriteLine(avion.ToString());

            avion.Decoller();
            avion.Atterrir();

            Helicopter helicopter = new Helicopter("Modèle H1", "Fabricant A", 4, "123456789", 4);
            Console.WriteLine(helicopter.ToString());

            helicopter.Voler();
            helicopter.DecollerHelicopter();
            helicopter.AtterrirHelicopter();


            Console.ReadLine();
        }
    }
     
    public class DescriptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Vol vol)
            {
                string info = $"Pilote: {vol.Pilote}, Copilote: {vol.Copilote}, Aeronef: {vol.Nom}";
                return info;
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
