namespace Company.Shorts.Domain
{
    using System;

    public class Example
    {
        public Example(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; }

        public string Name { get; }
    }
}
