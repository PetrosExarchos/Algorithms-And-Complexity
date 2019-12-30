using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;


    class InputHandler
    {
        
        public void Opening_Message()
        {
            Console.WriteLine("//////////////////////////////////////////////////////////////");
            Console.WriteLine("/// ALGORITHMS AND COMPLEXITY ///");
            Console.WriteLine("/// KNAPSACK 0-1 DP ///");
            Console.WriteLine("/// PETROS EXARCHOS ///");
            Console.WriteLine("/// University of Ioannina - Department of Informatics and Telecommunications - 2019 ///");
            Console.WriteLine("/// VERSION 1.0 ///");
            Console.WriteLine("/// YOUR ROOT DIR IS : " + Program.GetExecutingDirectoryName() + "  ///");
            Console.WriteLine("//////////////////////////////////////////////////////////////");
            WaitForInput();
        }

        public void Help(string choice)
        {

            Console.WriteLine("\n\nHint ("+choice+"):\n");

            switch (choice) 
            {
                case "1i":
                    Console.WriteLine("This is the default testing procedure using 320 pre generated knapsack problems");
                    break; 
                case "2i": 
                    Console.WriteLine("In this procedure, 320 knapsack problems are generated from a modifed version of the David Pisinger ksnapsack problem generator (http://hjemmesider.diku.dk/~pisinger/generator.c) and stored in folder. Afterwards the application DP knapack solver algorithm, along with the google or-tools algorithm, solve each generated file one by one and print the solution and time taken to solve in ms");
                    break;
                case "3i":
                    Console.WriteLine("In this procedure one user specified knapsack problem is sovled by the application knapack DP solver algorithm and google or-tools algorithm. The file must be located inside /_userFiles/ directory");
                    break;
                case "4i":
                    Console.WriteLine("In this procedure one user specified knapsack problem is sovled by the application recursive knapack solver algorithm. The file must be located inside /_userFiles/ directory. It is NOT recommended to trying to solve problems with more than 40 items as the complexity of the algorithm is (2^N)");
                    break;
                case "5i":
                    Console.WriteLine("In this procedure one user specified folder is parsed and sovled by the application knapack DP solver algorithm. The folder must be located inside root execution directory");
                    break;
                case "0i":
                    Console.WriteLine("An extremely sophisticated and complex procedure. For more information, refer to NASA documents and sources");
                    break;
            }

            Console.WriteLine("\n\n");
        }

        public string MainMenu()
        {
            Console.WriteLine("Select an option by typing the corresponding number and pressing ENTER");
            Console.WriteLine("For more information regarding a choice type the number followed by 'i' e.x: '3i'");

            Console.WriteLine("\n(1) - Execute standard procedure");
            Console.WriteLine("(2) - Execute pseudorandom problem generator & solver procedure");
            Console.WriteLine("(3) - Solve a knapsack problem from a .txt file using Dynamic programming");
            Console.WriteLine("(4) - Solve a knapsack problem from a .txt file using Recursive programming");
            Console.WriteLine("(5) - Solve multiple knapsack problems from a folder using Dynamic programming");
            Console.WriteLine("(0) - Exit the application");

            return HI_MainMenu();
        }

        public string HI_MainMenu(int errorCount = 0)
        {
            string choice = Console.ReadLine();

            switch (choice) 
            {
            case "1": 
                break; 
            case "2": 
                break; 
            case "3": 
                break;
            case "4":
                break;
            case "5":
                break;
            case "0":
                break;
            case "1i":
            Help("1i");
                break; 
            case "2i":
            Help("2i");
                break; 
            case "3i":
            Help("3i");
                break;
            case "4i":
            Help("4i");
                break;
            case "5i":
            Help("5i");
                break;
            case "0i":
            Help("0i");
                break;
            default:
                Print_Error(errorCount);
                errorCount+=1;
                choice = HI_MainMenu(errorCount);
                break;
            }

            return choice;

        }

        public void Print_Error(int errC)
        {
            Console.WriteLine("Invald Selection ");
        }

        public void WaitForInput()
        {
            Console.WriteLine("\nPress ENTER to continue");
            Console.ReadLine();
        }
        
        public bool ParseKsFile(string path, out int[] values , out int[] weights , out int new_CarryLoad)
        {
            
            new_CarryLoad = 0;
            values = new int[]{};
            weights = new int[]{};
            
            List<int> new_Itm_Values = new List<int>();
            List<int> new_Itm_Weights = new List<int>();

            try
            {
                StreamReader sr = new StreamReader(path);

                string line;
                int line_count = 0;
                int itmCount = 0;
                

                line = sr.ReadLine(); // starting
                while (line != null) 
                {
                    
                    if (line_count == 0)
                    {
                        itmCount = int.Parse(line);
                        line_count++; // force counter
                        line = sr.ReadLine(); // force next line
                        //Console.WriteLine("Item Count = " + itmCount);
                        continue;
                    }

                    if (line_count == itmCount+1)
                    {
                        new_CarryLoad = int.Parse(line);
                        line = sr.ReadLine(); // force next line
                        //Console.WriteLine("Max Carry Load = " + new_CarryLoad);
                        break;
                    }
                
                    string[] frag = Regex.Split(line, @"\s+");
                    new_Itm_Values.Add(int.Parse(frag[2]));
                    new_Itm_Weights.Add(int.Parse(frag[3]));
                    //Console.WriteLine("Index = " + frag[1] +" , Value = " + frag[2] + " , Weight = "+frag[3]);
    
                    line_count++; // ending
                    line = sr.ReadLine(); // ending

                }
            }
            catch (Exception ex)
            {
                string dummy = ex.Message;
                Console.WriteLine("FILE PARSING FAILED !!!!");
                return false;
            }

            values = new_Itm_Values.ToArray();
            weights = new_Itm_Weights.ToArray();
            return true;
        }

        public bool ManualInputAndParseKsFile(out int[] values , out int[] weights , out int new_CarryLoad)
        {
            new_CarryLoad = 0;
            values = new int[]{};
            weights = new int[]{};
            
            List<int> new_Itm_Values = new List<int>();
            List<int> new_Itm_Weights = new List<int>();

            Console.WriteLine("Type file name (do not forget file extension at the end e.x: .txt) Or type '0' to return to menu");
            string fileName = Console.ReadLine();
            if (fileName == "0")
                return false;

            string root = Program.GetExecutingDirectoryName();//AppDomain.CurrentDomain.BaseDirectory;
            String filePath = Path.GetFullPath(Path.Combine(root,"_userFiles",fileName));
            
            if (!File.Exists(filePath))
            {
                Console.WriteLine("FILE NOT FOUND !");
                return ManualInputAndParseKsFile(out values , out weights , out new_CarryLoad);
            }

            try
            {
                StreamReader sr = new StreamReader(filePath);

                string line;
                int line_count = 0;
                int itmCount = 0;
                
                Console.WriteLine("\n\n");

                line = sr.ReadLine(); // starting
                while (line != null) 
                {
                    
                    if (line_count == 0)
                    {
                        itmCount = int.Parse(line);
                        line_count++; // force counter
                        line = sr.ReadLine(); // force next line
                        Console.WriteLine("Item Count = " + itmCount);
                        continue;
                    }

                    if (line_count == itmCount+1)
                    {
                        new_CarryLoad = int.Parse(line);
                        line = sr.ReadLine(); // force next line
                        Console.WriteLine("Max Carry Load = " + new_CarryLoad);
                        break;
                    }
                
                    string[] frag = Regex.Split(line, @"\s+");
                    new_Itm_Values.Add(int.Parse(frag[2]));
                    new_Itm_Weights.Add(int.Parse(frag[3]));
                    Console.WriteLine("Index = " + frag[1] +" , Value = " + frag[2] + " , Weight = "+frag[3]);
    
                    line_count++; // ending
                    line = sr.ReadLine(); // ending

                }
            }
            catch (Exception ex)
            {
                string dummy = ex.Message;
                Console.WriteLine("FILE PARSING FAILED !!!!");
                return ManualInputAndParseKsFile(out values , out weights , out new_CarryLoad);
            }

            Console.WriteLine("\nFile Loaded !!!\n");
            WaitForInput();


            values = new_Itm_Values.ToArray();
            weights = new_Itm_Weights.ToArray();
            return true;
        }

        public string[] ManualParseFolder(out bool dirExists)
        {
            Console.WriteLine("Type folder name Or type '0' to return to menu");
            string path = Console.ReadLine();
            string[] files = Get_AllFolderFiles(Program.GetExecutingDirectoryName()+"/"+path , out dirExists );
            while (!dirExists && path != "0")
            {
                Console.WriteLine("Directory not Found !");
                Console.WriteLine("Try again or return to main menu by typing '0'");
                path = Console.ReadLine();
                files = Get_AllFolderFiles(Program.GetExecutingDirectoryName()+"/"+path , out dirExists);
            }
            return files;
        }

        public int ManualInputInteger()
        {
            string input = Console.ReadLine();
            int number;
            bool flag = Int32.TryParse(input, out number);
            while(!flag)
            {
                Console.WriteLine("PARSING FAILED: Please Enter an Integer !!");
                input = Console.ReadLine();
                flag = Int32.TryParse(input, out number);
            }
            return number;
        }

        public void ManualSaveTxtFile(string[] text)
        {
            Console.WriteLine("Would you like to save the results ?");
            Console.WriteLine("y / n");
            string answ = Console.ReadLine();
            while(answ != "y" && answ != "n")
            {
                Console.WriteLine("Please type only 'y' or 'n'");
                answ = Console.ReadLine();
            }
            if (answ == "n")
                return;
            Console.WriteLine("Type file name (NO extentions)");
            string fname = Console.ReadLine();
            CreateTxtFile(text , fname);
            WaitForInput();
            return;
        }

        public bool CreateTxtFile(string[] text , string fname , string pathOveride = "")
        {
            try
            {
                string path;
                if (pathOveride == "")
                    path = Program.GetExecutingDirectoryName()+"/_savedFiles/"+fname+".txt";
                else
                    path = pathOveride+fname+".txt";
                System.IO.File.WriteAllLines(path , text);
                Console.WriteLine("File successfully created at "+Program.GetExecutingDirectoryName()+"/_savedFiles");
                return true;
            } 
            catch(Exception ex)
            {
                string dummy = ex.Message;
                return false;
            }
            
        }

        public string[] Get_AllFolderFiles(string path , out bool dirExists)
        {     
            dirExists = true;
            string root = Program.GetExecutingDirectoryName();
            String filePath = Path.GetFullPath(Path.Combine(root,path));

            if (!Directory.Exists(path))
            {
                //Console.WriteLine("Directory Not found : "+path);
                dirExists = false;
                return new string[]{};
            }

            DirectoryInfo info = new DirectoryInfo(filePath);
            FileInfo[] files = info.GetFiles().OrderBy(p => p.CreationTime).ToArray();

            string[] filenames = new string[files.Length];

            for(int i = 0; i < files.Length; i++)
            {
                Console.WriteLine(files[i].FullName);
                filenames[i] = files[i].FullName;
            }
            Console.WriteLine("\n\nFILE GENERATION SUCCESSFULLY COMPLETED !!!\n\n");
            WaitForInput();
            return filenames;
        }

    }

