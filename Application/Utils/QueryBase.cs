namespace Application.Utils
{
    public record QueryBase
    {
        public int pageSize {  get; set; }
        public int pageIndex { get; set; }
    }
}
