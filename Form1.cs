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
            string numberPattern = @"\d+(\.\d+)?";
            string operators = @":=|==|[+\-*/<>]";
            string symbols = @"[;{}()]";
            string comment_stmt = @"\b/*[a-zA-Z]*/|/*[0-9]*[a-zA-Z]*/\b";
            string condition_op = @"<|>|=|<>|<=|>=";
            string term = @"(\d+(\.\d+)?|[a-zA-Z_][a-zA-Z0-9_]*)";
            string conditionPattern = $@"^{identifier}\s*{condition_op}\s*{term}$";
            string bool_op = @"\s+($$||||)\s+";
            string condition = $@"{identifier}*\s*{condition_op}\s*([a-zA-Z_][a-zA-Z0-9_]*|{numberPattern})";
            string ifStatement = $@"^\s*if\s+{conditionPattern}\s+then\s+.*?(else\s+if|else|end)";

            string masterPattern = $"{keywords}|{identifier}|{numberPattern}|{operators}|{symbols}";
            DataTable dt=new DataTable();
            dt.Columns.Add("lexemes");
            dt.Columns.Add("Token type");

            MatchCollection matches = Regex.Matches(userInput, masterPattern);

            foreach (Match match in matches) {
                string lexeme = match.Value;
                if (string.IsNullOrWhiteSpace(lexeme)) continue;
                string type = "";

                if (Regex.IsMatch(lexeme, keywords))type = "keyword";
                else if (Regex.IsMatch(lexeme, $@"^{ifStatement}$")) type = "If_Statement_Header";
                else if (Regex.IsMatch(lexeme, $@"^{conditionPattern}$") || lexeme.Contains("&&") || lexeme.Contains("||")) type = "Condition_Statement";
                else if (Regex.IsMatch(lexeme, @"\b(if|then|else|elseif|end)\b")) type = "Reserved_Keyword";
                else if (Regex.IsMatch(lexeme, @"\b(num|text|check|otherwise|until|repeat)\b")) type = "Keyword";
                else if (Regex.IsMatch(lexeme, $@"^{bool_op}$")) type = "Boolean_Operator";
                else if (Regex.IsMatch(lexeme, $@"^{condition_op}$")) type = "Condition_Operator";
                else if (Regex.IsMatch(lexeme, numberPattern)) type = "numbers";
                else if (Regex.IsMatch(lexeme, identifier)) type = "identifier";
                else if (Regex.IsMatch(lexeme, operators)) type = "operators";
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
