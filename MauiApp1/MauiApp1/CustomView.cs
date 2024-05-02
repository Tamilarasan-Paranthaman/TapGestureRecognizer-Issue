using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Layouts;

namespace MauiApp1
{
    public class CustomControl : ControlLayout
    {
        
        CustomView customView;
        CustomView customView2;

        Label label;
        Label label2;

        public CustomControl()
        {
            TapGestureRecognizer tap = new TapGestureRecognizer();
            TapGestureRecognizer tap2 = new TapGestureRecognizer();
            
            customView = new CustomView();
            customView2 = new CustomView();

            label = new Label();
            label.Text = "View 1";
            label.BackgroundColor = Colors.Red;

            label2 = new Label();
            label2.Text = "View 2";
            label2.BackgroundColor = Colors.Blue;

            label.GestureRecognizers.Add(tap);
            label2.GestureRecognizers.Add(tap2);

            tap.Tapped += (s, e) =>
            {
                System.Diagnostics.Debug.WriteLine(label.Text + " Tapped");
            };

            tap2.Tapped += (s, e) =>
            {
                System.Diagnostics.Debug.WriteLine(label2.Text + " Tapped");
            };

            this.customView.Content = label;
            this.customView2.Content = label2;

            this.Add(customView);
            this.Add(customView2);
        }

        protected override ILayoutManager CreateLayoutManager()
        {
            return new ControlLayoutManager(this);
        }

        internal override Size LayoutArrangeChildren(Rect bounds)
        {
            (this.customView as IView).Arrange(new Rect(0, 0, 100, 50));
            (this.customView2 as IView).Arrange(new Rect(50, 0, 100, 50));
             
            // To clip the custom view 2
            this.customView2.Clip = new RectangleGeometry() { Rect = new Rect(50, 0, 100, 50) };
            return bounds.Size;
        }


        internal override Size LayoutMeasure(double widthConstraint, double heightConstraint)
        {
            this.label.HeightRequest = 50;
            label2.HeightRequest = 50;
            this.customView.Measure(100, 50);
            this.customView2.Measure(100, 50);
            return new Size(300, 300);
        }
    }

    internal class CustomView : ContentView
    {  

    }

    public abstract class ControlLayout : Layout
    {
        internal abstract Size LayoutArrangeChildren(Rect bounds);

        internal abstract Size LayoutMeasure(double widthConstraint, double heightConstraint);
    }

    internal class ControlLayoutManager : LayoutManager
    {
        ControlLayout layout;
        internal ControlLayoutManager(ControlLayout layout) : base(layout)
        {
            this.layout = layout;
        }

        public override Size ArrangeChildren(Rect bounds) => this.layout.LayoutArrangeChildren(bounds);

        public override Size Measure(double widthConstraint, double heightConstraint) => this.layout.LayoutMeasure(widthConstraint, heightConstraint);
    }
}
