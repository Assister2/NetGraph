using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

public class HeatMap : Control
{
    private Bitmap heatmapBitmap;
    private Color[,] cellColors;
    private bool heatmapChanged = true;

    public HeatMap(int rows, int cols)
    {
        DoubleBuffered = true;

        cellColors = new Color[rows, cols];
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                cellColors[row, col] = Color.Empty;
            }
        }

        heatmapBitmap = new Bitmap(cols, rows, PixelFormat.Format32bppArgb);
    }

    public void SetCellColor(int row, int col, Color color)
    {
        row = (100 - row); // Invert Row
        if (cellColors[row, col] != color)
        {
            cellColors[row, col] = color;
            heatmapChanged = true;
            this.Invalidate();
        }
        
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        if (heatmapChanged)
        {
            DrawHeatmap();
            heatmapChanged = false;
        }

        e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
        e.Graphics.DrawImage(heatmapBitmap, ClientRectangle);
    }

    public void Reset()
    {
        for (int row = 0; row < cellColors.GetLength(0); row++)
        {
            for (int col = 0; col < cellColors.GetLength(1); col++)
            {
                cellColors[row, col] = Color.White;
            }
        }

        heatmapChanged = true;
        this.Invalidate();
    }

    private void DrawHeatmap()
    {
        Rectangle rect = new Rectangle(0, 0, heatmapBitmap.Width, heatmapBitmap.Height);
        BitmapData bmpData = heatmapBitmap.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

        unsafe
        {
            int* p = (int*)bmpData.Scan0;

            for (int row = 0; row < cellColors.GetLength(0); row++)
            {
                for (int col = 0; col < cellColors.GetLength(1); col++)
                {
                    Color color = cellColors[row, col];

                    if (color != Color.Empty)
                    {
                        *p++ = color.ToArgb();
                    }
                    else
                    {
                        *p++ = 0; // transparent
                    }
                }
            }
        }

        heatmapBitmap.UnlockBits(bmpData);
    }
}
