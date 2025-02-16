/* Generated by MyraPad at 2/16/2025 4:59:58 PM */
using Myra;
using Myra.Graphics2D;
using Myra.Graphics2D.TextureAtlases;
using Myra.Graphics2D.UI;
using Myra.Graphics2D.Brushes;
using Myra.Graphics2D.UI.Properties;
using FontStashSharp.RichText;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RoguelikeBase2D.Windows.Generated
{
	partial class GameWindow: Panel
	{
		private void BuildUI()
		{
			var panel1 = new Panel();
			panel1.HorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Center;
			panel1.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
			panel1.Width = 952;
			panel1.Height = 20;
			panel1.Background = new SolidBrush("#FE3930FF");

			PlayerHealthLabel = new Label();
			PlayerHealthLabel.Text = "0 / 0";
			PlayerHealthLabel.TextAlign = FontStashSharp.RichText.TextHorizontalAlignment.Center;
			PlayerHealthLabel.HorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Center;
			PlayerHealthLabel.Id = "PlayerHealthLabel";

			var panel2 = new Panel();
			panel2.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Top;
			panel2.Width = 956;
			panel2.Height = 24;
			panel2.BorderThickness = new Thickness(2);
			panel2.Border = new SolidBrush("#611612FF");
			panel2.Widgets.Add(panel1);
			panel2.Widgets.Add(PlayerHealthLabel);

			Log = new TextBox();
			Log.Multiline = true;
			Log.Wrap = true;
			Log.Readonly = true;
			Log.Margin = new Thickness(0, 32, 0, 0);
			Log.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Stretch;
			Log.Background = new SolidBrush("#00000000");
			Log.Id = "Log";

			LogWindow = new Panel();
			LogWindow.HorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Left;
			LogWindow.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Bottom;
			LogWindow.Width = 960;
			LogWindow.Height = 162;
			LogWindow.BorderThickness = new Thickness(2);
			LogWindow.Background = new SolidBrush("#202020C0");
			LogWindow.Border = new SolidBrush("#42260BFF");
			LogWindow.Id = "LogWindow";
			LogWindow.Widgets.Add(panel2);
			LogWindow.Widgets.Add(Log);

			
			Id = "GamePanel";
			Widgets.Add(LogWindow);
		}

		
		public Label PlayerHealthLabel;
		public TextBox Log;
		public Panel LogWindow;
	}
}
