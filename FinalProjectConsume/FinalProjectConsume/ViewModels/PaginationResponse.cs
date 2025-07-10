namespace FinalProjectConsume.ViewModels
{
    public class PaginationResponse<T>
    {
        public IEnumerable<T> Datas { get; set; }
        public int TotalCount { get; set; }
        public int CurrentPage { get; set; }
    }
}
