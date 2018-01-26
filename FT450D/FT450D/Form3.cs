using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FT450D
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        //--------------------------------------------------
        public void textToTextbox(List<string> text)
        {
            textBox1.Text = String.Join(Environment.NewLine, text);
            textBox1.DeselectAll();
        }
        //--------------------------------------------------
    }
}
