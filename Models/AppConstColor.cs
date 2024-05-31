using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VidyamAcademy.Models
{
    public class AppConstantColor
    {
        private static List<Color> _randomColorList = new List<Color>()
        {
            Colors.Orange,
            Colors.LightBlue,
            Colors.LightCoral,
            Colors.LightGray,
            Colors.LightSalmon,
            Colors.LightSeaGreen

        };
        public static Color GetRandomColor()
        {
            return _randomColorList[new Random().Next(_randomColorList.Count)];

        }
    }
}
