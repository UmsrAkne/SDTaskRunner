using System.Collections.ObjectModel;
using Prism.Mvvm;
using SDTaskRunner.Models;

namespace SDTaskRunner.ViewModels
{
    public class GenerationRequestListViewModel : BindableBase
    {
        private GenerationRequest selectedItem;

        public ObservableCollection<GenerationRequest> Items { get; } = new ();

        public GenerationRequest SelectedItem
        {
            get => selectedItem;
            set => SetProperty(ref selectedItem, value);
        }
    }
}