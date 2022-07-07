
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public class Square
    {
        public int X, Y;
        public int W, H;
        public int colortype;
        public Square right;
        public Square left;
        public Square up;
        public Square down;
        public double cost = -1;
        public double cost_from_start;
        public int id;
        public string name;
        public int cost_so_far = 0;
        public int cost_he_so_far = 0;
        public int heuristic;
        public int dir = 0;
        public List<Square> adj = new List<Square>();
    }
    public class SquareList
    {
        public Square head;
        public Square last;


    }
    public class Circle
    {
        public int X, Y;
        public int W, H;
        public string option;
        public int color = 0;
    }

    public class cost_searcher
    {
        public List<Square> Open_Move = new List<Square>();
        public List<Square> Children = new List<Square>();
    }


    public partial class Form1 : Form
    {
        int N = 20;
        Bitmap off;
        bool isDrag = false;
        int catch_prev = -1;
        List<Square> Level = new List<Square>();
        List<Square> Open_Move = new List<Square>();
        List<Square> Closed_Move = new List<Square>();
        List<Square> Children = new List<Square>();
        List<cost_searcher> CS = new List<cost_searcher>();
        List<double> arr = new List<double>();
        Square goalnode = null;
        int count_flag = 0;
        public SquareList Li;
        List<Circle> CL = new List<Circle>();
        int flag_select = 0;
        Random rf = new Random();
        Square startnode = null;
        public int cost_flag = 0;
        public Form1()
        {
            this.WindowState = FormWindowState.Maximized;
            this.Load += Form1_Load;
            this.Paint += Form1_Paint;
            this.MouseDown += Form1_MouseDown;
            this.MouseMove += Form1_MouseMove;
            this.MouseUp += Form1_MouseUp;
            this.KeyDown += Form1_KeyDown;
        }

        void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
            {
                DFS();
            }
            if (e.KeyCode == Keys.S)
            {
                BFS();
            }
            if (e.KeyCode == Keys.C && goalnode != null && count_flag == 0)
            {
                Square g = goalnode;
                int flag = 0;
                ShowCost(g);
                ShowCostAdjacent();
                count_flag = 1;
                cost_flag = 1;
            }
            if (e.KeyCode == Keys.D)
            {
                UniformCost();
            }
            if (e.KeyCode == Keys.F)
            {
                Astar();
            }
            DrawDubb(CreateGraphics());
        }

        public void AddNode(SquareList L)
        {

            int X = 10; int Y = 10; int last_X = 10; int last_Y = 10;
            int subs = 2; int diff = subs - 1;
            Square temp = null;
            Square temp2 = null;
            Square last = null;
            int id = 0;
            char s = 'a';
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    Square sq = new Square();
                    sq.X = X;
                    sq.Y = Y;
                    sq.W = 90;
                    sq.H = 90;
                    sq.left = null;
                    sq.right = null;
                    sq.down = null;
                    sq.right = null;
                    sq.colortype = 1;
                    sq.cost = 0;
                    sq.id = id;
                    char fs = s;
                    sq.name += s;
                    sq.name += sq.id;
                    id++;
                    X += sq.W;
                    if (L.head == null)
                    {

                        L.head = sq;
                        L.last = sq;
                        temp = L.head;
                        last = L.head;
                    }
                    else
                    {
                        if (i > 0)
                        {

                            if (j == 0)
                            {
                                temp2 = sq;
                                temp = last;
                                temp.down = temp2;
                                temp2.up = temp;
                                temp = temp.right;
                                last = sq;
                            }
                            if (j > 0)
                            {
                                temp2 = sq;
                                temp.down = sq;
                                temp2.up = temp;
                                temp = temp.right;
                            }

                        }

                        if (j == 0)
                        {
                            L.last = sq;
                        }
                        else
                        {
                            sq.left = L.last;
                            L.last.right = sq;
                            L.last = sq;
                        }

                    }
                }
                s++;
                Y += 90;
                X = last_X;

            }

        }

        void DFS()
        {
            Level = new List<Square>();
            Open_Move = new List<Square>();
            Closed_Move = new List<Square>();
            Children = new List<Square>();
            CS = new List<cost_searcher>();
            Square pnn = startnode;
            pnn.colortype = 3;
            Open_Move.Add(pnn);
            int k = 0;
            int i = 0; int j = 0;
            int count = 0;
            Square curr = Open_Move[0];
            int re = 0;
            cost_searcher s = new cost_searcher();
            int goal_f = 0;
            int it = 0;

            while (Open_Move.Count != 0)
            {
                re++;
                count++;
                it++;
                curr = Open_Move[0];
                if (curr == goalnode)
                {
                    goal_f = 1;
                    Console.WriteLine("The line " + (re + 1) + " :");
                    for (int h = 0; h < Open_Move.Count; h++)
                    {
                        Console.WriteLine(Open_Move[h].name + ",");

                    }

                    MessageBox.Show("Goal found" + " no. of iterations : " + it);
                    break;
                }
                if (re == 2)
                {
                    Console.WriteLine("");
                }
                Console.WriteLine("The " + re + " line Open_Move ");
                for (int ff = 0; ff < Open_Move.Count; ff++)
                {
                    Console.WriteLine(Open_Move[ff].name + ",");
                }


                Children = new List<Square>();
                s = new cost_searcher();





                if (curr.left != null && curr.left.colortype != 3 && curr.left.colortype != 2 && curr.left.colortype != 10)
                {
                    Square a = curr.left;
                    a.colortype = 3;
                    int flag_r = 0;
                    for (int we = 0; we < Closed_Move.Count; we++)
                    {
                        if (curr.left == Closed_Move[we])
                        {
                            flag_r = 1;
                        }
                    }
                    if (flag_r == 0)
                    {
                        Children.Add(a);
                    }
                }
                if (curr.right != null && curr.right.colortype != 3 && curr.right.colortype != 2 && curr.right.colortype != 10)
                {
                    Square a = curr.right;
                    a.colortype = 3;
                    int flag_r = 0;
                    for (int we = 0; we < Closed_Move.Count; we++)
                    {
                        if (curr.right == Closed_Move[we])
                        {
                            flag_r = 1;
                        }
                    }
                    if (flag_r == 0)
                    {
                        Children.Add(a);
                    }
                }
                if (curr.up != null && curr.up.colortype != 3 && curr.up.colortype != 2 && curr.up.colortype != 10)
                {
                    Square a = curr.up;
                    a.colortype = 3;
                    int flag_r = 0;
                    for (int we = 0; we < Closed_Move.Count; we++)
                    {
                        if (curr.up == Closed_Move[we])
                        {
                            flag_r = 1;
                        }
                    }
                    if (flag_r == 0)
                    {
                        Children.Add(a);
                    }
                }
                if (curr.down != null && curr.down.colortype != 3 && curr.down.colortype != 2 && curr.down.colortype != 10)
                {
                    Square a = curr.down;
                    a.colortype = 3;
                    int flag_r = 0;
                    for (int we = 0; we < Closed_Move.Count; we++)
                    {
                        if (curr.down == Closed_Move[we])
                        {
                            flag_r = 1;
                        }
                    }
                    if (flag_r == 0)
                    {
                        Children.Add(a);
                    }
                }
                for (int bg = 0; bg < Children.Count; bg++)
                {
                    if (Children[bg] == goalnode)
                    {
                        Children.RemoveAt(bg);
                        Children.Add(goalnode);
                        break;
                    }
                }
                for (int ww = 0; ww < Children.Count; ww++)
                {

                    s.Children.Add(Children[ww]);
                }


                Console.WriteLine("The " + re + " line Children :");
                for (int ff = 0; ff < Children.Count; ff++)
                {
                    Console.WriteLine(Children[ff].name + ",");
                }

                for (int hh = 0; hh < Open_Move.Count; hh++)
                {

                    s.Open_Move.Add(Open_Move[hh]);

                }
                Open_Move.RemoveAt(0);

                for (int z = 0; z < Children.Count; z++)
                {
                    Open_Move.Insert(0, Children[z]);
                }



                CS.Add(s);



                if (Open_Move.Count == 0)
                {
                    MessageBox.Show("Goal not found");
                    break;
                }


                int x = 900;
                int y = 100;
                /*
                for (int f = 0; f < Open_Move.Count; f++)
                {
                    Graphics g = CreateGraphics();
                    g.DrawString(" " + Open_Move[f].id , new Font("System", 17), Brushes.DarkKhaki, x, 100);
                    x += 24;
                }
                 */

                curr.colortype = 10;
                DrawDubb(CreateGraphics());
            }

            if (goal_f == 1)
            {
                Square cur = Open_Move[0];
                int totalcost = 0;
                for (int c = CS.Count - 1; c >= 0; c--)
                {
                    for (int co = 0; co < CS[c].Children.Count; co++)
                    {
                        if (cur.name == CS[c].Children[co].name)
                        {
                            Console.WriteLine(cur.name + " ,");
                            for (int g = 0; g < CS[c].Open_Move[0].adj.Count; g++)
                            {
                                if (CS[c].Open_Move[0].adj[g].id == cur.id)
                                {
                                    totalcost += (int)CS[c].Open_Move[0].adj[g].cost;
                                }
                            }
                            cur = CS[c].Open_Move[0];
                            cur.colortype = 100;
                            break;
                        }
                    }
                }
                MessageBox.Show("The Total cost of the path : " + totalcost);
                Console.WriteLine(Li.head.name);
            }


        }
        void BFS()
        {

            Level = new List<Square>();
            Open_Move = new List<Square>();
            Closed_Move = new List<Square>();
            Children = new List<Square>();
            CS = new List<cost_searcher>();
            Square pnn = startnode;
            pnn.colortype = 3;
            Open_Move.Add(pnn);
            int k = 0;
            int i = 0; int j = 0;
            int count = 0;
            Square curr = Open_Move[0];
            int re = 0;
            int finish = 0;
            cost_searcher s = new cost_searcher();
            int it = 0;
            while (Open_Move.Count != 0)
            {
                re++;
                curr = Open_Move[0];
                it++;
                if (curr == goalnode)
                {
                    Console.WriteLine("The line " + (re + 1) + " :");
                    for (int h = 0; h < Open_Move.Count; h++)
                    {

                        Console.WriteLine(Open_Move[h].name + ",");

                    }
                    finish = 1;
                    MessageBox.Show("Goal found" + " no. of iterations : " + it);
                    break;
                }
                if (re == 2)
                {
                    Console.WriteLine("");
                }
                Console.WriteLine("The " + re + " line Open_Move ");
                for (int ff = 0; ff < Open_Move.Count; ff++)
                {
                    Console.WriteLine(Open_Move[ff].name + ",");
                }

                Children = new List<Square>();
                s = new cost_searcher();

                if (curr.left != null && curr.left.colortype != 3 && curr.left.colortype != 2 && curr.left.colortype != 10)
                {
                    Square a = curr.left;
                    a.colortype = 3;
                    int flag_r = 0;
                    for (int we = 0; we < Closed_Move.Count; we++)
                    {
                        if (curr.left == Closed_Move[we])
                        {
                            flag_r = 1;
                        }
                    }
                    if (flag_r == 0)
                    {
                        Children.Add(a);
                    }
                }
                if (curr.right != null && curr.right.colortype != 3 && curr.right.colortype != 2 && curr.right.colortype != 10)
                {
                    Square a = curr.right;
                    a.colortype = 3;
                    int flag_r = 0;
                    for (int we = 0; we < Closed_Move.Count; we++)
                    {
                        if (curr.right == Closed_Move[we])
                        {
                            flag_r = 1; break;
                        }
                    }
                    if (flag_r == 0)
                    {
                        Children.Add(a);
                    }


                }
                if (curr.up != null && curr.up.colortype != 3 && curr.up.colortype != 2 && curr.up.colortype != 10)
                {
                    Square a = curr.up;
                    a.colortype = 3;
                    int flag_r = 0;
                    for (int we = 0; we < Closed_Move.Count; we++)
                    {
                        if (curr.up == Closed_Move[we])
                        {
                            flag_r = 1; break;
                        }
                    }
                    if (flag_r == 0)
                    {
                        Children.Add(a);
                    }
                }
                if (curr.down != null && curr.down.colortype != 3 && curr.down.colortype != 2 && curr.down.colortype != 10)
                {
                    Square a = curr.down;
                    a.colortype = 3;
                    int flag_r = 0;
                    for (int we = 0; we < Closed_Move.Count; we++)
                    {
                        if (curr.down == Closed_Move[we])
                        {
                            flag_r = 1; break;
                        }
                    }
                    if (flag_r == 0)
                    {
                        Children.Add(a);
                    }

                }
                for (int ww = 0; ww < Children.Count; ww++)
                {

                    s.Children.Add(Children[ww]);
                }
                Console.WriteLine("The " + re + " line Children :");
                for (int ff = 0; ff < Children.Count; ff++)
                {
                    Console.WriteLine(Children[ff].name + ",");
                }
                for (int hh = 0; hh < Open_Move.Count; hh++)
                {

                    s.Open_Move.Add(Open_Move[hh]);

                }
                Open_Move.RemoveAt(0);

                for (int z = 0; z < Children.Count; z++)
                {
                    Open_Move.Add(Children[z]);
                }
                CS.Add(s);

                /*
                for (int z = 0; z < Children.Count; z++)
                {
                    Open_Move.Add(Children[z]);
                }
                 */
                if (Open_Move.Count == 0)
                {
                    MessageBox.Show("Done");
                    break;
                }
                int x = 900;
                int y = 100;
                /*
                for (int f = 0; f < Open_Move.Count; f++)
                {
                    Graphics g = CreateGraphics();
                    g.DrawString(" " + Open_Move[f].id , new Font("System", 17), Brushes.DarkKhaki, x, 100);
                    x += 24;
                }
                 */

                curr.colortype = 10;
                DrawDubb(CreateGraphics());


            }
            if (finish == 1)
            {
                int totalcost = 0;
                Square cur = Open_Move[0];
                for (int c = CS.Count - 1; c >= 0; c--)
                {
                    for (int co = 0; co < CS[c].Children.Count; co++)
                    {
                        if (cur.name == CS[c].Children[co].name)
                        {
                            Console.WriteLine(cur.name + " ,");
                            for (int g = 0; g < CS[c].Open_Move[0].adj.Count; g++)
                            {
                                if (CS[c].Open_Move[0].adj[g].id == cur.id)
                                {
                                    totalcost += (int)CS[c].Open_Move[0].adj[g].cost;
                                }
                            }
                            cur = CS[c].Open_Move[0];
                            cur.colortype = 100;
                            break;
                        }
                    }
                }
                MessageBox.Show("The Total cost of the path : " + totalcost);
                Console.WriteLine(Li.head.name);
            }

        }
        
        void Greedy()
        {
            Level = new List<Square>();
            Open_Move = new List<Square>();
            Closed_Move = new List<Square>();
            Children = new List<Square>();
            CS = new List<cost_searcher>();
            Square pnn = startnode;
            pnn.colortype = 3;
            Open_Move.Add(pnn);
            int k = 0;
            int i = 0; int j = 0;
            int count = 0;
            Square curr = Open_Move[0];
            int re = 0;
            int finish = 0;
            int it = 0;
            cost_searcher s = new cost_searcher();



            while (Open_Move.Count != 0)
            {
                re++;
                curr = Open_Move[0];
                it++;
                if (curr == goalnode)
                {
                    Console.WriteLine("The line " + (re + 1) + " :");
                    for (int h = 0; h < Open_Move.Count; h++)
                    {
                        Console.WriteLine(Open_Move[h].name + ",");

                    }
                    finish = 1;
                    MessageBox.Show("Goal found" + " The number of iterations is " + it); break;
                }
                if (re == 2)
                {
                    Console.WriteLine("");
                }
                Console.WriteLine("The " + re + " line Open_Move ");
                for (int ff = 0; ff < Open_Move.Count; ff++)
                {
                    Console.WriteLine(Open_Move[ff].name + ",");
                }


                Children = new List<Square>();
                s = new cost_searcher();





                if (curr.left != null && curr.left.colortype != 2 && curr.left.colortype != 10)
                {
                    Square a = curr.left; a.colortype = 3;

                    int flag = 1;
                    for (int r = 0; r < curr.adj.Count; r++)
                    {
                        if (curr.left.id == curr.adj[r].id)
                        {

                          
                            curr.left.cost_he_so_far = curr.left.heuristic;
                        }


                    }
                  
                    if (flag == 1)
                    {
                        Children.Add(a);
                    }

                }
                if (curr.right != null && curr.right.colortype != 2 && curr.right.colortype != 10)
                {
                    Square a = curr.right; a.colortype = 3;

                    int flag = 1;
                    for (int r = 0; r < curr.adj.Count; r++)
                    {
                        if (curr.right.id == curr.adj[r].id)
                        {

                            curr.right.cost_he_so_far = curr.right.heuristic;

                        }


                    }

                  
                    if (flag == 1)
                    {
                        Children.Add(a);
                    }

                }
                if (curr.up != null && curr.up.colortype != 2 && curr.up.colortype != 10)
                {
                    Square a = curr.up; a.colortype = 3;

                    int flag = 1;
                    for (int r = 0; r < curr.adj.Count; r++)
                    {
                        if (curr.up.id == curr.adj[r].id)
                        {

                          
                            curr.up.cost_he_so_far =  curr.up.heuristic;
                        }


                    }
                    
                    if (flag == 1)
                    {
                        Children.Add(a);
                    }

                }
                if (curr.down != null && curr.down.colortype != 2 && curr.down.colortype != 10)
                {
                    int flag = 1;
                    Square a = curr.down; a.colortype = 3;

                    for (int r = 0; r < curr.adj.Count; r++)
                    {
                        if (curr.down.id == curr.adj[r].id)
                        {

                      
                            curr.down.cost_he_so_far = curr.down.heuristic;
                        }


                    }
                  

                    if (flag == 1)
                    {
                        Children.Add(a);
                    }
                }


                for (int ww = 0; ww < Children.Count; ww++)
                {

                    s.Children.Add(Children[ww]);
                }
                for (int hh = 0; hh < Open_Move.Count; hh++)
                {

                    s.Open_Move.Add(Open_Move[hh]);

                }

                Open_Move.RemoveAt(0);

                for (int z = 0; z < Children.Count; z++)
                {
                    Open_Move.Insert(0, Children[z]);
                }

                Open_Move = Open_Move.OrderBy(saf => saf.cost_he_so_far).ToList();






                CS.Add(s);



                if (Open_Move.Count == 0)
                {
                    MessageBox.Show("Done");
                    break;
                }


                int x = 900;
                int y = 100;
                /*
                for (int f = 0; f < Open_Move.Count; f++)
                {
                    Graphics g = CreateGraphics();
                    g.DrawString(" " + Open_Move[f].id , new Font("System", 17), Brushes.DarkKhaki, x, 100);
                    x += 24;
                }
                 */

                curr.colortype = 10;
                DrawDubb(CreateGraphics());
            }

            if (finish == 1)
            {
                Square cur = Open_Move[0];
                int totalcost = 0;
                for (int c = CS.Count - 1; c >= 0; c--)
                {
                    for (int co = 0; co < CS[c].Children.Count; co++)
                    {
                        if (cur.name == CS[c].Children[co].name)
                        {
                            Console.WriteLine(cur.name + " ,");
                            for (int g = 0; g < CS[c].Open_Move[0].adj.Count; g++)
                            {
                                if (CS[c].Open_Move[0].adj[g].id == cur.id)
                                {
                                    totalcost += (int)CS[c].Open_Move[0].adj[g].cost;
                                }
                            }
                            cur = CS[c].Open_Move[0];
                            cur.colortype = 100;
                            break;
                        }
                    }
                }
                MessageBox.Show("The path cost is " + totalcost);
                Console.WriteLine(Li.head.name);
            }
        }

        void ShowCost(Square g)
        {
            Square h = Li.head;
            Square checkdown = Li.head;
            int diff_X = 0;
            int diff_Y = 0;
            double X_pw = 0;
            double Y_pw = 0;
            double sqr = 0;
            while (checkdown != null)
            {
                while (h != null)
                {
                    //Horizontal and Vertical Distance
                    diff_X = g.X - h.X;
                    if (diff_X < 0) { diff_X = -diff_X; }
                    diff_Y = g.Y - h.Y;
                    if (diff_Y < 0) { diff_Y = -diff_Y; }

                    //Power
                    X_pw = Math.Pow(diff_X, 2);
                    Y_pw = Math.Pow(diff_Y, 2);

                    //Distance between node and goal
                    sqr = Math.Sqrt(X_pw + Y_pw) / 5;
                    h.heuristic = (int)sqr;

                    h = h.right;
                }

                checkdown = checkdown.down;
                h = checkdown;
            }
        }
        void ShowCostAdjacent()
        {
            Square h = Li.head;
            Square checkdown = Li.head;
            Square h_last = null;
            while (checkdown != null)
            {
                while (h != null)
                {
                    h.adj = new List<Square>() ;

                    if (h.left != null)
                    {

                        h.adj.Add(h.left);

                        if (h.left.cost != -1)
                        {
                            for (int i = 0; i < h.left.adj.Count; i++)
                            {
                                if (h.left.adj[i].id == h.id)
                                {
                                    h.adj[h.adj.Count - 1].cost = h.left.adj[i].cost;
                                    break;
                                }
                            }
                        }
                        else
                        {

                            h.adj[h.adj.Count - 1].cost = rf.Next(1, 10);
                        }

                    }
                    if (h.up != null)
                    {
                        Square f = new Square();
                        f.id = h.up.id;
                        f.name = h.up.name;
                        h.adj.Add(f);
                        if (h.up.cost != -1)
                        {
                            for (int i = 0; i < h.up.adj.Count; i++)
                            {
                                if (h.up.adj[i].id == h.id)
                                {
                                    h.adj[h.adj.Count - 1].cost = h.up.adj[i].cost;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            int r = rf.Next(1, 10);
                            h.adj[h.adj.Count - 1].cost = rf.Next(1, 10);
                        }
                    }
                    if (h.right != null)
                    {
                        Square f = new Square();
                        f.id = h.right.id;
                        f.cost = rf.Next(1, 10);
                        f.name = h.right.name;
                        h.adj.Add(f);

                    }

                    if (h.down != null)
                    {
                        Square f = new Square();
                        f.id = h.down.id;
                        f.cost = rf.Next(1, 10);
                        f.name = h.down.name;
                        h.adj.Add(f);
                    }

                    h_last = h;
                    h = h.right;
                }


                checkdown = checkdown.down;
                h = checkdown;
            }
        }
        void UniformCost()
        {
            Level = new List<Square>();
            Open_Move = new List<Square>();
            Closed_Move = new List<Square>();
            Children = new List<Square>();
            CS = new List<cost_searcher>();
            Square pnn = startnode;
            pnn.colortype = 3;
            Open_Move.Add(pnn);
            int k = 0;
            int i = 0; int j = 0;
            int count = 0;
            Square curr = Open_Move[0];
            int re = 0;
            int finish = 0;
            int it = 0;
            cost_searcher s = new cost_searcher();



            while (Open_Move.Count != 0)
            {
                re++;
                it++;
                curr = Open_Move[0];
                 
                if (curr == goalnode)
                {
                    Console.WriteLine("The line " + (re + 1) + " :");
                    for (int h = 0; h < Open_Move.Count; h++)
                    {
                        Console.WriteLine(Open_Move[h].name + ",");

                    }
                    finish = 1;
                    MessageBox.Show("Goal found" + " The number of iterations is " + it); break;
                }
                if (re == 2)
                {
                    Console.WriteLine("");
                }
                Console.WriteLine("The " + re + " line Open_Move ");
                for (int ff = 0; ff < Open_Move.Count; ff++)
                {
                    Console.WriteLine(Open_Move[ff].name + ",");
                }


                Children = new List<Square>();
                s = new cost_searcher();





                if (curr.left != null && curr.left.colortype != 2 && curr.left.colortype != 10)
                {
                    Square a = curr.left; a.colortype = 3;

                    int flag = 1;
                    for (int r = 0; r < curr.adj.Count; r++)
                    {
                        if (curr.left.id == curr.adj[r].id)
                        {

                            curr.left.cost_so_far += curr.cost_so_far + Convert.ToInt32(curr.adj[r].cost);

                        }


                    }
                    for (int gg = 0; gg < Open_Move.Count; gg++)
                    {
                        if (Open_Move[gg].id == curr.left.id && (Open_Move[gg].cost_so_far == curr.left.cost_so_far
                                                                      ))
                        {
                            flag = 0; break;
                        }
                        if(Open_Move[gg].id == curr.left.id && Open_Move[gg].cost_so_far > curr.left.cost_so_far)
                        {
                            flag = 1; Open_Move.RemoveAt(gg);
                        }
                        if (Open_Move[gg].id == curr.left.id && (Open_Move[gg].cost_so_far < curr.left.cost_so_far))
                        {
                            flag = 0; break;
                        }
                    }
                    if (flag == 1)
                    {
                        Children.Add(a);
                    }

                }
                if (curr.right != null && curr.right.colortype != 2 && curr.right.colortype != 10)
                {
                    Square a = curr.right; a.colortype = 3;
                     
                    int flag = 1;
                    for (int r = 0; r < curr.adj.Count; r++)
                    {
                        if (curr.right.id == curr.adj[r].id)
                        {

                            curr.right.cost_so_far += curr.cost_so_far + Convert.ToInt32(curr.adj[r].cost);

                        }


                    }

                    for (int gg = 0; gg < Open_Move.Count; gg++)
                    {
                        if (Open_Move[gg].id == curr.right.id && Open_Move[gg].cost_from_start == curr.right.cost_from_start)
                        {
                            flag = 0; break;
                        }
                        if (Open_Move[gg].id == curr.right.id  && Open_Move[gg].cost_so_far > curr.right.cost_so_far)
                        {
                            flag = 1; Open_Move.RemoveAt(gg);
                        }
                        if (Open_Move[gg].id == curr.right.id && (Open_Move[gg].cost_so_far < curr.right.cost_so_far))
                        {
                            flag = 0;  break;
                        }
                    }
                    if (flag == 1)
                    {
                        Children.Add(a);
                    }

                }
                if (curr.up != null && curr.up.colortype != 2 && curr.up.colortype != 10)
                {
                    Square a = curr.up; a.colortype = 3;

                    int flag = 1;
                    for (int r = 0; r < curr.adj.Count; r++)
                    {
                        if (curr.up.id == curr.adj[r].id)
                        {

                            curr.up.cost_so_far += curr.cost_so_far + Convert.ToInt32(curr.adj[r].cost);

                        }


                    }
                    for (int gg = 0; gg < Open_Move.Count; gg++)
                    {
                        if (Open_Move[gg].id == curr.up.id && (Open_Move[gg].cost_so_far == curr.up.cost_so_far))
                        {
                            flag = 0; break;
                        }
                          if(Open_Move[gg].id == curr.up.id && Open_Move[gg].cost_so_far > curr.up.cost_so_far)
                        {
                            flag = 1; Open_Move.RemoveAt(gg);
                        }
                        if (Open_Move[gg].id == curr.up.id && (Open_Move[gg].cost_so_far < curr.up.cost_so_far))
                        {
                            flag = 0; break;
                        }
                    }
                    if (flag == 1)
                    {
                        Children.Add(a);
                    }

                }
                if (curr.down != null && curr.down.colortype != 2 && curr.down.colortype != 10)
                {
                    int flag = 1;
                    Square a = curr.down; a.colortype = 3;

                    for (int r = 0; r < curr.adj.Count; r++)
                    {
                        if (curr.down.id == curr.adj[r].id)
                        {

                            curr.down.cost_so_far += curr.cost_so_far + Convert.ToInt32(curr.adj[r].cost);

                        }


                    }
                    for (int gg = 0; gg < Open_Move.Count; gg++)
                    {
                        if (Open_Move[gg].id == curr.down.id && (Open_Move[gg].cost_so_far == curr.down.cost_so_far ))
                        {
                            flag = 0; break;
                        }
                           if(Open_Move[gg].id == curr.down.id && Open_Move[gg].cost_so_far > curr.down.cost_so_far)
                        {
                            flag = 1; Open_Move.RemoveAt(gg);
                        }

                        if (Open_Move[gg].id == curr.down.id && (Open_Move[gg].cost_so_far < curr.down.cost_so_far))
                        {
                            flag = 0;  break;
                        }
                    }


                    if (flag == 1)
                    {
                        Children.Add(a);
                    }
                }


                for (int ww = 0; ww < Children.Count; ww++)
                {

                    s.Children.Add(Children[ww]);
                    if(Children[ww] == goalnode)
                    {
                        Console.WriteLine("he");
                    }
                }
                for (int hh = 0; hh < Open_Move.Count; hh++)
                {

                    s.Open_Move.Add(Open_Move[hh]);

                }

                Open_Move.RemoveAt(0);
                int fl_goal = 0;
                for (int bg = 0; bg < Children.Count; bg++)
                {
                    if (Children[bg] == goalnode)
                    {
                        Children.RemoveAt(bg);
                        Children.Add(goalnode);
                        fl_goal = 1;
                        break;
                    }
                }
                for (int z = 0; z < Children.Count; z++)
                {
                    Open_Move.Insert(0, Children[z]);
                }

                if (fl_goal != 1)
                {
                    Open_Move = Open_Move.OrderBy(saf => saf.cost_so_far).ToList();
                }


                CS.Add(s);



                if (Open_Move.Count == 0)
                {
                    MessageBox.Show("Done");
                    break;
                }


                int x = 900;
                int y = 100;
                /*
                for (int f = 0; f < Open_Move.Count; f++)
                {
                    Graphics g = CreateGraphics();
                    g.DrawString(" " + Open_Move[f].id , new Font("System", 17), Brushes.DarkKhaki, x, 100);
                    x += 24;
                }
                 */

                curr.colortype = 10;
                DrawDubb(CreateGraphics());
            }

            if (finish == 1)
            {
                Square cur = Open_Move[0];
                int totalcost = 0;
                for (int c = CS.Count - 1; c >= 0; c--)
                {
                    for (int co = 0; co < CS[c].Children.Count; co++)
                    {
                        if (cur.name == CS[c].Children[co].name)
                        {
                            Console.WriteLine(cur.name + " ,");
                            for (int g = 0; g < CS[c].Open_Move[0].adj.Count; g++)
                            {
                                if (CS[c].Open_Move[0].adj[g].id == cur.id)
                                {
                                    totalcost += (int)CS[c].Open_Move[0].adj[g].cost;
                                }
                            }
                            cur = CS[c].Open_Move[0];
                            cur.colortype = 100;

                            break;
                        }
                    }
                }
                MessageBox.Show("The path cost is " + totalcost);
                Console.WriteLine(Li.head.name);
            }

        }

        void Astar()
        {
            Level = new List<Square>();
            Open_Move = new List<Square>();
            Closed_Move = new List<Square>();
            Children = new List<Square>();
            CS = new List<cost_searcher>();
            Square pnn = startnode;
            pnn.colortype = 3;
            Open_Move.Add(pnn);
            int k = 0;
            int i = 0; int j = 0;
            int count = 0;
            Square curr = Open_Move[0];
            int re = 0;
            int finish = 0;
            int it = 0;
            cost_searcher s = new cost_searcher();



            while (Open_Move.Count != 0)
            {
                re++;
                curr = Open_Move[0];
                it++;
                if (curr == goalnode)
                {
                    Console.WriteLine("The line " + (re + 1) + " :");
                    for (int h = 0; h < Open_Move.Count; h++)
                    {
                        Console.WriteLine(Open_Move[h].name + ",");

                    }
                    finish = 1;
                    MessageBox.Show("Goal found" + " The number of iterations is " + it); break;
                }
                if (re == 2)
                {
                    Console.WriteLine("");
                }
                Console.WriteLine("The " + re + " line Open_Move ");
                for (int ff = 0; ff < Open_Move.Count; ff++)
                {
                    Console.WriteLine(Open_Move[ff].name + ",");
                }


                Children = new List<Square>();
                s = new cost_searcher();





                if (curr.left != null && curr.left.colortype != 2 && curr.left.colortype != 10)
                {
                    Square a = curr.left; a.colortype = 3;

                    int flag = 1;
                    for (int r = 0; r < curr.adj.Count; r++)
                    {
                        if (curr.left.id == curr.adj[r].id)
                        {

                            curr.left.cost_so_far += curr.cost_so_far + Convert.ToInt32(curr.adj[r].cost);
                            curr.left.cost_he_so_far = curr.left.cost_so_far + curr.left.heuristic;
                        }


                    }
                    for (int gg = 0; gg < Open_Move.Count; gg++)
                    {
                        if (Open_Move[gg].id == curr.left.id && (Open_Move[gg].cost_he_so_far == curr.left.cost_he_so_far 
                                                                      ))
                        {
                            flag = 0; break;
                        }
                        if(Open_Move[gg].id == curr.left.id && Open_Move[gg].cost_he_so_far > curr.left.cost_he_so_far)
                        {
                            flag = 1;  Open_Move.RemoveAt(gg);
                        }
                        if (Open_Move[gg].id == curr.left.id && (Open_Move[gg].cost_he_so_far < curr.left.cost_he_so_far))
                        {
                            flag = 0;  break;
                        }
                    }
                    if (flag == 1)
                    {
                        Children.Add(a);
                    }

                }
                if (curr.right != null && curr.right.colortype != 2 && curr.right.colortype != 10)
                {
                    Square a = curr.right; a.colortype = 3;

                    int flag = 1;
                    for (int r = 0; r < curr.adj.Count; r++)
                    {
                        if (curr.right.id == curr.adj[r].id)
                        {

                            curr.right.cost_so_far += curr.cost_so_far + Convert.ToInt32(curr.adj[r].cost);
                            curr.right.cost_he_so_far = curr.right.cost_so_far + curr.right.heuristic;

                        }


                    }

                    for (int gg = 0; gg < Open_Move.Count; gg++)
                    {
                        if (Open_Move[gg].id == curr.right.id && Open_Move[gg].cost_he_so_far == curr.right.cost_he_so_far)
                        {
                            flag = 0; break;
                        }
                        if (Open_Move[gg].id == curr.right.id && Open_Move[gg].cost_he_so_far > curr.right.cost_he_so_far)
                        {
                            flag = 1; Open_Move.RemoveAt(gg);
                        }
                        if (Open_Move[gg].id == curr.right.id && (Open_Move[gg].cost_he_so_far < curr.right.cost_he_so_far))
                        {
                            flag = 0; Open_Move.RemoveAt(gg); break;
                        }
                    }
                    if (flag == 1)
                    {
                        Children.Add(a);
                    }

                }
                if (curr.up != null && curr.up.colortype != 2 && curr.up.colortype != 10)
                {
                    Square a = curr.up; a.colortype = 3;

                    int flag = 1;
                    for (int r = 0; r < curr.adj.Count; r++)
                    {
                        if (curr.up.id == curr.adj[r].id)
                        {

                            curr.up.cost_so_far += curr.cost_so_far + Convert.ToInt32(curr.adj[r].cost);
                            curr.up.cost_he_so_far = curr.up.cost_so_far + curr.up.heuristic;
                        }


                    }
                    for (int gg = 0; gg < Open_Move.Count; gg++)
                    {
                        if (Open_Move[gg].id == curr.up.id && (Open_Move[gg].cost_he_so_far == curr.up.cost_he_so_far))
                        {
                            flag = 0; break;
                        }
                        if (Open_Move[gg].id == curr.up.id && Open_Move[gg].cost_he_so_far > curr.up.cost_he_so_far)
                        {
                            flag = 1; Open_Move.RemoveAt(gg);
                        }
                        if (Open_Move[gg].id == curr.up.id && (Open_Move[gg].cost_he_so_far < curr.up.cost_he_so_far))
                        {
                            flag = 0; Open_Move.RemoveAt(gg); break;
                        }
                    }
                    if (flag == 1)
                    {
                        Children.Add(a);
                    }

                }
                if (curr.down != null && curr.down.colortype != 2 && curr.down.colortype != 10)
                {
                    int flag = 1;
                    Square a = curr.down; a.colortype = 3;

                    for (int r = 0; r < curr.adj.Count; r++)
                    {
                        if (curr.down.id == curr.adj[r].id)
                        {

                            curr.down.cost_so_far += curr.cost_so_far + Convert.ToInt32(curr.adj[r].cost);
                            curr.down.cost_he_so_far = curr.down.cost_so_far + curr.down.heuristic;
                        }


                    }
                    for (int gg = 0; gg < Open_Move.Count; gg++)
                    {
                        if (Open_Move[gg].id == curr.down.id && (Open_Move[gg].cost_he_so_far == curr.down.cost_he_so_far))
                        {
                            flag = 0; break;

                        }
                        if (Open_Move[gg].id == curr.down.id && Open_Move[gg].cost_he_so_far > curr.down.cost_he_so_far)
                        {
                            flag = 1; Open_Move.RemoveAt(gg);
                        }
                        if (Open_Move[gg].id == curr.down.id && (Open_Move[gg].cost_he_so_far < curr.down.cost_he_so_far))
                        {
                            flag = 0; Open_Move.RemoveAt(gg); break;
                        }
                    }


                    if (flag == 1)
                    {
                        Children.Add(a);
                    }
                }


                for (int ww = 0; ww < Children.Count; ww++)
                {

                    s.Children.Add(Children[ww]);
                }
                for (int hh = 0; hh < Open_Move.Count; hh++)
                {

                    s.Open_Move.Add(Open_Move[hh]);

                }

                Open_Move.RemoveAt(0);

                for (int z = 0; z < Children.Count; z++)
                {
                    Open_Move.Insert(0, Children[z]);
                }

                Open_Move = Open_Move.OrderBy(saf => saf.cost_he_so_far).ToList();






                CS.Add(s);



                if (Open_Move.Count == 0)
                {
                    MessageBox.Show("Done");
                    break;
                }


                int x = 900;
                int y = 100;
                /*
                for (int f = 0; f < Open_Move.Count; f++)
                {
                    Graphics g = CreateGraphics();
                    g.DrawString(" " + Open_Move[f].id , new Font("System", 17), Brushes.DarkKhaki, x, 100);
                    x += 24;
                }
                 */

                curr.colortype = 10;
                DrawDubb(CreateGraphics());
            }

            if (finish == 1)
            {
                Square cur = Open_Move[0];
                int totalcost = 0;
                for (int c = CS.Count - 1; c >= 0; c--)
                {
                    for (int co = 0; co < CS[c].Children.Count; co++)
                    {
                        if (cur.name == CS[c].Children[co].name)
                        {
                            Console.WriteLine(cur.name + " ,");
                            for (int g = 0; g < CS[c].Open_Move[0].adj.Count; g++)
                            {
                                if (CS[c].Open_Move[0].adj[g].id == cur.id)
                                {
                                    totalcost += (int)CS[c].Open_Move[0].adj[g].cost;
                                }
                            }
                            cur = CS[c].Open_Move[0];
                            cur.colortype = 100;
                            break;
                        }
                    }
                }
                MessageBox.Show("The path cost is " + totalcost);
                Console.WriteLine(Li.head.name);
            }
        }
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {

            isDrag = true;
            Square h = Li.head;
            Square checkdown = Li.head;
            int flag = 0;

            while (checkdown != null)
            {
                while (h != null)
                {
                    if (e.X > h.X && e.X < h.X + h.W &&
                       e.Y > h.Y && e.Y < h.Y + h.H)
                    {
                        if (e.Button == MouseButtons.Left)
                        {
                            if (flag_select == 2)
                            {
                                h.colortype = 2; flag = 1; break;
                            }
                            if (flag_select == 1)
                            {
                                if (startnode != null) { startnode.colortype = 1; }
                                startnode = h;
                                h.colortype = -2; flag = 1; break;
                            }
                            if (flag_select == 3)
                            {
                                if (goalnode != null) { goalnode.colortype = 1; }
                                h.colortype = 4; flag = 1;
                                goalnode = h;
                                ShowCost(goalnode);
                                ShowCostAdjacent();
                                break;
                            }
                        }
                       
                    }
                    h = h.right;
                }
                if (flag == 1) { break; }

                checkdown = checkdown.down;
                h = checkdown;
            }
            for (int i = 0; i < CL.Count; i++)
            {
                if (e.X > CL[i].X && e.X < CL[i].X + CL[i].W &&
                    e.Y > CL[i].Y && e.Y < CL[i].Y + CL[i].H)
                {
                    for (int f = 0; f < CL.Count; f++)
                    {

                        CL[f].color = 0;

                    }
                    CL[i].color = 1;
                    int k = i;

                    if (CL[i].option == "StartPoint")
                    {
                        flag_select = 1;
                    }
                    if (CL[i].option == "EndPoint") {
                        flag_select = 3;
                    }
                    if (CL[i].option == "Obstacle")
                    {
                        flag_select = 2;
                    }
                    if (CL[i].option == "DFS")
                    {
                        flag_select = 0;
                        if (startnode == null)
                        {
                            MessageBox.Show("Please choose a starting node");
                        }
                        if (goalnode == null)
                        {
                            MessageBox.Show("Please choose a goal node");
                        }
                        if (startnode != null && goalnode != null)
                        {
                            DFS();
                        }
                    }

                    if (CL[i].option == "BFS")
                    {
                        flag_select = 0;
                        if (startnode == null)
                        {
                            MessageBox.Show("Please choose a starting node");
                        }
                        if (goalnode == null)
                        {
                            MessageBox.Show("Please choose a goal node");
                        }
                        if (startnode != null && goalnode != null)
                        {
                            BFS();
                        }
                    }
                    if (CL[i].option == "UC")
                    {
                        flag_select = 0;
                        if (startnode == null)
                        {
                            MessageBox.Show("Please choose a starting node");
                        }
                        if (goalnode == null)
                        {
                            MessageBox.Show("Please choose a goal node");
                        }
                        if (startnode != null && goalnode != null)
                        {
                            UniformCost();
                        }
                    }

                    if (CL[i].option == "Astar")
                    {
                        flag_select = 0;
                        if (startnode == null)
                        {
                            MessageBox.Show("Please choose a starting node");
                        }
                        if (goalnode == null)
                        {
                            MessageBox.Show("Please choose a goal node");
                        }
                       
                        if (startnode != null && goalnode != null )
                        {

                            Astar();
                        }
                    }
                    if (CL[i].option == "Greedy")
                    {
                        flag_select = 0;
                        if (startnode == null)
                        {
                            MessageBox.Show("Please choose a starting node");
                        }
                        if (goalnode == null)
                        {
                            MessageBox.Show("Please choose a goal node");
                        }

                        if (startnode != null && goalnode != null)
                        {

                            Greedy();
                        }
                    }
                   
                    if (CL[i].option == "Reset")
                    {
                        flag_select = 0;
                        h = Li.head;
                        checkdown = Li.head;
                        flag = 0;

                        while (checkdown != null)
                        {
                            while (h != null)
                            {

                                if (h.colortype != 2)
                                {
                                    h.colortype = 1;
                                    h.cost_so_far = 0;
                                    h.cost_he_so_far = 0;
                                }

                                h = h.right;
                            }
                            if (flag == 1) { break; }

                            checkdown = checkdown.down;
                            h = checkdown;
                        }

                    }
                   
                    break;
                }
            }
            DrawDubb(CreateGraphics());
        }
        int end; int end2;

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            isDrag = false;
            DrawDubb(CreateGraphics());
        }



        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {


            if (isDrag == true)
            {
                Square h = Li.head;
                Square checkdown = Li.head;
                while (checkdown != null)
                {
                    while (h != null)
                    {
                        if (e.X > h.X && e.X < h.X + h.W &&
                           e.Y > h.Y && e.Y < h.Y + h.H)
                        {
                            if (flag_select == 2)
                            {
                                h.colortype = 2; break;
                            }
                            if (flag_select == 1 && startnode == null)
                            {
                                h.colortype = -2; flag_select = 0; break;
                            }
                        }
                        h = h.right;
                    }

                    checkdown = checkdown.down;
                    h = checkdown;
                }


                DrawDubb(CreateGraphics());
            }




        }


        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawDubb(e.Graphics);
        }


        void CreateOptions()
        {
            int y = 0;

            Circle c = new Circle();
            c.X = 1100;
            c.Y = y;
            c.H = 80;
            c.W = 150;
            c.option = "StartPoint";   
            CL.Add(c);

            c = new Circle();
            c.X = 1260;
            c.Y = y;
            c.H = 80;
            c.W = 150;
            c.option = "EndPoint";
            CL.Add(c);

            y += 105;

            c = new Circle();
            c.X = 1100;
            c.Y = y;
            c.H = 80;
            c.W = 150;
            c.option = "Obstacle";
            CL.Add(c);

            y += 105;

            c = new Circle();
            c.X = 1100;
            c.Y = y;
            c.H = 80;
            c.W = 150;
            c.option = "DFS";
            CL.Add(c);

            y += 105;

            c = new Circle();
            c.X = 1100;
            c.Y = y;
            c.H = 80;
            c.W = 150;
            c.option = "BFS";
            CL.Add(c);

            y += 120;

            c = new Circle();
            c.X = 1100;
            c.Y = y;
            c.H = 80;
            c.W = 150;
            c.option = "UC";
            CL.Add(c);

            y += 105;

            c = new Circle();
            c.X = 1100;
            c.Y = y;
            c.H = 80;
            c.W = 150;
            c.option = "Astar";
            CL.Add(c);

            y += 105;
            c = new Circle();
            c.X = 1100;
            c.Y = y;
            c.H = 80;
            c.W = 150;
            c.option = "Greedy";
            CL.Add(c);
           
            y += 105;
            c = new Circle();
            c.X = 1100;
            c.Y = y;
            c.H = 80;
            c.W = 150;
            c.option = "Reset";
            CL.Add(c);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            off = new Bitmap(ClientSize.Width, ClientSize.Height);
            Li = new SquareList();
            CreateOptions();
            AddNode(Li);


        }


        void DrawDubb(Graphics g)
        {
            Graphics g2 = Graphics.FromImage(off);
            DrawScene(g2);
            g.DrawImage(off, 0, 0);
        }

        int r = 150;


        void DrawScene(Graphics g)
        {
            Square curr = Li.head;
            Square checkdown = Li.head;
            g.Clear(Color.White);

            curr = Li.head;

            int i = 0;

            while (checkdown != null)
            {
                while (curr != null)
                {
                    i++;

                    for (int rt = 0; rt < CL.Count; rt++)
                    {
                        if (CL[rt].color == 1)
                        {
                            g.DrawEllipse(new Pen(Color.SpringGreen, 4), CL[rt].X, CL[rt].Y, CL[rt].W, CL[rt].H);
                            g.FillEllipse(Brushes.SpringGreen, CL[rt].X, CL[rt].Y, CL[rt].W, CL[rt].H);
                        }
                        if (CL[rt].color == 0)
                        {
                            g.DrawEllipse(new Pen(Color.Black, 4), CL[rt].X, CL[rt].Y, CL[rt].W, CL[rt].H);


                        }
                        g.DrawString(" " + CL[rt].option, new Font("System", 16), Brushes.Red, CL[rt].X + 30, CL[rt].Y + 20);
                    }
                    if (curr.colortype == 1)
                    {
                        g.DrawRectangle(new Pen(Color.Black, 4), curr.X, curr.Y, curr.W, curr.H);
                        g.DrawString(" " + curr.heuristic, new Font("System", 15),
                             Brushes.Green,
                              curr.X, curr.Y);
                        g.DrawString(" " + curr.name, new Font("System", 17), Brushes.Crimson, curr.X + 40, curr.Y);
                        int df = curr.Y + 30;
                        for (int gt = 0; gt < curr.adj.Count; gt++)
                        {
                            g.DrawString(" " + curr.adj[gt].name + "=" + curr.adj[gt].cost, new Font("System", 12), Brushes.Navy, curr.X + 20, df);
                            df = df + 14;
                        }
                    }
                    if (curr.colortype == 3)
                    {
                        g.DrawRectangle(new Pen(Color.Black, 4), curr.X, curr.Y, curr.W, curr.H);
                        g.FillRectangle(Brushes.Yellow, curr.X, curr.Y, curr.W, curr.H);
                        g.DrawString(" " + curr.heuristic, new Font("System", 15),
                            Brushes.Green,
                             curr.X, curr.Y);

                        g.DrawString(" " + curr.name, new Font("System", 17), Brushes.Crimson, curr.X + 40, curr.Y);
                        int df = curr.Y + 30;
                        for (int gt = 0; gt < curr.adj.Count; gt++)
                        {
                            g.DrawString(" " + curr.adj[gt].name + "=" + curr.adj[gt].cost, new Font("System", 12), Brushes.Navy, curr.X + 20, df);
                            df = df + 14;
                        }
                    }
                    if (curr.colortype == 2)
                    {
                        if (curr == Li.head)
                        {
                            Console.WriteLine(i);
                        }
                        g.DrawRectangle(new Pen(Color.Black, 4), curr.X, curr.Y, curr.W, curr.H);
                        g.FillRectangle(Brushes.Black, curr.X, curr.Y, curr.W, curr.H);
                        g.DrawString(" " + curr.heuristic, new Font("System", 15),
                            Brushes.Green,
                             curr.X, curr.Y);

                        g.DrawString(" " + curr.name, new Font("System", 17), Brushes.Crimson, curr.X + 40, curr.Y);
                        int df = curr.Y + 30;
                        for (int gt = 0; gt < curr.adj.Count; gt++)
                        {
                            g.DrawString(" " + curr.adj[gt].name + "=" + curr.adj[gt].cost, new Font("System", 12), Brushes.Navy, curr.X + 20, df);
                            df = df + 14;
                        }
                    }
                    if (curr.colortype == -2)
                    {
                        if (curr == Li.head)
                        {
                            Console.WriteLine(i);
                        }
                        g.DrawRectangle(new Pen(Color.Black, 4), curr.X, curr.Y, curr.W, curr.H);
                        g.FillRectangle(Brushes.SteelBlue, curr.X, curr.Y, curr.W, curr.H);
                        g.DrawString(" " + curr.heuristic, new Font("System", 15),
                            Brushes.Green,
                             curr.X, curr.Y);

                        g.DrawString(" " + curr.name, new Font("System", 17), Brushes.Crimson, curr.X + 40, curr.Y);
                        int df = curr.Y + 30;
                        for (int gt = 0; gt < curr.adj.Count; gt++)
                        {
                            g.DrawString(" " + curr.adj[gt].name + "=" + curr.adj[gt].cost, new Font("System", 12), Brushes.Navy, curr.X + 20, df);
                            df = df + 14;
                        }
                    }
                    if (curr.colortype == 4)
                    {
                        if (curr == Li.head)
                        {

                            Console.WriteLine(i);
                        }
                        g.DrawRectangle(new Pen(Color.Black, 4), curr.X, curr.Y, curr.W, curr.H);
                        g.FillRectangle(Brushes.Gold, curr.X, curr.Y, curr.W, curr.H);
                        g.DrawString(" " + curr.heuristic, new Font("System", 15),

                            Brushes.Green,
                             curr.X, curr.Y);

                        g.DrawString(" " + curr.name, new Font("System", 17), Brushes.Crimson, curr.X + 40, curr.Y);
                        int df = curr.Y + 30;
                        for (int gt = 0; gt < curr.adj.Count; gt++)
                        {
                            g.DrawString(" " + curr.adj[gt].name + "=" + curr.adj[gt].cost, new Font("System", 12), Brushes.Navy, curr.X + 20, df);
                            df = df + 14;
                        }

                    }
                    if (curr == goalnode)
                    {
                        g.DrawRectangle(new Pen(Color.Black, 4), curr.X, curr.Y, curr.W, curr.H);
                        g.FillRectangle(Brushes.Gold, curr.X, curr.Y, curr.W, curr.H);
                        g.DrawString(" " + curr.heuristic, new Font("System", 15),

                            Brushes.Green,
                             curr.X, curr.Y);

                        g.DrawString(" " + curr.name, new Font("System", 17), Brushes.Crimson, curr.X + 40, curr.Y);
                        int df = curr.Y + 30;
                        for (int gt = 0; gt < curr.adj.Count; gt++)
                        {
                            g.DrawString(" " + curr.adj[gt].name + "=" + curr.adj[gt].cost, new Font("System", 12), Brushes.Navy, curr.X + 20, df);
                            df = df + 14;
                        }
                    }

                    if (curr.colortype == 10)
                    {
                        if (curr == Li.head)
                        {

                            Console.WriteLine(i);
                        }

                        g.DrawRectangle(new Pen(Color.Black, 4), curr.X, curr.Y, curr.W, curr.H);
                        g.FillRectangle(Brushes.PowderBlue, curr.X, curr.Y, curr.W, curr.H);
                        g.DrawString(" " + curr.heuristic, new Font("System", 15),

                            Brushes.Green,
                             curr.X, curr.Y);
                        g.DrawString(" " + curr.name, new Font("System", 17), Brushes.Crimson, curr.X + 40, curr.Y);
                        int df = curr.Y + 30;
                        for (int gt = 0; gt < curr.adj.Count; gt++)
                        {
                            g.DrawString(" " + curr.adj[gt].name + "=" + curr.adj[gt].cost, new Font("System", 12), Brushes.Navy, curr.X + 20, df);
                            df = df + 14;
                        }
                    }

                    if (curr.colortype == 100)
                    {
                        if (curr == Li.head)
                        {

                            Console.WriteLine(i);
                        }
                        g.DrawRectangle(new Pen(Color.Black, 4), curr.X, curr.Y, curr.W, curr.H);
                        g.FillRectangle(Brushes.OliveDrab, curr.X, curr.Y, curr.W, curr.H);
                        g.DrawString(" " + curr.heuristic, new Font("System", 15),

                            Brushes.Green,
                             curr.X, curr.Y);
                        g.DrawString(" " + curr.name, new Font("System", 17), Brushes.Crimson, curr.X + 40, curr.Y);
                        int df = curr.Y + 30;
                        for (int gt = 0; gt < curr.adj.Count; gt++)
                        {
                            g.DrawString(" " + curr.adj[gt].name + "=" + curr.adj[gt].cost, new Font("System", 12), Brushes.Navy, curr.X + 20, df);
                            df = df + 14;
                        }
                    }
                    if (curr == startnode)
                    {

                        if (curr == Li.head)
                        {
                            Console.WriteLine(i);
                        }
                        g.DrawRectangle(new Pen(Color.Black, 4), curr.X, curr.Y, curr.W, curr.H);
                        g.FillRectangle(Brushes.SteelBlue, curr.X, curr.Y, curr.W, curr.H);
                        g.DrawString(" " + curr.heuristic, new Font("System", 15),
                            Brushes.Green,
                             curr.X, curr.Y);

                        g.DrawString(" " + curr.name, new Font("System", 17), Brushes.Crimson, curr.X + 40, curr.Y);
                        int df = curr.Y + 30;
                        for (int gt = 0; gt < curr.adj.Count; gt++)
                        {
                            g.DrawString(" " + curr.adj[gt].name + "=" + curr.adj[gt].cost, new Font("System", 12), Brushes.Navy, curr.X + 20, df);
                            df = df + 14;
                        }
                    }
                    curr = curr.right;
                }
                checkdown = checkdown.down;
                curr = checkdown;

            }


        }
        static void Main(string[] args)
        {
            Form1 obj;
            obj = new Form1();
            Application.Run(obj);
        }

    }
}