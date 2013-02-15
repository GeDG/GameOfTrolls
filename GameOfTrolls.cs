using System;
using System.Diagnostics;

class Tile : IComparable
{
    public int tileHeight;
    public int row;
    public int column;
    //void put();
    //void take();

    public Tile(int tileHeight, int row, int column)
    {
        this.tileHeight = tileHeight;
        this.row = row;
        this.column = column;
    }

    public int CompareTo(object obj)
    {
        if ((obj as Tile).tileHeight - tileHeight < 0)
            return -1;
        else if ((obj as Tile).tileHeight - tileHeight > 0)
            return 1;
        else
            return 0;
    }
}

class Entry : IComparable
{
    public Tile[] collectTile;
    public int blocks;
    public int turnsForAlignment;
    public decimal efficiency;

    public Entry(Tile[] collectTile)
    {
        this.collectTile = collectTile;


        blocks = 0;
        for (int i = 0; i < collectTile.Length; i++)
        {
            blocks += collectTile[i].tileHeight;
        }
        decimal dHeight;

        if (collectTile.Length % 2 == 1)
        {
            Array.Sort(collectTile);
            dHeight = collectTile[collectTile.Length / 2].tileHeight;
        }
        else
        {
            dHeight = blocks / collectTile.Length;
        }
        int height = (int)(dHeight + 0.5M);
        turnsForAlignment = 0;
        for (int i = 0; i < collectTile.Length; i++)
        {
            turnsForAlignment += Math.Abs(collectTile[i].tileHeight - height);
        }
        if (turnsForAlignment == 0)
            efficiency = -1;
        else
            efficiency = (decimal)(blocks) / turnsForAlignment;
    }

    public int CompareTo(object obj)
    {
        if (((obj as Entry).efficiency - this.efficiency) < 0)
            return -1;
        else if (((obj as Entry).efficiency - this.efficiency) > 0)
            return 1;
        else
            return 0;
    }

    internal void MakeMove()
    {
        GameOfTrolls.turns -= turnsForAlignment;
        GameOfTrolls.collectedBlocks += blocks;
        for (int i = 0; i < collectTile.Length; i++)
        {
            collectTile[i].tileHeight = 0;
         //   Console.WriteLine("\tCollect tile [{0}][{1}]", collectTile[i].row, collectTile[i].column);
        }
        //Console.WriteLine("\tCollect {0} blocks", blocks);
    }
}

class Field
{
    Tile[] neighbour;
    public Tile source;
    public Entry[] solutionEntry;

