

namespace MyDatabase.Repository.Settings
{
    public class SettingsRepo
    {
        public static int GetLastUpdate()
        {
            /*int lastUpdate = Proper.Default.LastUpdate;
            if (lastUpdate == 0)
            {
                Properties.Settings.Default.LastUpdate = 1;
                Properties.Settings.Default.Save();
                return 1;
            }*/
            return 0; // lastUpdate;
        }

        public static int ChangeLastUpdate(int lastUpdate = 0)
        {
            /*if (lastUpdate == 0) lastUpdate = GetLastUpdate() + 1;

            Properties.Settings.Default.LastUpdate = lastUpdate;
            Properties.Settings.Default.Save();*/
            return 0; // lastUpdate;
        }
    }
}
