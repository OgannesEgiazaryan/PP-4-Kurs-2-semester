using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;

namespace WpfApp19.Classes
{


    internal class Captcha
    {
        public static string text_ = String.Empty;


        public static BitmapImage CreateImage(int width, int height)
        {
            if (width <= 0 || height <= 0)
            {
                throw new ArgumentException("Width and height must be greater than zero.");
            }

            Random rnd = new Random();

            RenderTargetBitmap result = new RenderTargetBitmap(width, height, 96, 96, PixelFormats.Pbgra32);

            Brush[] colors = { Brushes.Black, Brushes.Red, Brushes.RoyalBlue, Brushes.Green };

            DrawingVisual visual = new DrawingVisual();
            using (DrawingContext context = visual.RenderOpen())
            {
                Brush backgroundColor = new SolidColorBrush(Color.FromRgb((byte)rnd.Next(256), (byte)rnd.Next(256), (byte)rnd.Next(256)));
                context.DrawRectangle(backgroundColor, null, new Rect(0, 0, width, height));

                text_ = String.Empty;
                string ALF = "1234567890QWERTYUIOPASDFGHJKLZXCVBNM";
                for (int i = 0; i < 5; ++i)
                    text_ += ALF[rnd.Next(ALF.Length)];

                int textSize = 25;
                int maxTextWidth = width - 50;
                int maxTextHeight = height - 15;
                int Xpos = rnd.Next(0, maxTextWidth);
                int Ypos = rnd.Next(textSize, maxTextHeight);

                FontStyle[] fontStyles = { FontStyles.Normal, FontStyles.Italic };
                FontWeight[] fontWeights = { FontWeights.Normal, FontWeights.Bold };
                TextDecorationCollection[] textDecorations = { null, new TextDecorationCollection { new TextDecoration(TextDecorationLocation.Underline, null, 0, TextDecorationUnit.Pixel, TextDecorationUnit.Pixel) } };
                Typeface typeface = new Typeface("Arial");
                FormattedText formattedText = new FormattedText(text_, System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, typeface, textSize, colors[rnd.Next(colors.Length)]);
                formattedText.SetFontStyle(fontStyles[rnd.Next(fontStyles.Length)]);
                formattedText.SetFontWeight(fontWeights[rnd.Next(fontWeights.Length)]);
                formattedText.SetTextDecorations(textDecorations[rnd.Next(textDecorations.Length)]);

                context.DrawText(formattedText, new Point(Xpos, Ypos));

                for (int i = 0; i < 10; ++i)
                {
                    Point startPoint = new Point(rnd.Next(width), rnd.Next(height));
                    Point endPoint = new Point(rnd.Next(width), rnd.Next(height));
                    context.DrawLine(new Pen(Brushes.Black, 1), startPoint, endPoint);
                }

                for (int i = 0; i < width; ++i)
                {
                    for (int j = 0; j < height; ++j)
                    {
                        if (rnd.Next() % 20 == 0)
                        {
                            context.DrawRectangle(Brushes.White, null, new Rect(i, j, 1, 1));
                        }
                    }
                }
            }

            result.Render(visual);

            BitmapImage bitmapImage = new BitmapImage();
            using (var memoryStream = new System.IO.MemoryStream())
            {
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(result));
                encoder.Save(memoryStream);
                memoryStream.Position = 0;
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = memoryStream;
                bitmapImage.EndInit();
            }

            return bitmapImage;
        }
    }
}
