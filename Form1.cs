using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace compilerProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string userInput = textBox1.Text;
            string keywords = @"\b(num|text|check|otherwise|until|repeat)";
            string identifier = @"\b[a-zA-Z][a-zA-Z0-9]*\b";
            string numbers = @"\b[0-9]+\b";
            string operators = @":=|==|[+\-*/<>]";
            string symbols = @"[;{}()]";

            string masterPattern = $"{keywords}|{identifier}|{numbers}|{operators}|{symbols}";
            DataTable dt=new DataTable();
            dt.Columns.Add("lexemes");
            dt.Columns.Add("Token type");

            MatchCollection matches = Regex.Matches(userInput, masterPattern);

            foreach (Match match in matches) {
                string lexeme = match.Value;
                if (string.IsNullOrWhiteSpace(lexeme)) continue;
                string type = "";

                if (Regex.IsMatch(lexeme, keywords))type = "keyword";
                else if (Regex.IsMatch(lexeme, numbers))type = "numbers";
                else if (Regex.IsMatch(lexeme, identifier))type = "identifier";
                else if (Regex.IsMatch(lexeme, operators))type = "operators";
                else if (Regex.IsMatch(lexeme, symbols)) type = "symbols";
                else if (lexeme == ":=") type = "Assignment_operator";
                else if (lexeme == "==") type = "Equal_operator";
                else if (lexeme == "+") type = "Plus_operator";
                else if (lexeme == "-") type = "Minus_operator";
                else if (lexeme == ">") type = "Greater_than_operator";
                else if (lexeme == "<") type = "Less_than_operator";
                else if (lexeme == ";") type = "Semi-colon";
                else if (lexeme == "(") type = "left_paran";
                else if (lexeme == ")") type = "Right_paran";
                else if (lexeme == "{") type = "left_brace";
                else if (lexeme == "}") type = "Right_brace";
                else type = "Unknown";

                dt.Rows.Add(lexeme,type);
            }
            dataGridView1.DataSource = dt;
        }
    }
}
