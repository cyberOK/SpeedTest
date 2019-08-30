using SpeedTest.RingSliceControl;
using System;
using System.Collections.Generic;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace SpeedTest.ArcControl
{
    public class ArcWithGradient : Canvas
    {
        #region Fields

        const double MaxPercentValue = 400;
        const double MaxAngle = 360;

        private List<Brush> brushCollection;
        private List<RingSlice> ringSliceCollection;

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
            this.brushCollection = new List<Brush>();
            this.ringSliceCollection = new List<RingSlice>();
        }

        #endregion

        #region Events Methods from Dependency Properties

        private static void OnSizePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ArcWithGradient control = (ArcWithGradient)d;
            control.SetBrushAndRingSliceCollections();
            control.SetControlSize();
            control.Draw();
        }

        private static void OnPercentValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ArcWithGradient control = (ArcWithGradient)d;
            control.SetControlSize();
            control.Draw();
        }

        #endregion

        #region Helpful methods

        private void SetBrushAndRingSliceCollections()
        {
            this.brushCollection.Clear();
            this.ringSliceCollection.Clear();

            for (int i = 0; i < MaxAngle; i++)
            {
                this.brushCollection.Add(new SolidColorBrush(this.Fill.Interpolate(this.FirstGradientColor, (double)i / MaxAngle)));
                this.ringSliceCollection.Add(new RingSlice() { StartAngle = i, EndAngle = i + 1, Fill = this.brushCollection[i], Stroke = this.brushCollection[i], Radius = this.Radius, InnerRadius = this.Radius - this.Thickness });
            }
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

        private void Draw()
        {
            if (this.EnableGradient)
            {
                Children.Clear();

                for (int i = 0; i < this.GetAngle(); i++)
                {
                    Children.Add(this.ringSliceCollection[i]);
                }
            }

            else
            {
                Children.Clear();

                Path radialStrip = GetCircleSegment(this.GetCenterPoint(), this.Radius, this.GetAngle());
                radialStrip.Stroke = GetGradientBrush(this.Fill, this.EnableGradient);
                radialStrip.StrokeThickness = this.Thickness;

                Children.Add(radialStrip);
            }
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
                angle = 359.999;
            }
            return angle;
        }

        private Brush GetGradientBrush(Color fillColor, bool enableGradient)
        {
            if (enableGradient)
            {
                GradientStopCollection gradientCollection = new GradientStopCollection();
                gradientCollection.Add(new GradientStop { Color = Colors.LightBlue, Offset = 0.9 });
                gradientCollection.Add(new GradientStop { Color = fillColor, Offset = 0.3 });

                LinearGradientBrush linearGradientBrush = new LinearGradientBrush
                {
                    StartPoint = new Point { X = 0.6, Y = 0.8 },
                    EndPoint = new Point { X = 0.4, Y = 1 },
                    GradientStops = gradientCollection,
                    ColorInterpolationMode = ColorInterpolationMode.ScRgbLinearInterpolation
                };

                return linearGradientBrush;
            }
            else
            {
                return new SolidColorBrush(fillColor);
            }
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

