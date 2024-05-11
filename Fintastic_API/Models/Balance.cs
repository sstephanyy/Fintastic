using System.ComponentModel.DataAnnotations.Schema;

namespace Fintastic_API.Models
{
    public class Balance
    {
        public int BalanceId { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Total { get; set; }

        [Column(TypeName = "decimal(18,2)")] // Define o tipo decimal com 18 dígitos no total e 2 dígitos após a vírgula
        public decimal TotalIncome { get; set; }

        [Column(TypeName = "decimal(18,2)")] 
        public decimal TotalSpent { get; set; }
    }
}
