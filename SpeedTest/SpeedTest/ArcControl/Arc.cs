using System;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace SpeedTestUWP.ArcControl
{
    public class Arc : Canvas
    {
        #region Dependency properties

        static readonly DependencyProperty RadiusProperty = DependencyProperty.Register("Radius", typeof(double), typeof(Arc), new PropertyMetadata(50.0, OnSizePropertyChanged));
        static readonly DependencyProperty ThicknessProperty = DependencyProperty.Register("Thickness", typeof(double), typeof(Arc), new PropertyMetadata(2.0, OnSizePropertyChanged));
        static readonly DependencyProperty FillProperty = DependencyProperty.Register("Fill", typeof(Color), typeof(Arc), new PropertyMetadata(Color.FromArgb(1, 1, 1, 1), OnSizePropertyChanged));
        static readonly DependencyProperty PercentValueProperty = DependencyProperty.Register("PercentValue", typeof(double), typeof(Arc), new PropertyMetadata(0.0, OnPercentValuePropertyChanged));
        static readonly DependencyProperty EnableGradientProperty = DependencyProperty.Register("EnableGradient", typeof(bool), typeof(Arc), new PropertyMetadata(true, OnSizePropertyChanged));

        #endregion

        #region Properties

        public double Radius
        {
            get => (double)GetValue(RadiusProperty);
            set => SetValue(RadiusProperty, value);
        }

        public double Thickness
        {
            get => (double)GetValue(ThicknessProperty);
            set => SetValue(ThicknessProperty, value);
        }

        public Color Fill
        {
            get => (Color)GetValue(FillProperty);
            set => SetValue(FillProperty, value);
        }

        public double PercentValue
        {
            get => (double)GetValue(PercentValueProperty);
            set => SetValue(PercentValueProperty, value);
        }

        public bool EnableGradient
        {
            get => (bool)GetValue(EnableGradientProperty);
            set => SetValue(EnableGradientProperty, value);
        }

        #endregion

        #region Helpful methods

        private static void OnSizePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Arc control = (Arc)d;
            control.SetControlSize();
            control.Draw();
        }

        private static void OnPercentValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Arc control = (Arc)d;
            control.SetControlSize();
            control.Draw();
        }

        private void SetControlSize()
        {
            Width = this.Radius * 2 + this.Thickness;
            Height = this.Radius * 2 + this.Thickness;
        }

        private void Draw()
        {
            Children.Clear();

            Path radialStrip = GetCircleSegment(this.GetCenterPoint(), this.Radius, this.GetAngle());
            radialStrip.Stroke = new SolidColorBrush(this.Fill);           
            radialStrip.StrokeThickness = this.Thickness;

            Children.Add(radialStrip);
        }

        private Point GetCenterPoint()
        {
            return new Point(this.Radius + (this.Thickness / 2), this.Radius + (this.Thickness / 2));
        }

        private double GetAngle()
        {
            double angle = 0;

            if(this.PercentValue <= 100) // under 100
            {
                angle = this.PercentValue / 100 * 225;
            }
            else if(this.PercentValue > 100) // before 100 to 400
            {
                angle = ((this.PercentValue - 100) / 300 * 135) + (225);
            }

            if (angle >= 360)
            {
                angle = 359.999;
            }
            return angle;
        }

        #endregion

        #region ArcHelper methods

        private const double RADIANS = Math.PI / 180;

        private static Path GetCircleSegment(Point centerPoint, double radius, double angle)
        {
            Path path = new Path();
            PathGeometry pathGeometry = new PathGeometry();

            Point circleStart = new Point(centerPoint.X, centerPoint.Y + radius);

            ArcSegment arcSegment = new ArcSegment
            {
                IsLargeArc = angle > 180.0,
                Point = ScaleUnitCirclePoint(centerPoint, angle, radius),
                Size = new Size(radius, radius),
                SweepDirection = SweepDirection.Clockwise               
            };

            PathFigure pathFigure = new PathFigure
            {
                StartPoint = circleStart,
                IsClosed = false
            };

            pathFigure.Segments.Add(arcSegment);
            pathGeometry.Figures.Add(pathFigure);

            path.Data = pathGeometry;

            return path;
        }

        private static Point ScaleUnitCirclePoint(Point origin, double angle, double radius)
        {
            return new Point(origin.Y - Math.Sin(RADIANS * angle) * radius, origin.X + Math.Cos(RADIANS * angle) * radius);
        }

        #endregion
    }
}
