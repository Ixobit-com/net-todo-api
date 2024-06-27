namespace Todo.Common {
    public class PagingResult<T> {
        public int TotalItems { get; set; }
        public int FilteredItems { get; set; }
        public IEnumerable<T>? Items { get; set; }
    }
}