﻿using System.Collections;

// string[] lines = System.IO.File.ReadAllLines(@"E:\GITHUB\Language_Compiler\res\SOURCE_CODE.txt");

// ArrayList words = new ArrayList();

// char[] breakers = { '(',')','[',']','{','}',';',':',',',        // punctuators  
//                     '+','-','*','/','%','<','>','=','!',       // operators
//                     ' ','\'','"' };

// string word = "";
// char ch;
// int i;
// bool flag = false;
// foreach (string line in lines)
// {
//     string l = line + " ";
//     for (i = 0; i <= l.Length - 1; i++)
//     {
//         if (l[i] == '$' || flag == true)                    // This condition is for the MULTI LINE comment
//         {
//             int index;
//             if (flag == true) index = l.IndexOf('$', i);
//             else index = l.IndexOf('$', i + 1);

//             if (index == -1) { flag = true; break; }

//             i = index + 1;
//             flag = false;
//         }

//         if (l[i] == '#') break;                            // This condition is for the SINGLE LINE comment

//         if (l[i] == '.')
//         {
//             bool isNumeric;
//             if(word!="") isNumeric = int.TryParse(word, out _);
//             else isNumeric=true;

//             if (isNumeric)
//             {
//                 AddCharacter(l[i]);
//                 isNumeric = int.TryParse(l[i].ToString(), out _);
//                 if (isNumeric)
//                 {
//                     while (!breakers.Contains(l[i]) && l[i]!='.')
//                     {
//                         AddCharacter(l[i]);
//                     }
//                     createWord(word);
//                     i--;
//                     continue;
//                 }
//                 else{
//                     createWord(word);
//                     AddCharacter(l[i]);
//                     i--;
//                     continue;
//                 }
//             }
//             else
//             {
//                 createWord(word);
//                 isNumeric = int.TryParse(l[i + 1].ToString(), out _);
//                 if (isNumeric)
//                 {
//                     AddCharacter(l[i]);
//                     AddCharacter(l[i + 1]);
//                     continue;
//                 }
//                 createWord(l[i].ToString());
//                 continue;
//             }
//         }
//         if (breakers.Contains(l[i]))
//         {
//             if (l[i] == '"')
//             {
//                 if (word != "") { createWord(word); AddCharacter(l[i]); }

//                 else { AddCharacter(l[i]); }

//                 ch = ' ';

//                 while (ch != '"' && i < l.Length - 1)
//                 {
//                     if (l[i] == '\\')
//                     {
//                         AddCharacter(l[i]);
//                         if (i == l.Length - 1) break;     //Case: {"\ }

//                         AddCharacter(l[i]);
//                         ch = ' ';
//                         continue;                        //Added continue for this case { "\" }
//                     }
//                     AddCharacter(l[i]);
//                 }
//                 createWord(word);
//                 i--;
//                 continue;
//             }
//             if (l[i] == '\'')
//             {
//                 int count = 0;
//                 if (word != "") { createWord(word); AddCharacter(l[i]); }

//                 else { AddCharacter(l[i]); }
//                 count += 1;
//                 ch = ' ';
//                 while (ch != '\'' && i < l.Length - 1 && count != 3)
//                 {
//                     if (l[i] == '\\')
//                     {
//                         AddCharacter(l[i]);
//                         count += 1;
//                         if (i == l.Length - 1) break;     //Case: {"\ }

//                         AddCharacter(l[i]);
//                         ch = ' ';
//                         continue;                        //Added continue for this case { "\" }
//                     }
//                     AddCharacter(l[i]);
//                     count += 1;
//                 }
//                 createWord(word);
//                 i--;
//                 continue;
//             }
//             if (word != "") createWord(word);

//             if (l[i] == ' ') continue;

//             if ((l[i] == '>' || l[i] == '<' || l[i] == '=' || l[i] == '!') && l[i + 1] == '=')
//             {
//                 createWord(l[i].ToString() + l[i + 1].ToString());
//                 i++;
//                 continue;
//             }
//             words.Add(l[i].ToString());
//             continue;
//         }
//         word = word + l[i];
//     }
// }

// foreach (string item in words)
// {
//     System.Console.WriteLine(item);
// }

// void AddCharacter(char character)
// {
//     ch = character;
//     word += character;
//     i++;
// }

// void createWord(string w)
// {
//     words.Add(w);
//     word = "";
// }


