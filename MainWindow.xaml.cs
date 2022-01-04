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

namespace ToClip
{

  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
    }
    private void RegisterClipText_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      if (!(this.SaveTextBox.Text == ""))
      {
        this.ClipList.Items.Add(SaveTextBox.Text.ToString());
        this.SaveTextBox.Text = "";
      }
    }
    private void DeleteClipText_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      MessageBox.Show("削除");
    }

  }
}
