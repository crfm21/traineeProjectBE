using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RecipeApp.Domain.Core
{
    public class CoreEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "bit")]
        [JsonIgnore]
        public bool IsDeleted { get; set; }

        //[JsonIgnore]
        public DateTime CreationDate { get; set; } = DateTime.Now;

        [JsonIgnore]
        public DateTime? UpdateDate { get; set; }
    }
}
