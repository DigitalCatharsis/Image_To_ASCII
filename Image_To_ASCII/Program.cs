using System.Drawing;
using Image_To_ASCII;
using System.IO;

// настройки шрифтов консоли 8 consola

const double widthOffset = 2;  //фиксация размеров шрифта
const int maxWidth = 200;  //Разрешение  //474 is max
static Bitmap ResizeBitmap(Bitmap bitmap)
{
    var newHeight = bitmap.Height / widthOffset * maxWidth / bitmap.Width;
    if (bitmap.Width > maxWidth || bitmap.Height > newHeight)
    {
        bitmap = new Bitmap(bitmap, new Size(maxWidth, (int) newHeight));
    }
    return bitmap;
}

Console.ForegroundColor = ConsoleColor.Cyan;
//Console.BackgroundColor = ConsoleColor.Black;
//Console.ForegroundColor = ConsoleColor.White;

string path = "C:\\Temp\\1111111.jpg";

while (true)
{
    Console.ReadLine();

    if (path == null)
    {
        continue;
    }

    Console.Clear();

    var bitmap = new Bitmap(path);
    bitmap = ResizeBitmap(bitmap);
    bitmap.ToGrayScale();

    var converter = new BitmapToASCIIConverter(bitmap);
    var rows = converter.Convert();

    foreach (var row in rows)
    {
        Console.WriteLine(row);
    }

    var rowsNegative = converter.ConvertAsNegative();
    File.WriteAllLines("Image.Txt", rowsNegative.Select(r=> new string(r)));

    Console.SetCursorPosition(0,0);



}

