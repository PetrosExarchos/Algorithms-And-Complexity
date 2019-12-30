using System;
using Google.OrTools.Algorithms;

    class GoogleOrToolsSolver
    {

        public void ExecuteDPKnapsack(int[] new_itv , int[] new_itw , int new_crld , out long DPresult , out long execTime)
        {
            long[] values = {};
            long[,] weights = {};
            long[] capacities = {};

            DataTransformer(ref values , ref weights , ref capacities , new_itv , new_itw , new_crld);

            KnapsackSolver solver = new KnapsackSolver
            (
            KnapsackSolver.SolverType.KNAPSACK_DYNAMIC_PROGRAMMING_SOLVER,
            "KnapsackExample"
            );

            solver.Init(values, weights, capacities);

            System.Diagnostics.Stopwatch watch = System.Diagnostics.Stopwatch.StartNew();
            DPresult = solver.Solve();
            watch.Stop();
            execTime = watch.ElapsedMilliseconds;

            //Console.WriteLine(" <<>> OrTools Algorithm = result: "+computedValue+" ExecTime : "+elapsedMs+"ms");
            //Console.WriteLine("<<<ALGORITHM FINISHED>>>\n\n");

        }

        private void DataTransformer(ref long[] values , ref long[,] weights , ref long[] capacities , int[] new_itv , int[] new_itw , int new_crld)
        {

            // DATA TRANSFORMATION START

            values = new long[new_itv.Length];
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = (long)new_itv[i];
            }

            weights = new long[1,new_itw.Length];
            for (int i = 0; i < weights.Length; i++)
            {
                weights[0,i] = (long)new_itw[i];
            }

            capacities = new long[]{new_crld};

            // DATA TRANSFORMATION END
        }

    }




