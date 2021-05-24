using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SqlGen2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string input = textBox1.Text;
            if (File.Exists("Input.txt"))
            {
                File.Delete("Input.txt");
            }

            /*
             
             DATA6212","Database),SQAT6312","Software),CNOS5112","Client),WEDE6011","Web),ISEC6311","Information),
             */

            using (StreamWriter writer = new StreamWriter("Input.txt", false))
            {
                writer.WriteLine(input);
            }
            string line;
            int countS = 0;

            string display = "INSERT INTO " + textBox3.Text + " VALUES\n";
            StreamReader file =
    new System.IO.StreamReader("Input.txt");
            while ((line = file.ReadLine()) != null)
            {
                bool skip = false;
                display += "(\"";
                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i] == '*')
                    {
                        if (countS >= 1)
                        {
                        //    display += "\"";
                            skip = true;
                            countS = 0;
                        }
                        else
                        {
                            display += line[i];
                            countS++;
                        }
                    }
                    else
                    {


                        if (line[i] == ' ')
                        {
                            if (countS > 0)
                            {
                                display += " ";
                            }
                            else
                            {

                                display += "\",\"";
                            }



                        }

                        else
                        {

                            display += line[i];
                        }
                    }
                }
                if (countS > 0) { display += "\"),\n"; }
                else
                {
                    display += "\"),\n";
                }

            }
            display = display.Remove(display.Length - 1);
            display = display.Replace("*", "");
            textBox2.Text = display+";";
            file.Close();

        }
    }
}
