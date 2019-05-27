using System;
using PET.Application.DTOs;
using PET.Domain.Models;

namespace PET.Application.Builders
{
    public class FileBuilder : IFileBuilder
    {
        public File Build(FileSaveDto fileSaveDto)
        {
            return new File
            {
                Id = Guid.NewGuid(),
                WayToFile = $"{Guid.NewGuid()}{Guid.NewGuid()}.{fileSaveDto.Extension}"
            };
        }
    }
}