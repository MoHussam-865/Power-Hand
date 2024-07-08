﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Refit;

namespace Power_Hand.Data.Other
{
    /// <summary>
    /// This is a Helper to handle password input in the Home View (Login Page)
    /// </summary>
    public static class PasswordBoxHelper
    {

        public static readonly DependencyProperty BoundPasswordProperty =
            DependencyProperty.RegisterAttached("BoundPassword", typeof(string), typeof(PasswordBoxHelper),
                new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault
                    ,OnBoundPasswordChanged));


        public static string GetBoundPassword(DependencyObject d)
        {
            return (string) d.GetValue(BoundPasswordProperty);
        }

        public static void SetBoundPassword(DependencyObject d, string value)
        {
            d.SetValue(BoundPasswordProperty, value);
        }

        private static void OnBoundPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is PasswordBox passwordBox)
            {
                passwordBox.PasswordChanged -= MyPasswordChanged;
                //passwordBox.Password = e.NewValue as string;
                passwordBox.PasswordChanged += MyPasswordChanged;
            }
        }

        private static void MyPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox passwordBox)
            {
                SetBoundPassword(passwordBox, passwordBox.Password);
            }   
        }
    }
}
