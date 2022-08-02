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

    public class Car
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = default!;

        public int Consumption { get; set; }

        public int NumberOfCylinders { get; set; }

        public int HorsePower { get; set; }

        public int Weight { get; set; }

        public int Acceleration { get; set; }

        public DateTimeOffset Year { get; set; }

        public string Origin { get; set; } = default!;

        public Car()
        {
        }

        public Car(
            Guid id,
            string name,
            int consumption,
            int numberOfCylinders,
            int horsePower,
            int weight,
            int acceleration,
            DateTimeOffset year,
            string origin)
        {
            Id = id;
            Name = name;
            Consumption = consumption;
            NumberOfCylinders = numberOfCylinders;
            HorsePower = horsePower;
            Weight = weight;
            Acceleration = acceleration;
            Year = year;
            Origin = origin;
        }
    }
}
