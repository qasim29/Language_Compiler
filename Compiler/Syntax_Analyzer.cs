using System.Collections;
using System.Collections.Generic;
public class Syntax_Analyzer
{
    SE_Semantic_Analyzer se = new SE_Semantic_Analyzer();
    Dictionary<string, List<string[]>> rules;
    List<Token> tokens;
    List<Token> ptokens;
    HashSet<string> btokens;

    int index = 0;
    bool expmode = false;

    public Syntax_Analyzer(List<Token> tokens)
    {
        this.rules = new Dictionary<string, List<string[]>>();
        this.ptokens = new List<Token>();
        this.btokens = new HashSet<string>() { "SC", };
        this.tokens = tokens;
        this.getRules();
        // this.printRules();
    }
    private void getRules()
    {
        foreach (string line in System.IO.File.ReadLines(@"E:\GITHUB\Language_Compiler\res\temp.txt"))
        {
            if (line == "") { continue; }
            if (line[0] == '#') { continue; }
            string[] arr = line.Split("->");
            if (rules.ContainsKey(arr[0].Trim())) rules[arr[0].Trim()].Add(arr[1].Trim().Split(" "));
            else
            {
                List<string[]> val = new List<string[]>();
                val.Add(arr[1].Trim().Split(" "));
                rules.Add(arr[0].Trim(), val);
            }
        }
    }

    public void printRules()
    {
        // printing rules from hash table to terminal
        string[] keys = new string[rules.Keys.Count];
        rules.Keys.CopyTo(keys, 0);
        int index = 0;
        foreach (List<string[]> items in rules.Values)
        {
            System.Console.Write($"{keys[index]} -> ");
            index += 1;
            System.Console.Write("[");
            foreach (string[] item in items)
            {
                System.Console.Write("[");
                foreach (string s in item)
                {
                    System.Console.Write(s);
                    System.Console.Write(",");
                }
                System.Console.Write("]");
            }
            System.Console.Write("]\n");
        }
    }
    public bool checkSyntax()
    {
        // System.Console.WriteLine("tokens.Length() == " + tokens.Length());
        if (helper("<START>") && index >= tokens.Count)
        {
            System.Console.WriteLine("INDEX == " + index);
            return true;
        }
        else
        {
            // System.Console.WriteLine("error wala token = " + invalidToken.value + " ,line no =" + invalidToken.line);
            System.Console.WriteLine("INDEX == " + index);
            return false;
        }
    }
    private bool helper(String curNT)
    {
        List<String[]> productionRules = rules[curNT];

        foreach (String[] pr in productionRules)
        {   // System.Console.WriteLine("------------"); // System.Console.WriteLine("% " + curNT + " -> " + String.Join(" ", pr));
            int prev = index;
            int j = 0;
            for (; j < pr.Length; j++)
            {

                String element = pr[j];// System.Console.WriteLine("\nElement :" + element + "' { of :" + curNT + "}");

                if (element[0] == '~') { ++index; return true; }
                else if (element[0] == '<')
                {   // System.Console.WriteLine("into => " + element);
                    if (!helper(element)) { index = prev; break; }// System.Console.WriteLine("@ backing off");
                }
                else if (element.Length == 1 && element[0] == 'E') { continue; }
                else
                {   // System.Console.WriteLine("HERE IN TERMINAL"); // System.Console.WriteLine("token.class_part = " + tokens[index].class_Part.ToString()); // System.Console.WriteLine("token.word = " + tokens[index].word);// // string a = la.ht.Contains();
                    if (string.Equals(element, tokens[index].class_Part.ToString(), StringComparison.OrdinalIgnoreCase))
                    {
                        // if (updateExpMode()){}
                        // if (tokens[index].class_Part.ToString() == "AM" &&   =="<CLASS-ST_OOP>"){}
                        checkScope();
                        ptokens.Add(tokens[index]);
                        if (tokens[index].class_Part == TokenType.SC || tokens[index].class_Part == TokenType.OCB)
                        {
                            if (!secheck()) { return false; }
                        }
                        index++;
                    }
                    else { break; }
                }
            }
            if (j == pr.Length)
            {  // System.Console.WriteLine("Successfully parsed from here"); 
                return true;
            }
            // else { index = prev; }
        }
        return false;
    }



    private bool secheck()
    {
        if (ptokens[1].class_Part == TokenType.CLASS || ptokens[2].class_Part == TokenType.CLASS)
        {
            return classSE();
        }
        return true;
    }

    private bool classSE()
    {
        string name = "";
        string type = "";
        string tm = "";
        string ext = "";
        int i = 0;
        for (; i < ptokens.Count; i++)
        {
            if (ptokens[i].class_Part == TokenType.CLASS)
            {
                i++;
                type = "CLASS";
                name = ptokens[i].word;
            }

            else if (ptokens[i].class_Part == TokenType.CONST || ptokens[i].class_Part == TokenType.ABSTRACT) tm = ptokens[i].class_Part.ToString();

            else if (ptokens[i].class_Part == TokenType.CHILDOF)
            {
                i++;
                for (; i < ptokens.Count; i++)
                {
                    if (ptokens[i].class_Part == TokenType.OCB) continue;

                    else if (ptokens[i].class_Part == TokenType.COM) ext += ",";

                    else ext += ptokens[i].word.ToString();
                }
            }
        }

        if (se.lookUpMainTable(name) != null) { System.Console.WriteLine("Re-Decleared class:" + name + " at lineNo: " + tokens[index - 1].lineNo); return false; }

        else if (se.lookUpMainTable(ext) == null && ext != "") { System.Console.WriteLine("Parent class : " + ext + " isn't Decleared"); return false; }

        else if (se.lookUpMainTable(ext) != null)
        {
            if (se.lookUpMainTable(ext)?.tm == "CONST") { System.Console.WriteLine("Parent class : " + ext + " is Decleared as FINAL class"); return false; }
        }
        ptokens.Clear();
        se.curr_class_name = name;
        return se.insertMainTable(name, type, tm, ext);
    }

    // private bool updateExpMode()
    // {
    //     if (tokens[index].class_Part.ToString() == "ORB" || tokens[index].class_Part.ToString() == "ASI" || tokens[index].class_Part.ToString() == "OSB") { expmode = true; }
    //     else if (tokens[index].class_Part.ToString() == "CRB" || tokens[index].class_Part.ToString() == "SC" || tokens[index].class_Part.ToString() == "CSB") { expmode = false; getType(expression); }
    //     return expmode;
    // }

    public void checkScope()
    {
        System.Console.WriteLine("Matched Terminal = " + tokens[index].class_Part.ToString());

        if (tokens[index].class_Part == TokenType.CLASS) { se.scopeStack.Add(0); printScopeStack(); }

        else if (tokens[index].class_Part.ToString() == "ORB" && (tokens[index - 1].class_Part.ToString() == "ID" || tokens[index - 1].class_Part.ToString() == "EXECUTE")) { se.createScope(); printScopeStack(); }

        else if (tokens[index].class_Part.ToString() == "OCB" && tokens[index - 1].class_Part.ToString() == "CRB") { se.createScope(); printScopeStack(); }

        else if (tokens[index].class_Part.ToString() == "CCB") { se.destroyScope(); printScopeStack(); }


    }
    private void printScopeStack()
    {
        System.Console.WriteLine("Scope Count " + se.scope);
        System.Console.WriteLine("--scope stack--");
        System.Console.Write("[ ");

        foreach (int val in se.scopeStack) System.Console.Write(val + ",");

        System.Console.Write(" ]");
        System.Console.WriteLine();
    }
    string? getType(List<Token> expression)
    {

        return null;
    }
}
