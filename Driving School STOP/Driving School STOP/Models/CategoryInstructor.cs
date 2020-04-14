using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace DrivingSchoolSTOP.Models
{
    public class CategoryInstructor
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        // Foreign Key
        [Indexed]
        public int CategoryId { get; set; }
        [Indexed]
        public int InstructorId { get; set; }
    }
}
