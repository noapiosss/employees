using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Contracts.Database;
using Contracts.Http;
using Domain.Base;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Domain.Commands
{
    public class GetFiltersQuery : IRequest<GetFiltersQueryResult>
    {
    }

    public class GetFiltersQueryResult
    {
        public ICollection<Department> Departmens { get; init; }
        public ICollection<Position> Positions { get; init; }
        // public DateOnly MinEmploymentDate { get; init; }
        // public DateOnly MaxEmploymentDate { get; init; }
        // public int MinAge { get; init; }
        // public int MaxAge { get; init; }
        // public decimal MinSalary { get; init; }
        // public decimal MaxSalary { get; init; }
        public BoundFilterValues BoundFilterValues { get; init ;}
    }

    internal class GetFiltersQueryHandler : BaseHandler<GetFiltersQuery, GetFiltersQueryResult>
    {
        private readonly BaseSqlConnection _connection;
        
        public GetFiltersQueryHandler(BaseSqlConnection connection, ILogger<GetFiltersQueryHandler> logger)
            : base(logger)
        {
            _connection = connection;
        }

        protected override async Task<GetFiltersQueryResult> HandleInternal(GetFiltersQuery request, CancellationToken cancellationToken)
        {
            string getAllDepartmnesQuery =
            $@"
                SELECT *
                FROM departments
            ";

            List<Department> departments = await ExecuteCollectionSqlQuery<Department>(_connection, getAllDepartmnesQuery, cancellationToken);

            string getAllPositionsQuery =
            $@"
                SELECT *
                FROM positions
            ";

            List<Position> positions = await ExecuteCollectionSqlQuery<Position>(_connection, getAllPositionsQuery, cancellationToken);

            string getBoundFilterValuesQuery =
            $@"
                SELECT
                    MIN(employment_date) as min_employment_date,
                    MAX(employment_date) as max_employment_date,
                    MIN(EXTRACT(YEAR FROM AGE(CURRENT_DATE, birth_date))) as min_age,
                    MAX(EXTRACT(YEAR FROM AGE(CURRENT_DATE, birth_date))) as max_age,
                    MIN(salary) as min_salary,
                    MAX(salary) as max_salary
                FROM employees
            ";

            BoundFilterValues boundFilterValues = await ExecuteSqlQuery<BoundFilterValues>(_connection, getBoundFilterValuesQuery, cancellationToken);

            return new()
            {
                Departmens = departments,
                Positions = positions,
                BoundFilterValues = boundFilterValues
            };
        }
    }
}