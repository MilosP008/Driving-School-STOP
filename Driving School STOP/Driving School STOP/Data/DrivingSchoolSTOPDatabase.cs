using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using DrivingSchoolSTOP.Models;
using System.Threading.Tasks;

namespace DrivingSchoolSTOP.Data
{
    public class DrivingSchoolSTOPDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public DrivingSchoolSTOPDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTablesAsync<Category, CategoryInstructor, DrivingSchool, Instructor, Vehicle>().Wait();
        }

        // Category
        public Task<List<Category>> GetCategoriesAsync()
        {
            return _database.Table<Category>().ToListAsync();
        }

        public Task<Category> GetCategoryAsync(string name)
        {
            return _database.Table<Category>().Where(c => c.Name == name).FirstOrDefaultAsync();
        }

        public Task<int> AddCategoryAsync(Category category)
        {
            return _database.InsertAsync(category);
        }

        public Task<int> DeleteCategoryAsync(Category category)
        {
            return _database.DeleteAsync(category);
        }

        // CategoryInstructor
        public Task<int> AddCategoryInstructorAsync(CategoryInstructor categoryInstructor)
        {
            return _database.InsertAsync(categoryInstructor);
        }

        // DrivingSchool
        public Task<List<DrivingSchool>> GetDrivingSchoolsAsync()
        {
            return _database.Table<DrivingSchool>().ToListAsync();
        }

        public Task<DrivingSchool> GetDrivingSchoolAsync(int id)
        {
            return _database.Table<DrivingSchool>().Where(d => d.Id == id).FirstOrDefaultAsync();
        }

        public Task<DrivingSchool> GetDrivingSchoolByNameAsync(string name)
        {
            return _database.Table<DrivingSchool>().Where(d => d.Name == name).FirstOrDefaultAsync();
        }

        public Task<int> AddDrivingSchoolAsync(DrivingSchool drivingSchool)
        {
            return _database.InsertAsync(drivingSchool);
        }

        public Task<int> DeleteDrivingSchoolAsync(DrivingSchool drivingSchool)
        {
            return _database.DeleteAsync(drivingSchool);
        }

        // Instructor
        public Task<List<Instructor>> GetInstructorsAsync()
        {
            return _database.Table<Instructor>().ToListAsync();
        }

        public Task<Instructor> GetInstructorAsync(int id)
        {
            return _database.Table<Instructor>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<Instructor> GetInstructorByFullNameAsync(string fullName)
        {
            string name = fullName.Split(' ')[0];
            string surname = fullName.Split(' ')[1];
            return _database.Table<Instructor>().Where(i => i.Name == name && i.Surname == surname).FirstOrDefaultAsync();
        }

        public Task<int> AddInstructorAsync(Instructor instructor)
        {
            return _database.InsertAsync(instructor);
        }

        public Task<int> DeleteInstructorAsync(Instructor instructor)
        {
            return _database.DeleteAsync(instructor);
        }

        // Vehicle
        public Task<List<Vehicle>> GetVehiclesAsync()
        {
            return _database.Table<Vehicle>().ToListAsync();
        }

        public Task<Vehicle> GetVehicleAsync(string licensePlateNumber)
        {
            return _database.Table<Vehicle>().Where(v => v.LicensePlateNumber == licensePlateNumber).FirstOrDefaultAsync();
        }

        public Task<int> AddVehicleAsync(Vehicle vehicle)
        {
            return _database.InsertAsync(vehicle);
        }

        public Task<int> DeleteVehicleAsync(Vehicle vehicle)
        {
            return _database.DeleteAsync(vehicle);
        }
    }
}
