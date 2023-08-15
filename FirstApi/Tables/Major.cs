using System.ComponentModel.DataAnnotations;

namespace FirstApi.Tables
{
    public class Major
    {
        public Major()
        {
            this.Students=new HashSet<Student>();
        }
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}
