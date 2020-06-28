using System.Collections.Generic;

namespace MyProject.Models.ViewModels
{
    public class PagedResult<T>
    {
        public int Page { get; set; }
        public int Size { get; set; }
        public int Total { get; set; }
        public List<T> Data { get; set; } = new List<T>();

    }
}