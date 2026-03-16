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
            string strings = @"\""[^""]*\""";
            string operators = @":=|==|[+\-*/<>]";
            string symbols = @"[;{}()]";
            string comment_stmt = @"/\*.*?\*/";
            string whitespace = @"[ \t\n\r]+";
            string condition_op = @"<|>|=|<>|<=|>=";
            string term = @"(\d+(\.\d+)?|[a-zA-Z_][a-zA-Z0-9_]*)";
            string conditionPattern = $@"^{identifier}\s*{condition_op}\s*{term}$";
            string bool_op = @"\s*(\&\&|\|\|)\s*";
            string condition = $@"{identifier}*\s*{condition_op}\s*([a-zA-Z_][a-zA-Z0-9_]*|{numberPattern})";
            string ifStatement = $@"^\s*if\s+{conditionPattern}\s+then\s+.*?(else\s+if|else|end)";
            string arithmetic_op = @"\+|\-|\*|\/";
            string function_call = $@"{identifier}\((({identifier}|{numberPattern})(,({identifier}|{numberPattern}))*)?\)";
            string equation = $@"({term})(\s*({arithmetic_op})\s*({term}))+";
            string expression = $@"({strings}|{term}|{equation})";
            string assignment_stmt = $@"{identifier}\s*:=\s*{expression}";
            string datatype = @"\b(num|text)\b";
            string declaration_stmt = $@"{datatype}\s+{identifier}(\s*:=\s*{expression})?(,\s*{identifier}(\s*:=\s*{expression})?)*;";
            string write_stmt = $@"write\s+({expression}|endl)\s*;";
            string read_stmt = $@"read\s+{identifier}\s*;";
            string return_stmt = $@"return\s+{expression}\s*;";
            string elseif_stmt = @"elseif\s+.*\s+then";
            string else_stmt = @"else\s+.*\s+end";
            string repeat_stmt = @"repeat\s+.*\s+until\s+.*";

            string masterPattern =
            $"{keywords}|{strings}|{numberPattern}|{operators}|{symbols}|{identifier}|{condition_op}";
            DataTable dt=new DataTable();
            dt.Columns.Add("lexemes");
            dt.Columns.Add("Token type");

            MatchCollection matches = Regex.Matches(userInput, masterPattern);

            foreach (Match match in matches) {
                string lexeme = match.Value;
                if (string.IsNullOrWhiteSpace(lexeme)) continue;
                string type = "";

                if (Regex.IsMatch(lexeme, keywords)) type = "keyword";
                else if (Regex.IsMatch(lexeme, numberPattern)) type = "numbers";
                else if (Regex.IsMatch(lexeme, strings)) type = "String";
                else if (Regex.IsMatch(lexeme, operators)) type = "operators";
                else if (Regex.IsMatch(lexeme, symbols)) type = "symbols";
                else if (Regex.IsMatch(lexeme, identifier)) type = "identifier";
                else if (Regex.IsMatch(lexeme, $@"^{ifStatement}$")) type = "If_Statement_Header";
                else if (Regex.IsMatch(lexeme, $@"^{conditionPattern}$") || lexeme.Contains("&&") || lexeme.Contains("||")) type = "Condition_Statement";
                else if (Regex.IsMatch(lexeme, @"\b(if|then|else|elseif|end)\b")) type = "Reserved_Keyword";
                else if (Regex.IsMatch(lexeme, $@"^{bool_op}$")) type = "Boolean_Operator";
                else if (Regex.IsMatch(lexeme, $@"^{condition_op}$")) type = "Condition_Operator";
                else if (Regex.IsMatch(lexeme, function_call)) type = "Function_Call";
                else if (Regex.IsMatch(lexeme, equation)) type = "Equation";
                else if (Regex.IsMatch(lexeme, expression)) type = "Expression";
                else if (Regex.IsMatch(lexeme, assignment_stmt)) type = "Assignment_Statement";
                else if (Regex.IsMatch(lexeme, declaration_stmt)) type = "Declaration_Statement";
                else if (Regex.IsMatch(lexeme, write_stmt)) type = "Write_Statement";
                else if (Regex.IsMatch(lexeme, read_stmt)) type = "Read_Statement";
                else if (Regex.IsMatch(lexeme, return_stmt)) type = "Return_Statement";
                else if (Regex.IsMatch(lexeme, elseif_stmt)) type = "ElseIf_Statement";
                else if (Regex.IsMatch(lexeme, else_stmt)) type = "Else_Statement";
                else if (Regex.IsMatch(lexeme, repeat_stmt)) type = "Repeat_Statement";
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
