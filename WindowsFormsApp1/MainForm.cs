﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameClient;
using GameClient.Models;

namespace GameClient
{
    public partial class MainForm : Form
    {
        private System.Drawing.Graphics formGraphics;

        RequestsController requestController = new RequestsController();
        Player myPlayer = new Player();
        String url1 = "";
        public MainForm()
        {
            InitializeComponent();
            formGraphics = this.CreateGraphics();
        }

        // pagetina visus playerius ir juos atvaizduoja, vėliau sukuria playeri esančiam klientui
        private async void Form1_LoadAsync(object sender, EventArgs e)
        {
            Console.WriteLine("0)\tGet all player");
            requestController.client.BaseAddress = new Uri("https://topdown-shooter.azurewebsites.net/");
            requestController.client.DefaultRequestHeaders.Accept.Clear();
            requestController.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(requestController.mediaType));

            ICollection<Player> playersList = await requestController.GetAllPlayerAsync(requestController.client.BaseAddress.PathAndQuery);

            Random rnd = new Random();

            foreach (Player p in playersList)
            {
                //Coordinates coordinates = new Coordinates
                //{
                //    Id = p.Id,
                //    PosX = rnd.Next(10, this.Width),
                //    PosY = rnd.Next(10, this.Height)
                //};
                //var patchStatusCode = await requestController.PatchPlayerAsync(coordinates);
                this.Form1_PaintDot((int)p.PosX, (int)p.PosY, Color.Red);
            }

            // Create a new player
            Console.WriteLine("1.1)\tCreate the player");
            myPlayer = new Player
            {
                Name = "Studentas-" + playersList.Count.ToString(),
                Score = 100,
                PosX = rnd.Next(10, this.Width),
                PosY = rnd.Next(10, this.Height)
            };

            var url = await requestController.CreatePlayerAsync(myPlayer);

            url1 = url.PathAndQuery;
            myPlayer = await requestController.GetPlayerAsync(url.PathAndQuery);

            this.Form1_PaintDot((int)myPlayer.PosX, (int)myPlayer.PosY, Color.Blue);

            playersList = await requestController.GetAllPlayerAsync(requestController.client.BaseAddress.PathAndQuery);

        }

        // paint dot
        private void Form1_PaintDot(int x, int y, Color color)
        {
            System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(color);
            Rectangle rec = new Rectangle(x, y, 10, 10);
            formGraphics.FillRectangle(myBrush, rec);
            myBrush.Dispose();

        }

        //deletint playeri?
        private async void Form1_FormClosingAsync(object sender, FormClosingEventArgs e)
        {
            //Delete the player
            //Console.WriteLine("4.3)\tDelete the player");
            //var statusCode = await requestController.DeletePlayerAsync(myPlayer.Id);
            //Console.WriteLine($"Deleted (HTTP Status = {(int)statusCode})");
        }

        //timeris getina visus playerius ir atvaizduoja pagal koordinates
        private async void timer1_TickAsync(object sender, EventArgs e)
        {
            this.Invalidate();
            ICollection<Player> playersList = await requestController.GetAllPlayerAsync(requestController.client.BaseAddress.PathAndQuery);

            Random rnd = new Random();

            foreach (Player p in playersList)
            {

                if (p.Id == myPlayer.Id)
                {
                    this.Form1_PaintDot((int)p.PosX, (int)p.PosY, Color.Blue);
                }
                else
                {
                    this.Form1_PaintDot((int)p.PosX, (int)p.PosY, Color.Red);
                }
            }
        }

        //reaguoja į paspaustus mygtukus
        private async void Form1_KeyPressAsync(object sender, KeyPressEventArgs e)
        {
            //atnaujina kliento objektą iš duombazes
            myPlayer = await requestController.GetPlayerAsync(url1);
            Coordinates coordinates = new Coordinates
            {
                Id = myPlayer.Id,
                PosX = myPlayer.PosX,
                PosY = myPlayer.PosY
            };
            var patchStatusCode = (Object)null;

            switch (e.KeyChar)
            {
                case 'w':
                    coordinates.PosY -= 15;
                    patchStatusCode = await requestController.PatchPlayerAsync(coordinates);
                    break;
                case 'a':
                    coordinates.PosX -= 15;
                    patchStatusCode = await requestController.PatchPlayerAsync(coordinates);
                    break;
                case 's':
                    coordinates.PosY += 15;
                    patchStatusCode = await requestController.PatchPlayerAsync(coordinates);
                    break;
                case 'd':
                    coordinates.PosX += 15;
                    patchStatusCode = await requestController.PatchPlayerAsync(coordinates);
                    break;
                case 'x':
                    var statusCode = await requestController.DeletePlayerAsync(myPlayer.Id);
                    this.Close();
                    break;
            }
        }
    }
}
