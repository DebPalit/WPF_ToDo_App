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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void createTodo_Click(object sender, RoutedEventArgs e)
        {
            string taskInputText = taskInput.Text;

            if (!string.IsNullOrEmpty(taskInputText))
            {
                string taskInputText_timestamp = $"[{DateTime.Now}]: {taskInputText}";
                //TextBlock newTask = new TextBlock
                //{
                //    Text = taskInputText,
                //    Margin = new Thickness(5),
                //    Foreground = new SolidColorBrush(Colors.White)
                //};

                //taskShow.Children.Add(newTask);
                CreateDynamicCheckBox(taskInputText_timestamp);
                taskInput.Clear();
            }
            else
            {
                MessageBox.Show("Please enter a task before creating a To-Do item.", "Input Required", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void removeTodo_Click(object sender, RoutedEventArgs e)
        {
            RemoveCheckedTasks(taskShow);
        }

        private void CreateDynamicCheckBox(string taskText)
        {
            CheckBox newCheckBox = new CheckBox
            {
                Content = taskText,
                Margin = new Thickness(5),
                Foreground = new SolidColorBrush(Colors.DarkBlue),
                FontWeight = FontWeights.Bold,
                FontSize = 15
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
        }
    }
}