    public Field(Tile source, int currentTurns)
    {
        this.source = source;
        int neighbours = 0;
        int[] whereIsNeighbourX = new int[4];
        int[] whereIsNeighbourY = new int[4];
        if (source.row > 0)
        {
            whereIsNeighbourX[neighbours] = -1;
            whereIsNeighbourY[neighbours] = 0;
            neighbours++;
        }
        if (source.row < GameOfTrolls.fieldWidth - 1)
        {
            whereIsNeighbourX[neighbours] = 1;
            whereIsNeighbourY[neighbours] = 0;
            neighbours++;
        }
        if (source.column > 0)
        {
            whereIsNeighbourX[neighbours] = 0;
            whereIsNeighbourY[neighbours] = -1;
            neighbours++;
        }
        if (source.column < GameOfTrolls.fieldWidth - 1)
        {
            whereIsNeighbourX[neighbours] = 0;
            whereIsNeighbourY[neighbours] = 1;
            neighbours++;
        }
        neighbour = new Tile[neighbours];
        for (int i = 0; i < neighbours; i++)
        {
            neighbour[i] = GameOfTrolls.game[source.row + whereIsNeighbourX[i], source.column + whereIsNeighbourY[i]];
        }

        if (neighbour.Length == 2)
        {
            solutionEntry = new Entry[3];
            Tile[] collTiles = { source, neighbour[0], neighbour[1] };
            solutionEntry[0] = new Entry(collTiles);
            Tile[] a = { source, neighbour[0] };
            solutionEntry[1] = new Entry(a);
            Tile[] b = { source, neighbour[1] };
            solutionEntry[2] = new Entry(b);
        }

        if (neighbour.Length == 3)
        {
            solutionEntry = new Entry[7];
            Tile[] collTiles = { source, neighbour[0], neighbour[1], neighbour[2] };
            solutionEntry[0] = new Entry(collTiles);
            Tile[] a = { source, neighbour[0], neighbour[1] };
            solutionEntry[1] = new Entry(a);
            Tile[] b = { source, neighbour[0], neighbour[2] };
            solutionEntry[2] = new Entry(b);
            Tile[] c = { source, neighbour[1], neighbour[2] };
            solutionEntry[3] = new Entry(c);
            Tile[] d = { source, neighbour[0] };
            solutionEntry[4] = new Entry(d);
            Tile[] e = { source, neighbour[1] };
            solutionEntry[5] = new Entry(e);
            Tile[] f = { source, neighbour[2] };
            solutionEntry[6] = new Entry(f);
        }

        if (neighbour.Length == 4)
        {
            solutionEntry = new Entry[15];
            Tile[] collTiles = { source, neighbour[0], neighbour[1], neighbour[2], neighbour[3] };
            solutionEntry[0] = new Entry(collTiles);
            Tile[] collTiles2 = { source, neighbour[0], neighbour[1], neighbour[2] };
            solutionEntry[1] = new Entry(collTiles2);
            Tile[] collTiles3 = { source, neighbour[1], neighbour[2], neighbour[3] };
            solutionEntry[2] = new Entry(collTiles3);
            Tile[] collTiles4 = { source, neighbour[2], neighbour[3], neighbour[0] };
            solutionEntry[3] = new Entry(collTiles4);
            Tile[] collTiles5 = { source, neighbour[3], neighbour[0], neighbour[1] };
            solutionEntry[4] = new Entry(collTiles5);
            Tile[] a = { source, neighbour[0], neighbour[1] };
            solutionEntry[5] = new Entry(a);
            Tile[] b = { source, neighbour[1], neighbour[2] };
            solutionEntry[6] = new Entry(b);
            Tile[] c = { source, neighbour[2], neighbour[3] };
            solutionEntry[7] = new Entry(c);
            Tile[] d = { source, neighbour[3], neighbour[0] };
            solutionEntry[8] = new Entry(d);
            Tile[] e = { source, neighbour[1], neighbour[3] };
            solutionEntry[9] = new Entry(e);
            Tile[] f = { source, neighbour[0], neighbour[2] };
            solutionEntry[10] = new Entry(f);
            Tile[] g = { source, neighbour[0] };
            solutionEntry[11] = new Entry(g);
            Tile[] h = { source, neighbour[1] };
            solutionEntry[12] = new Entry(h);
            Tile[] i = { source, neighbour[2] };
            solutionEntry[13] = new Entry(i);
            Tile[] j = { source, neighbour[3] };
            solutionEntry[14] = new Entry(j);
        }
    }
}

class Move
{
    public Tile[] oldFields;
    public int[] oldHeights;
    public int oldTurns;
    public Move(Entry whichMove)
    {
        oldFields = whichMove.collectTile;
        oldHeights = new int[oldFields.Length];
        for (int j = 0; j < whichMove.collectTile.Length; j++)
        {
            oldHeights[j] = whichMove.collectTile[j].tileHeight;
        }
        oldTurns = whichMove.turnsForAlignment;
    }
}
class GameOfTrolls
{
    public static Tile[,] game;
    public static Tile[] sortedTowers;
    public static Field[] fieldData;
    public static Move[] allMoves;
    public static Entry[] currentTurnSelection;
    public static Entry[] currentTurnAllBlocks;
    public static int fieldWidth;
    public static int turns;
    public static int collectedBlocks = 0;
    public static int SizeOfMoves = 0;

    static void RadixSort(Tile[] toSort)
    {
        Tile[,] bucket = new Tile[10, toSort.Length];
        int[] inBucket = new int[10];

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                inBucket[j] = 0;
            }

