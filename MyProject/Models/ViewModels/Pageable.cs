namespace MyProject.Models.ViewModels
{
    public class Pageable
    {
        public int Page { get; set; }
        public int Size { get; set; }
        public string SortedField { get; set; }
        public string SortedOrder { get; set; }

        public Pageable(int page, int size)
        {
            Page = page;
            Size = size;
        }
    }
}