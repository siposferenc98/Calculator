using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string TextBoxeredmeny;
        private List<string> muveleteksorrend = new List<string> { "*", "/", "+", "-" };

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Click(object sender, RoutedEventArgs e)
        {
            var tag = ((Button)sender).Tag;
            if (TextBoxeredmeny != null)
            {
                if (muveleteksorrend.Contains(TextBoxeredmeny[TextBoxeredmeny.Length - 1].ToString()))
                {
                    if (muveleteksorrend.Contains(tag))
                        TextBoxeredmeny = TextBoxeredmeny.Remove(TextBoxeredmeny.Length - 1,1);
                }
            }
            else
            {
                if (muveleteksorrend.Contains(tag))
                    return;
            }
            TextBoxeredmeny += tag.ToString();
            TB.Text = TextBoxeredmeny;
        }
        private void muveletekElvegez(string muveletjel, string muveletjel2 ,List<string> muveletek)
        {
            while (muveletek.Any(x => x == muveletjel || x == muveletjel2))
            {
                double temp = 0;
                int muvjelI = muveletek.IndexOf(muveletek.Where(x => x == muveletjel || x == muveletjel2).First());
                switch (muveletek[muvjelI])
                {
                    case "*":
                        temp += Convert.ToDouble(muveletek[muvjelI - 1]) * Convert.ToDouble(muveletek[muvjelI + 1]);
                        break;
                    case "/":
                        temp += Convert.ToDouble(muveletek[muvjelI - 1]) / Convert.ToDouble(muveletek[muvjelI + 1]);
                        break;
                    case "+":
                        temp += Convert.ToDouble(muveletek[muvjelI - 1]) + Convert.ToDouble(muveletek[muvjelI + 1]);
                        break;
                    case "-":
                        temp += Convert.ToDouble(muveletek[muvjelI - 1]) - Convert.ToDouble(muveletek[muvjelI + 1]);
                        break;
                }
                if(muvjelI >= 2)
                {
                    muveletek.Insert(muvjelI - 1 ,temp.ToString());
                }
                else
                {
                    muveletek.Insert(0,temp.ToString());
                }
                    muveletek.RemoveRange(muveletek.IndexOf(muveletek.Where(x => x == muveletjel || x == muveletjel2).First()) - 1, 3);
            }

        }

        private void Eredmeny(object sender, RoutedEventArgs e)
        {
            
            List<string> muveletek = Regex.Split(TextBoxeredmeny, @"(-)|(/)|(\+)|(\*)").ToList();
            
            for(int i = 0; i < muveleteksorrend.Count-1; i+=2)
            {
                muveletekElvegez(muveleteksorrend[i], muveleteksorrend[i+1], muveletek);
            }
            Elozmenyek.Items.Add(TextBoxeredmeny);
            TextBoxeredmeny = muveletek.First().ToString();
            TB.Text = TextBoxeredmeny;
        }

        private void ElozmenySelect(object sender, SelectionChangedEventArgs e)
        {
            TextBoxeredmeny = Elozmenyek.SelectedValue.ToString();
            TB.Text = TextBoxeredmeny;
        }

        private void Backspace(object sender, RoutedEventArgs e)
        {
            if(TextBoxeredmeny != null)
               TextBoxeredmeny = TextBoxeredmeny.Remove(TextBoxeredmeny.Length - 1, 1);
            TB.Text = TextBoxeredmeny;
        }

        private void Torol(object sender, RoutedEventArgs e)
        {
            if (TextBoxeredmeny != null)
                TextBoxeredmeny = null;
            TB.Text = TextBoxeredmeny;
        }
    }
}
