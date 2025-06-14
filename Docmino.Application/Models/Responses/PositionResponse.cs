namespace Docmino.Application.Models.Responses
{
    public class PositionResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }
}