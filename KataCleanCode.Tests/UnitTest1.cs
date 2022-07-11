using NFluent;

namespace KataCleanCode.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void WhenInitializeTaskManager_WeHaveEmptyTaskList()
        {
            // Arrange
            var taskManager = new TaskManager();

            // Act
            // Assert
            Check.That(taskManager.Tasks).IsEmpty();
        }

        [Fact]
        public void WhenPush_ExitLette()
        {
            // Arrange
            var taskManager = new TaskManager();

            // Act
            taskManager.AddCommand("q");

            // Assert
            Check.That(taskManager.Tasks).IsEmpty();
        }

        [Fact]
        public void WhenPush_AddTask()
        {
            // Arrange
            var taskManager = new TaskManager();

            // Act
            taskManager.AddCommand("+ Learn Python");

            // Assert
            Check.That(taskManager.Tasks).ContainsExactly("1 [ ] Learn Python");
        }

        [Fact]
        public void WhenPush_AddSecondTask()
        {
            // Arrange
            var taskManager = new TaskManager();

            // Act
            taskManager.AddCommand("+ Learn Python");
            taskManager.AddCommand("+ Learn TDD");

            // Assert
            Check.That(taskManager.Tasks).ContainsExactly("1 [ ] Learn Python", "2 [ ] Learn TDD");
        }
    }

    internal class TaskManager
    {
        public TaskManager()
        {
        }

        public IEnumerable<string> Tasks { get; set; } = Enumerable.Empty<string>();

        internal void AddCommand(string command)
        {
            if (command == "q")
            {
                Tasks = Enumerable.Empty<string>();
            }
            else
            {
                var taskName = command.Remove(0, 2);
                Tasks = Tasks.Append($"{Tasks.Count() + 1} [ ] {taskName}");
            }
        }
    }
}