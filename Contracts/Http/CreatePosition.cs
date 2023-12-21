namespace Contracts.Http
{
    public struct CreatePositionRequest
    {
        public string Name { get; init; }
    }

    public struct CreatePositionResponse
    {
        public int Id { get; set; }
    }
}