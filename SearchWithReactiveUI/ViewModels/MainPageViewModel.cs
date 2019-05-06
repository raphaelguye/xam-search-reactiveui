﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SearchWithReactiveUI.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private readonly ObservableCollection<string> items;

        public MainPageViewModel()
        {
            items = new ObservableCollection<string> { "John", "Tom", "Alex", "Tomas", "Alexandre", "Johnatan", "Jonas", "Joel", "James" };

            ItemsDisplayed = items; // Initial value

            SearchCommand = new Command(async () => await SearchCommandExecuteAsync());
        }

        private string searchText;
        public string SearchText
        {
            get => searchText;
            set
            {
                searchText = value;
                OnPropertyChanged(nameof(SearchText));
            }
        }

        private ObservableCollection<string> itemsDisplayed;
        public ObservableCollection<string> ItemsDisplayed
        {
            get => itemsDisplayed;
            set
            {
                itemsDisplayed = value;
                OnPropertyChanged(nameof(ItemsDisplayed));
            }
        }

        public ICommand SearchCommand { get; private set; }

        private async Task SearchCommandExecuteAsync()
        {
            if(string.IsNullOrEmpty(SearchText))
            {
                ItemsDisplayed = items;
            }
            else
            {
                var lowerSearch = SearchText.ToLower();
                var listResult = items.Where(item => item.ToLower().Contains(lowerSearch)).ToList();

                await Task.Delay(1000); // Faking a time consuming query during the search

                ItemsDisplayed = new ObservableCollection<string>(listResult);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
