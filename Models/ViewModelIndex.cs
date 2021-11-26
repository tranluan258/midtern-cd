using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Midterm_Chuyende.Models
{
    public class ViewModelIndex
    {
        public List<Post> getAllPost { get; set; }
        public List<Topic> getAllTopic { get; set; }
        public Account getAccount { get; set; }

    }
}