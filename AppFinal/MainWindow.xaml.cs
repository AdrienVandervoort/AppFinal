using Newtonsoft.Json;
using System;
using System.IO;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace AppFinal
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<Vol> listeVols;
        private StringBuilder heuresAtterrissage;

        public MainWindow()
        {
            InitializeComponent();

            listeVols = new ObservableCollection<Vol>();
            listViewForme.ItemsSource = listeVols;
            heuresAtterrissage = new StringBuilder();
            DeserializeData();
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
                    Nom = nom
                };

                listeVols.Add(vol);
                Console.WriteLine($"{vol.Type} décollé : {vol.Nom}");
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
                listeVols.Remove(vol);
                Console.WriteLine($"{vol.Type} atterri : {vol.Nom}");
                heuresAtterrissage.AppendLine($"[{DateTime.Now.ToString("HH:mm:ss")}] {vol.Type} atterri : {vol.Nom}");
                TextBlockHeureAtterrissage.Text = heuresAtterrissage.ToString();
            }
        }
        private void SerializeData()
        {
            string jsonData = JsonConvert.SerializeObject(listeVols);
            File.WriteAllText("data.json", jsonData);
        }

        private void DeserializeData()
        {
            if (File.Exists("data.json"))
            {
                string jsonData = File.ReadAllText("data.json");
                listeVols = JsonConvert.DeserializeObject<ObservableCollection<Vol>>(jsonData);
                listViewForme.ItemsSource = listeVols;
            }
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
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
    }
}
