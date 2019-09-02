using Windows.UI;

namespace SpeedTest.RingSliceControl
{
    public static class ColorExtension
    {
        // Interpolates two colors
        public static Color Interpolate(this Color color1, Color color2, double fraction)
        {
            if (fraction > 1)
            {
                fraction = 1;
            }

            if (fraction < 0)
            {
                fraction = 0;
            }

            Color result = new Color();
            result.A = 255;
            result.R = (byte)(color1.R * fraction + color2.R * (1 - fraction));
            result.G = (byte)(color1.G * fraction + color2.G * (1 - fraction));
            result.B = (byte)(color1.B * fraction + color2.B * (1 - fraction));

            return result;
        }
    }
}
