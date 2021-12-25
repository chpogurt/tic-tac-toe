using System;
using SCE;

namespace Tic_Tac_Toe//каким магическим образом помесить ещё один кусок класса внутрь Form1.cs я не нашёл
{
    partial class Game //control event part
    {
        public Game()
        {
            InitializeComponent();
            ControlCondition.AddClear(button1, button1_Click);
            ControlCondition.AddClear(button2, button2_Click);
            ControlCondition.AddClear(button3, button3_Click);
            ControlCondition.AddClear(button4, button4_Click);
            ControlCondition.AddClear(button5, button5_Click);
            ControlCondition.AddClear(button6, button6_Click);
            ControlCondition.AddClear(button7, button7_Click);
            ControlCondition.AddClear(button8, button8_Click);
            ControlCondition.AddClear(button9, button9_Click);
            ControlCondition.AddClear(button10, button10_Click);
            ControlCondition.AddClear(button11, button11_Click);
            ControlCondition.AddClear(button12, button12_Click);
            ControlCondition.AddClear(button13, button13_Click);
            ControlCondition.AddClear(button14, button14_Click);
            ControlCondition.SaveCondition();
            Field[0, 0] = button1;
            Field[0, 1] = button2;
            Field[0, 2] = button3;
            Field[1, 0] = button4;
            Field[1, 1] = button5;
            Field[1, 2] = button6;
            Field[2, 0] = button7;
            Field[2, 1] = button8;
            Field[2, 2] = button9;
        }

        private SystemControlEvent ControlCondition = new SystemControlEvent();

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
            ControlCondition.LoadSave();
            button11.Enabled = true;

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
            Mode = Difficulty.Meddium;
        }//medium

        private void button13_Click(object sender, EventArgs e)
        {
            label1.Text = "Hard";
            button12.Enabled = true;
            button13.Enabled = false;
            button14.Enabled = true;
            Mode = Difficulty.Hard;
        }//hard

        private void button14_Click(object sender, EventArgs e)
        {
            label1.Text = "Easy";
            button12.Enabled = true;
            button13.Enabled = true;
            button14.Enabled = false;
            Mode = Difficulty.Easy;
        }//easy

    }
}

