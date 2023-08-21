using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TodoApplication.Models
{
    public class Todo
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Title")]
        public string Title { get; set; }

        [DisplayName("Note")]
        public string? Note { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Dead Line Date")]
        public DateTimeOffset DeadLineDate { get; set; }

        [DisplayName("Is Complete")]
        public bool IsComplete { get; set; }
    }
}
