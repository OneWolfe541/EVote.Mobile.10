using System;
using System.Collections.Generic;
using System.Drawing;
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
using EVote.Factories;
using EVote.Logging;
using EVote.Methods;
using EVote.Utilities.Models;

namespace EVote.Views
{
    /// <summary>
    /// Interaction logic for SignatureCaptureView.xaml
    /// </summary>
    public partial class SignatureCaptureView : UserControl
    {
        EVoteLogger _signatureLogger = new EVoteLogger("EVoteLogs", true);

        System.Windows.Point currentPoint = new System.Windows.Point();

        private IViewParametersModel _parameters;

        public SignatureCaptureView(IViewParametersModel Parameters)
        {
            InitializeComponent();

            _parameters = Parameters;
        }

        private void Canvas_MouseDown_1(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
                currentPoint = e.GetPosition((Canvas)sender);
        }

        private void Canvas_MouseMove_1(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Line line = new Line();

                line.Stroke = System.Windows.SystemColors.WindowFrameBrush;
                line.StrokeThickness = 3;
                line.X1 = currentPoint.X;
                line.Y1 = currentPoint.Y;
                line.X2 = e.GetPosition((Canvas)sender).X;
                line.Y2 = e.GetPosition((Canvas)sender).Y;

                currentPoint = e.GetPosition((Canvas)sender);

                paintSurface.Children.Add(line);
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveImage();

            SaveImageToDatabaseAsync();

            Navigation.SignatureResultsView(_parameters);
        }

        private void SaveImage()
        {
            paintSurface.UpdateLayout();

            var actualWidth = (int)paintSurface.ActualWidth + 600;
            var actualHeight = (int)paintSurface.ActualHeight + 100;
            var renderWidth = (int)paintSurface.RenderSize.Width;
            var renderHeight = (int)paintSurface.RenderSize.Height;

            //RenderTargetBitmap rtb = new RenderTargetBitmap(actualWidth,
            //actualHeight, 96d, 96d, System.Windows.Media.PixelFormats.Default);
            //rtb.Render(paintSurface);

            double dpi = 96.0;

            Rect bounds = VisualTreeHelper.GetDescendantBounds(paintSurface);

            var scale = dpi / 96.0;
            var width = (bounds.Width + bounds.X) * scale;
            var height = (bounds.Height + bounds.Y) * scale;

            RenderTargetBitmap rtb =
                new RenderTargetBitmap((int)Math.Round(width, MidpointRounding.AwayFromZero),
                (int)Math.Round(height, MidpointRounding.AwayFromZero),
                dpi, dpi, System.Windows.Media.PixelFormats.Default);

            DrawingVisual dv = new DrawingVisual();
            using (DrawingContext ctx = dv.RenderOpen())
            {
                VisualBrush vb = new VisualBrush(paintSurface);
                System.Windows.Media.Pen whitePen = new System.Windows.Media.Pen(System.Windows.Media.Brushes.White, 1);
                ctx.DrawRectangle(vb, whitePen,
                    new Rect(new System.Windows.Point(bounds.X, bounds.Y), new System.Windows.Point(width, height)));
            }

            rtb.Render(dv);

            //var crop = new CroppedBitmap(rtb, new Int32Rect(0, 0, 975, 300));
            //var bitmap = CreateResizedImage(crop, 650, 200, 0);
            var bitmap = new CroppedBitmap(rtb, new Int32Rect(0, 0, 975, 300));

            BitmapEncoder bmpEncoder = null;
            if (AppSettings.System.SignatureType == "jpg" || AppSettings.System.SignatureType == "jpeg")
            {
                bmpEncoder = new JpegBitmapEncoder();
                bmpEncoder.Frames.Add(BitmapFrame.Create(bitmap));
            }
            else if (AppSettings.System.SignatureType == "png")
            {
                bmpEncoder = new PngBitmapEncoder();
                bmpEncoder.Frames.Add(BitmapFrame.Create(bitmap));
            }            

            string folder = AppSettings.System.SignatureFolder;
            string path = folder + "\\" + _parameters.Voter.VoterID.ToString() + "." + AppSettings.System.SignatureType;

            // Check if folder exists and create it
            System.IO.Directory.CreateDirectory(folder);

            using (var fs = System.IO.File.OpenWrite(path))
            {
                bmpEncoder.Save(fs);
            }
        }

        private async void SaveImageToDatabaseAsync()
        {
            try
            {
                string folder = AppSettings.System.SignatureFolder;
                var image = SignatureMethods.LoadImageDataFromFile(_parameters.Voter, folder);
                if (AppSettings.TrainingMode == false)
                {
                    var signatureRecord = await SignatureMethods.SaveToDatabaseAsync(image, _parameters.Voter);
                    if (signatureRecord != null && AppSettings.OfflineMode == false)
                    {
                        UploadSignatureToAPI(signatureRecord);
                    }
                }
                else
                {
                    var signatureRecord = await SignatureMethods.SaveToTrainingDatabaseAsync(image, _parameters.Voter);
                }
            }
            catch (Exception e)
            {
                _signatureLogger.WriteLog("Signature Save Error: " + e.Message);
            }
        }

        private async void UploadSignatureToAPI(LocalDatabase.Signature signature)
        {
            try
            {
                var voterFactory = new VoterFactory();
                await voterFactory.UploadSignature(new EVote.Utilities.Models.Signature(signature), AppSettings.System.APIDB);
            }
            catch (Exception e)
            {
                _signatureLogger.WriteLog("Signature Upload Error: " + e.Message);                
            }
        }

        // https://stackoverflow.com/questions/15779564/resize-image-in-wpf
        private BitmapFrame CreateResizedImage(ImageSource source, int width, int height, int margin)
        {
            var rect = new Rect(margin, margin, width - margin * 2, height - margin * 2);

            var group = new DrawingGroup();
            RenderOptions.SetBitmapScalingMode(group, BitmapScalingMode.HighQuality);
            group.Children.Add(new ImageDrawing(source, rect));

            var drawingVisual = new DrawingVisual();
            using (var drawingContext = drawingVisual.RenderOpen())
            {
                group.Opacity = 1;
                drawingContext.DrawDrawing(group);
            }

            var resizedImage = new RenderTargetBitmap(
                width, height,         // Resized dimensions
                96, 96,                // Default DPI values
                PixelFormats.Default); // Default pixel format
            resizedImage.Render(drawingVisual);

            return BitmapFrame.Create(resizedImage);
        }

        public Bitmap ResizeImage(Bitmap imgToResize, System.Drawing.Size size)
        {
            try
            {
                Bitmap b = new Bitmap(size.Width, size.Height);
                using (Graphics g = Graphics.FromImage((System.Drawing.Image)b))
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.DrawImage(imgToResize, 0, 0, size.Width, size.Height);
                }
                return b;
            }
            catch
            {
                Console.WriteLine("Bitmap could not be resized");
                return imgToResize;
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            paintSurface.Children.Clear();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Navigation.VoterSearchView(null);
        }
    }
}
