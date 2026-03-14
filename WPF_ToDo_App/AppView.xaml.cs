using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_ToDo_App
{
    /// <summary>
    /// Interaction logic for AppView.xaml
    /// </summary>
    public partial class AppView : UserControl
    {
        private bool removeNothingTodo;
        public Guid UserId { get; }
        TaskCRUDLogic task = new TaskCRUDLogic();
        public AppView(Guid userid)
        {
            InitializeComponent();

            this.UserId = userid;

            task.GetTasksByUserIdAsync(UserId).ContinueWith(t =>
            {
                if (t.Result.Count == 0)
                {
                    Dispatcher.Invoke(() => NothingTodo());
                    removeNothingTodo = true;
                }
                else
                {
                    foreach (var task in t.Result)
                    {
                        Dispatcher.Invoke(() => CreateDynamicCheckBox($"[{task.CreationDateTime}]: {task.TaskDetails}", task.TaskId));
                    }
                }
            });

        }

        private async void CreateTodo_Click(object sender, RoutedEventArgs e)
        {
            string taskInputText = taskInput.Text;

            if (!string.IsNullOrEmpty(taskInputText) && taskInputText.Length < 200)
            {
                DateTime CreatedAt = DateTime.Now;
                string taskInputText_timestamp = $"[{CreatedAt}]: {taskInputText}";

                Guid taskId = await task.CreateTaskAsync(UserId, CreatedAt, taskInputText);
                if (taskId != Guid.Empty) 
                {
                    if (removeNothingTodo)
                    {
                        taskShow.Children.Clear();
                        removeNothingTodo = false;
                    }
                    CreateDynamicCheckBox(taskInputText_timestamp, taskId);
                }
                else
                    MessageBox.Show("Failed to create task. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                taskInput.Clear();
            }
            else
            {
                MessageBox.Show("Please enter a valid task before creating a To-Do item." +
                    "\nNote: task must be under 200 characters.", "Input Required", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void RemoveTodo_Click(object sender, RoutedEventArgs e)
        {
            if (!taskShow.Children.OfType<CheckBox>().Any())
            {
                MessageBox.Show("There are no tasks to remove.", "No Tasks", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            RemoveCheckedTasks(taskShow);
        }

        private void CreateDynamicCheckBox(string taskText, Guid taskid)
        {

            TextBlock textBlock = new TextBlock
            {
                Text = taskText + "\n",
                TextWrapping = TextWrapping.Wrap,
                Foreground = new SolidColorBrush(Colors.DarkBlue),
                FontWeight = FontWeights.Bold,
                FontSize = 15
            };

            CheckBox newCheckBox = new CheckBox
            {
                Content = textBlock,
                Tag = taskid,
                VerticalContentAlignment = VerticalAlignment.Top
            };

            taskShow.Children.Add(newCheckBox);
        }
        private async void RemoveCheckedTasks(StackPanel stackPanel)
        {
            List<CheckBox> toRemove = stackPanel.Children
                            .OfType<CheckBox>()
                            .Where(cb => cb.IsChecked == true)
                            .ToList();

            List<Guid> toRemoveIds = toRemove.Select(cb => (Guid)cb.Tag).ToList();

            if (!toRemove.Any())
            {
                MessageBox.Show("Please select a task to remove.", "No Task Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            foreach (Guid taskid in toRemoveIds) 
            {
                bool removedTask = await task.DeleteTaskAsync(taskid);

                if (removedTask) 
                {
                    stackPanel.Children.Remove(toRemove.First(cb => (Guid)cb.Tag == taskid));
                }
                else
                    MessageBox.Show($"Failed to remove task with ID: {taskid}. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }

            if (!stackPanel.Children.OfType<CheckBox>().Any())
            {
                NothingTodo();
                removeNothingTodo = true;
            }
        }

        private void NothingTodo()
        {
            TextBlock textBlock = new TextBlock
            {
                Text = "Nothing To Do! Enjoy Life!",
                TextWrapping = TextWrapping.Wrap,
                Foreground = new SolidColorBrush(Colors.MediumOrchid),
                FontWeight = FontWeights.Bold,
                FontSize = 15,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            taskShow.Children.Add(textBlock);
        }
    }
}