            for (int j = 0; j < toSort.Length; j++)
            {
                int whichBucket = (int)(toSort[j].tileHeight / Math.Pow(10, i)) % 10;
                bucket[whichBucket, inBucket[whichBucket]] = toSort[j];
                inBucket[whichBucket]++;
            }
            int where = 0;
            for (int j = 9; j >= 0; j--)
            {
                for (int k = 0; k < inBucket[j]; k++)
                {
                    toSort[where] = bucket[j, k];
                    where++;
                }
            }
        }

    }
    static void PrintBoard()
    {
        for (int i = 0; i < game.GetLength(0); i++)
        {
            for (int j = 0; j < game.GetLength(1); j++)
            {
                Console.Write("{0,5}", game[i, j].tileHeight);
            }
            Console.WriteLine();
        }
    }
    static void Main()
    {
        Stopwatch sw = new Stopwatch();
        System.IO.StreamReader file = new System.IO.StreamReader("MatrixMil.txt");
        if (file == null)
        {
            Console.WriteLine("Invalid file");
            return;
        }

        Console.BufferWidth = 5000;
        Console.BufferHeight = 10000;
        turns = int.Parse(Console.ReadLine());
        fieldWidth = int.Parse(Console.ReadLine());

        game = new Tile[fieldWidth, fieldWidth];
        sortedTowers = new Tile[fieldWidth * fieldWidth];
        fieldData = new Field[fieldWidth * fieldWidth];
        allMoves = new Move[turns];
        currentTurnAllBlocks = new Entry[turns];

        Random rand = new Random();
        for (int i = 0; i < fieldWidth; i++)
        {
            string row = Console.ReadLine();
            string[] cell = row.Split(' ');

            for (int j = 0; j < fieldWidth; j++)
            {
                game[i, j] = new Tile(int.Parse(cell[j]), i, j);
                sortedTowers[i * fieldWidth + j] = game[i, j];
            }
        }

        //for (int i = 0; i < fieldWidth; i++)
        //{
        //    for (int j = 0; j < fieldWidth; j++)
        //    {
        //        game[i, j] = new Tile(rand.Next(1,1001), i, j);
        //        sortedTowers[i * fieldWidth + j] = game[i, j];
        //    }
        //}
        //PrintBoard();

        sw.Start();
        RadixSort(sortedTowers);

        int movesToConsider = 3000;
        bool moveMade = false;

        while (turns > 0)
        {

            int turnSelectionCount = 0;

            for (int i = 0; i < movesToConsider; i++)
            {
                fieldData[i] = new Field(sortedTowers[i], GameOfTrolls.turns);
                turnSelectionCount += fieldData[i].solutionEntry.Length;
            }

            currentTurnSelection = new Entry[turnSelectionCount];
            int turnSelectionAt = 0;

            for (int i = 0; i < movesToConsider; i++)
            {
                for (int j = 0; j < fieldData[i].solutionEntry.Length; j++)
                {
                    currentTurnSelection[turnSelectionAt] = fieldData[i].solutionEntry[j];
                    turnSelectionAt++;
                }
            }

            Array.Sort(currentTurnSelection);

            for (int i = 0; i < currentTurnSelection.Length; i++)
            {
                if (currentTurnSelection[i].turnsForAlignment == GameOfTrolls.turns && currentTurnSelection[i].turnsForAlignment > 0)
                {
                    currentTurnAllBlocks[SizeOfMoves] = currentTurnSelection[i];
                    break;
                }
            }

            moveMade = false;

            for (int i = 0; i < turnSelectionCount; i++)
            {
                if (currentTurnSelection[i].turnsForAlignment <= GameOfTrolls.turns && currentTurnSelection[i].turnsForAlignment > 0)
                {
                    //Console.WriteLine(SizeOfMoves);
                    allMoves[SizeOfMoves] = new Move(currentTurnSelection[i]);
                    SizeOfMoves++;

                    currentTurnSelection[i].MakeMove();
                    //PrintBoard();
                    //Console.WriteLine("Turns left: {0}", GameOfTrolls.turns);
                    moveMade = true;
                    break;
                }
            }

        }


        if (turns > 0)
        {
            int oldScore = GameOfTrolls.collectedBlocks + GameOfTrolls.turns;

            for (int i = SizeOfMoves - 1; i >= 0; i--)
            {
                for (int j = 0; j < allMoves[i].oldFields.Length; j++)
                {
                    allMoves[i].oldFields[j].tileHeight = allMoves[i].oldHeights[j];
                    GameOfTrolls.collectedBlocks -= allMoves[i].oldHeights[j];
                }

                GameOfTrolls.turns += allMoves[i].oldTurns;
                Console.WriteLine("Returning");
                //Console.WriteLine("Turns left: {0}", GameOfTrolls.turns);
                //GameOfTrolls.PrintBoard();

                if (currentTurnAllBlocks[i] != null && currentTurnAllBlocks[i].blocks + GameOfTrolls.collectedBlocks > oldScore)
                {
                    currentTurnAllBlocks[i].MakeMove();
                }

                if (GameOfTrolls.turns == 0)
                {
                    break;
                }
            }

            if (turns > 0)
            {
                for (int i = 0; i < SizeOfMoves; i++)
                {
                    for (int j = 0; j < allMoves[i].oldFields.Length; j++)
                    {
                        allMoves[i].oldFields[j].tileHeight = 0;
                        collectedBlocks += allMoves[i].oldHeights[j];
                    }
                    turns -= allMoves[i].oldTurns;
                }
            }
            //GameOfTrolls.PrintBoard();
        }

        Console.WriteLine("Game score: {0}", GameOfTrolls.collectedBlocks);
        //Console.WriteLine("We are left with {0} turns with which to take some blocks and finish the game", GameOfTrolls.turns);
        //***************************************
        sw.Stop();
        //Writing Execution Time in label
        string ExecutionTimeTaken = string.Format("Mili seconds :{0}", sw.Elapsed.TotalMilliseconds);
        Console.WriteLine(ExecutionTimeTaken);
        //***************************************
    }
}

