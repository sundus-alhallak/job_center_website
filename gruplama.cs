# 20 gruplama.cs
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JobSearch.Models
{
    public class Categories
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("grup ismi ")]
        public string Name { get; set; }

        [Required]
        [DisplayName("grupun açıklaması ")]
        public string Discription { get; set; }

        public virtual ICollection<Jobs> jobs { get; set; }
    }
}
