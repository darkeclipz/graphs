using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace Graph.Algorithms
{
    public class EuclidianGraph
    {
        public static void ToImage(Graph g)
        {
            int width = 800;
            int height = 800;
            int cx = width / 2;
            int cy = width / 2;
            int sx = 80;
            int sy = 80;
            float spread = 1.5f;

            var bmp = new Bitmap(width, height);
            var whiteBrush = Brushes.White;
            var whitePenThick = new Pen(whiteBrush)
            {
                Width = 2
            };
            var whitePenSharp = new Pen(whiteBrush);

            var positions = new List<Tuple<int, int>>();

            using (var graphics = Graphics.FromImage(bmp))
            {
                for(int v=0; v < g.Vertices; v++)
                {
                    int x = (int)(Math.Cos((float)v / g.Vertices * 2 * Math.PI) * cx / spread + cx);
                    int y = (int)(Math.Sin((float)v / g.Vertices * 2 * Math.PI) * cy / spread + cy);
                    positions.Add(new Tuple<int, int>(x, y));
                }

                for (int v = 0; v < g.Vertices; v++)
                {
                    var p1 = positions[v];
                    foreach(var u in g.Adjacent(v))
                    {
                        var p2 = positions[u];
                        graphics.DrawLine(whitePenSharp, new Point(p1.Item1, p1.Item2), new Point(p2.Item1, p2.Item2));
                    }
                }

                int label = 0;
                foreach(var p in positions)
                {                 
                    graphics.DrawEllipse(whitePenThick, new Rectangle(p.Item1 - sx / 2, p.Item2 - sy / 2, sx, sy));
                    graphics.DrawString((label++).ToString(), new Font(FontFamily.GenericSansSerif, sx / 4), Brushes.Cyan, new PointF(p.Item1 - sx / 6, p.Item2 - sy / 6));
                }

                
                graphics.Save();
            }


            using (var stream = new MemoryStream())
            {
                bmp.Save(stream, ImageFormat.Jpeg);
                using (var file = new FileStream($"graph_{DateTime.Now.ToString("yyyyMMdd_hhmmss_ffff")}.jpg", FileMode.Create))
                    stream.WriteTo(file);
            }
                
        }
    }
}
