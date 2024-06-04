namespace VidyamAcademy.Models
{
    public class AppConstantColor
    {
        private static List<Color> _randomColorList = new List<Color>()
        {
            Colors.Orange,
            Colors.LightBlue,
            Colors.LightCoral,
            Colors.Teal,
            Colors.LightCyan,
            Colors.LightSteelBlue,
            Colors.LightGrey,
            Colors.LightGoldenrodYellow,
            Colors.Violet,
            Colors.ForestGreen,
            Colors.LightSalmon,
            Colors.LightSeaGreen,
            Colors.Maroon

        };
        public static Color GetRandomColor()
        {
            return _randomColorList[new Random().Next(_randomColorList.Count)];

        }
    }
}
