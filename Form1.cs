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
            string symbol = "";
            if (chkOrcale.Checked)
            {
                symbol = "\'";

            }
            else
            {
                symbol = "\"";
            }


            using (StreamWriter writer = new StreamWriter("Input.txt", false))
            {
                writer.WriteLine(input);
            }
            string line;
            int countS = 0;
            
            string display = "INSERT INTO " + textBox3.Text + " VALUES\n";
            string tbl = display;
            StreamReader file =
    new System.IO.StreamReader("Input.txt");
            while ((line = file.ReadLine()) != null)
            {
                bool skip = false;
                display += "(" + symbol;
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

                                display += symbol + "," + symbol;
                            }



                        }

                        else
                        {

                            display += line[i];
                        }
                    }
                }
                if (symbol.Equals("\'"))
                {
                    if (countS > 0) { display += symbol + ");\n"+tbl; }
                    else
                    {
                        display += symbol + ");\n"+tbl;
                    }
                }
                else
                {
                    if (countS > 0) { display += symbol + "),\n"; }
                    else
                    {
                        display += symbol + "),\n";
                    }
                }

            }
          
            if (!chkOrcale.Checked)
            {
                display = display.Replace(display.ElementAt(display.Length - 2),' ');
            }
            else
            {
                display = display.Substring(0, display.Length - (tbl.Length + 2));
            }
            display = display.Replace("*", "");
            textBox2.Text = display + ";";
            file.Close();

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
