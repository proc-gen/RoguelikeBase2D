using Myra.Graphics2D.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguelikeBase2D.Utils.Myra
{
    public static class WidgetExtensions
    {
        public static bool FindWidgetToClick(this IMultipleItemsContainer baseWidget)
        {
            bool clicked = false;
            int index = 0;
            while (!clicked && index < baseWidget.Widgets.Count)
            {
                if (baseWidget.Widgets[index] is ImageTextButton button && button.IsKeyboardFocused)
                {
                    button.DoClick();
                    clicked = true;
                }
                else if (baseWidget.Widgets[index] is IMultipleItemsContainer widgetContainer && baseWidget.Widgets[index].Visible)
                {
                    clicked = widgetContainer.FindWidgetToClick();
                }
                index++;
            }
            return clicked;
        }

        public static List<T> FindAllWidgetsOfType<T>(this Widget baseWidget)
            where T : Widget
        {
            List<T> widgets = new List<T>();
            if (baseWidget is T)
            {
                widgets.Add(baseWidget as T);
            }
            if (baseWidget is IMultipleItemsContainer containerWidget)
            {
                foreach (var widget in containerWidget.Widgets)
                {
                    if (widget is IMultipleItemsContainer)
                    {
                        widgets.AddRange(widget.FindAllWidgetsOfType<T>());
                    }

                    else if (widget is T typedWidget)
                    {
                        widgets.Add(typedWidget);
                    }
                }
            }
            return widgets;
        }
    }
}
