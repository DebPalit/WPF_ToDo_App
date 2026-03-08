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
        public AppView()
        {
            InitializeComponent();
            NothingTodo();
        }

        private bool removeNothingTodo = false;
        private void CreateTodo_Click(object sender, RoutedEventArgs e)
        {
            if (!removeNothingTodo)
            {
                taskShow.Children.Clear();
                removeNothingTodo = true;
            }
            string taskInputText = taskInput.Text;

            if (!string.IsNullOrEmpty(taskInputText) && taskInputText.Length < 200)
            {
                string taskInputText_timestamp = $"[{DateTime.Now}]: {taskInputText}";

                CreateDynamicCheckBox(taskInputText_timestamp);

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

        private void CreateDynamicCheckBox(string taskText)
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
                VerticalContentAlignment = VerticalAlignment.Top
            };

            taskShow.Children.Add(newCheckBox);
        }
        private void RemoveCheckedTasks(StackPanel stackPanel)
        {
            List<CheckBox> toRemove = stackPanel.Children
                            .OfType<CheckBox>()
                            .Where(cb => cb.IsChecked == true)
                            .ToList();

            if (!toRemove.Any())
            {
                MessageBox.Show("Please select a task to remove.", "No Task Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            foreach (CheckBox cb in toRemove)
                stackPanel.Children.Remove(cb);

            if (!stackPanel.Children.OfType<CheckBox>().Any())
            {
                NothingTodo();
                removeNothingTodo = false;
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
