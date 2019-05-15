using System.IO;
using System.Threading.Tasks;

namespace PET.Application.Services
{
    public interface IFileStorageService
    {
        Task Save(MemoryStream memoryStream, string wayToFile);

        Task Load(MemoryStream memoryStream, string wayToFile);

        Task Delete(string wayToFile);
    }
}