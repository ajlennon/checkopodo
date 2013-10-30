using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using GDIDB;
using ZedGraph;

namespace ClassLibTest
{
	public partial class Form1 : Form
	{
		private DBGraphics memGraphics;
		protected GraphPane myPane;

		public Form1()
		{
			InitializeComponent();

			memGraphics = new DBGraphics();
		}

		private void Form1_Load( object sender, EventArgs e )
		{
			memGraphics.CreateDoubleBuffer( this.CreateGraphics(),
				this.ClientRectangle.Width, this.ClientRectangle.Height );

			myPane = new GraphPane( new Rectangle( 10, 10, 10, 10 ),
				"Wacky Widget Company\nProduction Report",
				"Time, Days\n(Since Plant Construction Startup)",
				"Widget Production\n(units/hour)" );
			SetSize();

			double[] x = { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };
			double[] y = { 20, 10, 50, 25, 35, 75, 90, 40, 33, 50 };
			LineItem curve;
			curve = myPane.AddCurve( "Larry", x, y, Color.Green, SymbolType.Circle );
			curve.Line.Width = 1.5F;
			curve.Line.Fill = new Fill( Color.White, Color.FromArgb( 60, 190, 50 ), 90F );
			curve.Line.IsSmooth = true;
			curve.Line.SmoothTension = 0.6F;
			curve.Symbol.Fill = new Fill( Color.White );
			curve.Symbol.Size = 10;

			double[] x3 = { 150, 250, 400, 520, 780, 940 };
			double[] y3 = { 5.2, 49.0, 33.8, 88.57, 99.9, 36.8 };
			curve = myPane.AddCurve( "Moe", x3, y3, Color.FromArgb( 200, 55, 135 ), SymbolType.Triangle );
			curve.Line.Width = 1.5F;
			//curve.Line.IsSmooth = true;
			curve.Symbol.Fill = new Fill( Color.White );
			curve.Line.Fill = new Fill( Color.White, Color.FromArgb( 160, 230, 145, 205 ), 90F );
			curve.Symbol.Size = 10;

			double[] x4 = { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };
			double[] y4 = { 30, 45, 53, 60, 75, 83, 84, 79, 71, 57 };
			BarItem bar = myPane.AddBar( "Wheezy", x4, y4, Color.SteelBlue );
			bar.Bar.Fill = new Fill( Color.RosyBrown, Color.White, Color.RosyBrown );
			myPane.BarSettings.ClusterScaleWidth = 100;
			myPane.BarSettings.Type = BarType.Stack;

			double[] x2 = { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };
			double[] y2 = { 10, 15, 17, 20, 25, 27, 29, 26, 24, 18 };
			bar = myPane.AddBar( "Curly", x2, y2, Color.RoyalBlue );
			bar.Bar.Fill = new Fill( Color.RoyalBlue, Color.White, Color.RoyalBlue );
			myPane.BarSettings.ClusterScaleWidth = 100;

			myPane.Fill = new Fill( Color.WhiteSmoke, Color.Lavender, 0F );
			myPane.Chart.Fill = new Fill( Color.FromArgb( 255, 255, 245 ),
				Color.FromArgb( 255, 255, 190 ), 90F );

			myPane.XAxis.MajorGrid.IsVisible = true;
			myPane.YAxis.MajorGrid.IsVisible = true;
			myPane.YAxis.Scale.Max = 120;

			TextObj text = new TextObj( "First Prod\n21-Oct-93", 175F, 80.0F );
			text.Location.AlignH = AlignH.Center;
			text.Location.AlignV = AlignV.Bottom;
			text.FontSpec.Fill = new Fill( Color.White, Color.PowderBlue, 45F );
			myPane.GraphObjList.Add( text );

			ArrowObj arrow = new ArrowObj( Color.Black, 12F, 175F, 77F, 100F, 45F );
			arrow.Location.CoordinateFrame = CoordType.AxisXYScale;
			myPane.GraphObjList.Add( arrow );

			text = new TextObj( "Upgrade", 700F, 50.0F );
			text.FontSpec.Angle = 90;
			text.FontSpec.FontColor = Color.Black;
			text.Location.AlignH = AlignH.Right;
			text.Location.AlignV = AlignV.Center;
			text.FontSpec.Fill.IsVisible = false;
			text.FontSpec.Border.IsVisible = false;
			myPane.GraphObjList.Add( text );

			arrow = new ArrowObj( Color.Black, 15, 700, 53, 700, 80 );
			arrow.Location.CoordinateFrame = CoordType.AxisXYScale;
			arrow.PenWidth = 2.0F;
			myPane.GraphObjList.Add( arrow );

			text = new TextObj( "Confidential", 0.85F, -0.03F );
			text.Location.CoordinateFrame = CoordType.ChartFraction;

			text.FontSpec.Angle = 15.0F;
			text.FontSpec.FontColor = Color.Red;
			text.FontSpec.IsBold = true;
			text.FontSpec.Size = 16;
			text.FontSpec.Border.IsVisible = false;
			text.FontSpec.Border.Color = Color.Red;
			text.FontSpec.Fill.IsVisible = false;

			text.Location.AlignH = AlignH.Left;
			text.Location.AlignV = AlignV.Bottom;
			myPane.GraphObjList.Add( text );

			BoxObj box = new BoxObj( 0, 110, 1200, 10,
				Color.Empty, Color.FromArgb( 225, 245, 225 ) );
			box.Location.CoordinateFrame = CoordType.AxisXYScale;

			box.Location.AlignH = AlignH.Left;
			box.Location.AlignV = AlignV.Top;
			box.ZOrder = ZOrder.E_BehindAxis;
			myPane.GraphObjList.Add( box );

			text = new TextObj( "Peak Range", 1170, 105 );
			text.Location.CoordinateFrame = CoordType.AxisXYScale;
			text.Location.AlignH = AlignH.Right;
			text.Location.AlignV = AlignV.Center;
			text.FontSpec.IsItalic = true;
			text.FontSpec.IsBold = false;
			text.FontSpec.Fill.IsVisible = false;
			text.FontSpec.Border.IsVisible = false;
			myPane.GraphObjList.Add( text );

			myPane.AxisChange( this.CreateGraphics() );
		}

