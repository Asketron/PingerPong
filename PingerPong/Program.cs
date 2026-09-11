// See https://aka.ms/new-console-template for more information

using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Diagnostics;
using System.Reflection.Metadata;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Globalization;
using System.Collections.Generic;


public class Block
{
    public int x1;
    public int x2;
    public int y1;
    public int y2;
}

public class JepsiPepsi
{

    List<Block> blocks = new List<Block>();
    
    int xKant = 148;
    int yKant = 40;
    int batLængde = 10;
    bool igang = false;
    float batPos = -1;
    int batSpeed = 1;
    float ballspeed = 0.3f;

    bool ballRunning = false;

    int blockX = 2;
    int blockY = 8;
    int blockNumX = 3;
    int blockNumY = 5;

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

       

        //Console.ForegroundColor = ConsoleColor.Red
        Console.OutputEncoding = System.Text.Encoding.UTF8;

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

        GetBlocks(jepsi.blockX, jepsi.blockY, jepsi.blockNumX, jepsi.blockNumY, jepsi.xKant, ref jepsi.blocks);
        MakeBlocks(jepsi.blocks);
        double x = 0;
        double y = 0;

        jepsi.igang = true;
        while (jepsi.igang)
        {
            bool hit = false;

            BallBounce(jepsi.xKant / 20, (int)(jepsi.batPos - jepsi.batLængde / 2) - 1, jepsi.xKant / 20 + 1, (int)(jepsi.batPos + jepsi.batLængde / 2) + 1, ref ballAng, ref ballx, ref bally, ref hit);

            if (hit == true)
            {
                y = 0;
                x = 0;
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
        Console.Write("●");
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
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("┌");
        Console.ForegroundColor = ConsoleColor.White;
        for (int i = 0; i < xKant; i++)
        {
            Console.Write("─");
        }
        Console.Write("┐");
        //Midten
        for (int i = 0; i < yKant - 2; i++)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("\n│");
            Console.ForegroundColor = ConsoleColor.White;

            for (int o = 0; o < xKant; o++)
            {
                Console.Write(" ");
            }
            Console.Write("│");
        }
        // Bunden linje
        Console.Write("\n");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("└");
        Console.ForegroundColor = ConsoleColor.White;
        for (int i = 0; i < xKant; i++)
        {
            Console.Write("─");
        }
        Console.Write("┘");

        
    }


    public static void BallBounce(int x1, int y1, int x2, int y2, ref float ang, ref int ballx, ref int bally, ref bool hit)
    {
        Random rnd = new Random();

        if (x1 - 2 <= ballx && x2 + 2 >= ballx && y2 >= bally && y1 <= bally) // med top/bund af bat   _
        {

            hit = true;

            int i = rnd.Next(-10, 10);

            ang = (180f - ang + 360f) % 360f + i;

            
            if (x1 - 2 >= ballx) // top
            {
                ballx = x1 - 3; // top
            }
            else
            {
                ballx = x2 + 3; // bund
            }
        }
        else if (x1 <= ballx && x2 >= ballx && (y2 == bally || y1 == bally)) // Kollidere med Siderne på bat  |
        {

            hit = true;
            int i = rnd.Next(-10, 10);

            ang = (360 - ang) % 360 + i;

            
            if (y2 == bally)
            {
                bally = y2 + 1; // højer
            }
            else
            {
                bally = y1 - 1; // venstre
            }

        }
    }

    public static void GetBlocks(int blockX, int blockY, int blockNumX, int blockNumY, int xKant, ref List<Block> blocks)
    {
        for (int i = 0; i < blockNumX; i++)
        {
            for (int o = 0; o < blockNumY; i++)
            {
                blocks.Add(new Block()
                {
                    x1 = xKant - blockX - blockX * i,
                    x2 = xKant - blockX * i,
                    y1 = o * blockY + 1,
                    y2 = o * blockY + blockY + 1

                });

            }




        }
    }

    public static void MakeBlocks(List<Block> blocks)
    {

    }


}
