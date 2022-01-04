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

namespace ToClip
{
  public partial class MainWindow : Window
  {
    private int ClipSelectIndex;
    public MainWindow()
    {
      InitializeComponent();
    }
    private void RegisterClipBtn_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      if (!(this.SaveTextBox.Text == ""))
      {
        this.ClipList.Items.Add(SaveTextBox.Text.ToString());
        this.SaveTextBox.Text = "";
        this.AppStatus.Content = this.RegisterClipBtn.Content.ToString();
      }
    }
    private void DeleteClipBtn_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      ListBox ClipListObj = this.ClipList;

      if (ClipListObj.SelectedIndex < 0)
      {
        return;
      }

      ClipSelectIndex = ClipListObj.Items.Count;
      if (ClipSelectIndex > 0)
      {
        ClipListObj.Items.Remove(ClipListObj.SelectedItem.ToString());
        this.AppStatus.Content = this.DeleteClipBtn.Content.ToString();
        return;
      }
    }
    private void ClipList_SelectionChanged(object sender, System.Windows.RoutedEventArgs e)
    {
      ListBox ClipListObj = this.ClipList;

      if (ClipListObj.SelectedIndex < 0)
      {
        return;
      }

      ClipSelectIndex = ClipList.Items.Count;
      if (ClipSelectIndex > 0)
      {
        this.SaveTextBox.Text = this.ClipList.SelectedItem.ToString();
        Clipboard.SetText(this.ClipList.SelectedItem.ToString());
        this.AppStatus.Content = "クリップボードにコピーしました。";
      }
    }

  }
}
