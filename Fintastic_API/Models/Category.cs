using Fintastic_API.Enums;
using System.ComponentModel.DataAnnotations;

namespace Fintastic_API.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Insira um título")]
        [StringLength(100, ErrorMessage = "O Título deve ter no máximo 100 caracteres.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Insira um emoji")]
        public string Icon { get; set; }
        public CategoryType Type { get; set; } // Adicionado para diferenciar entre despesa e ganho

    }
}
