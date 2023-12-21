using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Contracts.Database;
using Domain.Base;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Domain.Commands
{
    public class GetFilteredEmployeesQuery : IRequest<GetFilteredEmployeesQueryResult>
    {
        public int DepartmentId { get; init; }
        public int PositionId { get; init; }
        public DateTime MinEmploymentDate { get; init; }
        public DateTime MaxEmploymentDate { get; init; }
        public int MinAge { get; init; }
        public int MaxAge { get; init; }
        public decimal MinSalary { get; init; }
        public decimal MaxSalary { get; init; }
        public string SearchRequest { get; init; }
    }

    public class GetFilteredEmployeesQueryResult
    {
        public ICollection<Employee> Employees { get; init; }
    }

    internal class GetFilteredEmployeesQueryHandler : BaseHandler<GetFilteredEmployeesQuery, GetFilteredEmployeesQueryResult>
    {
        private readonly BaseSqlConnection _connection;
        
        public GetFilteredEmployeesQueryHandler(BaseSqlConnection connection, ILogger<GetFilteredEmployeesQueryHandler> logger) :
            base(logger)
        {
            _connection = connection;
        }

        protected override async Task<GetFilteredEmployeesQueryResult> HandleInternal(GetFilteredEmployeesQuery request, CancellationToken cancellationToken)
        {
            string getFilteredEmployeesQuery = "";

            List<Employee> employees = await ExecuteCollectionSqlQuery<Employee>(_connection, getFilteredEmployeesQuery, cancellationToken);

            return new()
            {
                Employees = employees
            };
        }
    }
}