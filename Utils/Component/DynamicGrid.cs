using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Power_Hand.Interfaces;

namespace Power_Hand.Utils.Component
{
    /// <summary>
    /// this is a panel set usually to GridPaginationView.xaml and others its set mainly in item control view and 
    /// responsable to arrange the items in each page (pagination) in "rows & columns" that are provided to the panel
    /// it arrange them with equal size it arrange them form top left to bottom right
    /// </summary>
    public class DynamicGrid: Panel
    {


        #region dependancy properties
        public int Rows
        {
            get { return (int)GetValue(RowsProperty); }
            set { SetValue(RowsProperty, value); }
        }
        public static readonly DependencyProperty RowsProperty =
            DependencyProperty.Register(nameof(Rows), typeof(int), typeof(DynamicGrid),
                new FrameworkPropertyMetadata(1,FrameworkPropertyMetadataOptions.AffectsMeasure));

        public int Columns
        {
            get { return (int)GetValue(ColumsProperty); }
            set { SetValue(ColumsProperty, value); }
        }
        public static readonly DependencyProperty ColumsProperty =
            DependencyProperty.Register(nameof(Columns), typeof(int), typeof(DynamicGrid),
                new FrameworkPropertyMetadata(1, FrameworkPropertyMetadataOptions.AffectsMeasure));


        private int ItemsPerPage => Rows * Columns;

        #endregion

        protected override Size MeasureOverride(Size availableSize)
        {
            if (InternalChildren.Count == 0 || ItemsPerPage == 0 || Columns ==0 || Rows ==0) return new(0, 0);

            double itemsWidth = double.IsInfinity(availableSize.Width)? 100: availableSize.Width / Columns;
            double itemsHeight = double.IsInfinity(availableSize.Height) ? 100 : availableSize.Height / Rows;

            var x = itemsWidth * itemsHeight;

            foreach (UIElement child in InternalChildren)
            {
                if (double.IsInfinity(itemsHeight) || double.IsInfinity(itemsWidth))
                {
                    return new(0, 0);
                }
                child.Measure(new Size(itemsWidth, itemsHeight));
            }
            var desieredWidth = itemsWidth * Columns;
            var desieredHeight = itemsHeight * Rows;

            return new Size(
                double.IsInfinity(desieredWidth) ? availableSize.Width: desieredWidth,
                double.IsInfinity(desieredHeight) ? availableSize.Height: desieredHeight
                );
        }


        protected override Size ArrangeOverride(Size finalSize)
        {
            int colums = Columns; 
            int rows = Rows;

            double itemsWidth = finalSize.Width / colums;
            double itemsHeight = finalSize.Height / rows;

            var x = itemsWidth * itemsHeight;

            for (int i = 0; i < InternalChildren.Count;i++)
            {
                int row = i / colums;
                int column = i % colums;

                Rect rect = new( column*itemsWidth, row*itemsHeight, itemsWidth,itemsHeight);
                
                InternalChildren[i].Arrange(rect);                
            }

            return finalSize;
        }

    }
}
