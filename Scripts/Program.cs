using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

    class Program
    {

        [DllImport("GeneratorDll.dll", CharSet = CharSet.Ansi , CallingConvention = CallingConvention.Cdecl , EntryPoint="genKSFiles")]
        public static extern int genKSFiles(int seed , [MarshalAs( UnmanagedType.LPStr )]string path); // DLL FUNCTION OF generator.c // generates 320 knapsack problems inside ./generatorFiles Folder

        static void Main()
        {
            
            Create_Directories();

            InputHandler inptH = new InputHandler();
            inptH.Opening_Message();
            string selection;
            do
            {
                selection = inptH.MainMenu();
                if (selection == "1")
                    Initiate_Standard_Sequence(inptH);
                else if (selection == "2")
                    Initiate_File_Generation_Sequence(inptH);
                else if (selection == "3")
                    Initiate_Manual_DP_Sequence(inptH);
                else if (selection == "4")
                    Initiate_Manual_REC_Sequence(inptH);
                else if (selection == "5")
                    Initiate_Maual_Folder_Parsing_Sequence(inptH);

            }
            while(selection != "0");
            
                
        }

        static void Initiate_Standard_Sequence(InputHandler inptH)
        {
            string[] flNames = inptH.Get_AllFolderFiles(GetExecutingDirectoryName()+"/_StandardFiles" , out bool dirExists);
            if (!dirExists)
            {
                Console.WriteLine("Directory Not found");
                return;
            }

            Knapsack ks = new Knapsack();
            GoogleOrToolsSolver sl = new GoogleOrToolsSolver();

            Console.WriteLine("\n\nPROBLEM SOLVING SEQUENCE INITIATED !!!\n\n");
            string[] text = new string[flNames.Length];
            for(int i = 0; i < flNames.Length; i++)
            {
                if(inptH.ParseKsFile(flNames[i] , out int[] values , out int[] weights , out int new_crld))
                {
                    ks.ExecuteDP(values , weights , new_crld , out int res1 , out long time1);
                    sl.ExecuteDPKnapsack(values , weights , new_crld , out long res2 , out long time2);
                    text[i] = "Solved Test No:"+(i+1)+" App result: "+res1+" execT: "+time1+"ms - OrTools result: "+res2+" execT: "+time2+"ms";
                    Console.WriteLine("Solved Test No:"+(i+1)+" App result: "+res1+" execT: "+time1+"ms - OrTools result: "+res2+" execT: "+time2+"ms");
                }
                else
                    text[i] = "Test No:"+(i+1)+" , PARSING FAILED";
            }
            inptH.ManualSaveTxtFile(text);
            Console.WriteLine("\n\nSOLVING SEQUENCE SUCCESSFULLY COMPLETED !!!\n\n");
        }

        static void Initiate_File_Generation_Sequence(InputHandler inptH)
        {
            Console.WriteLine("Type seed Off-set (As Integer) (Default is (0) 'zero')");
            int offSet = inptH.ManualInputInteger();
            if(!Generate_KnapsackFiles(offSet))
                return;

            
            string[] flNames = inptH.Get_AllFolderFiles(GetExecutingDirectoryName()+"/_generatorFiles", out bool dirExists);
            if (!dirExists)
            {
                Console.WriteLine("Directory Not found");
                return;
            }
                

            Knapsack ks = new Knapsack();
            GoogleOrToolsSolver sl = new GoogleOrToolsSolver();
            string[] text = new string[flNames.Length];
            Console.WriteLine("\n\nPROBLEM SOLVING SEQUENCE INITIATED !!!\n\n");
            for(int i = 0; i < flNames.Length; i++)
            {
                if(inptH.ParseKsFile(flNames[i] , out int[] values , out int[] weights , out int new_crld))
                {

                    ks.ExecuteDP(values , weights , new_crld , out int res1 , out long time1);
                    sl.ExecuteDPKnapsack(values , weights , new_crld , out long res2 , out long time2);
                    text[i] = "Solved Test No:"+(i+1)+" App result: "+res1+" execT: "+time1+"ms - OrTools result: "+res2+" execT: "+time2+"ms";
                    Console.WriteLine("Solved Test No:"+(i+1)+" App result: "+res1+" execT: "+time1+"ms - OrTools result: "+res2+" execT: "+time2+"ms");
                }
                else
                    text[i] = "Test No:"+(i+1)+" , PARSING FAILED";
            }
            inptH.ManualSaveTxtFile(text);
            Console.WriteLine("\n\nSOLVING SEQUENCE SUCCESSFULLY COMPLETED !!!\n\n");
        }

        static void Initiate_Manual_DP_Sequence(InputHandler inptH)
        {
            string[] text = new string[1];
            if(inptH.ManualInputAndParseKsFile(out int[] values , out int[] weights , out int new_crld))
            {
                Console.WriteLine("\nAlgorithm Started !\n");
                
                Knapsack ks = new Knapsack();
                GoogleOrToolsSolver sl = new GoogleOrToolsSolver();
                ks.ExecuteDP(values , weights , new_crld , out int res1 , out long time1);
                sl.ExecuteDPKnapsack(values , weights , new_crld , out long res2 , out long time2);
                text[0] = "App result: "+res1+" execT: "+time1+"ms - OrTools result: "+res2+" execT: "+time2+"ms";
                Console.WriteLine("App result: "+res1+" execT: "+time1+"ms - OrTools result: "+res2+" execT: "+time2+"ms");
                Console.WriteLine("\nAlgorithm Finised !\n");
                inptH.WaitForInput();
                inptH.ManualSaveTxtFile(text);
            }
            
        }

        static void Initiate_Manual_REC_Sequence(InputHandler inptH)
        {
            string[] text = new string[1];
            if(inptH.ManualInputAndParseKsFile(out int[] values , out int[] weights , out int new_crld))
            {
                Console.WriteLine("\nAlgorithm Started !\n");
                Knapsack ks = new Knapsack();
                ks.ExecuteREC(values , weights , new_crld , out int res , out long time);

                text[0] = "App (Recursive) result: "+res+" execT: "+time+"ms";
                Console.WriteLine("App (Recursive) result: "+res+" execT: "+time+"ms");
                Console.WriteLine("\nAlgorithm Finised !\n");
                inptH.WaitForInput();
                inptH.ManualSaveTxtFile(text);
            }
        }

        public static void Initiate_Maual_Folder_Parsing_Sequence(InputHandler inptH)
        {
            string[] flNames = inptH.ManualParseFolder(out bool dirExists);
            if (dirExists)
            {
                Knapsack ks = new Knapsack();
                GoogleOrToolsSolver sl = new GoogleOrToolsSolver();

                Console.WriteLine("\n\nPROBLEM SOLVING SEQUENCE INITIATED !!!\n\n");
                string[] text = new string[flNames.Length];
                for(int i = 0; i < flNames.Length; i++)
                {
                    if(inptH.ParseKsFile(flNames[i] , out int[] values , out int[] weights , out int new_crld))
                    {
                        ks.ExecuteDP(values , weights , new_crld , out int res1 , out long time1);
                        sl.ExecuteDPKnapsack(values , weights , new_crld , out long res2 , out long time2);
                        text[i] = "Solved Test No:"+(i+1)+" App result: "+res1+" execT: "+time1+"ms - OrTools result: "+res2+" execT: "+time2+"ms";
                        Console.WriteLine("Solved Test No:"+(i+1)+" App result: "+res1+" execT: "+time1+"ms - OrTools result: "+res2+" execT: "+time2+"ms");
                    }
                    else
                        text[i] = "Test No:"+(i+1)+" , PARSING FAILED";
                }
                inptH.ManualSaveTxtFile(text);
                Console.WriteLine("\n\nSOLVING SEQUENCE SUCCESSFULLY COMPLETED !!!\n\n");
            }
        }

        public static void Create_Directories()
        {
            string Dir1 = GetExecutingDirectoryName()+"/_generatorFiles";
            string Dir2 = GetExecutingDirectoryName()+"/_userFiles";
            string Dir3 = GetExecutingDirectoryName()+"/_savedFiles";
            Directory.CreateDirectory(Dir1);
            Directory.CreateDirectory(Dir2);
            Directory.CreateDirectory(Dir3);
        }

        public static string GetExecutingDirectoryName()
        {
            //var location = new Uri(Assembly.GetEntryAssembly().GetName().CodeBase);
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);//new FileInfo(location.AbsolutePath).Directory.FullName;
        }

        public static bool Generate_KnapsackFiles(int seed)
        {
            try
            {
                genKSFiles(0 , GetExecutingDirectoryName()+"/_generatorFiles/");
                return true;
            }
            catch (Exception  ex)
            {
                Console.WriteLine(ex);
                string dummy = ex.Message;
                Console.WriteLine("GeneratorDll is not loaded or is not functioning correctly !");
                return false;
            }
        }

    }
