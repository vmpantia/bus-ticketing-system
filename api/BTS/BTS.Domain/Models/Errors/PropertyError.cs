namespace BTS.Domain.Models.Errors
{
    public class PropertyError
    {
        public required string Property { get; set; }
        public required IEnumerable<string> Messages { get; set; }
    }
}
