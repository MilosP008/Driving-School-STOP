using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace DrivingSchoolSTOP.Models
{
    public class Vehicle
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Unique, MaxLength(7)]
        public string LicensePlateNumber { get; set; }
        [MaxLength(20)]
        public string Type { get; set; }
        [MaxLength(20)]
        public string Model { get; set; }
        // Foreign Key
        [Indexed]
        public int InstructorId { get; set; }
        [Indexed]
        public int DrivingSchoolId { get; set; }
    }
}
