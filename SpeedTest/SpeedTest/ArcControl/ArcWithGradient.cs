using SpeedTestIPerf.RingSliceControl;
using System;
using System.Collections.Generic;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace SpeedTestIPerf.ArcControl
{
    public class ArcWithGradient : Canvas
    {
        #region Fields

        private const double MaxAngle = 360;

        private readonly List<SolidColorBrush> brushCollection;
        private readonly List<RingSlice> ringSliceCollection;

        #endregion

        #region Dependency properties

        static readonly DependencyProperty RadiusProperty = DependencyProperty.Register("Radius", typeof(double), typeof(ArcWithGradient), new PropertyMetadata(50.0, OnSizePropertyChanged));
        static readonly DependencyProperty ThicknessProperty = DependencyProperty.Register("Thickness", typeof(double), typeof(ArcWithGradient), new PropertyMetadata(2.0, OnSizePropertyChanged));
        static readonly DependencyProperty FillProperty = DependencyProperty.Register("Fill", typeof(Color), typeof(ArcWithGradient), new PropertyMetadata(Color.FromArgb(1, 1, 1, 1), OnSizePropertyChanged));
        static readonly DependencyProperty FirstGradientColorProperty = DependencyProperty.Register(" FirstGradientColor", typeof(Color), typeof(ArcWithGradient), new PropertyMetadata(Color.FromArgb(1, 1, 1, 1), OnSizePropertyChanged));
        static readonly DependencyProperty PercentValueProperty = DependencyProperty.Register("PercentValue", typeof(double), typeof(ArcWithGradient), new PropertyMetadata(0.0, OnPercentValuePropertyChanged));
        static readonly DependencyProperty EnableGradientProperty = DependencyProperty.Register("EnableGradient", typeof(bool), typeof(ArcWithGradient), new PropertyMetadata(true, OnSizePropertyChanged));

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

        public Color FirstGradientColor
        {
            get => (Color)GetValue(FirstGradientColorProperty);
            set => SetValue(FirstGradientColorProperty, value);
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

        #region Constructor

        public ArcWithGradient()
        {
            this.brushCollection = new List<SolidColorBrush>();
            this.ringSliceCollection = new List<RingSlice>();

            this.InitializeBrushAndRingSliceCollections();
        }

        #endregion

        #region Events Methods from Dependency Properties

        private static void OnSizePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ArcWithGradient control = (ArcWithGradient)d;

            control.SetNewPropertyBrushAndRingSliceCollections();
            control.SetControlSize();
            control.Draw();
        }

        private static void OnPercentValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ArcWithGradient control = (ArcWithGradient)d;

            control.CollapsedChildrenVisibility();
            control.SetControlSize();
            control.Draw();
        }

        #endregion

        #region Helpful methods

        private void InitializeBrushAndRingSliceCollections()
        {
            for (int i = 0; i < MaxAngle; i++)
            {
                this.brushCollection.Add(new SolidColorBrush(this.Fill.Interpolate(this.FirstGradientColor, (double)i / MaxAngle)));
                this.ringSliceCollection.Add(new RingSlice() { StartAngle = i, EndAngle = i + 1, Fill = this.brushCollection[i], Stroke = this.brushCollection[i], Radius = this.Radius, InnerRadius = this.Radius - this.Thickness , Visibility = Visibility.Collapsed});
                Children.Add(this.ringSliceCollection[i]);
            }
        }

        private void SetNewPropertyBrushAndRingSliceCollections()
        {
            for (int i = 0; i < MaxAngle; i++)
            {
                this.brushCollection[i].Color = this.Fill.Interpolate(this.FirstGradientColor, (double)i / MaxAngle);

                this.ringSliceCollection[i].StartAngle = i;
                this.ringSliceCollection[i].EndAngle = i + 1;
                this.ringSliceCollection[i].Fill = this.brushCollection[i];
                this.ringSliceCollection[i].Stroke = this.brushCollection[i];
                this.ringSliceCollection[i].Radius = this.Radius;
                this.ringSliceCollection[i].InnerRadius = this.Radius - this.Thickness;
                this.ringSliceCollection[i].Visibility = Visibility.Collapsed;
            }
        }

        private void CollapsedChildrenVisibility()
        {
            for (int i = 0; i < MaxAngle; i++)
            {
                this.Children[i].Visibility = Visibility.Collapsed;
            }
        }

        private void Draw()
        {
            if (this.EnableGradient)
            {
                this.DrawArcGradient();         
            }

            else
            {
                this.DrawArcWithoutGradient();
            }
        }

        private void DrawArcGradient()
        {
            for (int i = 0; i < GetAngle(); i++)
            {
                var ringSlice = Children[i];
                ringSlice.Visibility = Visibility.Visible;
            }
        }

        private void DrawArcWithoutGradient()
        {
            Children.Clear();

            Path radialStrip = GetCircleSegment(this.GetCenterPoint(), this.Radius, this.GetAngle());
            radialStrip.Stroke = new SolidColorBrush(this.Fill);
            radialStrip.StrokeThickness = this.Thickness;

            Children.Add(radialStrip);
        }

        private void SetControlSize()
        {
            Width = this.Radius * 2;
            Height = this.Radius * 2;
        }

        private Point GetCenterPoint()
        {
            return new Point(this.Radius + (this.Thickness / 2), this.Radius + (this.Thickness / 2));
        }

        private double GetAngle()
        {
            double angle = 0;

            if (this.PercentValue <= 100) // under 100
            {
                angle = this.PercentValue / 100 * 225;
            }
            else if (this.PercentValue > 100) // before 100 to 400
            {
                angle = ((this.PercentValue - 100) / 300 * 135) + (225);
            }

            if (angle >= 360)
            {
                angle = 360;
            }
            return angle;
        }

        #endregion

        #region ArcWithGradientHelper methods

        private const double RADIANS = Math.PI / 180;

        private static Path GetCircleSegment(Point centerPoint, double radius, double angle)
        {
            Path path = new Path();
            PathGeometry pathGeometry = new PathGeometry();

            Point circleStart = new Point(centerPoint.X, centerPoint.Y + radius);

            ArcSegment ArcWithGradientSegment = new ArcSegment
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

            pathFigure.Segments.Add(ArcWithGradientSegment);
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

