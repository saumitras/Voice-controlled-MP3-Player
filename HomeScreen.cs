using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using Microsoft.DirectX.DirectSound;
using Microsoft.DirectX.DirectPlay;
using SpeechLib;
using System.Speech.Recognition;


using Microsoft.DirectX.AudioVideoPlayback;



namespace media_player
{
    public partial class Form1 : Form
    {
        SpeechRecognizer recognizer = new SpeechRecognizer();
        Audio song;
        String song_path;
        bool ismute = false;
        int temp=0;
        

        public Form1()
        {
            InitializeComponent();
            Choices ch = new Choices();
            ch.Add("PLAY");
            ch.Add("PAUSE");
            ch.Add("STOP");
            ch.Add("ONE");
            ch.Add("TWO");
            ch.Add("MUTE");
            ch.Add("PLUS");
            ch.Add("MINUS");
            //ch.Add("SHUT");

            Grammar gr = new Grammar(new GrammarBuilder(ch));
            gr.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(gr_SpeechRecognized);
            recognizer.LoadGrammar(gr);
        }

        void gr_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {

            if (e.Result.Text == "ADD")
            {
                timer1.Stop();
                timer2.Stop();

                OpenFileDialog dlg = new OpenFileDialog();
                dlg.ShowDialog();
                song_path = dlg.FileName.ToString();
                textBox1.Text = song_path;
                try
                {

                    song.Stop();
                }
                catch (Exception ex) { }


                try
                {
                    song = new Audio(song_path);
                    timer1.Interval = (int)(song.Duration * 10);
                    progressBar1.Value = 0;
                    //MessageBox.Show(timer1.Interval.ToString());
                }
                catch (Exception ex) { }
                
            }
            ///////////////////////////////////
            if (e.Result.Text == "PLAY")
            {
                try
                {
                    song.Play();
                    timer1.Start();
                    timer2.Start();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("NO SONG SELECTED FOR PLAYING");
                }


            }

            ///////////////////////////////////////
            if (e.Result.Text == "PAUSE")
            {
                try
                {
                    if (song.Playing)
                    {
                        song.Pause();

                    }

                }
                catch (Exception ex)
                {

                    MessageBox.Show("NO SONG PLAYING CURRENTLY");

                }

            }

            if (e.Result.Text == "STOP")
            {
                try
                {
                    if (song.Playing || song.Paused)
                    {
                        song.Stop();

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("NO SONG PLAYING CURRENTLY");
                }

            }
            //////////////////////////////////
            if (e.Result.Text == "ONE")        /////forward
            {
                try
                {
                    if (song.Paused || song.Playing)
                    {
                        if (song.CurrentPosition < (song.Duration - 5))
                            song.CurrentPosition = song.CurrentPosition + 5;
                        else
                            song.Stop();
                    }
                    else
                    {
                        MessageBox.Show("NO SONG PLAYING CURRENTLY");
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("NO SONG PLAYING CURRENTLY");
                }
            }

            ///////////////////////////////
            if (e.Result.Text == "TWO")
            {
                try
                {
                    if (song.Paused || song.Playing)
                    {
                        if (song.CurrentPosition > 5)
                            song.CurrentPosition = song.CurrentPosition - 5;
                        else
                            song.CurrentPosition = 0;
                    }
                    else
                    {
                        MessageBox.Show("NO SONG PLAYING CURRENTLY");
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("NO SONG PLAYING CURRENTLY");
                }

            }
            /////////////////////////
            if (e.Result.Text == "SHUT")
            {
                if (!ismute)
                {
                    temp = song.Volume;
                    song.Volume = -10000;
                    button9.Text = "UNMUTE";
                    ismute = true;
                }
                else
                {
                    song.Volume = temp;
                    button9.Text = "MUTE";
                    ismute = false;
                }
            }


            /////////////////////////
            if (e.Result.Text == "PLUS")
            {
                try
                {
                    song.Volume = song.Volume + 500;
                }
                catch (Exception ex)
                {

                }

            }

            ////////////////////////
            /////////////////////////
            if (e.Result.Text == "MINUS")
            {
                try
                {
                    song.Volume = song.Volume - 500;
                    if (song.Volume < -3000)
                        song.Volume = -3000;
                }
                catch (Exception ex)
                {

                }
            }

        }
   
        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            timer2.Stop();

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.ShowDialog();
            song_path = dlg.FileName.ToString();
            textBox1.Text = song_path;
            try
            {

                song.Stop();
            }
            catch (Exception ex) { }


            try
            {
                song = new Audio(song_path);
                timer1.Interval = (int)(song.Duration * 10);
                progressBar1.Value = 0;
                //MessageBox.Show(timer1.Interval.ToString());
            }
            catch (Exception ex) { }
                       
        
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                song.Play();
                timer1.Start();
                timer2.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("NO SONG SELECTED FOR PLAYING");
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (song.Playing)
                {
                    song.Pause();
                    
                }
                
            }
            catch (Exception ex)
            {

                MessageBox.Show("NO SONG PLAYING CURRENTLY");
                
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
                   
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (song.Playing || song.Paused)
                {
                    song.Stop();
                    progressBar1.Value = 0;
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("NO SONG PLAYING CURRENTLY");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

            try
            {
                if (song.Paused || song.Playing)
                {
                    if (song.CurrentPosition < (song.Duration - 5))
                        song.CurrentPosition = song.CurrentPosition + 5;
                    else
                        song.Stop();
                }
                else
                {
                    MessageBox.Show("NO SONG PLAYING CURRENTLY");
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("NO SONG PLAYING CURRENTLY");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (song.Paused || song.Playing)
                {
                    if (song.CurrentPosition > 5)
                        song.CurrentPosition = song.CurrentPosition - 5;
                    else
                        song.CurrentPosition = 0; 
                }
                else
                {
                    MessageBox.Show("NO SONG PLAYING CURRENTLY");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("NO SONG PLAYING CURRENTLY");
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                song.Volume = song.Volume + 500;
            }
            catch (Exception ex)
            {
                
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                song.Volume = song.Volume - 500;
                if (song.Volume < -3000)
                    song.Volume = -3000;
            }
            catch (Exception ex)
            {
                
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (!ismute)
            {
                temp = song.Volume;
                song.Volume = -10000;
                button9.Text = "UNMUTE";
                ismute = true;
            }
            else
            {
                song.Volume = temp;
                button9.Text = "MUTE";
                ismute = false;
            }
            
        }


        private void timer1_Tick(object sender, EventArgs e)
        {

            textBox2.Text = ((int)(song.Duration)).ToString();
            textBox3.Text = ((int)(song.CurrentPosition)).ToString();

            textBox4.Text = ((int)(song.Duration - song.CurrentPosition)).ToString();
            //label3.Text = song.CurrentPosition.ToString();

            //label4.Text = interval.ToString();

            //if((song.CurrentPosition % interval)==0)
            progressBar1.Increment(1);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            textBox2.Text = ((int)(song.Duration)).ToString();
            textBox3.Text = ((int)(song.CurrentPosition)).ToString();

            textBox4.Text = ((int)(song.Duration - song.CurrentPosition)).ToString();

        }

       
    }
}
