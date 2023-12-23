using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Contracts.DTO;
using Domain.Base;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Domain.Commands
{
    public class GetDepartmentsQuery : IRequest<GetDepartmentsQueryResult>
    {
    }

    public class GetDepartmentsQueryResult
    {
        public List<DepartmentDTO> Departments { get; init; }
    }

    internal class GetDepartmentsQueryHandler : BaseHandler<GetDepartmentsQuery, GetDepartmentsQueryResult>
    {
        private readonly BaseSqlConnection _connection;
        
        public GetDepartmentsQueryHandler(BaseSqlConnection connection, ILogger<GetDepartmentsQueryHandler> logger) :
            base(logger)
        {
            _connection = connection;
        }

        protected override async Task<GetDepartmentsQueryResult> HandleInternal(GetDepartmentsQuery request, CancellationToken cancellationToken)
        {
            string getDepartmentsQuery =
            $@"
                SELECT *
                FROM departments
            ";

            List<DepartmentDTO> departments = await ExecuteCollectionSqlQuery<DepartmentDTO>(_connection, getDepartmentsQuery, cancellationToken);

            return new()
            {
                Departments = departments
            };
        }
    }
}