using PET.Application.DTOs;
using PET.Domain.Models;

namespace PET.Application.Builders
{
    public interface IFileBuilder
    {
        File Build(FileSaveDto fileSaveDto);
    }
}