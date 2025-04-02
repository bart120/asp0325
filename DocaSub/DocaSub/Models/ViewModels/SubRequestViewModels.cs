namespace DocaSub.Models.ViewModels
{
    public class SubRequestIndexViewModel
    {
        public IQueryable<SubRequest> SubRequestActive { get; set; }

        public IQueryable<SubRequest> SubRequesteOlder { get; set; }
    }


    public class SubRequestCreateViewModel
    {
    }
}
