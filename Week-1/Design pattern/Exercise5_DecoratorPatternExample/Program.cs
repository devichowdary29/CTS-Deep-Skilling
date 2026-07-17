using System;

namespace DecoratorPatternExample
{
    public interface INotifier
    {
        void Send(string message);
    }

    public class EmailNotifier : INotifier
    {
        public void Send(string message)
        {
            Console.WriteLine($"Sending Email: {message}");
        }
    }

    public abstract class NotifierDecorator : INotifier
    {
        protected INotifier _wrappedNotifier;

        public NotifierDecorator(INotifier notifier)
        {
            _wrappedNotifier = notifier;
        }

        public virtual void Send(string message)
        {
            _wrappedNotifier.Send(message);
        }
    }

    public class SMSNotifierDecorator : NotifierDecorator
    {
        public SMSNotifierDecorator(INotifier notifier) : base(notifier) { }

        public override void Send(string message)
        {
            base.Send(message);
            Console.WriteLine($"Sending SMS: {message}");
        }
    }

    public class SlackNotifierDecorator : NotifierDecorator
    {
        public SlackNotifierDecorator(INotifier notifier) : base(notifier) { }

        public override void Send(string message)
        {
            base.Send(message);
            Console.WriteLine($"Sending Slack Message: {message}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            INotifier notifier = new EmailNotifier();
            notifier = new SMSNotifierDecorator(notifier);
            notifier = new SlackNotifierDecorator(notifier);

            notifier.Send("System Alert: High CPU Usage!");
        }
    }
}
