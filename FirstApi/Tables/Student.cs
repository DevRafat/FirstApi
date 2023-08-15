using System.Text.Json.Serialization;

namespace FirstApi.Tables
{
    public class Student
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public int MajorID { get; set; }
        public Major Major { get; set; }
    }
}
