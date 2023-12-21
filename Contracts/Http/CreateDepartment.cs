namespace Contracts.Http
{
    public struct CreateDepartmentRequest
    {
        public string Name { get; init; }
    }

    public struct CreateDepartmentResponse
    {
        public int Id { get; set; }
    }
}