namespace DocaSub.Models.ViewModels
{
    public class SubRequestIndexViewModel
    {
        public IEnumerable<SubRequest> SubRequestActive { get; set; }

        public IEnumerable<SubRequest> SubRequesteOlder { get; set; }
    }


    public class SubRequestCreateViewModel
    {
    }
}
