using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Drawing;
using SixLabors.ImageSharp.Processing.Overlays;
using SixLabors.ImageSharp.Processing.Text;
using SixLabors.Primitives;
using System;
using System.Collections.Generic;
using System.Threading;
using Ssd130;

namespace Ssd130.example
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Show netBot_white.bmp");
            using (Image<Rgba32> image = Image.Load("netBot_white.bmp"))
            using (SSD130 Ssd130 = new SSD130())
            {
                Ssd130.Begin();
                Ssd130.Clear();
                Ssd130.Display();
                Ssd130.Image(image);
                Ssd130.Display();
            }
            Console.WriteLine("Press enter to next example");
            Console.ReadLine();

            Console.WriteLine("Show animation");
            using (var image = new Image<Rgba32>(128, 32))
            using (SSD130 Ssd130 = new SSD130())
            {
                int w = 32;
                int h = 128;
                int x = 0;
                int y = 0;

                for (int i = 0; i < 1; i++)
                {
                    Ssd130.Begin();
                    Ssd130.Clear();
                    Ssd130.Display();

                    var limit = w / 2;
                    var _h = h;
                    var _w = w;

                    while (true)
                    {
                        image.Mutate(ctx => ctx
                        .BackgroundColor(Rgba32.Black)
                        .Draw(
                            new GraphicsOptions(false),
                            Rgba32.White,
                            1,
                            new RectangleF(x, y + 1, h - 1, w - 1)
                        ));
                        Ssd130.Image(image);
                        Ssd130.Display();

                        x += 1;
                        y = x;
                        h = _h - (x * 2);
                        w = _w - (x * 2);
                        //Thread.Sleep(1);
                        if (x >= limit) break;
                    }

                    w = 32;
                    h = 128;
                    x = 0;
                    y = 0;

                    while (true)
                    {
                        image.Mutate(ctx => ctx
                        .BackgroundColor(Rgba32.White)
                        .Draw(
                            new GraphicsOptions(false),
                            Rgba32.Black,
                            1,
                            new RectangleF(x, y + 1, h - 1, w - 1)
                        ));
                        Ssd130.Image(image);
                        Ssd130.Display();

                        x += 1;
                        y = x;
                        h = _h - (x * 2);
                        w = _w - (x * 2);
                        Thread.Sleep(1);
                        if (x >= limit) break;
                    }

                }
            }
            Console.WriteLine("Press enter to next example");
            Console.ReadLine();

            Console.WriteLine("Show clock");
            using (var image = new Image<Rgba32>(128, 32))
            using (SSD130 Ssd130 = new SSD130())
            {
                Ssd130.Begin();
                Ssd130.Clear();
                Ssd130.Display();

                List<string> fuentes = new List<string>()
                { "DejaVu Sans Light", "DejaVu Sans Mono","Piboto Thin", "Noto Mono" };
                fuentes = new List<string>() { "Noto Mono" };
                foreach (var fonts in fuentes)
                {
                    while (true)
                    {
                        var fontSize = 25;
                        var time = DateTime.Now;
                        var font = SystemFonts.CreateFont(fonts, fontSize, FontStyle.Regular);
                        image.Mutate(ctx => ctx
                            .Fill(Rgba32.Black)
                             .DrawText($"{time.Hour.ToString("D2")}:{time.Minute.ToString("D2")}:{time.Second.ToString("D2")}", 
                             font, Rgba32.White, new PointF(0, 0))
                        );
                        Ssd130.Image(image);
                        Ssd130.Display();

                        Thread.Sleep(100);
                    }
                }
            }
        }
    }
}