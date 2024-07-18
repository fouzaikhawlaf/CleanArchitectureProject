using CleanArchitecture.UseCases.InterfacesUse;


using CleanArchitecture.FrameworksAndDrivers.Data.Interfaces;


namespace CleanArchitecture.UseCases.Services
{
    public abstract class GenericService<TEntity, TDto, TCreateDto, TUpdateDto> : IGenericService<TEntity, TDto, TCreateDto, TUpdateDto>
         where TEntity : class
         where TDto : class
         where TCreateDto : class
         where TUpdateDto : class
    {
        private readonly IGenericRepository<TEntity> _repository;

        protected GenericService(IGenericRepository<TEntity> repository)
        {
            _repository = repository;
        }

        protected abstract TDto MapToDto(TEntity entity);
        protected abstract TEntity MapToEntity(TCreateDto createDto);
        protected abstract void MapToEntity(TUpdateDto updateDto, TEntity entity);

        public async Task<TDto> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity == null ? null : MapToDto(entity);
        }

        public async Task<List<TDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<TDto> AddAsync(TCreateDto createDto)
        {
            var entity = MapToEntity(createDto);
            await _repository.AddAsync(entity);
            return MapToDto(entity);
        }

        public async Task UpdateAsync(int id, TUpdateDto updateDto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Entity with ID {id} not found.");
            }

            MapToEntity(updateDto, entity);
            await _repository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Entity with ID {id} not found.");
            }

            await _repository.DeleteAsync(id);
        }
    }
}
