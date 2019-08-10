using System;

namespace CC.Models
{
    public class Like
    {
        public string ID { get; set; }
        public User User { get; set; }
        public int MovieID { get; set; }
    }
}
