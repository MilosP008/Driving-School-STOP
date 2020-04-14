using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DrivingSchoolSTOP.Data;
using System.IO;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace DrivingSchoolSTOP
{
    public partial class App : Application
    {
        static DrivingSchoolSTOPDatabase database;

        public static DrivingSchoolSTOPDatabase Database
        {
            get
            {
                if(database == null)
                {
                    database = new DrivingSchoolSTOPDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DrivingSchoolSTOP.db3"));
                }

                return database;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
