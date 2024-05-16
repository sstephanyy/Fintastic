using Fintastic_API.Models;
namespace Fintastic_API.Services
{
    public interface IService<T>
    {
        Task AddRegister(T register);
        Task<IEnumerable<T>> GetRegisters();
        Task<T?> GetRegisterById(int id);
        Task<T> UpdateRegister(int id, T register);
        Task<bool> DeleteRegister(int id);
    }
}

