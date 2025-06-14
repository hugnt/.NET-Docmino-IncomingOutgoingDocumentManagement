using Docmino.Application.Common.Messages;
using Docmino.Application.Helpers;
using Docmino.Application.Models;
using Docmino.Application.Models.Lookups;
using Docmino.Application.Models.Requests;
using Docmino.Application.Models.Responses;
using Docmino.Domain.Abstractions;
using Docmino.Domain.Entities;
using FluentValidation;
using System.Linq.Expressions;
using System.Net;

namespace Docmino.Application.Services.Implement
{
    public class PositionService : IPositionService
    {
        private readonly IRepository<Position> _repository;
        private readonly IValidator<PositionRequest> _validator;

        public PositionService(IRepository<Position> repository, IValidator<PositionRequest> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<Result> Lookup()
        {
            var res = await _repository.GetAllAsync(selectQuery: x => new PositionLookup
            {
                Id = x.Id,
                Name = x.Name,
                DepartmentName = x.Department.Name
            });
            return Result<List<PositionLookup>>.SuccessWithBody(res.ToList());
        }

        public async Task<Result> GetAll(FilterRequest filter)
        {
            Expression<Func<Position, bool>> queryFilter = x =>
                                        (filter.SearchValue.IsEmpty() || x.Name.Contains(filter.SearchValue!) || x.Department.Name.Contains(filter.SearchValue!));


            var res = await _repository.GetByFilterAsync(filter.PageSize, filter.PageNumber, predicate: queryFilter, selectQuery: PositionMapping.SelectResponseExpression);
            return FilterResult<List<PositionResponse>>.Success(res.Data.ToList(), res.TotalCount);
        }

        public async Task<Result> GetById(int id)
        {
            var selectedEntity = await _repository.FirstOrDefaultAsync(x => x.Id == id, selectQuery: PositionMapping.SelectResponseExpression);
            if (selectedEntity == null)
            {
                return Result.Error(HttpStatusCode.NotFound, ErrorMessage.ObjectNotFound(id, "Chức vụ"));
            }
            return Result<PositionResponse>.SuccessWithBody(selectedEntity);
        }

        public async Task<Result> Add(PositionRequest modelRequest)
        {
            var validateResult = _validator.Validate(modelRequest);
            if (!validateResult.IsValid)
            {
                return Result.ErrorValidation(validateResult);
            }
            if (await _repository.AnyAsync(x => x.Name == modelRequest.Name.Trim()))
            {
                return Result.Error(HttpStatusCode.BadRequest, ErrorMessage.ObjectExisted(modelRequest.Name, "Chức vụ"));
            }

            var newEntity = modelRequest.ToEntity();

            _repository.Add(newEntity);
            await _repository.SaveChangesAsync();
            return Result.Success(HttpStatusCode.Created, SuccessMessage.CreatedSuccessfully("Chức vụ"));
        }

        public async Task<Result> Update(int id, PositionRequest modelRequest)
        {
            var selectedEntity = await _repository.FirstOrDefaultAsync(x => x.Id == id);
            if (selectedEntity == null)
            {
                return Result.Error(HttpStatusCode.NotFound, ErrorMessage.ObjectNotFound(id, "Chức vụ"));
            }

            var validateResult = _validator.Validate(modelRequest);
            if (!validateResult.IsValid)
            {
                return Result.ErrorValidation(validateResult);
            }
            if (await _repository.AnyAsync(x => x.Id != id && x.Name == modelRequest.Name.Trim()))
            {
                return Result.Error(HttpStatusCode.BadRequest, ErrorMessage.ObjectExisted(modelRequest.Name, "Chức vụ"));
            }

            selectedEntity.MappingFieldFrom(modelRequest);

            _repository.Update(selectedEntity);
            await _repository.SaveChangesAsync();

            return Result.SuccessNoContent();
        }

        public async Task<Result> Delete(int id)
        {
            var selectedEntity = await _repository.FirstOrDefaultAsync(x => x.Id == id);
            if (selectedEntity == null)
            {
                return Result.Error(HttpStatusCode.NotFound, ErrorMessage.ObjectNotFound(id, "Chức vụ"));
            }
            if (await _repository.AnyAsync(x => x.ProcessDetails != null))
            {
                return Result.ErrorWithMessage(ErrorMessage.ObjectIsInOtherProcess("Chức vụ", selectedEntity.Name));
            }
            _repository.Delete(selectedEntity);
            await _repository.SaveChangesAsync();
            return Result.SuccessNoContent();
        }
    }
}