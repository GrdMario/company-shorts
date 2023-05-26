namespace Company.Shorts.Domain
{
    using System;

    public class User
    {
        public Guid Id { get; set; }

        public string UserName { get; set; } = default!;

        public string Email { get; set; } = default!;

        public string Address { get; set; } = default!;

        public string ProfilePicture { get; set; } = default!;

        public int Integer { get; protected set; }

        public long Long { get; protected set; }

        public decimal Decimal { get; protected set; }

        public bool Boolean { get; protected set; }

        public DateTime Date { get; protected set; }
    }
}
