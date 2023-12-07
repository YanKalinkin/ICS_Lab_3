using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICS_Lab_3
{
    
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((textBox3.Text.Length == 0)||(textBox4.Text.Length == 0))
            {
                MessageBox.Show("Plaease enter offset and key word!");
            }
            else
            {
                textBox2.Text = Data.Cesar_with_key(textBox1.Text, Convert.ToInt16(textBox3.Text), textBox4.Text, true);
                textBox1.Text = "";
            }  
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = Data.Cesar_with_key(textBox2.Text, Convert.ToInt16(textBox3.Text), textBox4.Text, false);
            textBox2.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Data.Vigenere("Key", "Key", textBox5);
            textBox2.Text = Data.space_replace(textBox1.Text, false);
        }
    }

    public static class Data
    {
        public static string alphabet_h = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public static string alphabet_l = "abcdefghijklmnopqrstuvwxyz";
        public static string alphabet_s = "_1234567890";
        public static string alphabet_common = alphabet_h + alphabet_l + alphabet_s;
        public static string key_word = "";
        public static string common = "";
        public static int index;
        public static string space_replace(string find_space, bool mode)
        {
            string space_replaced = "";
            for (int i = 0; i < find_space.Length; i++)
            {
                if (mode)
                {
                    if (find_space[i] != ' ')
                        space_replaced += find_space[i];
                    else
                        space_replaced += '_';
                }
                else
                {
                    if (find_space[i] != '_')
                        space_replaced += find_space[i];
                    else
                        space_replaced += ' ';
                }
                
            }
            return space_replaced;
        }
        public static string Cesar_with_key(string to_encrypt, int position, string key_word, bool mode)
        {
            string encrypted = "";
            string alph = alphabet_h + alphabet_l + alphabet_s;
            string alph_b = alph;
            string before_key = "";
            //Deleting key letters from alphabet
            for (int i = 0; i < alph.Length; i++)
            {
                for (int j = 0; j < key_word.Length; j++)
                {
                    if (alph[i] == key_word[j])
                    {
                        alph = alph.Remove(i, 1);
                    }
                }
            }
            //Seacring letters to set before key word (shift)
            for (int k = alph.Length - position; k < alph.Length; k++)
            {
                before_key += alph[k];
            }
            //Deleting shifted letters from alphabet
            for (int i = 0; i < alph.Length; i++)
            {
                for (int j = 0; j < before_key.Length; j++)
                {
                    if (alph[i] == before_key[j])
                    {
                        alph = alph.Remove(i, 1);
                    }
                }
            }
            //Collecting parts of the key string
            common = before_key + key_word + alph;
            //Now common is key, alph_b is alphabet
            if (mode)
            {
                for (int n = 0; n < to_encrypt.Length; n++)
                {
                    for (int m = 0; m < alph_b.Length; m++)
                    {
                        if (to_encrypt[n] == alph_b[m])
                        {
                            encrypted += common[m];
                        }
                    }
                }
            }
            else
            {
                for (int n = 0; n < to_encrypt.Length; n++)
                {
                    for (int m = 0; m < common.Length; m++)
                    {
                        if (to_encrypt[n] == common[m])
                        {
                            encrypted += alph_b[m];
                        }
                    }
                }
            }
            return encrypted;
        }
        public static string Vigenere(string to_encrypt, string slogan, TextBox t1)
        {
            string alph = alphabet_h + alphabet_l + alphabet_s;
            TextBox tb = t1;
            string[] vigenere_matrix = new string[alph.Length];
            string encrypted = "1";
            string shift(string to_shift, int step)
            {
                string after = "";
                string before = "";
                for (int i = 0; i < step; i++)
                {
                    if (to_shift[i] == 32)
                    {
                        after += " ";
                    }
                    else
                    {
                        after += to_shift[i];
                    }
                }
                before = to_shift.Remove(0, step);
                return before + after;
            }
            //Matrix filling
            for (int i = 0; i < vigenere_matrix.Length; i++)
            {
                tb.AppendText(shift(alph, i) + '\n');
            }
            return encrypted;
        }
    }
}
