using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reciply.API.Models
{
    public class Follow
    {
        public int FollowerId { get; set; }
        public int FolloweeId { get; set; }
        public User Follower { get; set; }
        public User Followee { get; set; }
    }
}
