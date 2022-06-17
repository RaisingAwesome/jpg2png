using System.Drawing;
//dotnet publish -r win-x64 --self-contained true /p:PublishSingleFile=true -o pickme
public class jpg2png
{
    static int Main(string[] args)
    {
        // Display the number of command line arguments.
        string message=String.Empty;
        if (args.Length<2) message="Please provide an input and output filename";
        else if (!File.Exists(args[0])) message="Error: Input file does not exist";
        if (message!=String.Empty)
        {
            Console.WriteLine("\njpg2png by Sean J. Miller 2022\nConverts and crops jpg and png files to a png output file\nVisit us at www.raisingawesome.site\nusage:  jpg2png <input_filename> <output_PNG_filename>");
            Console.WriteLine(message);
            return 0;
        }

        return(DoIt(args));	    
    }
    private static int DoIt(string[] args){
        try{
            Image img = System.Drawing.Image.FromFile(args[0]);
            Bitmap myBM=new Bitmap(img);
            Rectangle myRect=GetAutoCropBounds(myBM);
            Image img2=cropImage(img,myRect);
            img2.Save(args[1], System.Drawing.Imaging.ImageFormat.Png);
        }
        catch{
            return(0);
        }
        return(1);
    }
    private static Image cropImage(Image img, Rectangle cropArea)
    {
        Bitmap bmpImage = new Bitmap(img);
        return bmpImage.Clone(cropArea, bmpImage.PixelFormat);
    }
    public static Rectangle GetAutoCropBounds(Bitmap bitmap)
    {
        int maxX = 0;
        int maxY = 0;

        int minX = bitmap.Width;
        int minY = bitmap.Height;

        for (int x = 0; x < bitmap.Width; x++)
        {
            for (int y = 0; y < bitmap.Height; y++)
            {
                var c = bitmap.GetPixel(x, y);
                var w = Color.White;
                if (c.R != w.R || c.G != w.G || c.B != w.B)
                {
                    if (x > maxX)
                        maxX = x;
                    if (x < minX)
                        minX = x;
                    if (y > maxY)
                        maxY = y;
                    if (y < minY)
                        minY = y;
                }
            }
        }

        
        //for a 15 pixel boarder        
        //maxX += 15; 
        //maxY += 30; 
        //return new Rectangle(minX-15, minY-15, maxX - minX, maxY - minY);
        
        //maxX += 2;
        return new Rectangle(minX, minY, maxX - minX, maxY - minY);
    }
    /*
    private static Image resizeImage(Image imgToResize, Size size)
    {
        int sourceWidth = imgToResize.Width;
        int sourceHeight = imgToResize.Height;

        float nPercent = 0;
        float nPercentW = 0;
        float nPercentH = 0;

        nPercentW = ((float)size.Width / (float)sourceWidth);
        nPercentH = ((float)size.Height / (float)sourceHeight);

        if (nPercentH < nPercentW)
            nPercent = nPercentH;
        else
            nPercent = nPercentW;

        int destWidth = (int)(sourceWidth * nPercent);
        int destHeight = (int)(sourceHeight * nPercent);

        Bitmap b = new Bitmap(destWidth, destHeight);
        Graphics g = Graphics.FromImage((Image)b);
        g.InterpolationMode = InterpolationMode.HighQualityBicubic;

        g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
        g.Dispose();

        return (Image)b;
    }
    */
}