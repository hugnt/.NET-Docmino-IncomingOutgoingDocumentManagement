namespace Docmino.Application.Models.Requests
{
    public class PositionRequest
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int DepartmentId { get; set; }
    }
}