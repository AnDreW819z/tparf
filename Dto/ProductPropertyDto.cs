using System.Text.Json.Serialization;
using tparf.Models;

namespace tparf.Dto
{
    public class ProductPropertyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

    }
}
