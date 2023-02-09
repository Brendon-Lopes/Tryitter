using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BackEndTryitter.Models
{
    public class Post
    {
        [Key]
        public Guid PostId { get; set; }
        [MaxLength(300)]
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public Guid UserId { get; set; }
        [InverseProperty("Post")]
        public ICollection<Image> Images { get; set; }
    }
}
