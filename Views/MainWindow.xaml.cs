using SkiaSharp;
using SkiaSharp.Views.Desktop;
using SkiaSharp.Views.WPF;
using System.Windows;
using System.Windows.Input;
using TekstilDoktoru.ViewModels;
using System.Collections.Generic;

namespace TekstilDoktoru.Views
{
    public partial class MainWindow : Window
    {
        private bool isDrawingLine = false;
        private SKPoint startPoint;
        private SKPoint endPoint;
        private List<(SKPoint, SKPoint)> lines = new();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();

            SkiaCanvas.MouseLeftButtonDown += SkiaCanvas_MouseLeftButtonDown;
            SkiaCanvas.MouseMove += SkiaCanvas_MouseMove;
            SkiaCanvas.MouseLeftButtonUp += SkiaCanvas_MouseLeftButtonUp;
        }

        private void SkiaCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (isDrawingLine)
            {
                var p = e.GetPosition(SkiaCanvas);
                startPoint = new SKPoint((float)p.X, (float)p.Y);
            }
        }

        private void SkiaCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawingLine && e.LeftButton == MouseButtonState.Pressed)
            {
                var p = e.GetPosition(SkiaCanvas);
                endPoint = new SKPoint((float)p.X, (float)p.Y);
                SkiaCanvas.InvalidateVisual();
            }
        }

        private void SkiaCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (isDrawingLine)
            {
                var p = e.GetPosition(SkiaCanvas);
                endPoint = new SKPoint((float)p.X, (float)p.Y);
                lines.Add((startPoint, endPoint));
                isDrawingLine = false;
                SkiaCanvas.ReleaseMouseCapture();
                SkiaCanvas.InvalidateVisual();
            }
        }

        private void OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;
            canvas.Clear(SKColors.White);

            using var paint = new SKPaint
            {
                Color = SKColors.Black,
                StrokeWidth = 2,
                IsStroke = true
            };

            foreach (var (p1, p2) in lines)
            {
                canvas.DrawLine(p1, p2, paint);
            }

            if (isDrawingLine)
            {
                canvas.DrawLine(startPoint, endPoint, paint);
            }
        }

        private void LineTool_Click(object sender, RoutedEventArgs e)
        {
            isDrawingLine = true;
            SkiaCanvas.CaptureMouse();
        }
    }
}