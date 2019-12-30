using Xunit;

public class XUnit_UnitTests
{
    [Fact]
    public void UnitTest_Knapsack01()
    {
        Knapsack ksObj = new Knapsack();

         Assert.Equal(220 , ksObj.DP_Ks(new int[]{60,100,120}, new int[]{10,20,30} , 50));
    }

    [Fact]
    public void UnitTest_Knapsack_Rec()
    {
        Knapsack ksObj = new Knapsack();

         Assert.Equal(220 , ksObj.Rec_Ks(new int[]{60,100,120}, new int[]{10,20,30} , 2 , 50));
    }

    [Fact]
    public void UnitTest_OrTools_KapsackDP()
    {

        GoogleOrToolsSolver orToolsSolver = new GoogleOrToolsSolver();
        orToolsSolver.ExecuteDPKnapsack(new int[]{60,100,120}, new int[]{10,20,30} , 50 , out long DPresult , out long execTime);
        Assert.Equal(220 , Get(DPresult));
    }

    [Fact]
    public void UnitTest_FileCreatorAndParser()
    {
        Program.Create_Directories();

        string[] txt = new string[5];
        txt[0] = "3";
        txt[1] = "    1   60   10";
        txt[2] = "    1   100   20";
        txt[3] = "    1   120   30";
        txt[4] = "50";

        InputHandler inpt = new InputHandler();

        Assert.True(inpt.CreateTxtFile(txt , "unittestfile" , Program.GetExecutingDirectoryName()+"/"));

        Assert.True(inpt.ParseKsFile(Program.GetExecutingDirectoryName()+"/unittestfile.txt" ,out int[]values , out int[] weights , out int cry_load));

        Assert.Equal(60 , Get(values[0]));
        Assert.Equal(100 , Get(values[1]));
        Assert.Equal(120 , Get(values[2]));

        Assert.Equal(10 , Get(weights[0]));
        Assert.Equal(20 , Get(weights[1]));
        Assert.Equal(30 , Get(weights[2]));

        Assert.Equal(50 , Get(cry_load));

    }

    private int Get(int value)
    {
        return value;
    }

    private long Get(long value)
    {
        return value;
    }
}
