// See https://aka.ms/new-console-template for more information

using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Diagnostics;
using System.Reflection.Metadata;

public class JepsiPepsi
{
    
    int xKant = 148;
    int yKant = 40;
    int batLængde = 10;
    bool igang = false;
    float batPos = -1;
    int batSpeed = 1;
    float ballspeed = 0.3f;
    

    // yKant = 40, default
    // xKant = 148, default
    public static void Main(string[] args)
    {
        


        JepsiPepsi jepsi = new();
        jepsi.batPos = jepsi.yKant / 2;

        int bally = jepsi.yKant / 2;
        int ballx = jepsi.xKant / 2;
        int lastY = bally;
        int lastX = ballx;

        BaneKant(jepsi.xKant, jepsi.yKant);
        PlayerBat(jepsi.xKant, jepsi.yKant, jepsi.batLængde, 0);
        
        Stopwatch timer = Stopwatch.StartNew();

        Random rnd = new Random();
        float ballAng;

        int o = rnd.Next(0, 4);
        if (o == 1 || o == 2)
        {
            ballAng = rnd.Next(100, 260);
        }
        else if (o == 3)
        {
            ballAng = rnd.Next(0, 80);
        }else
        {
            ballAng = rnd.Next(280, 360);
        }



        double x = 0;
        double y = 0;

        jepsi.igang = true;

       
        
        while (jepsi.igang)
        {

              if (jepsi.xKant / 20 - 2 <= ballx && jepsi.xKant / 20 + 3 >= ballx && jepsi.batPos + jepsi.batLængde / 2 >= bally && jepsi.batPos - jepsi.batLængde / 2 <= bally)
                {
                    ballAng = (180f - ballAng + 360f) % 360f;

                      y = 0;
                     x = 0;
                if (jepsi.xKant / 20 - 2 >= ballx)
                {
                    ballx = jepsi.xKant / 20 - 3;
                }
                else
                {
                    ballx = jepsi.xKant / 20 + 4;
                }
            }
            else if (jepsi.xKant / 20 <= ballx && jepsi.xKant / 20 + 1 >= ballx && (jepsi.batPos + jepsi.batLængde / 2 + 1 == bally || jepsi.batPos - jepsi.batLængde / 2 - 1 == bally))
            {
                ballAng = (360 - ballAng) % 360;

                y = 0;
                x = 0;
                if (jepsi.batPos + 1 + jepsi.batLængde / 2 == bally)
                {
                    bally = (int)Math.Round(jepsi.batPos + jepsi.batLængde / 2 + 2);
                }
                else
                {
                    bally = (int)Math.Round(jepsi.batPos - jepsi.batLængde / 2 - 2);
                }

            }



            if (1 >= ballx || jepsi.xKant - 3 <= ballx)//´´_
            {
                ballAng = (180f - ballAng + 360f) % 360f;
                y = 0;
                x = 0;
                if (1 >= ballx)
                {
                    ballx = 2;
                }
                else
                {
                    ballx = jepsi.xKant - 4;
                }
            }






            if (1 >= bally || jepsi.yKant - 3 <= bally) //´´|
            {
                ballAng = (360 - ballAng) % 360;

                y = 0;
                x = 0;
                if (1 >= bally)
                {
                    bally = 2;
                }
                else
                {
                    bally = jepsi.yKant - 4;
                }
            }

            double radians = ballAng * Math.PI / 180.0;

            x += Math.Cos(radians) * jepsi.ballspeed;

            y += Math.Sin(radians) * jepsi.ballspeed;

            ballx += (int)Math.Round(x);
            bally += (int)Math.Round(y);



            y -= (int)Math.Round(y);
            x -= (int)Math.Round(x);



            balls(ballx, bally, lastX, lastY, jepsi.xKant, jepsi.yKant);
            lastY = bally;
            lastX = ballx;



            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.W || key.Key == ConsoleKey.A)
                {
                    int i = -1;
                    if (jepsi.batPos == (jepsi.batLængde / 2) + 1)
                    {
                        i = 0;
                    }

                    if (jepsi.batPos - jepsi.batSpeed >= (jepsi.batLængde / 2) + 1)
                    {
                        jepsi.batPos -= jepsi.batSpeed;
                    }
                    else
                    {
                        jepsi.batPos = (jepsi.batLængde / 2) + 1;
                    }



                    PlayerBat(jepsi.xKant, (int)Math.Round(jepsi.batPos) * 2, jepsi.batLængde, i);
                }
                else if (key.Key == ConsoleKey.D || key.Key == ConsoleKey.S)
                {
                    int i = 1;
                    if (jepsi.batPos == (jepsi.yKant - jepsi.batLængde + 3))
                    {
                        i = 0;
                    }

                    if (jepsi.batPos + jepsi.batSpeed <= (jepsi.yKant - jepsi.batLængde) + 3)
                    {
                        jepsi.batPos += jepsi.batSpeed;
                    }
                    else
                    {
                        jepsi.batPos = jepsi.yKant - jepsi.batLængde + 3;
                    }

                    PlayerBat(jepsi.xKant, (int)Math.Round(jepsi.batPos) * 2, jepsi.batLængde, i);
                }

                
            }


            Thread.Sleep(5);
        }
        

       
        
           
         

        
    }



    public static void balls(int x, int y, int lastX, int lastY, int xKant, int yKant)
    {
        if (xKant != x && 0 != x && y != yKant && 0 != yKant)
        {
            Console.SetCursorPosition(lastX, lastY);
            Console.Write(" ");
        }
       

        Console.SetCursorPosition(x, y);
        Console.Write("o");
        Console.CursorVisible = false;
        
        
    }




    public static void PlayerBat(int xKant, int yKant, int batLængde, int badChange)
    {
        
        if (0 >  badChange)
        {
            Console.SetCursorPosition(xKant / 20, yKant / 2  + batLængde / 2 + 1);
            Console.Write("  ");
        }
        else if (0 < badChange)
        {
            Console.SetCursorPosition(xKant / 20, yKant / 2 - batLængde / 2 - 1);
            Console.Write("  ");
        }

        Console.SetCursorPosition(xKant / 20, yKant / 2 - batLængde / 2);
        Console.WriteLine("┌┐");
        for (int i = 1; i < batLængde; i++)
        {
            Console.SetCursorPosition(xKant / 20, yKant / 2 - batLængde/2 + i);
            Console.WriteLine("││");
        }
        Console.SetCursorPosition(xKant / 20, yKant / 2 + batLængde / 2);
        Console.WriteLine("└┘");
    }









    public static void BaneKant(int xKant, int yKant)
    {
        //top linje
        Console.Write("┌");
        for (int i = 0; i < xKant; i++)
        {
            Console.Write("─");
        }
        Console.Write("┐");
        //Midten
        for (int i = 0; i < yKant - 2; i++)
        {
            Console.Write("\n│");
            for (int o = 0; o < xKant; o++)
            {
                Console.Write(" ");
            }
            Console.Write("│");
        }
        // Bunden linje
        Console.Write("\n");
        Console.Write("└");
        for (int i = 0; i < xKant; i++)
        {
            Console.Write("─");
        }
        Console.Write("┘");

        
    }

    
}
