using System;

namespace Tic_Tac_Toe//каким магическим образом помесить ещё один кусок класса внутрь Form1.cs я не нашёл
{
    partial class Game //control event part
    {
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
                item.Click += null;
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
            button13.Enabled = true;
            button12.Enabled = false;
            Mode = false;
        }//medium

        private void button13_Click(object sender, EventArgs e)
        {
            button13.Enabled = false;
            button12.Enabled = true;
            Mode = true;
        }//hard

    }
}
