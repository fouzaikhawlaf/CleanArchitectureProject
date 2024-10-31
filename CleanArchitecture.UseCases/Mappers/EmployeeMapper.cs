using CleanArchitecture.Entities.Users;
using CleanArchitecture.UseCases.Dtos.ProjectDtos.EmployeeDtos;
using CleanArchitecture.UseCases.Dtos.ProjectDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Mappers
{
    public static class EmployeeMapper
    {
        public static EmployeeDto MapToDto(Employee entity)
        {
            if (entity == null) return null;

            return new EmployeeDto
            {
                Id = entity.Id,
                FirstName = entity.FirstName ?? string.Empty,
                LastName = entity.LastName ?? string.Empty,
                Email = entity.Email ?? string.Empty,
                Department = entity.Department ?? string.Empty,
             
                Projects = entity.Projects.Select(p => new ProjectDto
                {
                    Id = p.Id,
                    Name = p.Name ?? string.Empty,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate
                }).ToList()
            };
        }

        public static Employee MapToEntity(EmployeeCreateDto createDto)
        {
            if (createDto == null) return null;

            return new Employee
            {
                FirstName = createDto.FirstName,
                LastName = createDto.LastName,
                Email = createDto.Email,
                Department = createDto.Department,
              
            };
        }

        public static void MapToEntity(EmployeeUpdateDto updateDto, Employee entity)
        {
            if (updateDto == null || entity == null) return;

            entity.FirstName = updateDto.FirstName;
            entity.LastName = updateDto.LastName;
            entity.Email = updateDto.Email;
            entity.Department = updateDto.Department;
           
        }
    }
}
