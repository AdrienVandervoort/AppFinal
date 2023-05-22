using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AppFinal
{
    public class ViewModel : INotifyPropertyChanged
    {
        private string _selectedObjectType;
        private string _selectedPilot;
        private string _selectedCopilot;
        private string _aircraftName;

        public string SelectedObjectType
        {
            get { return _selectedObjectType; }
            set
            {
                _selectedObjectType = value;
                OnPropertyChanged();
                UpdateAircraftName();
            }
        }

        public string SelectedPilot
        {
            get { return _selectedPilot; }
            set
            {
                _selectedPilot = value;
                OnPropertyChanged();
            }
        }

        public string SelectedCopilot
        {
            get { return _selectedCopilot; }
            set
            {
                _selectedCopilot = value;
                OnPropertyChanged();
            }
        }

        public string AircraftName
        {
            get { return _aircraftName; }
            set
            {
                _aircraftName = value;
                OnPropertyChanged();
            }
        }

        private void UpdateAircraftName()
        {
            switch (SelectedObjectType)
            {
                case "Avion":
                    AircraftName = "Nom de l'avion";
                    break;
                case "Hélicoptère":
                    AircraftName = "Nom de l'hélicoptère";
                    break;
                default:
                    AircraftName = string.Empty;
                    break;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
