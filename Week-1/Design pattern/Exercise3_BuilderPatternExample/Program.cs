using System;

namespace BuilderPatternExample
{
    public class Computer
    {
        public string CPU { get; private set; }
        public string RAM { get; private set; }
        public string Storage { get; private set; }

        private Computer(Builder builder)
        {
            CPU = builder.CPU;
            RAM = builder.RAM;
            Storage = builder.Storage;
        }

        public override string ToString()
        {
            return $"Computer Config: CPU={CPU}, RAM={RAM}, Storage={Storage}";
        }

        public class Builder
        {
            public string CPU { get; private set; }
            public string RAM { get; private set; }
            public string Storage { get; private set; }

            public Builder SetCPU(string cpu)
            {
                CPU = cpu;
                return this;
            }

            public Builder SetRAM(string ram)
            {
                RAM = ram;
                return this;
            }

            public Builder SetStorage(string storage)
            {
                Storage = storage;
                return this;
            }

            public Computer Build()
            {
                return new Computer(this);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Computer basicComputer = new Computer.Builder()
                .SetCPU("Intel i3")
                .SetRAM("8GB")
                .SetStorage("256GB SSD")
                .Build();

            Computer gamingComputer = new Computer.Builder()
                .SetCPU("Intel i9")
                .SetRAM("32GB")
                .SetStorage("1TB NVMe")
                .Build();

            Console.WriteLine(basicComputer);
            Console.WriteLine(gamingComputer);
        }
    }
}
