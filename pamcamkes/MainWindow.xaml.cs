using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.IO;

namespace pamcamkes
{
    public partial class MainWindow : Window
    {
        int price = 200;
        int total = 0;
        public MainWindow()
        {
            InitializeComponent();
            Start();
        }
        void Start()
        {
            number.GotFocus += click;
            typeD.GotFocus += click;
            typeF.GotFocus += click;

            number.LostFocus += noclick;
            typeD.LostFocus += noclick;
            typeF.LostFocus += noclick;

            add.Click += (s, e) =>
            {
                Add();
            };

            cancel.Click += (s, e) =>
            {
                number.Text = (string)number.Tag;
                typeD.Text = (string)typeD.Tag;
                typeF.Text = (string)typeF.Tag;
            };

            order.Click += (s, e) =>
            {
                Write();
            };

            panel.Children.Add(new Label() { Content = "Order details:", Foreground = new SolidColorBrush(Color.FromRgb(20, 80, 255)) });
            panel.Children.Add(new Label() { Content = "Total: " + total, Foreground = new SolidColorBrush(Color.FromRgb(20, 80, 255)) });
        }
        void Add()
        {
            if (int.TryParse(number.Text, out int num) && num > 0 && typeD.Text.Length > 0 && typeF.Text.Length > 0)
            {
                panel.Children.RemoveAt(panel.Children.Count - 1);
                panel.Children.Add(new Label() { Content = number.Text, Foreground = new SolidColorBrush(Color.FromRgb(20, 80, 255)) });
                panel.Children.Add(new Label() { Content = typeD.Text, Foreground = new SolidColorBrush(Color.FromRgb(20, 80, 255)) });
                panel.Children.Add(new Label() { Content = typeF.Text, Foreground = new SolidColorBrush(Color.FromRgb(20, 80, 255)) });
                total += price;
                panel.Children.Add(new Label() { Content = "Total: " + total, Foreground = new SolidColorBrush(Color.FromRgb(20, 80, 255)) });
            }
            else
            {
                MessageBox.Show("Invalid input");
            }
        }
        void Write()
        {
            using (StreamWriter write = new StreamWriter("file.txt", false, Encoding.UTF8))
            {
                for (int i = 0; i < panel.Children.Count; i++)
                {
                    if (i > 0 && i < panel.Children.Count - 1 && panel.Children[i] is Label) write.WriteLine((panel.Children[i] as Label).Content);
                }
            }
        }
        void click(object s, EventArgs e)
        {
            if (s is TextBox)
            {
                TextBox tb = s as TextBox;
                if (tb.Text == (string)tb.Tag) tb.Clear();
            }
        }
        void noclick(object s, EventArgs e)
        {
            if (s is TextBox)
            {
                TextBox tb = s as TextBox;
                if (tb.Text == "") tb.Text = (string)tb.Tag;
            }
        }
    }
}
