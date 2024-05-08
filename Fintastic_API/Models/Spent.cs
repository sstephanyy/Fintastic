using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fintastic_API.Models
{
    public class Spent
    {
        [Key]
        public int SpendingId { get; set; }

        [StringLength(100, ErrorMessage = "A descrição deve ter no máximo 100 caracteres")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Informe um valor gasto", AllowEmptyStrings = false)]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "O valor deve ter no máximo duas casas decimais.")]
        public decimal AmountSpent { get; set; }

        [Required(ErrorMessage = "É obrigatório colocar a data")]
        [DisplayFormat(DataFormatString = "dd/mm/yyyy")]
        public DateTime TransactionDate { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
