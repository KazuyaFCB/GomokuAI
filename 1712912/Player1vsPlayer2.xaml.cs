using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace _1712912
{
    /// <summary>
    /// Interaction logic for Player1vsPlayer2.xaml
    /// </summary>
    public partial class Player1vsPlayer2 : Window
    {
        int MAX_COL = 6;
        int MAX_ROW = 6;
        const int CELL_WIDTH = 28;
        const int CELL_HEIGHT = 28;
        int[,] board;
        Button[,] gridButton;
        int numberOfMove = 0;

        const int LEFT_OFFSET = 5;
        const int TOP_OFFSET = 2;
        public Player1vsPlayer2()
        {
            InitializeComponent();

            board = new int[MAX_ROW, MAX_COL];
            gridButton = new Button[MAX_ROW, MAX_COL];

            var gridLength = new GridLength(CELL_WIDTH);
            for (int i = 0; i < MAX_COL + LEFT_OFFSET * 2; i++)
            {
                ColumnDefinition col = new ColumnDefinition();
                col.Width = gridLength;
                UI.ColumnDefinitions.Add(col);
            }

            for (int i = 0; i < MAX_ROW + TOP_OFFSET * 2; i++)
            {
                RowDefinition row = new RowDefinition();
                row.Height = gridLength;
                UI.RowDefinitions.Add(row);
            }

            for (int i = 0; i < MAX_ROW; i++)
            {
                for (int j = 0; j < MAX_COL; j++)
                {
                    Button cell = new Button();
                    cell.Background = Brushes.Lavender;
                    cell.Width = CELL_WIDTH;
                    cell.Height = CELL_HEIGHT;

                    cell.Tag = new Tuple<int, int>(i, j);
                    cell.Click += cell_Click;

                    gridButton[i, j] = cell;

                    UI.Children.Add(cell);
                    Grid.SetColumn(cell, j + LEFT_OFFSET);
                    Grid.SetRow(cell, i + TOP_OFFSET);
                }
            }

            initBoard();
            
            img_x_bold.Source = bmp_x_bold;
            img_o_bold.Source = bmp_o_bold;
        }

        int isXTurn = 1;
        int row_last = -1;
        int col_last = -1;

        BitmapImage bmp_x_bold = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Images\\x_bold.png"));
        BitmapImage bmp_o_bold = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Images\\o_bold.png"));
        Image img_x_bold = new Image();
        Image img_o_bold = new Image();


        private void cell_Click(object sender, RoutedEventArgs e)
        {
            Button cell = sender as Button;
            int row_cur = (cell.Tag as Tuple<int, int>).Item1;
            int col_cur = (cell.Tag as Tuple<int, int>).Item2;
            if (makeMove(row_cur, col_cur) == 0 && numberOfMove == MAX_ROW * MAX_COL)
            {
                MessageBox.Show("Hòa");
                initBoard();
            }
            
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "caro.txt";
            if (saveFileDialog.ShowDialog() == true)
            {
                string data = MAX_ROW.ToString() + "\t" + isXTurn.ToString() + "\t" + row_last.ToString() + "\t" + col_last.ToString();
                for (int i = 0; i < MAX_ROW; i++)
                {
                    data = data + "\n";
                    for (int j = 0; j < MAX_COL; j++)
                    {
                        data = data + board[i, j] + "\t";
                    }
                }
                File.WriteAllText(saveFileDialog.FileName, data);
            }
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                var lines = File.ReadAllLines(openFileDialog.FileName);
                char[] seperator = { '\t' };
                var tokens = lines[0].Split(seperator, StringSplitOptions.RemoveEmptyEntries);
                MAX_ROW = int.Parse(tokens[0]);
                MAX_COL = int.Parse(tokens[0]);
                initBoard();
                isXTurn = int.Parse(tokens[1]);
                row_last = int.Parse(tokens[2]);
                col_last = int.Parse(tokens[3]);

                for (int i = 1; i < lines.Length; i++)
                {
                    var tokens2 = lines[i].Split(seperator, StringSplitOptions.RemoveEmptyEntries);
                    for (int j = 0; j < tokens2.Length; j++)
                    {
                        board[i - 1, j] = int.Parse(tokens2[j]);

                        if (board[i - 1, j] == 1)
                        {
                            setBitmapToButton(i - 1, j, AppDomain.CurrentDomain.BaseDirectory + "Images\\x.png");
                        }                               
                        else if (board[i - 1, j] == 0)  
                        {                               
                            setBitmapToButton(i - 1, j, AppDomain.CurrentDomain.BaseDirectory + "Images\\o.png");
                        }
                    }
                }
                if (isXTurn == 1)
                {
                    setBitmapToButton(row_last, col_last, AppDomain.CurrentDomain.BaseDirectory + "Images\\o_bold.png");
                }
                else
                {
                    setBitmapToButton(row_last, col_last, AppDomain.CurrentDomain.BaseDirectory + "Images\\x_bold.png");
                }
            }
        }

        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            initBoard();
        }

        bool checkWin(int piece, int x_cur, int y_cur)
        {
            int count = 1, dx = 1, dy;
            ////duyệt ngang
            //duyệt qua trái
            while (x_cur - dx >= 0 && board[x_cur - dx, y_cur] == piece && count < 5)
            {
                count++;
                dx++;
            }
            if (count == 5)
                return true;
            //duyệt qua phải
            dx = 1;
            while (x_cur + dx < MAX_ROW && board[x_cur + dx, y_cur] == piece && count < 5)
            {
                count++;
                dx++;
            }
            if (count == 5)
                return true;
            ////duyệt dọc
            //duyệt lên trên
            count = 1;
            dy = 1;
            while (y_cur - dy >= 0 && board[x_cur, y_cur - dy] == piece && count < 5)
            {
                count++;
                dy++;
            }
            if (count == 5)
                return true;
            //duyệt xuống dưới
            dy = 1;
            while (y_cur + dy < MAX_COL && board[x_cur, y_cur + dy] == piece && count < 5)
            {
                count++;
                dy++;
            }
            if (count == 5)
                return true;
            ////duyệt chéo ngược
            //chéo ngược lên
            dx = dy = count = 1;
            while (x_cur - dx >= 0 && y_cur - dy >= 0 && board[x_cur - dx, y_cur - dy] == piece && count < 5)
            {
                count++;
                dx++;
                dy++;
            }
            if (count == 5)
                return true;
            //chéo ngược xuống
            dx = dy = 1;
            while (x_cur + dx < MAX_ROW && y_cur + dy < MAX_COL && board[x_cur + dx, y_cur + dy] == piece && count < 5)
            {
                count++;
                dx++;
                dy++;
            }
            if (count == 5)
                return true;
            ////duyệt chéo xuôi
            //chéo xuôi lên
            dx = dy = count = 1;
            while (x_cur + dx < MAX_ROW && y_cur - dy >= 0 && board[x_cur + dx, y_cur - dy] == piece && count < 5)
            {
                count++;
                dx++;
                dy++;
            }
            if (count == 5)
                return true;
            //chéo xuôi xuống
            dx = dy = 1;
            while (x_cur - dx >= 0 && y_cur + dy < MAX_COL && board[x_cur - dx, y_cur + dy] == piece && count < 5)
            {
                count++;
                dx++;
                dy++;
            }
            if (count == 5)
                return true;

            return false;
        }

        void initBoard()
        {
            isXTurn = 1;
            row_last = -1;
            col_last = -1;
            numberOfMove = 0;
            for (int i = 0; i < MAX_ROW; i++)
            {
                for (int j = 0; j < MAX_COL; j++)
                {
                    board[i, j] = -1;
                    if (gridButton[i, j] != null)
                    {
                        gridButton[i, j].IsEnabled = true;
                        gridButton[i, j].Content = null;
                    }
                }
            }
        }

        int makeMove(int row, int col)
        {
            numberOfMove++;
            if (isXTurn == 1)
            {
                if (row_last != -1)
                {
                    setBitmapToButton(row_last, col_last, AppDomain.CurrentDomain.BaseDirectory + "Images\\o.png");
                }
                gridButton[row, col].Content = img_x_bold;
                board[row, col] = isXTurn;
                if (checkWin(1, row, col))
                {
                    MessageBox.Show("X thắng");
                    initBoard();
                    return 1;
                }
                isXTurn ^= 1;
                row_last = row;
                col_last = col;
                gridButton[row_last, col_last].IsEnabled = false;
            }
            else if (isXTurn == 0)
            {
                if (row_last != -1)
                {
                    setBitmapToButton(row_last, col_last, AppDomain.CurrentDomain.BaseDirectory + "Images\\x.png");
                }
                gridButton[row, col].Content = img_o_bold;
                board[row, col] = isXTurn;
                if (checkWin(0, row, col))
                {
                    MessageBox.Show("O thắng");
                    initBoard();
                    return 1;
                }
                isXTurn ^= 1;
                row_last = row;
                col_last = col;
                gridButton[row_last, col_last].IsEnabled = false;
            }
            return 0;
        }
        void setBitmapToButton(int row, int col, string pathImage)
        {
            Image img = new Image();
            img.Source = new BitmapImage(new Uri(pathImage));
            img.Width = CELL_WIDTH;
            img.Height = CELL_HEIGHT;
            gridButton[row, col].Content = img;
        }
    }
}

