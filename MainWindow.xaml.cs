﻿using System;
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
using System.Diagnostics;

namespace Dziennik_Szkolny
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            LoginWindow lw = new LoginWindow();
            lw.Show();

            InitializeComponent();
            using (var context = new MyContext())
            {
                var studenci = context.Klasy.ToArray();
                var studenci1 = context.Studenci.ToArray();
                var Ocenki = context.Oceny.ToArray();
                var ilosc = studenci[studenci1[20].class_id].Uzytkownicy.Count;
                Test.Text = ilosc.ToString();
            }
        }
    }
}
