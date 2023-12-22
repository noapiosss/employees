namespace Contracts.Http
{
    public enum ErrorCode
    {
        BadRequest = 40000,
        ExecutAccessForbidden = 40302,
        DepartmentNotFound = 40401,
        PositionNotFound = 40402,
        EmployeeNotFound = 40403,
        DepartmentPositionNotFound = 40404,
        NotAcceptable = 40600,
        DepartmentAlreadyExists = 40901,
        PositionAlreadyExists = 40902,
        InternalServerError = 50000,
        DbFailureError = 50001
    }

    public class ErrorResponse
    {
        public ErrorCode Code { get; init; }
        public string Message { get; init; }
    }
}