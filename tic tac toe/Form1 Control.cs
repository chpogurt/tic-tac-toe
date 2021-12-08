using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;

namespace Tic_Tac_Toe//каким магическим образом помесить ещё один кусок класса внутрь Form1.cs я не нашёл
{
    partial class Game //control event part
    {
        public Game()
        {
            InitializeComponent();
            Field[0, 0] = button1;
            DefaultEvent.Add(button1_Click);
            Field[0, 1] = button2;
            DefaultEvent.Add(button2_Click);
            Field[0, 2] = button3;
            DefaultEvent.Add(button3_Click);
            Field[1, 0] = button4;
            DefaultEvent.Add(button4_Click);
            Field[1, 1] = button5;
            DefaultEvent.Add(button5_Click);
            Field[1, 2] = button6;
            DefaultEvent.Add(button6_Click);
            Field[2, 0] = button7;
            DefaultEvent.Add(button7_Click);
            Field[2, 1] = button8;
            DefaultEvent.Add(button8_Click);
            Field[2, 2] = button9;
            DefaultEvent.Add(button9_Click);
            DefaultEvent.Add(button11_Click);
            DefaultEvent.Add(button12_Click);
            DefaultEvent.Add(button13_Click);
            DefaultEvent.Add(button14_Click);
        }

        private readonly List<EventHandler> DefaultEvent = new List<EventHandler>();

        private void button1_Click(object sender, EventArgs e)
        {
            ChangeField(button1, button1_Click);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ChangeField(button2, button2_Click);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ChangeField(button3, button3_Click);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ChangeField(button4, button4_Click);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ChangeField(button5, button5_Click);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ChangeField(button6, button6_Click);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ChangeField(button7, button7_Click);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ChangeField(button8, button8_Click);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ChangeField(button9, button9_Click);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            PlayerMove = true;
            foreach (var item in Field)
            {
                item.Enabled = true;
                item.UseVisualStyleBackColor = true;
                item.Text = "";
            }
            button1.Click -= new System.EventHandler(this.button1_Click);
            button1.Click += new System.EventHandler(this.button1_Click);
            button2.Click -= new System.EventHandler(this.button2_Click);
            button2.Click += new System.EventHandler(this.button2_Click);
            button3.Click -= new System.EventHandler(this.button3_Click);
            button3.Click += new System.EventHandler(this.button3_Click);
            button4.Click -= new System.EventHandler(this.button4_Click);
            button4.Click += new System.EventHandler(this.button4_Click);
            button5.Click -= new System.EventHandler(this.button5_Click);
            button5.Click += new System.EventHandler(this.button5_Click);
            button6.Click -= new System.EventHandler(this.button6_Click);
            button6.Click += new System.EventHandler(this.button6_Click);
            button7.Click -= new System.EventHandler(this.button7_Click);
            button7.Click += new System.EventHandler(this.button7_Click);
            button8.Click -= new System.EventHandler(this.button8_Click);
            button8.Click += new System.EventHandler(this.button8_Click);
            button9.Click -= new System.EventHandler(this.button9_Click);
            button9.Click += new System.EventHandler(this.button9_Click);
            button11.Click -= new System.EventHandler(this.button11_Click);
            button11.Click += new System.EventHandler(this.button11_Click);
            button11.Enabled = true;
            button12.Click -= new System.EventHandler(this.button12_Click);
            button12.Click += new System.EventHandler(this.button12_Click);
            button13.Click -= new System.EventHandler(this.button13_Click);
            button13.Click += new System.EventHandler(this.button13_Click);
            button14.Click -= new System.EventHandler(this.button14_Click);
            button14.Click += new System.EventHandler(this.button14_Click);
        }//reset

        private void button11_Click(object sender, EventArgs e)
        {
            PlayerMove = false;
            FirstStepPC();
            button11.Click -= button11_Click;
            button11.Enabled = false;
        }//second

        private void button12_Click(object sender, EventArgs e)
        {
            label1.Text = "Medium";
            button12.Enabled = false;
            button13.Enabled = true;
            button14.Enabled = true;
            Mode = false;
        }//medium

        private void button13_Click(object sender, EventArgs e)
        {
            label1.Text = "Hard";
            button12.Enabled = true;
            button13.Enabled = false;
            button14.Enabled = true;
            Mode = true;
        }//hard

        private void button14_Click(object sender, EventArgs e)
        {
            label1.Text = "Easy";
            button12.Enabled = true;
            button13.Enabled = true;
            button14.Enabled = false;
            Mode = null;
        }//easy

        //private void label1_udpate(object sender, EventArgs e)
        //{
        //    if (Mode == null)
        //        label1.Text = "Easy";
        //    else if (Mode == false)
        //        label1.Text = "Medium";
        //    else
        //        label1.Text = "Hard";
        //}
    }
}

