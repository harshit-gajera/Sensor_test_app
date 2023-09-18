namespace Location_app_2
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void GetLocation_Clicked(object sender, EventArgs e)
        {
            try
            {
                var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

                if (status != PermissionStatus.Granted)
                {
                    status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();

                    if (status != PermissionStatus.Granted)
                    {
                        // Permission denied, handle accordingly
                        await DisplayAlert("Permission Denied", "Location permission is required to get your location.", "OK");
                        return;
                    }
                }

                // Set the desired duration for location retrieval
                TimeSpan duration = TimeSpan.FromHours(1) + TimeSpan.FromSeconds(30);

                // Get the start time for the retrieval
                DateTime startTime = DateTime.Now;

                Location location = null;
                int timer = 3600;
                // Keep retrieving location until the specified duration has passed
                while (DateTime.Now - startTime < duration)
                {
                    location = await Geolocation.GetLocationAsync(new GeolocationRequest
                    {
                        DesiredAccuracy = GeolocationAccuracy.Medium
                    });

                    if (location != null)
                    {
                        // Handle the location data here
                        double latitude = location.Latitude;
                        double longitude = location.Longitude;
                        // You can display or process the latitude and longitude as needed
                    }
                    else
                    {
                        // Handle if the location is null (e.g., timeout or error)
                    }

                    // Pause for a moment before the next location retrieval (optional)
                    //await Task.Delay(TimeSpan.FromSeconds(1)); // Adjust the delay as needed
                    timer = timer - 1;
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }
    }
}
