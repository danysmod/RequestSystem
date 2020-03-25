using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IUnitOfWork
    {
        Task<int> Save();
    }
}
