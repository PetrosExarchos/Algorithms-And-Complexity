using System;

    class Knapsack
    {
        public Knapsack()
        {

        }

        public void ExecuteDP(int[] new_Item_Values , int[] new_Item_Weights , int new_MaxCarryLoad , out int DPresult , out long execTime)
        {
            System.Diagnostics.Stopwatch watch = System.Diagnostics.Stopwatch.StartNew();
            DPresult = DP_Ks(new_Item_Values , new_Item_Weights , new_MaxCarryLoad);
            watch.Stop();
            execTime = watch.ElapsedMilliseconds;
        }

        public int DP_Ks(int[] new_Item_Values , int[] new_Item_Weights , int new_MaxCarryLoad)
        {
            int i, w;
            int n = new_Item_Values.Length; // can be param
            int W = new_MaxCarryLoad; // can be param
            int[,] K = new int[n+1,W+1];
            
            for (i = 0; i <= n; i++)  // Iterate all Items
            {
                for (w = 0; w <= W; w++) 
                {
                    if (i==0 || w==0)
                    {
                        K[i,w] = 0;
                    }
                    else if (new_Item_Weights[i-1] <= w) 
                    {
                        K[i,w] = Math.Max(new_Item_Values[i-1] + K[i-1,w-new_Item_Weights[i-1]],  K[i-1,w]);
                    }
                    else
                    {
                        K[i,w] = K[i-1,w];
                    }
                }
            }
            
            return K[K.GetLength(0)-1 , K.GetLength(1)-1]; 
        }

        public void ExecuteREC(int[] new_Item_Values , int[] new_Item_Weights , int new_MaxCarryLoad , out int RECresult , out long execTime)
        {
            System.Diagnostics.Stopwatch watch = System.Diagnostics.Stopwatch.StartNew();
            RECresult = Rec_Ks(new_Item_Values, new_Item_Weights, new_Item_Values.Length-1, new_MaxCarryLoad);
            watch.Stop();
            execTime = watch.ElapsedMilliseconds;
        }

        public int Rec_Ks(int[] new_Item_Values , int[] new_Item_Weights, int c_index , int c_carryLoad, int Val = 0)
        {
            if (c_index == -1 || c_carryLoad == 0)
            {
                return Val;
            }
            else if ((c_carryLoad - new_Item_Weights[c_index]) < 0)
            {
                return Rec_Ks(new_Item_Values, new_Item_Weights, c_index-1, c_carryLoad , Val); // not enough space // try next item
            }
            else
            {
                int tmp1 = Rec_Ks(new_Item_Values, new_Item_Weights, c_index-1, c_carryLoad , Val); // ignore item // try next item
                int tmp2 = Rec_Ks(new_Item_Values, new_Item_Weights, c_index-1 , c_carryLoad - new_Item_Weights[c_index] ,Val+new_Item_Values[c_index]); // store item // try next item
                if (tmp1 >= tmp2)
                    return tmp1;
                else
                    return tmp2;
            }
        }
        
    }

