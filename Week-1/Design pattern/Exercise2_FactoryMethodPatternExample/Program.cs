using System;

namespace FactoryMethodPatternExample
{
    public interface IDocument
    {
        void Open();
    }

    public class WordDocument : IDocument
    {
        public void Open() => Console.WriteLine("Opening Word Document...");
    }

    public class PdfDocument : IDocument
    {
        public void Open() => Console.WriteLine("Opening PDF Document...");
    }

    public class ExcelDocument : IDocument
    {
        public void Open() => Console.WriteLine("Opening Excel Document...");
    }

    public abstract class DocumentFactory
    {
        public abstract IDocument CreateDocument();
    }

    public class WordFactory : DocumentFactory
    {
        public override IDocument CreateDocument() => new WordDocument();
    }

    public class PdfFactory : DocumentFactory
    {
        public override IDocument CreateDocument() => new PdfDocument();
    }

    public class ExcelFactory : DocumentFactory
    {
        public override IDocument CreateDocument() => new ExcelDocument();
    }

    class Program
    {
        static void Main(string[] args)
        {
            DocumentFactory factory = new WordFactory();
            IDocument doc = factory.CreateDocument();
            doc.Open();

            factory = new PdfFactory();
            doc = factory.CreateDocument();
            doc.Open();

            factory = new ExcelFactory();
            doc = factory.CreateDocument();
            doc.Open();
        }
    }
}
