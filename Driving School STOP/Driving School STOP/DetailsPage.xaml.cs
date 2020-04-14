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
	public partial class DetailsPage : ContentPage
	{
		public DetailsPage (object data)
		{
			InitializeComponent();

            switch(data.GetType().Name)
            {
                case "Category":
                    Category category = (Category)data;

                    Grid categoryGrid = new Grid()
                    {
                        Margin = new Thickness(20, 10),
                        RowDefinitions =
                        {
                            new RowDefinition() { Height = GridLength.Star },
                            new RowDefinition() { Height = GridLength.Auto },
                            new RowDefinition() { Height = GridLength.Star },
                            new RowDefinition() { Height = GridLength.Auto },
                            new RowDefinition() { Height = GridLength.Star }
                        }
                    };
                    FormattedString categoryIdFormattedString = new FormattedString();
                    categoryIdFormattedString.Spans.Add(new Span() { Text = "Id: ", FontAttributes = FontAttributes.Bold, FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Span)) });
                    categoryIdFormattedString.Spans.Add(new Span() { Text = category.Id.ToString() });
                    categoryGrid.Children.Add(new Label() { FormattedText = categoryIdFormattedString, FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)) }, 0, 0);

                    categoryGrid.Children.Add(new BoxView() { BackgroundColor = Color.Blue, HeightRequest = 2 }, 0, 1);

                    FormattedString categoryNameFormattedString = new FormattedString();
                    categoryNameFormattedString.Spans.Add(new Span() { Text = "Name: ", FontAttributes = FontAttributes.Bold, FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Span)) });
                    categoryNameFormattedString.Spans.Add(new Span() { Text = category.Name });
                    categoryGrid.Children.Add(new Label() { FormattedText = categoryNameFormattedString, FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)) }, 0, 2);

                    categoryGrid.Children.Add(new BoxView() { BackgroundColor = Color.Blue, HeightRequest = 2 }, 0, 3);

                    FormattedString categoryTypeFormattedString = new FormattedString();
                    categoryTypeFormattedString.Spans.Add(new Span() { Text = "Type: ", FontAttributes = FontAttributes.Bold, FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Span)) });
                    categoryTypeFormattedString.Spans.Add(new Span() { Text = category.Type });
                    categoryGrid.Children.Add(new Label() { FormattedText = categoryTypeFormattedString, FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)) }, 0, 4);
                    details.Children.Add(categoryGrid);
                    break;
                case "Instructor":
                    Instructor instructor = (Instructor)data;

                    Grid instructorGrid = new Grid()
                    {
                        Margin = new Thickness(20, 10),
                        RowDefinitions =
                        {
                            new RowDefinition() { Height = GridLength.Star },
                            new RowDefinition() { Height = GridLength.Auto },
                            new RowDefinition() { Height = GridLength.Star },
                            new RowDefinition() { Height = GridLength.Auto },
                            new RowDefinition() { Height = GridLength.Star },
                            new RowDefinition() { Height = GridLength.Auto },
                            new RowDefinition() { Height = GridLength.Star },
                            new RowDefinition() { Height = GridLength.Auto },
                            new RowDefinition() { Height = GridLength.Star }
                        }
                    };
                    FormattedString instructorIdFormattedString = new FormattedString();
                    instructorIdFormattedString.Spans.Add(new Span() { Text = "Id: ", FontAttributes = FontAttributes.Bold, FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Span)) });
                    instructorIdFormattedString.Spans.Add(new Span() { Text = instructor.Id.ToString() });
                    instructorGrid.Children.Add(new Label() { FormattedText = instructorIdFormattedString, FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)) }, 0, 0);

                    instructorGrid.Children.Add(new BoxView() { BackgroundColor = Color.Blue, HeightRequest = 2 }, 0, 1);

                    FormattedString instructorJMBGFormattedString = new FormattedString();
                    instructorJMBGFormattedString.Spans.Add(new Span() { Text = "JMBG: ", FontAttributes = FontAttributes.Bold, FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Span)) });
                    instructorJMBGFormattedString.Spans.Add(new Span() { Text = instructor.JMBG });
                    instructorGrid.Children.Add(new Label() { FormattedText = instructorJMBGFormattedString, FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)) }, 0, 2);

                    instructorGrid.Children.Add(new BoxView() { BackgroundColor = Color.Blue, HeightRequest = 2 }, 0, 3);

                    FormattedString instructorNameFormattedString = new FormattedString();
                    instructorNameFormattedString.Spans.Add(new Span() { Text = "Name: ", FontAttributes = FontAttributes.Bold, FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Span)) });
                    instructorNameFormattedString.Spans.Add(new Span() { Text = instructor.Name });
                    instructorGrid.Children.Add(new Label() { FormattedText = instructorNameFormattedString, FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)) }, 0, 4);

                    instructorGrid.Children.Add(new BoxView() { BackgroundColor = Color.Blue, HeightRequest = 2 }, 0, 5);

                    FormattedString instructorSurnameFormattedString = new FormattedString();
                    instructorSurnameFormattedString.Spans.Add(new Span() { Text = "Surname: ", FontAttributes = FontAttributes.Bold, FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Span)) });
                    instructorSurnameFormattedString.Spans.Add(new Span() { Text = instructor.Surname });
                    instructorGrid.Children.Add(new Label() { FormattedText = instructorSurnameFormattedString, FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)) }, 0, 6);

                    instructorGrid.Children.Add(new BoxView() { BackgroundColor = Color.Blue, HeightRequest = 2 }, 0, 7);

                    FormattedString instructorDrivingSchoolFormattedString = new FormattedString();
                    instructorDrivingSchoolFormattedString.Spans.Add(new Span() { Text = "Driving school: ", FontAttributes = FontAttributes.Bold, FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Span)) });
                    instructorDrivingSchoolFormattedString.Spans.Add(new Span() { Text = Task.Run(async () => await App.Database.GetDrivingSchoolAsync(instructor.DrivingSchoolId)).Result.Name });
                    instructorGrid.Children.Add(new Label() { FormattedText = instructorDrivingSchoolFormattedString, FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)) }, 0, 8);
                    details.Children.Add(instructorGrid);
                    break;
                case "Vehicle":
                    Vehicle vehicle = (Vehicle)data;
                    Instructor instructorOfTheVehicle = Task.Run(async () => await App.Database.GetInstructorAsync(vehicle.InstructorId)).Result;

                    Grid vehicleGrid = new Grid()
                    {
                        Margin = new Thickness(20, 10),
                        RowDefinitions =
                        {
                            new RowDefinition() { Height = GridLength.Star },
                            new RowDefinition() { Height = GridLength.Auto },
                            new RowDefinition() { Height = GridLength.Star },
                            new RowDefinition() { Height = GridLength.Auto },
                            new RowDefinition() { Height = GridLength.Star },
                            new RowDefinition() { Height = GridLength.Auto },
                            new RowDefinition() { Height = GridLength.Star },
                            new RowDefinition() { Height = GridLength.Auto },
                            new RowDefinition() { Height = GridLength.Star },
                            new RowDefinition() { Height = GridLength.Auto },
                            new RowDefinition() { Height = GridLength.Star }
                        }
                    };
                    FormattedString vehicleIdFormattedString = new FormattedString();
                    vehicleIdFormattedString.Spans.Add(new Span() { Text = "Id: ", FontAttributes = FontAttributes.Bold, FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Span)) });
                    vehicleIdFormattedString.Spans.Add(new Span() { Text = vehicle.Id.ToString() });
                    vehicleGrid.Children.Add(new Label() { FormattedText = vehicleIdFormattedString, FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)) }, 0, 0);

                    vehicleGrid.Children.Add(new BoxView() { BackgroundColor = Color.Blue, HeightRequest = 2 }, 0, 1);

                    FormattedString vehicleLicensePlateNumberFormattedString = new FormattedString();
                    vehicleLicensePlateNumberFormattedString.Spans.Add(new Span() { Text = "License plate number: ", FontAttributes = FontAttributes.Bold, FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Span)) });
                    vehicleLicensePlateNumberFormattedString.Spans.Add(new Span() { Text = vehicle.LicensePlateNumber });
                    vehicleGrid.Children.Add(new Label() { FormattedText = vehicleLicensePlateNumberFormattedString, FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)) }, 0, 2);

                    vehicleGrid.Children.Add(new BoxView() { BackgroundColor = Color.Blue, HeightRequest = 2 }, 0, 3);

                    FormattedString vehicleTypeFormattedString = new FormattedString();
                    vehicleTypeFormattedString.Spans.Add(new Span() { Text = "Type: ", FontAttributes = FontAttributes.Bold, FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Span)) });
                    vehicleTypeFormattedString.Spans.Add(new Span() { Text = vehicle.Type });
                    vehicleGrid.Children.Add(new Label() { FormattedText = vehicleTypeFormattedString, FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)) }, 0, 4);

                    vehicleGrid.Children.Add(new BoxView() { BackgroundColor = Color.Blue, HeightRequest = 2 }, 0, 5);

                    FormattedString vehicleModelFormattedString = new FormattedString();
                    vehicleModelFormattedString.Spans.Add(new Span() { Text = "Model: ", FontAttributes = FontAttributes.Bold, FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Span)) });
                    vehicleModelFormattedString.Spans.Add(new Span() { Text = vehicle.Model });
                    vehicleGrid.Children.Add(new Label() { FormattedText = vehicleModelFormattedString, FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)) }, 0, 6);

                    vehicleGrid.Children.Add(new BoxView() { BackgroundColor = Color.Blue, HeightRequest = 2 }, 0, 7);

                    FormattedString vehicleDrivingSchoolFormattedString = new FormattedString();
                    vehicleDrivingSchoolFormattedString.Spans.Add(new Span() { Text = "Driving school: ", FontAttributes = FontAttributes.Bold, FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Span)) });
                    vehicleDrivingSchoolFormattedString.Spans.Add(new Span() { Text = Task.Run(async () => await App.Database.GetDrivingSchoolAsync(vehicle.DrivingSchoolId)).Result.Name });
                    vehicleGrid.Children.Add(new Label() { FormattedText = vehicleDrivingSchoolFormattedString, FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)) }, 0, 8);

                    vehicleGrid.Children.Add(new BoxView() { BackgroundColor = Color.Blue, HeightRequest = 2 }, 0, 9);

                    FormattedString vehicleInstructorFormattedString = new FormattedString();
                    vehicleInstructorFormattedString.Spans.Add(new Span() { Text = "Instructor: ", FontAttributes = FontAttributes.Bold, FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Span)) });
                    vehicleInstructorFormattedString.Spans.Add(new Span() { Text = instructorOfTheVehicle.Name + ' ' + instructorOfTheVehicle.Surname });
                    vehicleGrid.Children.Add(new Label() { FormattedText = vehicleInstructorFormattedString, FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),  }, 0, 10);
                    details.Children.Add(vehicleGrid);
                    break;
            }
		}
	}
}