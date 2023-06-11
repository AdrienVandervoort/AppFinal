using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace AppFinal
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<Vol> listeVols;
        private ObservableCollection<string> heuresAtterrissage;
        private const string RegistryKeyPath = "Software\\WindowPositionApp";
        private const string LeftRegistryValue = "Left";
        private const string TopRegistryValue = "Top";

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ViewModel();

            listeVols = new ObservableCollection<Vol>();
            listViewForme.ItemsSource = listeVols;
            heuresAtterrissage = new ObservableCollection<string>();
            DeserializeData();
            RegistryKey key = Registry.CurrentUser.OpenSubKey(RegistryKeyPath);
            if (key != null)
            {
                double left = Convert.ToDouble(key.GetValue(LeftRegistryValue));
                double top = Convert.ToDouble(key.GetValue(TopRegistryValue));

                Left = left;
                Top = top;
            }
        }


        private void CbObjet_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbObjet.SelectedItem != null && cbObjet.SelectedItem is ComboBoxItem item)
            {
                string content = item.Content.ToString();

                switch (content)
                {
                    case "Avion":
                        txtBoxNom.Text = "Nom de l'avion";
                        break;

                    case "Hélicoptère":
                        txtBoxNom.Text = "Nom de l'hélicoptère";
                        break;
                }
            }
            else
            {
                txtBoxNom.Text = "";
            }
        }

        private void BtnDecollage_OnClick(object sender, RoutedEventArgs e)
        {
            if (cbObjet.SelectedItem is ComboBoxItem selectedObjetItem
                && cbPilote.SelectedItem is ComboBoxItem selectedPiloteItem
                && cbCopilote.SelectedItem is ComboBoxItem selectedCopiloteItem)
            {
                string selectedObjet = selectedObjetItem.Content.ToString();
                string selectedPilote = selectedPiloteItem.Content.ToString();
                string selectedCopilote = selectedCopiloteItem.Content.ToString();
                string nom = txtBoxNom.Text;

                Vol vol = new Vol
                {
                    Type = selectedObjet,
                    Pilote = selectedPilote,
                    Copilote = selectedCopilote,
                    Nom = nom,
                };

                listeVols.Add(vol);
                Console.WriteLine($"{vol.Type} décollé : {vol.Nom}");

                SerializeData();
            }
        }

        private void ListViewForme_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listViewForme.SelectedItem == null)
            {
                LabelFormInfos.Content = "AUCUN VOL SÉLECTIONNÉ !";
                TextBlockHeureAtterrissage.Text = "Heure d'atterrissage :";
            }
            else
            {
                if (listViewForme.SelectedItem is Vol vol)
                {
                    LabelFormInfos.Content = $"Pilote: {vol.Pilote}, Copilote: {vol.Copilote}, Aeronef: {vol.Nom}";
                }
                else
                {
                    LabelFormInfos.Content = "Aeronef inconnu";
                }
            }
        }

        private void BtnAtteri_OnClick(object sender, RoutedEventArgs e)
        {
            if (listViewForme.SelectedItem is Vol vol)
            {
                if (!string.IsNullOrEmpty(vol.NouveauNom))
                {
                    vol.Nom = vol.NouveauNom;
                }

                listeVols.Remove(vol);
                Console.WriteLine($"{vol.Type} atterri : {vol.Nom}");
                heuresAtterrissage.Add($"[{DateTime.Now.ToString("HH:mm:ss")}] {vol.Type} atterri : {vol.Nom}");
                TextBlockHeureAtterrissage.Text = string.Join(Environment.NewLine, heuresAtterrissage);
                MessageBox.Show("Aircraft landing.", "Atteri", MessageBoxButton.OK, MessageBoxImage.Information);

                SerializeData();
            }
        }


        private void SerializeData()
        {
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "data.json");

            var data = new DataModel
            {
                ListeVols = listeVols,
                HeuresAtterrissage = heuresAtterrissage
            };

            string json = JsonConvert.SerializeObject(data, Formatting.Indented);

            File.WriteAllText(filePath, json);
        }

        private void DeserializeData()
        {
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "data.json");

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);

                var data = JsonConvert.DeserializeObject<DataModel>(json);

                if (data != null)
                {
                    listeVols = data.ListeVols ?? new ObservableCollection<Vol>();
                    heuresAtterrissage = data.HeuresAtterrissage ?? new ObservableCollection<string>();
                }
            }
            else
            {
                listeVols = new ObservableCollection<Vol>();
                heuresAtterrissage = new ObservableCollection<string>();
                MessageBox.Show("Le fichier de données n'existe pas.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            listViewForme.ItemsSource = listeVols;
            TextBlockHeureAtterrissage.Text = string.Join(Environment.NewLine, heuresAtterrissage);
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(RegistryKeyPath);
            if (key != null)
            {
                key.SetValue(LeftRegistryValue, Left);
                key.SetValue(TopRegistryValue, Top);
            }
            SerializeData(); // Sérialiser les données avant de fermer l'application
            base.OnClosing(e);
        }
    }

    public class Vol
    {
        public string Type { get; set; }
        public string Pilote { get; set; }
        public string Copilote { get; set; }
        public string Nom { get; set; }
        public string NouveauNom { get; set; }
    }

    public class DataModel
    {
        public ObservableCollection<Vol> ListeVols { get; set; }
        public ObservableCollection<string> HeuresAtterrissage { get; set; }
    }


}
