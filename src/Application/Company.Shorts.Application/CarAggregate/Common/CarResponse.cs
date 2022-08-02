namespace Company.Shorts.Application.CarAggregate.Common
{
    using System;

    public class CarResponse
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
    }
}
