using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLitePCL;

namespace DrivingSchoolSTOP.Models
{
    public class Instructor
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Unique, MaxLength(13)]
        public string JMBG { get; set; }
        [MaxLength(20)]
        public string Name { get; set; }
        [MaxLength(20)]
        public string Surname { get; set; }
        // Foreign Key
        [Indexed]
        public int DrivingSchoolId { get; set; }
    }
}
