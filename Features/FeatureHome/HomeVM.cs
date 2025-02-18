﻿using System.Diagnostics;
using System.Windows.Input;
using Power_Hand.Data.Other;
using Power_Hand.Data.Repository.Other;
using Power_Hand.Data.SharedData;
using Power_Hand.Features.FeatureApp;
using Power_Hand.Features.FeatureApp.FeatureCasher;
using Power_Hand.Interfaces;
using Power_Hand.Models;
using Prism.Events;

namespace Power_Hand.Features.FeatureHome
{
    class HomeVM : ViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IEmploeeRepo _emploeeRepo;
        private INavigationService _navigationService;
        public INavigationService NavigationService
        {
            get => _navigationService;
            set { _navigationService = value; OnPropertyChanged(nameof(_navigationService)); }
        }
        private string? _name;
        public string? Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }

        private string? _password;
        public string? Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(); }
        }


        public ICommand OnLoginCommand { get; set; }

        public HomeVM(INavigationService navigationService, IEmploeeRepo emploeeRepo, IEventAggregator eventAggregator)
        {
            _navigationService = navigationService;
            _eventAggregator = eventAggregator;
            _emploeeRepo = emploeeRepo;
            OnLoginCommand = new FunCommand(OnLoginClicked);
        }


        private void OnLoginClicked()
        {
            string? userName = _name;
            string? password = _password;
            Debug.WriteLine(userName + "  " + password);

            // hash the input text
            // check if there is a user with the same hash and userName
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
            {
                //password = password.GetHashCode().ToString();
                Emploee? emploee = _emploeeRepo.GetEmploee(userName, password);

                Debug.WriteLine("emploee" + emploee?.Name?.ToString());


                if (emploee != null)
                {
                    _eventAggregator.GetEvent<EmploeeShare>().Publish(emploee);
                    NavigationService.SetParentView<AppShellVM>();
                    NavigationService.NavigateTo<CasherVM>();
                    Clear();
                }
                else
                {
                    // display a message of wrong userName or password
                    Debug.WriteLine("wrong passwrd or user name");
                }
            }

        }


        private void Clear()
        {
            Name = "";
            Password = "";
        }

    }
}
