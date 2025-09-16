using KvizHub.Enums;
using System.ComponentModel.DataAnnotations;

namespace KvizHub.DTO
{
    public class PageInfo
    {
        [Range(0, int.MaxValue)]
        public int StartIndex { get; set; } = 0;

        [Range(1, int.MaxValue)]
        public int PageSize { get; set; } = 10;

        [MinLength(1, ErrorMessage = "Search term must be at least 1 character.")]
        public string? SearchTerm { get; set; }

        public Difficulty? Level { get; set; }

        public int? Category { get; set; }
    }
}
