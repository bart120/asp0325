﻿namespace DocaSub.Models.ViewModels
{
    public class SubRequestIndexViewModel
    {
        public IQueryable<SubRequest> SubRequestActive { get; set; }

        public IEnumerable<SubRequest> SubRequesteOlder { get; set; }
    }


    public class SubRequestCreateViewModel
    {
    }
}
