namespace Company.Shorts.Domain
{
    using System;

    public class Pet
    {
        public Pet(int id, string name, string tag)
        {
            this.Id = id;
            this.Name = name;
            this.Tag = tag;
        }

        public int Id { get; }

        public string Name { get; }

        public string Tag { get; }
    }
}
