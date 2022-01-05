using System;
using System.IO;
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
    private ListBox ClipListObj;
    private String ClipDataTxt;
    public MainWindow()
    {
      InitializeComponent();
      ClipListObj = this.ClipList;
      ClipDataTxt = System.Environment.CurrentDirectory + "\\data\\ClipData.csv";

      if (File.Exists(ClipDataTxt))
      {
        StreamReader rClipData = new StreamReader(ClipDataTxt);
        string line;
        while ((line = rClipData.ReadLine()) != null)
        {
          ClipListObj.Items.Add(line);
        }
        rClipData.Close();
      }
      else
      {
        StreamWriter wClipData = new StreamWriter(ClipDataTxt);
        wClipData.Write("");
        wClipData.Close();
      }
    }
    private void RegisterClipBtn_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      if (!(this.SaveTextBox.Text == ""))
      {
        this.ClipList.Items.Add(SaveTextBox.Text.ToString());
        FileStream WriterSr = new FileStream(ClipDataTxt, FileMode.Append);
        StreamWriter wClipData = new StreamWriter(WriterSr);
        wClipData.WriteLine(SaveTextBox.Text.ToString());
        wClipData.Close();
        WriterSr.Close();
        this.AppStatus.Content = this.RegisterClipBtn.Content.ToString();
      }
    }
    private void DeleteClipBtn_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      ClipListObj = this.ClipList;

      ClipSelectIndex = ClipListObj.SelectedIndex;

      if (ClipListObj.SelectedIndex < 0)
      {
        return;
      }
      else
      {
        if (ClipListObj.Items.Count >= 0)
        {
          ClipListObj.Items.RemoveAt(ClipSelectIndex);

          String ClipDataTmpFile = Environment.CurrentDirectory + "\\data\\ClipDataTmp.txt";

          // ClipData.txt のコピー
          File.Copy(ClipDataTxt, ClipDataTmpFile);

          // ClipDataTmp.txt を初期化
          FileStream fs = new FileStream(ClipDataTmpFile, FileMode.Create);
          StreamWriter wClipData = new StreamWriter(fs);
          wClipData.Write("");
          wClipData.Close();
          fs.Close();

          // ClipDataTmp.txt に現在のListBox値を出力
          fs = new FileStream(ClipDataTmpFile, FileMode.Append);
          wClipData = new StreamWriter(fs);

          foreach (String ListItem in ClipListObj.Items)
          {
            wClipData.WriteLine(ListItem.ToString());
          }
          wClipData.Close();
          fs.Close();

          // 問題なく出力が完了したらClipData.txt を削除
          File.Delete(ClipDataTxt);

          // ClipDataTmp.txt を ClipData.txt にリネーム
          File.Move(ClipDataTmpFile, ClipDataTxt);

          this.AppStatus.Content = this.DeleteClipBtn.Content.ToString();
          return;
        }
        else
        {
          return;
        }
      }
    }
    private void ClipList_SelectionChanged(object sender, System.Windows.RoutedEventArgs e)
    {
      ClipListObj = this.ClipList;

      if (ClipListObj.SelectedIndex < 0)
      {
        return;
      }

      ClipSelectIndex = ClipList.Items.Count;
      if (ClipSelectIndex > 0)
      {
        this.SaveTextBox.Text = this.ClipList.SelectedItem.ToString();
        Clipboard.SetText(this.ClipList.SelectedItem.ToString());
        this.AppStatus.Content = Clipboard.GetText() + "をクリップボードにコピーしました。";
      }
    }

  }
}
