using System.ComponentModel.DataAnnotations;

namespace FirstApi.Tables
{
    public class Major
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public string Description { get; set; }
        public string TestColumn { get; set; }
    }
}
