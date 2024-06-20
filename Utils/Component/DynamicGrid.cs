using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Power_Hand.Utils.Component
{
    public class DynamicGrid: Panel
    {
        protected override Size MeasureOverride(Size availableSize)
        {
            int itemsCount = InternalChildren.Count;
            int rows = (int) Math.Ceiling(Math.Sqrt(itemsCount));
            int columns = (int) Math.Ceiling((double) itemsCount/rows);
            
            double maxWidth = 0;
            double maxHeight = 0;

            foreach (UIElement child in InternalChildren)
            {
                child.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                maxWidth = Math.Max(maxWidth, child.DesiredSize.Width);
                maxHeight = Math.Max(maxHeight, child.DesiredSize.Height);
            }

            return new Size(columns * maxWidth, rows * maxHeight);
        }


        protected override Size ArrangeOverride(Size finalSize)
        {
            int itemsCount = InternalChildren.Count;
            int rows = (int)Math.Ceiling(Math.Sqrt(itemsCount));
            int columns = (int) Math.Ceiling((double) itemsCount/rows);

            double width = finalSize.Width / columns;
            double height = finalSize.Height / rows;

            for (int i = 0; i < itemsCount;i++)
            {
                int row = i / columns;
                int column = i % columns;

                double x = column * width;
                double y = row * height;

                Rect rect = new Rect(x, y, width, height);
                InternalChildren[i].Arrange(rect);

                
            }

            return finalSize;
        }

    }
}
