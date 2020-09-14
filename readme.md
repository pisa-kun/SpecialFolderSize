特殊パス取得・サイズ表示。windowイベントサンプル
====


## Description
### 

```cs
        /// <summary>
        /// 位置変更時にX・Y更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_LocationChanged(object sender, EventArgs e)
        {
            this.windowLRTBText.Text = $"Width : {this.Width}, Height : {this.Height}";
            this.windowXYPosText.Text = $"X : {Application.Current.MainWindow.Left}, Y : {Application.Current.MainWindow.Top}";
            this.Board.Background = new SolidColorBrush(Colors.Yellow);
        }
```

## Author
pisa