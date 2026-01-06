using System;
using System.Collections.ObjectModel;
using Prism.Mvvm;
using SDTaskRunner.Models;

namespace SDTaskRunner.ViewModels
{
    public class GenerationRequestListViewModel : BindableBase
    {
        private GenerationRequest selectedItem;

        public event Action<GenerationRequest> SelectedItemChanged;

        public ObservableCollection<GenerationRequest> Items { get; } = new ();

        public GenerationRequest SelectedItem
        {
            get => selectedItem;
            set
            {
                if (SetProperty(ref selectedItem, value))
                {
                    SelectedItemChanged?.Invoke(value);
                }
            }
        }
    }
}