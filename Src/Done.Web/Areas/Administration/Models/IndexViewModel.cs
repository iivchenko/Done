using System.Collections.Generic;
using Done.Core.Web.Pagination;
using Microsoft.AspNetCore.Identity;

namespace Done.Web.Areas.Administration.Models
{
     // TODO: Make it general purpose
    public sealed class IndexViewModel
    {
        public int Total { get; set; }

        public IEnumerable<IdentityUser> Users { get; set; }

        public PageViewModel Pagination { get; set; }

        public string Pattern { get; set; }
    }
}