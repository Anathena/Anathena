using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.ComponentModel;

namespace WpfSample10
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        D3DImageSlimDX D3DImageContainer;
        Scene D3DScene;
        Stopwatch Timer;

        public Window1()
        {
            Timer = new Stopwatch();
            Loaded += Window_Loaded;
            Closing += Window_Closing;
            InitializeComponent();
        }

        void Window_Loaded(object sender, RoutedEventArgs e)
        {
            D3DImageContainer = new D3DImageSlimDX();
            D3DImageContainer.IsFrontBufferAvailableChanged += OnIsFrontBufferAvailableChanged;

            SlimDXImage.Source = D3DImageContainer;

            D3DScene = new Scene();
            SlimDX.Direct3D10.Texture2D Texture = D3DScene.SharedTexture;

            D3DImageContainer.SetBackBufferSlimDX(Texture);
            BeginRenderingScene();
        }

        void Window_Closing(object sender, CancelEventArgs e)
        {
            if (D3DImageContainer != null)
            {
                D3DImageContainer.Dispose();
                D3DImageContainer = null;
            }

            if (D3DScene != null)
            {
                D3DScene.Dispose();
                D3DScene = null;
            }
        }

        void OnRendering(object sender, EventArgs e)
        {
            D3DScene.Render(Timer.Elapsed.Milliseconds);
            D3DImageContainer.InvalidateD3DImage();
        }

        void BeginRenderingScene()
        {
            if (D3DImageContainer.IsFrontBufferAvailable)
            {
                foreach (var item in SlimDX.ObjectTable.Objects)
                {
                }

                SlimDX.Direct3D10.Texture2D Texture = D3DScene.SharedTexture;
                D3DImageContainer.SetBackBufferSlimDX(Texture);
                CompositionTarget.Rendering += OnRendering;

                Timer.Start();
            }
        }

        void StopRenderingScene()
        {
            Timer.Stop();
            CompositionTarget.Rendering -= OnRendering;
        }

        void OnIsFrontBufferAvailableChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            // This fires when the screensaver kicks in, the machine goes into sleep or hibernate
            // and any other catastrophic losses of the d3d device from WPF's point of view
            if (D3DImageContainer.IsFrontBufferAvailable)
            {
                BeginRenderingScene();
            }
            else
            {
                StopRenderingScene();
            }
        }

        void ButtonA_Clicked(object sender, RoutedEventArgs e)
        {
        }

        void ButtonB_Clicked(object sender, RoutedEventArgs e)
        {
        }
    }
}