// Word_Breaker words = new Word_Breaker();
// ArrayList word=words.GetWords();
// System.Console.Write("\nKEY-WORDS");
// System.Console.Write("\t\t     LINE-NO\n");
// foreach (ArrayList item in word)
// {
//     foreach (var i in item)
//     {
//     System.Console.Write(" ");
//     System.Console.Write(i);
//     System.Console.Write("\t\t\t\t");
//     }
//     System.Console.WriteLine();
// }

// Word_Breaker words = new Word_Breaker();
// ArrayList word = words.GetWords();
// System.Console.Write("\nKEY-WORDS");
// System.Console.Write("\t\t     LINE-NO\n");

// foreach (string[] item in word)
// {
//     string text= item[0]+"\t"+item[1]+"\n"; 
//     System.Console.WriteLine(item.ToString());
//     await ExampleAsync(text);
// }











using System.Text.RegularExpressions;  

// char[] breakers = { '(',')','[',']','{','}',';',':',','};        // punctuators  

Word_Breaker breaker = new Word_Breaker();
ArrayList words = breaker.GetWords();
Lexical_Analyzer tokens=new Lexical_Analyzer();

foreach (Tokens t in tokens.GetTokens(words)) System.Console.WriteLine(t.ToString());

// string w=".23";
// Regex regex = new Regex(@"^[0-9]*[.][0-9]+$");
// if (regex.IsMatch(w)) System.Console.WriteLine("ok");

    





























// static async Task ExampleAsync(ArrayList word)
// {

//     using StreamWriter file = new(@"E:\GITHUB\Language_Compiler\res\words.txt");

//     foreach (string[] item in word)
//     {
//         string txt=item[0] + "\t" + item[1];
//         System.Console.WriteLine(item[0] + "\t" + item[1] );
//         await file.WriteLineAsync(txt );
//     }
// }

// await ExampleAsync(word);









// int validateString(int i, string l)
// {

//     int istart = i;
//     if (word != "")
//     {
//         arlist.Add(word);
//         word = "" + l[i];
//         i++;
//     }
//     else
//     {
//         word = "" + l[i];
//         i++;
//     }
//     char ch = ' ';
//     while (ch != '"' && i < l.Length - 1)
//     {
//         if (l[i] == '\\')
//         {
//             ch = l[i];
//             word += ch;
//             i++;
//         }
//         ch = l[i];
//         word += ch;
//         i++;
//     }
//     if (i == l.Length)
//     {
//         word = word.Substring(istart, i - 1);
//         arlist.Add(word);
//         word = "";
//         break;
//     }
//     arlist.Add(word);
//     i--;


//     return i;
// }


// BREAKER DIFFERENT APPROACH
// //this condition is for the comment 
// if (l[i] == '$') break;

// if (breakers.Contains(l[i]))
// {
// if (l[i] == ' ')
// {
//     if (word == "") continue;
//     arlist.Add(word);
//     word = "";

// }
// else
// {
//     if (word != "") arlist.Add(word);
//     word ="";
//     arlist.Add(l[i].ToString());

// }
// continue;



//  THIS IS WORKING CODE
// char ch = ' ';

// if (word != "") word = createWord(word)+l[i];

// else word = "" + l[i];

// i++;

// while (ch != '"' && i < l.Length - 1)
// {
//     if (l[i] == '\\')
//     {
//         ch = l[i];
//         word += ch;
//         i++;  
//         if(i==l.Length-1) break;          //Case: {"\ }

//         ch = l[i];
//         word += ch;
//         i++;
//         ch=' ';
//         continue; //Added continue for this case { "\" }
//     }
//     ch = l[i];
//     word += ch;
//     i++;
// }
// word = createWord(word);
// i--;
// continue;



//  LEFT IN BETWEEN  left cases "" 
// // char ch = ' ';

// if (word != "") word = createWord(word)+l[i];

// else word = "" + l[i];

// i++;

// while (l[i] != '"' && i < l.Length - 1)
// {
//     if (l[i] == '\\')
//     {
//         word += l[i];
//         i++;  
//         if(i==l.Length-1) break;          //Case: {"\ }

//         word += l[i];
//         i++;
//         continue; //Added continue for this case { "\" }
//     }
//     word += l[i];
//     i++;
// }
// word = createWord(word);
// i--;
// continue;