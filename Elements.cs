using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace StructuresAndAlgorithms
{
    class Elements : IComparable
    {
        public ProgressBar ProgressBar { get; private set; }
        public TextBlock TextBlock { get; private set; }
        public int Value { get; private set; }
        public int Number { get; private set; }
        public int StartNumber { get; set; }
        public Elements(int value, int number)
        {
            Value = value;
            Number = number;
            StartNumber = number;
            SetProgressBar();
        }
        private void SetProgressBar()
        {
            int x = Number * 30;
            ProgressBar = new ProgressBar();

            ProgressBar.Value = Value;
            ProgressBar.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 255));
            ProgressBar.Maximum = 100;
            ProgressBar.Minimum = 0;
            ProgressBar.Orientation = Orientation.Vertical;
            ProgressBar.Height = 100;
            ProgressBar.Width = 25;
            ProgressBar.TabIndex = Number;
            Canvas.SetLeft(ProgressBar, 25 + x);
            Canvas.SetBottom(ProgressBar, 25);

            TextBlock = new TextBlock();
            TextBlock.Text = Value.ToString();
            TextBlock.FontSize = 16;
            TextBlock.Foreground = Brushes.Black;
            Canvas.SetLeft(TextBlock, 27 + x);
            Canvas.SetBottom(TextBlock, 7);

        }
        public void SetStartPosition()
        {
            Number = StartNumber;
            int x = Number * 30;
            Canvas.SetLeft(ProgressBar, 25 + x);
            Canvas.SetBottom(ProgressBar, 25);

            Canvas.SetLeft(TextBlock, 27 + x);
            Canvas.SetBottom(TextBlock, 7);
        }
        public int CompareTo(object obj)
        {
            if (obj is Elements item)
                return Value.CompareTo(item.Value);

            else
                throw new ArgumentException($"object is not Element)");
        }
        public void SetPositionValue(int number)
        {
            Number = number;
            int x = number * 30;
            Canvas.SetLeft(ProgressBar, 25 + x);
            Canvas.SetBottom(ProgressBar, 25);

            Canvas.SetLeft(TextBlock, 27 + x);
            Canvas.SetBottom(TextBlock, 7);
        }
        public void SetColor(SolidColorBrush color)
        {
            ProgressBar.Foreground = color;

        }
        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
