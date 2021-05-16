using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using StructuresAndAlgorithms.Algorithms;

namespace StructuresAndAlgorithms
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static double _iterationTime = 50;
        List<Elements> items = new List<Elements>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            SelectSort(radioButton.Content.ToString());

        }
        private void SelectSort(string algorighm)
        {
            switch (algorighm)
            {
                case "Bubble Sort":
                    var bubble = new BubbleSort<Elements>(items);
                    AlgorithmStart(bubble);
                    break;
                case "Insertion Sort":
                    var insertion = new InsertionSort<Elements>(items);
                    AlgorithmStart(insertion);
                    break;
                case "Selection Sort":
                    var sekection = new SelectionSort<Elements>(items);
                    AlgorithmStart(sekection);
                    break;
                case "Quick Sort":
                    MessageBox.Show("Quick Sort not implemented");
                    break;
                case "Merge Sort":
                    MessageBox.Show("Merge Sort not implemented");
                    break;
                default:
                    MessageBox.Show("Something wrong");
                    break;
            }
        }

        private void AddOneItemButton_Click(object sender, RoutedEventArgs e)
        {
            if (items.Count < 25)
            {
                if (int.TryParse(AddOneItemTextBox.Text, out int value))
                {
                    var element = new Elements(value, items.Count);
                    items.Add(element);

                    FillBottomCanvas(items);
                }
                else
                    MessageBox.Show("Wrong number");
            }
            else
                MessageBox.Show("Maximum of items count 25");

            AddOneItemTextBox.Text = String.Empty;
        }
        private void AddFewItemsButton_Click(object sender, RoutedEventArgs e)
        {
            Random rand = new Random();
            if (int.TryParse(AddFewItemsTextBox.Text, out int range) && (range + items.Count) <= 25)
            {
                for (int i = 0; i < range; i++)
                {
                    int tempNumber = rand.Next(100);
                    var element = new Elements(tempNumber, items.Count);
                    items.Add(element);
                }
                FillBottomCanvas(items);
            }
            else
                MessageBox.Show("Maximum of items count 25");

            AddFewItemsTextBox.Text = String.Empty;
        }
        private void FillBottomCanvas(List<Elements> elements)
        {
            BottomCanvas.Children.Clear();
            BottomCanvas.Refresh();
            foreach (var item in elements)
            {
                BottomCanvas.Children.Add(item.ProgressBar);
                BottomCanvas.Children.Add(item.TextBlock);
            }

            BottomCanvas.Refresh();
        }
        private void RefreshItems()
        {
            foreach (var item in items)
            {
                item.SetStartPosition();
            }
            FillBottomCanvas(items);
        }
        private void Algorithm_SwapEvent(object sender, Tuple<Elements, Elements> e)
        {
            var temp = e.Item1.Number;
            e.Item1.SetPositionValue(e.Item2.Number);
            e.Item2.SetPositionValue(temp);
            BottomCanvas.Refresh();
        }

        private void Algorithm_CompereEvent(object sender, Tuple<Elements, Elements> e)
        {
            e.Item1.SetColor(new SolidColorBrush(Color.FromRgb(255, 0, 0)));
            e.Item2.SetColor(new SolidColorBrush(Color.FromRgb(255, 0, 0)));
            e.Item1.ProgressBar.Refresh();
            e.Item2.ProgressBar.Refresh();
            BottomCanvas.Refresh();
            Thread.Sleep((int)_iterationTime);

            e.Item1.SetColor(new SolidColorBrush(Color.FromRgb(0, 0, 255)));
            e.Item2.SetColor(new SolidColorBrush(Color.FromRgb(0, 0, 255)));
            e.Item1.ProgressBar.Refresh();
            e.Item2.ProgressBar.Refresh();
            BottomCanvas.Refresh();
            Thread.Sleep((int)_iterationTime);
        }
        private void AlgorithmStart(BaseAlgorithm<Elements> itemsList)
        {
            RefreshItems();
            LabelsClear();


            itemsList.CompereEvent += Algorithm_CompereEvent;
            itemsList.SwapEvent += Algorithm_SwapEvent;
            TimeSpan time = itemsList.Sort();


            ItemsCountLabel.Content += items.Count.ToString();
            TimeSpentLabel.Content += time.Seconds.ToString();
            SwapCounterLabel.Content += itemsList.SwapCount.ToString();
            SwapsCheck.Content += itemsList.CompareCount.ToString();
            IterationTimeDeleyLabel.Content += ((int)_iterationTime).ToString();
        }
        private void LabelsClear()
        {
            ItemsCountLabel.Content = "Items in the list: ";
            TimeSpentLabel.Content = "Time spent: ";
            SwapCounterLabel.Content = "Swap Counter: ";
            SwapsCheck.Content = "Swaps Check: ";
            IterationTimeDeleyLabel.Content = "Iteration time delay: ";

        }
        private void SliderValue(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _iterationTime = ((Slider)sender).Value * 2;
        }

        private void ClearBottomCanvas_Click(object sender, RoutedEventArgs e)
        {
            BottomCanvas.Children.Clear();
            items.Clear();
        }
    }
    public static class ExtensionMethods
    {
        private static readonly Action EmptyDelegate = delegate { };
        public static void Refresh(this UIElement uiElement)
        {
            uiElement.Dispatcher.Invoke(DispatcherPriority.Render, EmptyDelegate);
        }
    }
}
