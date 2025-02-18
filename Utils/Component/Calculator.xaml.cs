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

namespace Power_Hand.View
{
    /// <summary>
    /// Interaction logic for Calculator.xaml
    /// </summary>
    public partial class Calculator : UserControl
    {


        public Visibility Visability
        {
            get { return (Visibility)GetValue(VisabilityProperty); }
            set { SetValue(VisabilityProperty, value); }
        }
        public static readonly DependencyProperty VisabilityProperty =
            DependencyProperty.Register("Visability", typeof(Visibility), typeof(UserControl), new PropertyMetadata(0));


        public Calculator()
        {
            InitializeComponent();
            this.DataContext = this;
        }
    }
}
