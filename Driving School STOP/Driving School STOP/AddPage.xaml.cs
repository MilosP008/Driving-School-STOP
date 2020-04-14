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
	public partial class AddPage : ContentPage
	{
        List<string> drivingSchoolNames = new List<string>();
        List<string> instructorFullNames = new List<string>();
        List<string> categoryNames = new List<string>();

        public AddPage ()
		{
			InitializeComponent ();

            Title = "Add";
		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var drivingSchools = await App.Database.GetDrivingSchoolsAsync();
            var instructors = await App.Database.GetInstructorsAsync();
            var categories = await App.Database.GetCategoriesAsync();

            foreach(var drivingSchool in drivingSchools)
            {
                drivingSchoolNames.Add(drivingSchool.Name);
            }

            foreach(var instructor in instructors)
            {
                instructorFullNames.Add(instructor.Name + " " + instructor.Surname);
            }

            foreach(var category in categories)
            {
                categoryNames.Add(string.Format("{0} ({1})", category.Name, category.Type));
            }
        }

        void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            
            for(int x = 1; x < add.Count;)
            {
                add.RemoveAt(x);
            }

            switch((string)picker.SelectedItem)
            {
                case "Driving school":
                    add.Add(new EntryCell() { Label = "Name:", HorizontalTextAlignment = TextAlignment.Center });
                    break;
                case "Instructor":
                    add.Add(new EntryCell() { Label = "JMBG:", HorizontalTextAlignment = TextAlignment.Center, Keyboard = Keyboard.Numeric });
                    add.Add(new EntryCell() { Label = "Name:", HorizontalTextAlignment = TextAlignment.Center });
                    add.Add(new EntryCell() { Label = "Surname:", HorizontalTextAlignment = TextAlignment.Center });
                    add.Add(new ViewCell() { View = new Picker() { Title = "Select driving school", ItemsSource = drivingSchoolNames } });

                    for (int i = 0; i < categoryNames.Count; i++) {
                        add.Add(new SwitchCell()
                        {
                            Text = categoryNames[i] + ":",
                            StyleId = categoryNames[i]
                        });
                    }
                    break;
                case "Vehicle":
                    add.Add(new EntryCell() { Label = "License plate number:", HorizontalTextAlignment = TextAlignment.Center });
                    add.Add(new EntryCell() { Label = "Type:", HorizontalTextAlignment = TextAlignment.Center });
                    add.Add(new EntryCell() { Label = "Model:", HorizontalTextAlignment = TextAlignment.Center });
                    add.Add(new ViewCell() { View = new Picker() { Title = "Select instructor", ItemsSource = instructorFullNames } });
                    add.Add(new ViewCell() { View = new Picker() { Title = "Select driving school", ItemsSource = drivingSchoolNames } });
                    break;
                case "Category":
                    add.Add(new ViewCell() { View = new Picker() { Title = "Select category", ItemsSource = new List<string>() { "A (Motorcycle)", "B (Car)", "C (Truck)", "D (Bus)", "E (Sidecar)", "F (Tractor)", "T (Trolleybus)" } } });
                    break;
            }

            var addButton = new Button() { Text = "Add", BackgroundColor = Color.Green, StyleId = (string)picker.SelectedItem };
            addButton.SetDynamicResource(StyleProperty, "buttonStyle");
            addButton.Clicked += AddButtonClicked;
            add.Add(new ViewCell() { View = addButton });
        }

        private async void AddButtonClicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            switch(button.StyleId)
            {
                case "Driving school":
                    string drivingSchoolName = (string)add.ElementAt<Cell>(1).GetValue(EntryCell.TextProperty);
                    DrivingSchool drivingSchool = new DrivingSchool() { Name = drivingSchoolName, NumberOfInstructors = 0, NumberOfVehicles = 0 };
                    await App.Database.AddDrivingSchoolAsync(drivingSchool);
                    break;
                case "Instructor":
                    string instructorJMBG = (string)add.ElementAt<Cell>(1).GetValue(EntryCell.TextProperty);
                    string instructorName = (string)add.ElementAt<Cell>(2).GetValue(EntryCell.TextProperty);
                    string instructorSurname = (string)add.ElementAt<Cell>(3).GetValue(EntryCell.TextProperty);
                    ViewCell instructorDrivingSchoolNameViewCell = (ViewCell)add.ElementAt<Cell>(4);
                    Picker instructorDrivingSchoolNamePicker = (Picker)instructorDrivingSchoolNameViewCell.View;
                    string instructorDrivingSchoolName = (string)instructorDrivingSchoolNamePicker.SelectedItem;
                    int instructorDrivingSchoolId = App.Database.GetDrivingSchoolByNameAsync(instructorDrivingSchoolName).Result.Id;
                    Instructor instructor = new Instructor() { JMBG = instructorJMBG, Name = instructorName, Surname = instructorSurname, DrivingSchoolId = instructorDrivingSchoolId };
                    await App.Database.AddInstructorAsync(instructor).ContinueWith(AddCategoryInstructorAsync);
                    break;
                case "Vehicle":
                    string licensePlateNumber = (string)add.ElementAt<Cell>(1).GetValue(EntryCell.TextProperty);
                    string type = (string)add.ElementAt<Cell>(2).GetValue(EntryCell.TextProperty);
                    string model = (string)add.ElementAt<Cell>(3).GetValue(EntryCell.TextProperty);
                    ViewCell vehicleInstructorFullNameViewCell = (ViewCell)add.ElementAt<Cell>(4);
                    Picker vehicleInstructorFullNamePicker = (Picker)vehicleInstructorFullNameViewCell.View;
                    string vehicleInstructorFullName = (string)vehicleInstructorFullNamePicker.SelectedItem;
                    int vehicleInstructorId = App.Database.GetInstructorByFullNameAsync(vehicleInstructorFullName).Result.Id;
                    ViewCell vehicleDrivingSchoolNameViewCell = (ViewCell)add.ElementAt<Cell>(5);
                    Picker vehicleDrivingSchoolNamePicker = (Picker)vehicleDrivingSchoolNameViewCell.View;
                    string vehicleDrivingSchoolName = (string)vehicleDrivingSchoolNamePicker.SelectedItem;
                    int vehicleDrivingSchoolId = App.Database.GetDrivingSchoolByNameAsync(vehicleDrivingSchoolName).Result.Id;
                    Vehicle vehicle = new Vehicle() { LicensePlateNumber = licensePlateNumber, Type = type, Model = model, InstructorId = vehicleInstructorId, DrivingSchoolId = vehicleDrivingSchoolId };
                    await App.Database.AddVehicleAsync(vehicle);
                    break;
                case "Category":
                    ViewCell categoryViewCell = (ViewCell)add.ElementAt<Cell>(1);
                    Picker categoryPicker = (Picker)categoryViewCell.View;
                    string categoryName = Convert.ToString(categoryPicker.SelectedItem).Split(' ')[0];
                    string categoryType = Convert.ToString(categoryPicker.SelectedItem).Split(' ')[1];
                    categoryType = categoryType.Substring(categoryType.IndexOf('(') + 1, categoryType.IndexOf(')') - 1);
                    Category category = new Category() { Name = categoryName, Type = categoryType };
                    await App.Database.AddCategoryAsync(category);
                    break;
            }
        }

        async void AddCategoryInstructorAsync(Task<int> taskData)
        {
            for (int c = 0; c < categoryNames.Count; c++)
            {
                bool instructorCategoryOn = (bool)add.ElementAt<Cell>(c + 5).GetValue(SwitchCell.OnProperty);
                if (instructorCategoryOn == true)
                {
                    string instructorCategoryNameAndType = (string)add.ElementAt<Cell>(c + 5).GetValue(SwitchCell.TextProperty);
                    string instructorCategoryName = instructorCategoryNameAndType.Split(' ')[0];
                    int instructorCategoryId = App.Database.GetCategoryAsync(instructorCategoryName).Id;
                    CategoryInstructor categoryInstructor = new CategoryInstructor() { CategoryId = instructorCategoryId, InstructorId = taskData.Result };
                    await App.Database.AddCategoryInstructorAsync(categoryInstructor);
                }
            }
        }
    }
}