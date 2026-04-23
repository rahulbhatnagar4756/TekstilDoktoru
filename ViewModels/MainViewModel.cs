using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TekstilDoktoru.Models;

namespace TekstilDoktoru.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<PatternPiece> patternPieces = new();

        [RelayCommand]
        private void ImportDxf()
        {
            // DXF import logic here
        }
    }
}