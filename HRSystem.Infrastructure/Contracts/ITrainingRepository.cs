using HRSystem.BaseLibrary.Models;
using System.Threading.Tasks;

namespace HRSystem.Infrastructure.Contracts
{
    public interface ITPLTrainingRepository : IGenericRepository<TPLTraining>
    {
        // Logic 2: Check for duplicate training titles before creation
        Task<TPLTraining?> GetByTitleAsync(string title);
    }
}