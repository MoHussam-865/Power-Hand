using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Power_Hand.Commands;
using Power_Hand.Data;
using Power_Hand.Data.Repository.Other;
using Power_Hand.Interfaces;
using Power_Hand.Models;
using Prism.Events;

namespace Power_Hand.ViewModels
{
    class HomeVM : ViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IPeopleRepo _emploeeRepo;
        private INavigationService _navigationService;
        public INavigationService NavigationService
        {
            get => _navigationService;
            set
            {
                _navigationService = value;
                OnPropertyChanged(nameof(_navigationService));
            }
        }
        private string? _name;
        public string? Name 
        { 
            get => _name;
            set 
            {
                _name = value;
                OnPropertyChanged();
            }
        }
        
        private string? _password;
        public string? Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }


        public ICommand OnLoginCommand { get; set; }

        public HomeVM(INavigationService navigationService,IPeopleRepo emploeeRepo,IEventAggregator eventAggregator)
        {
            _navigationService = navigationService;
            _eventAggregator = eventAggregator;
            _emploeeRepo = emploeeRepo;
            OnLoginCommand = new RelayCommand<string?>((x)=> OnLoginClicked(_name, _password));
        }


        private void OnLoginClicked(string? userName, string? password)
        {
            // hash the input text
            // check if there is a user with the same hash and userName
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password) && password == "123")
            {
                Emploee? emploee = _emploeeRepo.GetEmploee(userName, password).Result;

                if (emploee != null)
                {
                    _eventAggregator.GetEvent<EmploeeShare>().Publish(emploee);
                    NavigationService.NavigateTo<CasherVM>();
                }
                else
                {
                    // display a message of wrong userName or password
                }
            }
        }


    }
}