		protected override void OnPaintBackground( PaintEventArgs pevent )
		{
		}

		private void Form1_Paint( object sender, PaintEventArgs e )
		{
			SolidBrush brush = new SolidBrush( Color.Gray );
			if ( memGraphics.CanDoubleBuffer() )
			{
				// Fill in Background (for effieciency only the area that has been clipped)
				memGraphics.g.FillRectangle( new SolidBrush( SystemColors.Window ),
					e.ClipRectangle.X, e.ClipRectangle.Y,
					e.ClipRectangle.Width, e.ClipRectangle.Height );

				// Do our drawing using memGraphics.g instead e.Graphics

				memGraphics.g.FillRectangle( brush, this.ClientRectangle );

				if ( myPane != null )
					myPane.Draw( memGraphics.g );

				// Render to the form
				memGraphics.Render( e.Graphics );
			}
			else	// if double buffer is not available, do without it
			{
				e.Graphics.FillRectangle( brush, this.ClientRectangle );
				if ( myPane != null )
					myPane.Draw( e.Graphics );
			}
		}

		private void Form1_Resize( object sender, EventArgs e )
		{
			memGraphics.CreateDoubleBuffer( this.CreateGraphics(),
					this.ClientRectangle.Width, this.ClientRectangle.Height );
			SetSize();
			Invalidate();
		}

		private void SetSize()
		{
			if ( this.myPane != null )
			{
				Rectangle paneRect = this.ClientRectangle;
				paneRect.Inflate( -10, -10 );
				this.myPane.Rect = paneRect;
			}
		}
	}
}