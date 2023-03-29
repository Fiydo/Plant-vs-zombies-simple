using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
namespace Final_mulit_project
{
    public class actor
    {
        public int x, y,frame,hit=0,mod=0,plant,move_speed=40,eating=0,eating_time=1,eating_number;
        public Bitmap im;
        public List<Bitmap> Lim = new List<Bitmap>();
    }
    
    public partial class Form1 : Form
    {
        Bitmap off;
        Bitmap image=new Bitmap("4.bmp");
        Bitmap image2=new Bitmap("p2_2.bmp");
        List<actor> Lfloor = new List<actor>();
        List<actor> Lcar = new List<actor>();
        List<actor> zom = new List<actor>();
        List<actor> lplantes = new List<actor>();
        List<actor> lball = new List<actor>();
        List<actor> suns = new List<actor>();
        List<actor> tiles_2 = new List<actor>();
        actor tile21 = new actor();
        //phase2
        List<actor> Lzom2 = new List<actor>();
        actor zom2;
        actor hero;
        //phase2
        actor coin;
        actor tile23;
        actor tile22;
        actor tile24;
        Timer tt = new Timer();
        Random rr = new Random();
        Random r = new Random();
        SoundPlayer sp = new SoundPlayer("remix.wav");
        Rectangle Rsrc;
        Rectangle Rdest;
        Bitmap img;
        
        int flag = 0 // touch plant
            ,ct2=0//coins
            ,ct=0,ct3= 50//timer
            ,flag_car=0,game_over=0,fast=100,flag_fast=0, flag_frame_plant=0,phase2=0,flag_scroll=0,ct_scroll=0,up=0,up2=0,jump=0,ct_jump=0,ct_up;
        public Form1()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.Text = "PvZ";
            this.Load += Form1_Load;
            this.Paint += Form1_Paint;
            this.MouseDown += Form1_MouseDown;
            this.KeyDown += Form1_KeyDown;
            sp.PlayLooping();
            tt.Tick += Tt_Tick;
            tt.Start();
            
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (phase2 == 0)
            {
                if (e.KeyCode == Keys.M)
                {
                    ct2 += 1000;
                }
                if (e.KeyCode == Keys.N)
                {
                    ct2 = 0;
                }
                if (e.KeyCode == Keys.Enter && game_over == 1)
                {
                    for (int i = 0, r = 80; i < 7; i++, r += 200)
                    {
                        actor tile = new actor();
                        tile.x = 100;
                        tile.y = r;
                        tile.im = new Bitmap("5.bmp");
                        tile.im.MakeTransparent(tile.im.GetPixel(0, 0));
                        Lcar.Add(tile);
                    }
                    flag_car = 0;
                    lplantes.Clear();
                    zom.Clear();
                    ct2 = 0;
                    lball.Clear();
                    game_over = 0;
                    drawdubb(CreateGraphics());
                }
                if (e.KeyCode == Keys.K)
                {
                    for (int i = 0; i < zom.Count; i++)
                    {
                        zom[i].hit = 5;
                    }
                }
                if (e.KeyCode == Keys.F)
                {
                    fast = 10;

                }
                if (e.KeyCode == Keys.D2)
                {
                    phase2=1;

                }

            }
            else
            {
                if (e.KeyCode == Keys.Right)
                {

                    if (ct_scroll < 30)
                    {
                        Rdest.X += 30;
                        ct_scroll++;
                        if (ct_scroll == 30)
                        {
                            flag_scroll = 1;
                        }
                    }
                    for (int i=0;i<tiles_2.Count;i++)
                    {
                        tiles_2[i].x -= 15;
                    }
                    hero.x += 15;
                }
                if (e.KeyCode == Keys.Left)
                {

                    if (ct_scroll > 0)
                    {
                        Rdest.X -= 30;
                        ct_scroll--;
                        if (ct_scroll == 0)
                        {
                            flag_scroll = 0;
                        }
                    }
                    for (int i = 0; i < tiles_2.Count; i++)
                    {
                        tiles_2[i].x += 15;
                    }
                    hero.x -= 15;
                }
                if (e.KeyCode == Keys.Up)
                {
                    jump = 1;

                }
            }
        }

