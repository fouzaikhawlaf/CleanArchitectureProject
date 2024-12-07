using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.InterfacesUse
{
    public interface IGenericService<TEntity, TDto, TCreateDto, TUpdateDto>
        where TEntity : class
        where TDto : class
        where TCreateDto : class
        where TUpdateDto : class
    {
        Task<TDto> GetByIdAsync(int id);
        Task<List<TDto>> GetAllAsync();
        Task<TDto> AddAsync(TCreateDto createDto);
        Task UpdateAsync(int id, TUpdateDto updateDto);
        Task DeleteAsync(int id);
    }
}
