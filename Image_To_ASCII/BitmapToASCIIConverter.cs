using System.Drawing;
using System.Runtime.InteropServices;

namespace Image_To_ASCII
{
    public class BitmapToASCIIConverter
    {
        private readonly char[] _asciiTable = { '.', ',', ':', '+', '*', '?', '%', 'S', '#', '@' };   //Яркость по возрастанию, эдакий градиент
        private readonly char[] _asciiTableNegative = { '@', '#', 'S', '%', '?', '*', '+', ':', ',', '.' };   //Яркость по возрастанию, эдакий градиент
        private readonly Bitmap _bitmap;

        public BitmapToASCIIConverter(Bitmap bitmap)
        {
            _bitmap = bitmap;
        }

        public char[][] Convert()
        {
            return Convertation(_asciiTable);
        }
        public char[][] ConvertAsNegative()
        {
            return Convertation(_asciiTableNegative);
        }

        private char[][] Convertation(char[] _asciiTable)
        {
            var result = new char[_bitmap.Height][];

            for (int y = 0; y < _bitmap.Height; y++)
            {
                result[y] = new char[_bitmap.Width];
                for (int x = 0; x < _bitmap.Width; x++)
                {
                    int mapIndex = (int)Map(_bitmap.GetPixel(x, y).R, 0, 255, 0, _asciiTable.Length - 1);
                    result[y][x] = _asciiTable[mapIndex];
                }
            }
            return result;
        }

        //Мапим значение из одного диапозона в другой (яркость)
        //start/stop 1 - диапозоны 0 - 255 пикселей в битпаме 
        //start/stop 2 - диаозон _asciiTable 
        private float Map(float valueToMap, float start1, float stop1, float start2, float stop2) 
        {
            return ((valueToMap - start1) / (stop1 - start1)) * (stop2 - start2) + start2;
        }
    }
}
