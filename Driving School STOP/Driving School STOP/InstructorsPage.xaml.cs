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
	public partial class InstructorsPage : ContentPage
	{
		public InstructorsPage ()
		{
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var instructors = Task.Run(async () => await App.Database.GetInstructorsAsync()).Result;

            foreach (var instructor in instructors)
            {
                Grid grid = new Grid()
                {
                    Margin = new Thickness(5, 0),
                    ColumnDefinitions = new ColumnDefinitionCollection()
                    {
                        new ColumnDefinition() { Width = GridLength.Star },
                        new ColumnDefinition() { Width = GridLength.Star }
                    }
                };
                grid.Children.Add(new Label() { Text = instructor.Name + " " + instructor.Surname }, 0, 0);
                grid.Children.Add(new Label() { Text = Task.Run(async () => await App.Database.GetDrivingSchoolAsync(instructor.DrivingSchoolId)).Result.Name }, 1, 0);
                Button deleteButton = new Button() { Text = "Delete", BackgroundColor = Color.Red };
                deleteButton.SetDynamicResource(StyleProperty, "buttonStyle");
                deleteButton.Clicked += OnDeleteClicked;
                grid.Children.Add(deleteButton, 3, 0);
                instructorsData.Add(new ViewCell() { View = grid });
                ViewCell viewCell = (ViewCell)grid.Parent;
                viewCell.Tapped += OnDetailsClicked;
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            
            for (int i = 0; i < Task.Run(async () => await App.Database.GetInstructorsAsync()).Result.Count; i++)
            {
                instructorsData.RemoveAt(1);
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
            string instructorFullName = (string)grid.Children.ElementAt<View>(0).GetValue(Label.TextProperty);

            await Navigation.PushAsync(new DetailsPage(Task.Run(async () => await App.Database.GetInstructorByFullNameAsync(instructorFullName)).Result));
        }

        async void OnDeleteClicked(object sender, EventArgs e)
        {
            Button deleteButton = (Button)sender;
            Grid grid = (Grid)deleteButton.Parent;
            string fullName = (string)grid.Children.ElementAt<View>(0).GetValue(Label.TextProperty);
            Instructor instructor = Task.Run(async () => await App.Database.GetInstructorByFullNameAsync(fullName)).Result;

            instructorsData.Remove((ViewCell)grid.Parent);
            await App.Database.DeleteInstructorAsync(instructor);
        }
    }
}