using System;
using System.Text;

namespace QueenSolution
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            new QueueSolution(8);
        }
    }

    /// <summary>
    /// 最大使用8x8的棋盘
    /// </summary>
    public class QueueSolution
    {

        private readonly StringBuilder _stringBuilder;
        private int _count;
        
        public QueueSolution(int size = 4)
        {
            _stringBuilder = new StringBuilder();
            _count = 0;
            Handle(0UL, 0, size);
        }

        // 递归处理
        private void Handle(ulong chessboard, int row, int size)
        {
            if (row == size)
            {
                Console.WriteLine($"第{++_count}种解法：");
                PrintChessboard(chessboard, size);
                return;
            }

            for (int col = 0; col < size; col++)
            {
                if (!CheckLegal(chessboard, row, col, size)) continue;
                var newBoard = chessboard;
                SetQueen(ref newBoard, row, col, size);
                Handle(newBoard, row + 1, size);
            }
        }

        // 打印棋盘
        private void PrintChessboard(ulong chessboard, int size)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    _stringBuilder.Append((chessboard & (1UL << (i * size + j))) == 0 ? 'X' : 'O');
                }
                Console.WriteLine(_stringBuilder.ToString());
                _stringBuilder.Clear();
            }
            Console.WriteLine();
        }

        // 落子皇后位置
        private void SetQueen(ref ulong chessboard, int row, int col, int size = 4)
        {
            chessboard |= (1UL << (row * size + col));
        }

        // 检测八皇后格子是否合法
        private bool CheckLegal(ulong chessboard, int row, int col, int size = 4)
        {
            // 检查上面所有行的格子，不包含当前行 
            var checkLength = row * size;
            for (int i = 0; i < checkLength; i++)
            {
                // 检查棋牌位是否有皇后
                if ((chessboard & (1UL << i)) == 0) continue;
                
                var usedCol = i % size;
                var usedRow = i / size;
                
                // 检查是否在同一列
                if(usedCol == col) return false;
                
                // 检查是否在同一135°斜线 \
                if (usedCol - usedRow == col - row) return false;
                
                // 检查是否在同一45°斜线  /
                if (usedCol + usedRow == col + row) return false;
            }

            return true;
        }
    }
}
