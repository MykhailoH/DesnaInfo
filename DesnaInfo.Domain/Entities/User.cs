using System;

namespace DesnaInfo.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string MessengerId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
