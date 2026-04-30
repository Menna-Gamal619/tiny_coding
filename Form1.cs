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

            string keywords = @"\b(num|text|check|otherwise|until|repeat|then)\b";
            string identifier = @"\b[a-zA-Z][a-zA-Z0-9]*\b";
            string numberPattern = @"\b[0-9]+\b";
            string operators = @":=|==|[\+\-\*/<>]";
            string symbols = @"[;{}()]";

            string masterPattern =
            $"{keywords}|{numberPattern}|{operators}|{symbols}|{identifier}";

            DataTable dt=new DataTable();
            dt.Columns.Add("lexemes");
            dt.Columns.Add("Token type");

            MatchCollection matches = Regex.Matches(userInput, masterPattern);

            List<Token> tokens = new List<Token>();

            foreach (Match match in matches) {
                string lexeme = match.Value;

                if (string.IsNullOrWhiteSpace(lexeme)) continue;

                string type = "";

                if (Regex.IsMatch(lexeme, keywords))
                    type = "Keyword";

                else if (Regex.IsMatch(lexeme, numberPattern))
                    type = "Number";

                else if (Regex.IsMatch(lexeme, identifier))
                    type = "Identifier";

                else if (lexeme == ":=")
                    type = "Assignment_Op";

                else if (lexeme == "==")
                    type = "Equal_Op";

                else if (lexeme == "!=")
                    type = "NotEqual_Op";

                else if (lexeme == "<=")
                    type = "LessEqual_Op";

                else if (lexeme == ">=")
                    type = "GreaterEqual_Op";

                else if (lexeme == "+")
                    type = "Plus_Op";

                else if (lexeme == "-")
                    type = "Minus_Op";

                else if (lexeme == "*")
                    type = "Multiply_Op";

                else if (lexeme == "/")
                    type = "Divide_Op";

                else if (lexeme == ">")
                    type = "Greater_Than_Op";

                else if (lexeme == "<")
                    type = "Less_Than_Op";

                else if (lexeme == ";")
                    type = "Semicolon";

                else if (lexeme == "(")
                    type = "Left_Paren";

                else if (lexeme == ")")
                    type = "Right_Paren";

                else if (lexeme == "{")
                    type = "Left_Brace";

                else if (lexeme == "}")
                    type = "Right_Brace";

                else
                    type = "Unknown";

                tokens.Add(new Token { Value = lexeme, Type = type });
                dt.Rows.Add(lexeme, type);
            }
            dataGridView1.DataSource = dt;

            try
            {
                if (tokens.Count == 0) return;
                
                MiniLParser miniLParser = new MiniLParser(tokens);
                miniLParser.ParseProgram();
                MessageBox.Show("Code is syntactically correct!");
            }
            catch(Exception ex) 
            {
                MessageBox.Show("Syntax Error"+ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
    }
}