        private void Tt_Tick(object sender, EventArgs e)
        {
            if (phase2 == 0)
            {
                if (ct2 < 100)
                {
                    image = new Bitmap("4_1.bmp");

                }
                else
                {
                    image = new Bitmap("4.bmp");
                }
                if (ct2 < 175)
                {
                    image2 = new Bitmap("p2_2.bmp");
                }
                else
                {
                    image2 = new Bitmap("p2.bmp");
                }
                if (ct % 5 == 0)
                {
                    coin.frame++;

                    if (coin.frame == 6)
                    {
                        coin.frame = 0;
                    }
                    for (int i = 0; i < lplantes.Count; i++)
                    {

                        if (lplantes[i].frame == 3)
                        {
                            flag_frame_plant = 1;
                        }
                        if (lplantes[i].frame == 0)
                        {
                            flag_frame_plant = 0;
                        }
                        if (flag_frame_plant == 0)
                            lplantes[i].frame++;
                        else
                            lplantes[i].frame--;
                    }
                    for (int i = 0; i < zom.Count; i++)
                    {
                        if (zom[i].hit < 3)
                        {
                            zom[i].frame++;
                            if (zom[i].frame == 4)
                            {
                                zom[i].frame = 0;
                            }
                            if (zom[i].frame == 14)
                            {
                                zom[i].frame = 10;
                            }

                        }
                        else if (zom[i].hit >= 3 && zom[i].hit < 5)
                        {
                            zom[i].frame++;
                            if (zom[i].frame == 9)
                            {
                                zom[i].frame = 4;
                            }
                            if (zom[i].frame == 19)
                            {
                                zom[i].frame = 14;
                            }

                        }
                        else if (zom[i].hit >= 5)
                        {

                            if (zom[i].frame < 9)
                            {
                                zom[i].frame++;
                            }
                            if (zom[i].frame < 19 && zom[i].frame > 10)
                            {
                                zom[i].frame++;
                            }
                        }
                        if (zom[i].hit < 5 && zom[i].eating == 0)
                        {
                            zom[i].x -= zom[i].move_speed;
                        }
                    }

                }

                for (int i = 0; i < zom.Count; i++)
                {
                    for (int j = 0; j < lball.Count; j++)
                    {
                        if (lball[j].x > zom[i].x && lball[j].x < zom[i].x + zom[i].im.Width && lball[j].y > zom[i].y && lball[j].y < zom[i].y + zom[i].im.Height)
                        {
                            zom[i].hit++;
                            if (zom[i].hit <= 5)
                            {
                                if (lball[j].plant == 2 && zom[i].move_speed > 10)
                                {
                                    zom[i].move_speed = 10;
                                    zom[i].frame += 10;
                                }
                                lball.RemoveAt(j);
                            }
                        }

                    }
                    for (int j = 0; j < Lcar.Count; j++)
                    {
                        if (Lcar[j].x > zom[i].x && Lcar[j].x < zom[i].x + zom[i].im.Width && Lcar[j].y > zom[i].y && Lcar[j].y < zom[i].y + zom[i].im.Height)
                        {
                            zom[i].hit = 5;
                            if (zom[i].frame < 10)
                            {
                                zom[i].frame = 9;
                            }
                            else if (zom[i].frame >= 10)
                            {
                                zom[i].frame = 19;
                            }
                            flag_car = 1;
                        }
                    }
                    for (int j = 0; j < lplantes.Count; j++)
                    {
                        if (lplantes[j].x > zom[i].x && lplantes[j].x < zom[i].x + zom[i].im.Width && lplantes[j].y > zom[i].y && lplantes[j].y < zom[i].y + zom[i].im.Height)
                        {
                            zom[i].eating = 1;
                            zom[i].eating_number = j;
                        }
                    }
                    for (int j = 0; j < zom.Count; j++)
                    {

                        if (zom[i].eating == 1)
                        {
                            zom[i].eating_time++;
                            if (zom[i].eating_time % 500 == 0 && zom[i].hit < 5)
                            {
                                zom[i].eating = 0;
                                lplantes.RemoveAt(zom[i].eating_number);
                                zom[i].eating_number = '\0';
                            }
                        }

                    }
                    if (zom[i].x < 0)
                    {
                        game_over = 1;
                    }
                }
                if (lplantes.Count == 4 && flag_fast == 0)
                {
                    fast = 20;
                    flag_fast = 1;
                }
                if (ct3 % fast == 0)
                {
                    create_zombie();
                }
                if (ct3 % 20 == 0)
                {
                    actor sun = new actor();
                    sun.x = rr.Next(50, 1000);
                    sun.y = 5;
                    sun.im = new Bitmap("sun.bmp");
                    sun.im.MakeTransparent(sun.im.GetPixel(0, 0));
                    suns.Add(sun);
                }
                for (int i = 0; i < suns.Count; i++)
                {
                    suns[i].y += 5;

                }
                for (int i = 0; i < lplantes.Count; i++)
                {
                    if (lplantes[i].mod % 40 == 0)
                    {

                        actor ball = new actor();
                        ball.x = lplantes[i].x + 70;
                        ball.y = lplantes[i].y;
                        if (lplantes[i].plant == 1)
                        {
                            ball.im = new Bitmap("ball.bmp");
                            ball.im.MakeTransparent(ball.im.GetPixel(0, 0));
                            ball.plant = 1;
                        }
                        else if (lplantes[i].plant == 2)
                        {
                            ball.im = new Bitmap("ball2.bmp");
                            ball.im.MakeTransparent(ball.im.GetPixel(0, 0));
                            ball.plant = 2;
                        }
                        lball.Add(ball);


                    }
                    lplantes[i].mod++;
                }

                for (int i = 0; i < lball.Count; i++)
                {
                    lball[i].x += 20;
                }
                if (flag_car == 1)
                {
                    for (int i = 0; i < Lcar.Count; i++)
                    {
                        Lcar[i].x += 15;
                        if (Lcar[i].x > 1400)
                            Lcar.Clear();
                    }

                }
                ct++;
                ct3++;
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////phase22222222222222222
            else
            {
                hero.frame++;
                if (hero.frame==4)
                {
                    hero.frame = 0;
                }
                if (up==0)
                {
                    tile23.y += 15;
                    if (tile23.y>750)
                    {
                        up = 1;
                    }
                }
                else
                {
                    tile23.y -= 15;
                    if (tile23.y < 500)
                    {
                        up = 0;
                    }
                }
                if (up2 == 0)
                {
                    tile24.y += 10;
                    if (tile24.y > 750)
                    {
                        up2 = 1;
                    }
                }
                else
                {
                    tile24.y -= 10;
                    if (tile24.y < 500)
                    {
                        up2 = 0;
                    }
                }
                if (jump==1)
                {
                    hero.y -= 15;
                    ct_up++;
                    if (ct_up==5)
                    {
                        jump = 0;
                        ct_up = 0;
                    }
                }
                if (hero.x < tile21.x + tile21.im.Width && jump == 0)
                {
                    if (hero.y < 630)
                    {
                        hero.y += 15;
                    }

                }
                else if (hero.x> tile23.x&&hero.x < tile23.x + tile23.im.Width && hero.y>tile23.y-50&&hero.y<tile23.y+tile23.im.Height)
                {
                    hero.y = tile23.y - 50;
                }
                else if (hero.x > tile24.x && hero.x < tile24.x + tile24.im.Width  && hero.y > tile24.y-50 && hero.y < tile24.y + tile24.im.Height)
                {
                    hero.y = tile24.y - 50;
                }
                else
                {
                    if (jump != 1)
                    {
                        hero.y += 15;
                    }
                }
                ct_jump++;
            }
            drawdubb(CreateGraphics());
        }
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (phase2 == 0)
            {
                if (e.Button == MouseButtons.Left)
                {

                    if (e.X > 10 && e.X < 10 + image.Width
                            && e.Y > 80 && e.Y < 80 + image.Height && ct2 >= 100)
                    {
                        flag = 1;

                    }
                    else if (e.X > 10 && e.X < 10 + image.Width
                         && e.Y > 150 && e.Y < 150 + image.Height && ct2 >= 175)
                    {
                        flag = 2;

                    }
                    if (flag == 1)
                    {
                        for (int i = 0; i < Lfloor.Count; i++)
                        {
                            if (e.X > Lfloor[i].x && e.X < Lfloor[i].x + Lfloor[i].im.Width
                                && e.Y > Lfloor[i].y && e.Y < Lfloor[i].y + Lfloor[i].im.Height)
                            {
                                ct2 -= 100;
                                create_plants(i);
                                flag = 0;
                                break;
                            }
                        }
                    }
                    if (flag == 2)
                    {
                        for (int i = 0; i < Lfloor.Count; i++)
                        {
                            if (e.X > Lfloor[i].x && e.X < Lfloor[i].x + Lfloor[i].im.Width
                                && e.Y > Lfloor[i].y && e.Y < Lfloor[i].y + Lfloor[i].im.Height)
                            {
                                ct2 -= 175;
                                create_plants(i);
                                flag = 0;
                                break;
                            }
                        }
                    }
                    for (int i = 0; i < suns.Count; i++)
                    {
                        if (e.X > suns[i].x && e.X < suns[i].x + suns[i].im.Width && e.Y > suns[i].y && e.Y < suns[i].y + suns[i].im.Height)
                        {
                            ct2 += 25;
                            suns.RemoveAt(i);
                        }
                    }

                }
            }
            else
            {

            }
            drawdubb(CreateGraphics());
        }

