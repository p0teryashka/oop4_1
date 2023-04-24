using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace oop4_1
{

    public partial class Form1 : Form
    {
        private List<CCircle> FCircles = new List<CCircle>();//лист всех кружков
        private int ctrl = 0;//переменная для CTRL
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)//описание события Paint
        {
            foreach (CCircle Circle in FCircles)//проход по всем кружкам в списке (кружок в список кружков)
            {
                Circle.drawCircle(e.Graphics);//Рисует все круги из списка
            }
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)//описание события нажатия мыши
        {
            if (ctrl == 0)//не нажат ctrl
            {
                foreach (CCircle Circle1 in FCircles)//проход по всем кружкам в списке (кружок в список кружков)
                {
                    Circle1.setColor("Blue");//снимает выделение со всех объектов(значит если кружок не выделен - он будет стандартного цвета(голубой))
                }
                CCircle Circle = new CCircle(e.X, e.Y, 30);//создает новый объект с выделением
                FCircles.Add(Circle);//добавление нового кружка в список 
            }
            if (ctrl == 1 && e.Button == MouseButtons.Left)//нажат ctrl
            {
                foreach (CCircle Circle1 in FCircles)//проход по всем кружкам в списке (кружок в список кружков)
                {
                    if (Circle1.checkCircle(e) == true && checkBox2.Checked == true)//проверка на массовое выделение ( будет выделять по одному кружочку)
                    {
                        break;
                    }

                }
                Refresh();
            }
            Refresh();
        }

        private void button1_click(object sender, EventArgs e)//описание события нажатия на кнопку Delete
        {

            for (int i = 0; i < FCircles.Count(); i++)//проходим по всем кружкам 
            {
                if (FCircles[i].getColor() == "Yellow")//проверка выделения объектов
                {
                    FCircles.RemoveAt(i);//удаление выделенных объектов
                    i--;
                }
            }
            Refresh();
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)//описание события нажатия клавиши на клавиатуре
        {
            if (ModifierKeys == Keys.Control)//если нажатая клавиша Control
            { 
                checkBox1.Checked = !checkBox1.Checked;//устанавливает флаг в чекбокс в определенное значение
            }

            Refresh();
            switch (ctrl)//в зависимости от состояния флага
            {
                case 0:
                    ctrl++;
                    foreach (CCircle Circle1 in FCircles)// массовое выделение кругов 
                    {
                        Circle1.setCtrl(true);//если нажат Control устанавливает определенное значение
                    }
                    break;
                case 1:
                    ctrl = 0;
                    foreach (CCircle Circle1 in FCircles) //дальнейшая отрисовка без выделения
                    {
                        Circle1.setCtrl(false);//если отжат Control устанавливает определенное значение
                    }
                    break;
            }

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        //private void Form1_MouseEnter(object sender, EventArgs e)
        //{

        //}

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }
        public class CCircle//описание объекта круга
        {
            private int x, y, rad;//координаты и радиус
            private string color = "Yellow";//цвет выделения(изначальный)
            private bool ctrl = false;
            public CCircle(int xp, int yp, int radp)//конструктор с параметрами
            {
                x = xp;
                y = yp;
                rad = radp;

            }
            public void drawCircle(Graphics Canvas)//метод отрисовки круга
            {
                if (color == "Yellow")
                {
                    Pen Pen = new Pen(Color.Yellow);//кисточка 
                    Pen.Width = 8.0F;//ширина 
                    Canvas.DrawEllipse(Pen, x - rad, y - rad, rad * 2, rad * 2);//относительно положения мышки рисует круг с нужной шириной и цветом 


                }
                else
                {
                    Pen Pen = new Pen(Color.Blue);
                    Pen.Width = 8.0F;
                    Canvas.DrawEllipse(Pen, x - rad, y - rad, rad * 2, rad * 2);// относительно положения мышки рисует круг с нужной шириной и цветом
                }
            }
            public void setColor(string Color)//сеттер цвета круга
            {
                color = Color;
            }
            public string getColor()//геттер цвета круга
            {
                return color;
            }
            public bool checkCircle(MouseEventArgs e)//проверка на попадание курсора мыши во внутрь круга
            {//когда зажата клавиша кнтрл - анализируется,попал ли курсор в координаты круга 
                if (ctrl)
                {
                    if (Math.Pow(e.X - x, 2) + Math.Pow(e.Y - y, 2) <= Math.Pow(rad, 2) && color != "Yellow")
                    {
                        color = "Yellow";
                        return true;
                    }
                }
                return false;
            }
            public void setCtrl(bool a)//сеттер флага выделения
            {
                ctrl = a;
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}