using Mpv.NET.Player;
using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace mpvCrash
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int Width, int Height, bool Repaint);
        //PInvoke declarations
        [DllImport("user32.dll", EntryPoint = "CreateWindowEx", CharSet = CharSet.Unicode)]
        internal static extern IntPtr CreateWindowEx(int dwExStyle,
                                                      string lpszClassName,
                                                      string lpszWindowName,
                                                      int style,
                                                      int x, int y,
                                                      int width, int height,
                                                      IntPtr hwndParent,
                                                      IntPtr hMenu,
                                                      IntPtr hInst,
                                                      [MarshalAs(UnmanagedType.AsAny)] object pvParam);

        [DllImport("user32.dll", EntryPoint = "DestroyWindow", CharSet = CharSet.Unicode)]
        internal static extern bool DestroyWindow(IntPtr hwnd);

        [DllImport("user32.dll")]
        private static extern Boolean ShowWindow(IntPtr hWnd, Int32 nCmdShow);

        IntPtr hwndHost; 
        IntPtr hwndHost2;
        private MpvPlayer player2;
        public MpvPlayer player;

        public MainWindow()
        {
            InitializeComponent();
            InitializePlayer();
        }

        private void InitializePlayer()
        {
            hwndHost = CreateWindowEx(0, "static", "window",
                  0x40000000 | 0x10000000,
                    0, 0,
                  0, 0,
                  new WindowInteropHelper(Application.Current.MainWindow).Handle,
                  (IntPtr)0x00000002,
                  IntPtr.Zero,
                    0);
            player = new MpvPlayer(hwndHost, @"libmpv-2.dll")
            {
                Loop = true,
                AutoPlay = true,
                LogLevel = Mpv.NET.API.MpvLogLevel.Debug,
                Volume = 100
            };

            //player.API.SetOptionString("vo", "gpu-next"); // hwdecoder enabled
            //player.API.SetOptionString("hwdec", "d3d11"); // hwdecoder enabled
            //player.API.SetOptionString("keepaspect", "no");

            player.Load("http://scircle-wowza.dcit.cz:1935/slowtv/slowtvzletisteen.stream/playlist.m3u8");

        }

        private void InitializePlayer2()
        {
            hwndHost2 = CreateWindowEx(0, "static", "window2",
                  0x40000000 | 0x10000000,
                    0, 0,
                  0, 0,
                  new WindowInteropHelper(Application.Current.MainWindow).Handle,
                  (IntPtr)0x00000002,
                  IntPtr.Zero,
                    0);
            player2 = new MpvPlayer(hwndHost, @"libmpv-2.dll")
            {
                Loop = true,
                AutoPlay = true,
                LogLevel = Mpv.NET.API.MpvLogLevel.Debug,
                Volume = 100
            };

            //player.API.SetOptionString("vo", "gpu-next"); // hwdecoder enabled
            //player2.API.SetOptionString("gpu-api", "vulkan");
            //player.API.SetOptionString("hwdec", "d3d11"); // hwdecoder enabled
            //player.API.SetOptionString("keepaspect", "no");

            player2.Load("http://scircle-wowza.dcit.cz:1935/slowtv/slowtvzletisteen.stream/playlist.m3u8");

        }

        private void disposePlayer_Click(object sender, RoutedEventArgs e)
        {
            player?.Dispose();
        }

        private void disposePlayer2_Click(object sender, RoutedEventArgs e)
        {
            player2?.Dispose();
        }

        private void reinitializePlayer_Click(object sender, RoutedEventArgs e)
        {
            InitializePlayer2();
        }
    }
}
