namespace FinalProjectConsume.Models.Paginate
{
    public class Paginated<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }

        public Paginated(IEnumerable<T> items, int totalPages, int currentPage)
        {
            Items = items;
            TotalPages = totalPages;
            CurrentPage = currentPage;
        }
    }
}
