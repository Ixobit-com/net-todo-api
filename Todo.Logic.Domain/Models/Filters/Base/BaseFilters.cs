namespace Todo.Logic.Domain.Models.Filters.Base {
    public abstract class BaseFilters<TOrderBy>
        where TOrderBy : Enum {
        public int Skip { get; set; }
        public int Take { get; set; }
        public TOrderBy? OrderBy { get; set; }
        public bool DescOrder { get; set; }

        public BaseFilters() {
            Skip = 0;
            Take = 0;
            OrderBy = (TOrderBy)Enum.Parse(typeof(TOrderBy), 0.ToString());
            DescOrder = false;
        }
    }
}