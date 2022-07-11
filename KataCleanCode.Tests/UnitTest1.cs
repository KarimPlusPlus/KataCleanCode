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

        [Fact]
        public void StatusOfFirstTask()
        {
            // Arrange
            var taskManager = new TaskManager();
            taskManager.AddCommand("+ Learn Python");
            taskManager.AddCommand("+ Learn TDD");

            // Act
            var result = taskManager.AddCommand("x 1");

            // Assert
            Check.That(result).IsEqualTo("1 [ ] Learn Python");
        }

        [Fact]
        public void StatusOfSecondTask()
        {
            // Arrange
            var taskManager = new TaskManager();
            taskManager.AddCommand("+ Learn Python");
            taskManager.AddCommand("+ Learn TDD");

            // Act
            var result = taskManager.AddCommand("x 2");

            // Assert
            Check.That(result).IsEqualTo("2 [ ] Learn TDD");
        }
    }

    internal class TaskManager
    {
        public TaskManager()
        {
        }

        public IEnumerable<string> Tasks { get; set; } = Enumerable.Empty<string>();

        public string AddCommand(string command)
        {
            var commandLetter = command.First();

            switch (commandLetter)
            {
                case 'q':
                    Tasks = Enumerable.Empty<string>();
                    break;
                case '+':
                    {
                        var taskName = command.Remove(0, 2);
                        Tasks = Tasks.Append($"{Tasks.Count() + 1} [ ] {taskName}");
                        break;
                    }

                case 'x':
                    {
                        var positionString = command.Remove(0, 2);
                        var position = int.Parse(positionString);
                        return $"{Tasks.ElementAt(position - 1)}";
                    }
            }

            return String.Empty;
        }
    }
}