using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrivingSchoolSTOP.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DrivingSchoolSTOP
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class VehiclesPage : ContentPage
	{
		public VehiclesPage ()
		{
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();

            foreach(var vehicle in Task.Run(async () => await App.Database.GetVehiclesAsync()).Result)
            {
                Grid grid = new Grid()
                {
                    Margin = new Thickness(5, 0),
                    ColumnDefinitions = new ColumnDefinitionCollection()
                    {
                        new ColumnDefinition() { Width = GridLength.Star },
                        new ColumnDefinition() { Width = GridLength.Star },
                        new ColumnDefinition() { Width = GridLength.Star },
                        new ColumnDefinition() { Width = GridLength.Star }
                    }
                };
                grid.Children.Add(new Label() { Text = vehicle.LicensePlateNumber }, 0, 0);
                grid.Children.Add(new Label() { Text = vehicle.Type }, 2, 0);
                Button deleteButton = new Button() { Text = "Delete", BackgroundColor = Color.Red };
                deleteButton.SetDynamicResource(StyleProperty, "buttonStyle");
                deleteButton.Clicked += OnDeleteClicked;
                grid.Children.Add(deleteButton, 3, 0);
                vehiclesData.Add(new ViewCell() { View = grid });
                ViewCell viewCell = (ViewCell)grid.Parent;
                viewCell.Tapped += OnDetailsClicked;
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            for(int v = 0; v < Task.Run(async () => await App.Database.GetVehiclesAsync()).Result.Count; v++)
            {
                vehiclesData.RemoveAt(1);
            }
        }

        async void OnAddClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddPage());
        }

        async void OnDetailsClicked(object sender, EventArgs e)
        {
            ViewCell viewCell = (ViewCell)sender;
            Grid grid = (Grid)viewCell.View;
            string vehicleLicensePlateNumber = (string)grid.Children.ElementAt<View>(0).GetValue(Label.TextProperty);

            await Navigation.PushAsync(new DetailsPage(Task.Run(async () => await App.Database.GetVehicleAsync(vehicleLicensePlateNumber)).Result));
        }

        async void OnDeleteClicked(object sender, EventArgs e)
        {
            Button deleteButton = (Button)sender;
            Grid grid = (Grid)deleteButton.Parent;
            string licensePlateNumber = (string)grid.Children.ElementAt<View>(0).GetValue(Label.TextProperty);
            Vehicle vehicle = Task.Run(async () => await App.Database.GetVehicleAsync(licensePlateNumber)).Result;

            vehiclesData.Remove((ViewCell)grid.Parent);
            await App.Database.DeleteVehicleAsync(vehicle);
        }
    }
}