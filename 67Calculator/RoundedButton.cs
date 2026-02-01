using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace _67Calculator
{
    public class RoundedButton : Button
    {
        // ===== Fields =====
        private int borderRadius = 20;
        private int borderSize = 2;
        private Color borderColor = Color.Black;

        private Color hoverBackColor;    // background on hover
        private Color clickBackColor;    // background on click
        private Color _defaultBackColor; // stores the “normal” BackColor

        // ===== Properties =====
        [Category("Appearance")]
        public int BorderRadius
        {
            get => borderRadius;
            set { borderRadius = value; this.Invalidate(); }
        }

        [Category("Appearance")]
        public int BorderSize
        {
            get => borderSize;
            set { borderSize = value; this.Invalidate(); }
        }

        [Category("Appearance")]
        public Color BorderColor
        {
            get => borderColor;
            set { borderColor = value; this.Invalidate(); }
        }

        [Category("Appearance")]
        public Color HoverBackColor
        {
            get => hoverBackColor;
            set => hoverBackColor = value;
        }

        [Category("Appearance")]
        public Color ClickBackColor
        {
            get => clickBackColor;
            set => clickBackColor = value;
        }

        // ===== Constructor =====
        public RoundedButton()
        {
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.BackColor = Color.DodgerBlue;
            this.ForeColor = Color.White;
            this.Size = new Size(120, 40);

            _defaultBackColor = this.BackColor;

            // Mouse events
            this.MouseEnter += (s, e) =>
            {
                if (!this.DesignMode && hoverBackColor != Color.Empty)
                    base.BackColor = hoverBackColor;
            };

            this.MouseLeave += (s, e) =>
            {
                if (!this.DesignMode)
                    base.BackColor = _defaultBackColor;
            };

            this.MouseDown += (s, e) =>
            {
                if (!this.DesignMode && clickBackColor != Color.Empty)
                    base.BackColor = clickBackColor;
            };

            this.MouseUp += (s, e) =>
            {
                if (!this.DesignMode && hoverBackColor != Color.Empty)
                    base.BackColor = hoverBackColor;
                else
                    base.BackColor = _defaultBackColor;
            };

            this.Resize += (s, e) => this.Invalidate();
        }

        // ===== Override BackColor to store default =====
        public override Color BackColor
        {
            get => base.BackColor;
            set
            {
                base.BackColor = value;
                _defaultBackColor = value; // store default
                this.Invalidate();
            }
        }

        // ===== Paint =====
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rectSurface = this.ClientRectangle;
            Rectangle rectBorder = Rectangle.Inflate(rectSurface, -borderSize / 2, -borderSize / 2);
            int radius = Math.Min(borderRadius, Math.Min(this.Width, this.Height) / 2);

            // Draw rounded rectangle
            using (GraphicsPath pathSurface = GetRoundedPath(rectSurface, radius))
            using (GraphicsPath pathBorder = GetRoundedPath(rectBorder, radius - borderSize))
            using (Pen penBorder = new Pen(borderColor, borderSize))
            using (SolidBrush brushBackground = new SolidBrush(this.BackColor))
            {
                e.Graphics.FillPath(brushBackground, pathSurface);
                this.Region = new Region(pathSurface);
                if (borderSize > 0)
                    e.Graphics.DrawPath(penBorder, pathBorder);
            }

            // ===== Center and scale text =====
            string text = this.Text;
            Font drawFont = this.Font;
            SizeF textSize = e.Graphics.MeasureString(text, drawFont);

            // Shrink font if text is bigger than button
            while ((textSize.Width > rectSurface.Width - 8 || textSize.Height > rectSurface.Height - 8)
                   && drawFont.Size > 4) // minimum font size
            {
                drawFont = new Font(drawFont.FontFamily, drawFont.Size - 0.5f, drawFont.Style);
                textSize = e.Graphics.MeasureString(text, drawFont);
            }

            using (SolidBrush brushText = new SolidBrush(this.ForeColor))
            using (StringFormat sf = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            })
            {
                e.Graphics.DrawString(text, drawFont, brushText, rectSurface, sf);
            }
        }


        private GraphicsPath GetRoundedPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            float r = radius;

            path.StartFigure();
            path.AddArc(rect.X, rect.Y, r, r, 180, 90);
            path.AddArc(rect.Right - r, rect.Y, r, r, 270, 90);
            path.AddArc(rect.Right - r, rect.Bottom - r, r, r, 0, 90);
            path.AddArc(rect.X, rect.Bottom - r, r, r, 90, 90);
            path.CloseFigure();

            return path;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);

        }
    }
}