        void drawscene(Graphics g)
        {
            if (phase2 == 0)
            {
                g.Clear(Color.Green);
                if (game_over != 1)
                {
                    g.DrawImage(new Bitmap("background.bmp"), 1275, 0);
                }
                if (game_over == 0)
                {
                    for (int i = 0; i < Lfloor.Count; i++)
                    {
                        g.DrawImage(Lfloor[i].im, Lfloor[i].x, Lfloor[i].y);
                    }
                    for (int i = 0; i < suns.Count; i++)
                    {
                        g.DrawImage(suns[i].im, suns[i].x, suns[i].y);
                    }
                    for (int i = 0; i < lball.Count; i++)
                    {
                        g.DrawImage(lball[i].im, lball[i].x, lball[i].y);
                    }
                    g.DrawImage(coin.Lim[coin.frame], coin.x, coin.y);
                    g.DrawString("" + ct2, new Font("system", 20), Brushes.Black, 55, 15);
                    for (int i = 0; i < zom.Count; i++)
                    {
                        g.DrawImage(zom[i].Lim[zom[i].frame], zom[i].x, zom[i].y);
                    }
                    for (int i = 0; i < lplantes.Count; i++)
                    {
                        if (lplantes[i].plant == 1)
                            g.DrawImage(lplantes[i].Lim[lplantes[i].frame], lplantes[i].x, lplantes[i].y);
                        else
                        {
                            g.DrawImage(lplantes[i].im, lplantes[i].x, lplantes[i].y);

                        }
                    }
                    for (int i = 0; i < Lcar.Count; i++)
                    {
                        g.DrawImage(Lcar[i].im, Lcar[i].x, Lcar[i].y);
                    }

                    g.DrawImage(image, 10, 80);
                    g.DrawImage(image2, 10, 150);

                }
                else
                {
                    g.DrawString("Game over", new Font("system", 70), Brushes.Black, 500, 300);
                    g.DrawString("press enter to begain", new Font("system", 30), Brushes.Black, 550, 400);

                }
            }
            else
            {
                g.DrawImage(img, Rsrc, Rdest, GraphicsUnit.Pixel);
                g.DrawImage(tile22.im, tile22.x, tile22.y);
                g.DrawImage(tile21.im, tile21.x, tile21.y);
                g.DrawImage(tile24.im,tile24.x, tile24.y);
                g.DrawImage(tile23.im, tile23.x,tile23.y);
                g.DrawImage(hero.Lim[hero.frame], hero.x, hero.y);
            }
        }
        void drawdubb(Graphics g)
        {
            Graphics g2;

                g2 = Graphics.FromImage(off);
                g.DrawImage(off, 0, 0);
            
            drawscene(g2);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            actor tile;
            int ct = 0;
            for (int i=0,r=70;i<4;i++,r+=200)
            {
                for (int j=0,c=207;j<10;j++,c+=107)
                {
                    tile = new actor();
                    tile.x = c;
                    tile.y = r;
                    if (ct == 0)
                    {
                        tile.im = new Bitmap("1.bmp");
                        ct = 1;
                    }
                    else
                    {
                        tile.im = new Bitmap("2.bmp");
                        ct = 0;
                    }
                    Lfloor.Add(tile);

                }
            }
            for (int i=0,r=80;i<7;i++,r+=200)
            {
                tile = new actor();
                tile.x = 100;
                tile.y = r;
                tile.im = new Bitmap("5.bmp");
                tile.im.MakeTransparent(tile.im.GetPixel(0, 0));
                Lcar.Add(tile);
                
            }
            create_coin();
            create_zombie();
            img = new Bitmap("backgron1.bmp");
           Rsrc = new Rectangle(0, 0, this.Width,this.Height);
            Rdest = new Rectangle(0, 0, img.Width/2, img.Height);
            off = new Bitmap(ClientSize.Width, ClientSize.Height);
            hero = new actor();
            hero.x = 100;
            hero.y = 630;
            hero.frame = 0;
            Bitmap pnn;
            hero.im = new Bitmap("p_1.bmp");
            hero.im.MakeTransparent(hero.im.GetPixel(0, 0));
            for (int k = 1; k <= 4; k++)
            {
                pnn = new Bitmap("p_" + (k) + ".bmp");
                pnn.MakeTransparent(pnn.GetPixel(0, 0));
                hero.Lim.Add(pnn);
            }
            tile23 = new actor();
            tile22 = new actor();
            tile24 = new actor();
            tile22.im = new Bitmap("tile.bmp");
            tile22.im.MakeTransparent(tile22.im.GetPixel(0, 0));
            tile22.x = 0;
            tile22.y = 700;
            tile23.x = tile22.x + tile22.im.Width + tile22.im.Width+30;
            tile23.y = 500;
            tile23.im = new Bitmap("tile.bmp");
            tile23.im.MakeTransparent(tile23.im.GetPixel(0, 0));
            tile24.x = tile22.x + tile22.im.Width + tile22.im.Width + 300;
            tile24.y = 700;
            tile24.im = new Bitmap("tile.bmp");
            tile24.im.MakeTransparent(tile24.im.GetPixel(0, 0));
            tile21.x = tile22.x + tile22.im.Width-30;
            tile21.y = tile22.y;
            tile21.im = new Bitmap("tile.bmp");
            tile21.im.MakeTransparent(tile21.im.GetPixel(0, 0));
            tiles_2.Add(tile21);
            tiles_2.Add(tile22);
            tiles_2.Add(tile23);
            tiles_2.Add(tile24);
        }
        void phase_2(Graphics g)
        {
            actor tile22 = new actor();
            tile22.im = new Bitmap("tile.bmp");
            tile22.im.MakeTransparent(tile22.im.GetPixel(0, 0));
            tile22.x = 0;
            tile22.y = 700;
            g.DrawImage(tile22.im, tile22.x, tile22.y);
        }
        void create_coin()
        {
            coin = new actor();
            coin.x = 5;
            coin.y = 5;
            coin.frame = 0;
            for (int i = 1; i <= 6; i++)
            {
                coin.im = new Bitmap("c" + i + ".bmp");
                coin.im.MakeTransparent(coin.im.GetPixel(0, 0));
                coin.Lim.Add(coin.im);
            }
        }
        void create_plants(int i)
        {
            actor plant = new actor();
            Bitmap pnn ;
            plant.frame = 0;
            plant.y = Lfloor[i].y + 5;
            if (flag == 1)
            {
                plant.x = Lfloor[i].x;
                plant.plant = 1;
                plant.im = new Bitmap("p_1.bmp");
                plant.im.MakeTransparent(plant.im.GetPixel(0, 0));
            }
            else if (flag==2)
            {
                plant.x = Lfloor[i].x-20;
                plant.plant = 2;
                plant.im = new Bitmap("3_2.bmp");
                plant.im.MakeTransparent(plant.im.GetPixel(0, 0));
            }
            for (int k=1;k<=4;k++)
            {
                pnn = new Bitmap("p_" + (k) + ".bmp");
                pnn.MakeTransparent(pnn.GetPixel(0, 0));
                plant.Lim.Add(pnn);
            }
            lplantes.Add(plant);
        }
        void create_zombie()
        {
            int[] priceArray = new int[] { 140, 340, 540, -50 };
            int randomIndex = r.Next(priceArray.Length);
            int randompos = priceArray[randomIndex];
            actor z = new actor();
            Bitmap img;
            z.x = 1500;
            z.y =randompos;
            z.frame = 0;
            z.im = new Bitmap("z1.bmp");
            for (int i = 1; i <= 20; i++)
            {
                img = new Bitmap("z" + i + ".bmp");
                img.MakeTransparent(img.GetPixel(0, 0));
                z.Lim.Add(img);
            }
            zom.Add(z);

        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            drawdubb(e.Graphics);
        }
    }
}
