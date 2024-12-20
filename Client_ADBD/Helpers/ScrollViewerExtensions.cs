using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Client_ADBD.Helpers
{
    public static class ScrollViewerExtensions
    {
        public static void ScrollToElement(this ScrollViewer scrollViewer, UIElement element)
        {
            if (element != null)
            {
                var rect = element.TransformToAncestor(scrollViewer).TransformBounds(new Rect(0, 0, element.RenderSize.Width, element.RenderSize.Height));
                scrollViewer.ScrollToVerticalOffset(rect.Top);
            }
        }
    }
}
