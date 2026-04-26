using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NumericKeyPad
{
    public partial class Form1 : Form
    {
        private TextBox focusedTextbox = null;
        public Form1()
        {
            InitializeComponent();
            touchScreen1.OnUserControlButtonClicked += new TouchScreen.ButtonClickedEventHandler(touchScreen1_OnUserControlButtonClicked);  
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            focusedTextbox = (TextBox)sender;
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            focusedTextbox = (TextBox)sender;
        }

        protected void touchScreen1_OnUserControlButtonClicked(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (focusedTextbox != null)
            {
                if (b.Text == "Back")
                {
                    if (focusedTextbox.Text.Length > 1)
                    {
                        focusedTextbox.Text = focusedTextbox.Text.Substring(0, focusedTextbox.Text.Length - 1);
                    }
                    else
                    {
                        focusedTextbox.Text = string.Empty;
                    }
                }
                else
                {
                    if (MyGlobal.bTouch)
                        focusedTextbox.Text = b.Text;
                    else
                    {
                        MyGlobal.bTouch = false;
                        focusedTextbox.Text += b.Text;
                    }
                }
            }
        }

        private void touchScreen1_Load(object sender, EventArgs e)
        {

        }
    }
}
