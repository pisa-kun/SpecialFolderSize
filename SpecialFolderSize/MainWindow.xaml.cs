using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Media;

namespace SpecialFolderSize
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.windowLRTBText.Text = $"Width : {this.Width}, Height : {this.Height}";
            this.Board.Background = new SolidColorBrush(Color.FromArgb(0xff, 0xb3, 0x3e, 0x5c));
        }

        /// <summary>
        /// windowサイズ変更時にWidth・Height更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.windowLRTBText.Text = $"Width : {this.Width}, Height : {this.Height}";
            this.windowXYPosText.Text = $"X : {Application.Current.MainWindow.Left}, Y : {Application.Current.MainWindow.Top}";
            this.Board.Background = new SolidColorBrush(Colors.White);
        }

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

        /// <summary>
        /// スペシャルフォルダーのサイズを計算して、何か一つをランダム表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var pictures = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            var musics = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
            var videos = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);

            // 特殊フォルダが取得できない or セキュリティ時はエラーがでる
            var d_size = GetDirectorySize(new DirectoryInfo(documents));
            var p_size = GetDirectorySize(new DirectoryInfo(pictures));
            var m_size = GetDirectorySize(new DirectoryInfo(musics));
            var v_size = GetDirectorySize(new DirectoryInfo(videos));

            var d = new SpecialDirectory("ドキュメント", documents, d_size, Colors.Red);
            var p = new SpecialDirectory("ピクチャ", pictures, p_size, Colors.Blue);
            var m = new SpecialDirectory("ミュージック", musics, m_size, Colors.Green);
            var v = new SpecialDirectory("ビデオ", videos, v_size, Colors.Purple);

            var map = new Dictionary<SpecialFolderType, SpecialDirectory>();
            map[SpecialFolderType.Documents] = d;
            map[SpecialFolderType.Pictures] = p;
            map[SpecialFolderType.Musics] = m;
            map[SpecialFolderType.Videos] = v;

            var r = new Random();
            var seed = r.Next(0, 100);
            seed = seed % 4;

            switch(seed)
            {
                case (int)SpecialFolderType.Documents:
                    var value = map[SpecialFolderType.Documents];
                    ShowText.Text = $"{value.FolderName} : {value.FolderPath} ,{value.FolderSize}B";
                    this.Board.Background = new SolidColorBrush(value.Color);
                    break;
                case (int)SpecialFolderType.Pictures:
                    value = map[SpecialFolderType.Pictures];
                    ShowText.Text = $"{value.FolderName} : {value.FolderPath} ,{value.FolderSize}B";
                    this.Board.Background = new SolidColorBrush(value.Color);
                    break;
                case (int)SpecialFolderType.Musics:
                    value = map[SpecialFolderType.Musics];
                    ShowText.Text = $"{value.FolderName} : {value.FolderPath} ,{value.FolderSize}B";
                    this.Board.Background = new SolidColorBrush(value.Color);
                    break;
                case (int)SpecialFolderType.Videos:
                    value = map[SpecialFolderType.Videos];
                    ShowText.Text = $"{value.FolderName} : {value.FolderPath} ,{value.FolderSize}B";
                    this.Board.Background = new SolidColorBrush(value.Color);
                    break;
                default:
                    ShowText.Text = "default";
                    break;
            }

        }

        /// <summary>
        /// フォルダのサイズを取得する
        /// </summary>
        /// <param name="dirInfo">サイズを取得するフォルダ</param>
        /// <returns>フォルダのサイズ（バイト）</returns>
        public static long GetDirectorySize(DirectoryInfo dirInfo)
        {
            long size = 0;
            
            // 存在しない場合
            if(!dirInfo.Exists)
            {
                return size;
            }
            //フォルダ内の全ファイルの合計サイズを計算する
            foreach (FileInfo fi in dirInfo.GetFiles())
                size += fi.Length;

            //サブフォルダのサイズを合計していく
            foreach (DirectoryInfo di in dirInfo.GetDirectories())
                size += GetDirectorySize(di);

            //結果を返す
            return size;
        }
    }

    class SpecialDirectory
    {
        public string FolderName { get; private set; }

        public string FolderPath { get; private set; }

        public long FolderSize { get; private set; }

        public Color Color { get; private set; }

        public SpecialDirectory(string fn, string fp, long fs, Color c)
        {
            FolderName = fn;
            FolderPath = fp;
            FolderSize = fs;
            Color = c;
        }
    }

    public enum SpecialFolderType
    {
        Documents,
        Pictures,
        Musics,
        Videos,
    }
}
