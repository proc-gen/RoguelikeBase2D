/* Generated by MyraPad at 2/25/2025 9:02:17 PM */
using Myra;
using Myra.Graphics2D;
using Myra.Graphics2D.TextureAtlases;
using Myra.Graphics2D.UI;
using Myra.Graphics2D.Brushes;
using Myra.Graphics2D.UI.Properties;
using FontStashSharp.RichText;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RoguelikeBase2D.Screens.Generated
{
    partial class GameWindow : Panel
    {
        private void BuildUI()
        {
            PlayerHealthBar = new Panel();
            PlayerHealthBar.HorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Left;
            PlayerHealthBar.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
            PlayerHealthBar.Width = 952;
            PlayerHealthBar.Height = 20;
            PlayerHealthBar.Background = new SolidBrush("#FE3930FF");
            PlayerHealthBar.Id = "PlayerHealthBar";

            PlayerHealthLabel = new Label();
            PlayerHealthLabel.Text = "0 / 0";
            PlayerHealthLabel.TextAlign = FontStashSharp.RichText.TextHorizontalAlignment.Center;
            PlayerHealthLabel.HorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Center;
            PlayerHealthLabel.Id = "PlayerHealthLabel";

            var panel1 = new Panel();
            panel1.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Top;
            panel1.Width = 956;
            panel1.Height = 24;
            panel1.BorderThickness = new Thickness(2);
            panel1.Border = new SolidBrush("#611612FF");
            panel1.Widgets.Add(PlayerHealthBar);
            panel1.Widgets.Add(PlayerHealthLabel);

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
            LogWindow.Widgets.Add(panel1);
            LogWindow.Widgets.Add(Log);

            var label1 = new Label();
            label1.Text = "Inventory";
            label1.Margin = new Thickness(8);
            label1.HorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Center;

            var label2 = new Label();
            label2.Text = "Backpack";
            label2.Margin = new Thickness(8);
            label2.HorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Center;

            BackpackList = new ListBox();
            BackpackList.Height = 160;
            BackpackList.Margin = new Thickness(8);
            BackpackList.Id = "BackpackList";

            var verticalStackPanel1 = new VerticalStackPanel();
            verticalStackPanel1.Width = 160;
            verticalStackPanel1.Margin = new Thickness(8);
            verticalStackPanel1.Widgets.Add(label2);
            verticalStackPanel1.Widgets.Add(BackpackList);

            var label3 = new Label();
            label3.Text = "Equipment";
            label3.Margin = new Thickness(8);
            label3.HorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Center;

            EquipmentTextbox = new TextBox();
            EquipmentTextbox.Multiline = true;
            EquipmentTextbox.Readonly = true;
            EquipmentTextbox.Margin = new Thickness(8);
            EquipmentTextbox.Background = new SolidBrush("#00000000");
            EquipmentTextbox.Id = "EquipmentTextbox";

            var verticalStackPanel2 = new VerticalStackPanel();
            verticalStackPanel2.Width = 160;
            verticalStackPanel2.Margin = new Thickness(8);
            verticalStackPanel2.Widgets.Add(label3);
            verticalStackPanel2.Widgets.Add(EquipmentTextbox);

            var horizontalStackPanel1 = new HorizontalStackPanel();
            horizontalStackPanel1.Widgets.Add(verticalStackPanel1);
            horizontalStackPanel1.Widgets.Add(verticalStackPanel2);

            InventoryPanel = new VerticalStackPanel();
            InventoryPanel.HorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Center;
            InventoryPanel.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
            InventoryPanel.BorderThickness = new Thickness(2);
            InventoryPanel.Visible = false;
            InventoryPanel.Background = new SolidBrush("#202020C0");
            InventoryPanel.Border = new SolidBrush("#42260BFF");
            InventoryPanel.Id = "InventoryPanel";
            InventoryPanel.Widgets.Add(label1);
            InventoryPanel.Widgets.Add(horizontalStackPanel1);


            Id = "GamePanel";
            Widgets.Add(LogWindow);
            Widgets.Add(InventoryPanel);
        }


        public Panel PlayerHealthBar;
        public Label PlayerHealthLabel;
        public TextBox Log;
        public Panel LogWindow;
        public ListBox BackpackList;
        public TextBox EquipmentTextbox;
        public VerticalStackPanel InventoryPanel;
    }
}
