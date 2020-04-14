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
	public partial class CategoriesPage : ContentPage
	{
		public CategoriesPage ()
		{
			InitializeComponent ();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            foreach (var category in Task.Run(async () => await App.Database.GetCategoriesAsync()).Result)
            {
                Grid grid = new Grid()
                {
                    Margin = new Thickness(5, 0),
                    ColumnDefinitions = new ColumnDefinitionCollection()
                    {
                        new ColumnDefinition() { Width = GridLength.Star },
                        new ColumnDefinition() { Width = GridLength.Star },
                        new ColumnDefinition() { Width = GridLength.Star },
                    }
                };
                grid.Children.Add(new Label() { Text = category.Name }, 0, 0);
                grid.Children.Add(new Label() { Text = category.Type }, 1, 0);
                Button deleteButton = new Button() { Text = "Delete", BackgroundColor = Color.Red };
                deleteButton.SetDynamicResource(StyleProperty, "buttonStyle");
                deleteButton.Clicked += OnDeleteClicked;
                grid.Children.Add(deleteButton, 2, 0);
                categoriesData.Add(new ViewCell() { View = grid });
                ViewCell viewCell = (ViewCell)grid.Parent;
                viewCell.Tapped += OnDetailsClicked;
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            for (int c = 0; c < Task.Run(async () => await App.Database.GetCategoriesAsync()).Result.Count; c++)
            {
                categoriesData.RemoveAt(1);
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
            string categoryName = (string)grid.Children.ElementAt<View>(0).GetValue(Label.TextProperty);

            await Navigation.PushAsync(new DetailsPage(Task.Run(async () => await App.Database.GetCategoryAsync(categoryName)).Result));
        }

        async void OnDeleteClicked(object sender, EventArgs e)
        {
            Button deleteButton = (Button)sender;
            Grid grid = (Grid)deleteButton.Parent;
            string name = (string)grid.Children.ElementAt<View>(0).GetValue(Label.TextProperty);
            Category category = Task.Run(async () => await App.Database.GetCategoryAsync(name)).Result;

            categoriesData.Remove((ViewCell)grid.Parent);
            await App.Database.DeleteCategoryAsync(category);
        }
    }
}