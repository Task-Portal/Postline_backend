using System.Threading.Tasks;
using Shared.DataTransferObjects.ForCreation;
using Shared.DataTransferObjects.ForShow;

namespace Service.Contracts
{
    public interface IPointService
    {
        Task<PointDto> CreatePointAsync(PointForCreationDto point, string name,bool trackChanges);
    }
}