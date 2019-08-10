using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CC.Models
{
	public class Movie
	{
        public int ID { get; set; }

		public string Name { get; set; }

		public double Price { get; set; }

        public string ImageUrl { get; set; }

        public Category Category { get; set; }

        public string Description { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<Like> Likes { get; set; }
    }
}