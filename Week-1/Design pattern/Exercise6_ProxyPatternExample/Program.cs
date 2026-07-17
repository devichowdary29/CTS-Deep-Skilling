using System;

namespace ProxyPatternExample
{
    public interface IImage
    {
        void Display();
    }

    public class RealImage : IImage
    {
        private string _filename;

        public RealImage(string filename)
        {
            _filename = filename;
            LoadFromDisk(filename);
        }

        private void LoadFromDisk(string filename)
        {
            Console.WriteLine($"Loading {filename} from remote server (simulating delay)...");
            System.Threading.Thread.Sleep(1000);
        }

        public void Display()
        {
            Console.WriteLine($"Displaying {_filename}");
        }
    }

    public class ProxyImage : IImage
    {
        private RealImage _realImage;
        private string _filename;

        public ProxyImage(string filename)
        {
            _filename = filename;
        }

        public void Display()
        {
            if (_realImage == null)
            {
                _realImage = new RealImage(_filename);
            }
            _realImage.Display();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            IImage image = new ProxyImage("test_image_10mb.jpg");

            // Image will be loaded from disk
            Console.WriteLine("First call to Display():");
            image.Display(); 
            
            // Image will not be loaded from disk again
            Console.WriteLine("\nSecond call to Display():");
            image.Display(); 
        }
    }
}
