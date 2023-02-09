using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEndTryitter.Models
{
    public class Image
    {
        [Key]
        public Guid ImageId { get; set; }
        public string ImageUrl { get; set; }
        [ForeignKey("PostId")]
        public Post Post { get; set; }
        public Guid PostId { get; set; }
    }
}
