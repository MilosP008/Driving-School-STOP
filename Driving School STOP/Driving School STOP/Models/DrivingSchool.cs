using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace DrivingSchoolSTOP.Models
{
    public class DrivingSchool
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Unique, MaxLength(20)]
        public string Name { get; set; }
        public int NumberOfVehicles { get; set; }
        public int NumberOfInstructors { get; set; }
    }
}
