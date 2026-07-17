using System;

namespace Exercise5_TaskManagementSystem
{
    public class Task
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public string Status { get; set; }

        public override string ToString()
        {
            return $"Task {TaskId}: {TaskName} [{Status}]";
        }
    }

    public class Node
    {
        public Task Data { get; set; }
        public Node Next { get; set; }

        public Node(Task data)
        {
            Data = data;
            Next = null;
        }
    }

    public class SinglyLinkedList
    {
        private Node head;

        public void AddTask(Task task)
        {
            Node newNode = new Node(task);
            if (head == null)
            {
                head = newNode;
            }
            else
            {
                Node current = head;
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = newNode;
            }
            Console.WriteLine($"Added: {task.TaskName}");
        }

        public Task SearchTask(int taskId)
        {
            Node current = head;
            while (current != null)
            {
                if (current.Data.TaskId == taskId)
                    return current.Data;
                current = current.Next;
            }
            return null;
        }

        public void TraverseTasks()
        {
            Console.WriteLine("\n--- Task List ---");
            Node current = head;
            while (current != null)
            {
                Console.WriteLine(current.Data);
                current = current.Next;
            }
            Console.WriteLine("-----------------\n");
        }

        public void DeleteTask(int taskId)
        {
            if (head == null) return;

            if (head.Data.TaskId == taskId)
            {
                head = head.Next;
                Console.WriteLine($"Deleted Task ID: {taskId}");
                return;
            }

            Node current = head;
            while (current.Next != null && current.Next.Data.TaskId != taskId)
            {
                current = current.Next;
            }

            if (current.Next != null)
            {
                current.Next = current.Next.Next;
                Console.WriteLine($"Deleted Task ID: {taskId}");
            }
            else
            {
                Console.WriteLine($"Task ID {taskId} not found.");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            SinglyLinkedList taskList = new SinglyLinkedList();

            taskList.AddTask(new Task { TaskId = 1, TaskName = "Design Database", Status = "In Progress" });
            taskList.AddTask(new Task { TaskId = 2, TaskName = "Develop API", Status = "Pending" });
            taskList.AddTask(new Task { TaskId = 3, TaskName = "Write Tests", Status = "Pending" });

            taskList.TraverseTasks();

            Console.WriteLine("Searching for Task ID 2:");
            var t = taskList.SearchTask(2);
            Console.WriteLine(t != null ? t.ToString() : "Not Found");

            Console.WriteLine("\nDeleting Task ID 2...");
            taskList.DeleteTask(2);
            taskList.TraverseTasks();
        }
    }
}
