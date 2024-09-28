using MyDatabase.Repository.Settings;
using Power_Hand.Other.Other;
using System.Windows.Input;

namespace Power_Hand.Features.FeatureApp.FeatureSettings
{
    public class SettingsVM: ViewModel
    {
		private int _lastUpdate;
		public int LastUpdate
		{
			get { return _lastUpdate; }
			set { _lastUpdate = value; OnPropertyChanged(); }
		}

		private string _myLastUpdate;

		public string MyLastUpdate
		{
			get { return _myLastUpdate; }
			set { _myLastUpdate = value; OnPropertyChanged(); }
		}

		public ICommand ChangeLastUpdateCommand {  get; set; }	
		
		public SettingsVM()
		{
			_myLastUpdate = "";
			_lastUpdate = SettingsRepo.GetLastUpdate();
			ChangeLastUpdateCommand = new FunCommand(OnDatabaseChangeRequested);
        }

		private void OnDatabaseChangeRequested()
		{
			try
			{
				int lastUpdate = int.Parse(MyLastUpdate);
				LastUpdate = SettingsRepo.ChangeLastUpdate(lastUpdate);
				MyLastUpdate = "";
			}
			catch (Exception)
            {
                LastUpdate = SettingsRepo.ChangeLastUpdate();
            }

        }


    }
}
