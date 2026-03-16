using System.Data;
using System;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace smartscanner
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void scan_Click(object sender, EventArgs e)
        {

            string inputString = textBox1.Text;

            string keywords = @"\b(int|float|string|read|write|repeat|until|if|elseif|else|then|return|endl)\b";
            string identifiers = @"\b[a-zA-Z][a-zA-Z0-9_]*\b";
            string numbers = @"\b[0-9]+\.[0-9]+|\b[0-9]+\b"; 
            string strings = @"\""[^""]*\""";
            string operators = @":=|==|!=|<=|>=|&&|\|\||[+\-*/<>]";  
            string symbols = @"[,;(){}]";
            string comments = @"/\*.*?\*/|//.*";  
            string whitespace = @"[ \t\n\r]+";

            string masterPattern = $"{comments}|{keywords}|{numbers}|{identifiers}|{strings}|{operators}|{symbols}|{whitespace}";

            DataTable dt = new DataTable();
            dt.Columns.Add("Lexeme");
            dt.Columns.Add("Token Type");

            MatchCollection tokens = Regex.Matches(inputString, masterPattern);

            foreach (Match token in tokens)
            {
                string lex = token.Value;

                if (Regex.IsMatch(lex, comments) || Regex.IsMatch(lex, whitespace))
                    continue;

                string type = "";

                if (Regex.IsMatch(lex, keywords))
                    type = "Keyword";
                else if (Regex.IsMatch(lex, @"^\d+\.\d+$")) 
                    type = "Number Float";
                else if (Regex.IsMatch(lex, @"^\d+$")) 
                    type = "Number Integer";
                else if (Regex.IsMatch(lex, strings))
                    type = "String";
                else if (Regex.IsMatch(lex, identifiers))
                    type = "Identifier";

                else if (lex == ":=") type = "Assign_Op";
                else if (lex == "==") type = "Equal_Op";
                else if (lex == "!=") type = "NotEqual_Op";
                else if (lex == "<=") type = "LessEqual_Op";
                else if (lex == ">=") type = "GreaterEqual_Op";
                else if (lex == "<") type = "Less_Op";
                else if (lex == ">") type = "Greater_Op";
                else if (lex == "&&") type = "And_Op";
                else if (lex == "||") type = "Or_Op";
                else if (lex == "+") type = "Plus_Op";
                else if (lex == "-") type = "Minus_Op";
                else if (lex == "*") type = "Mult_Op";
                else if (lex == "/") type = "Div_Op";
                else if (lex == ",") type = "Comma";
                else if (lex == ";") type = "Semicolon";
                else if (lex == "(") type = "Left_Paren";
                else if (lex == ")") type = "Right_Paren";
                else if (lex == "{") type = "Left_Brace";
                else if (lex == "}") type = "Right_Brace";

                else type = "Unknown";

                dt.Rows.Add(lex, type);


            }

            dataGridView1.DataSource = dt;
            MessageBox.Show($"{dt.Rows.Count} Tokens Found!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    
    }
}
