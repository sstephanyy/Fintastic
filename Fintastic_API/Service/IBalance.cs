namespace Fintastic_API.Service
{
    public interface IBalance<T>
    {
        Task<decimal> CalculateTotalIncome();
        Task<decimal> CalculateTotalSpent();
        Task<decimal> CalculateTotal();

    }
}
