using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace DrivingSchoolSTOP.Models
{
    public class Category
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Unique, MaxLength(1)]
        public string Name { get; set; }
        [Unique, MaxLength(20)]
        public string Type { get; set; }
    }
}
