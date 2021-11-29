using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Midterm_Chuyende.Models
{
    public class ViewModelDetail
    {
        public Post post { get; set; }
        public List<Comment> comments { get; set; }
        public Account account { get; set; }
    }
}