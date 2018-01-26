using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms.DataVisualization.Charting;

namespace FT450D
{

    class Scan
    {
        Form1 form1;
        private int start, stop, step;
        private SerialPort _serialPort;
        private Chart chart1;
        private bool stopKran = false;

        //--------------------------------------------------------------------
        public Scan(Form1 form1, SerialPort _serialPort, Chart chart1)
        {
            this.form1 = form1;
            this._serialPort = _serialPort;
            this.chart1 = chart1;
        }
        //--------------------------------------------------------------------
        public void setStopKran( bool state)
        {
            stopKran = state;
        }
        //--------------------------------------------------------------------
        public void setScanData(int a, int b, int c)
        {
            start = a;
            stop = b;
            step = c;
        }
        //-------------------------------------------------------------- Предбразование частоты из числа в строку
        private string chastota(int chas)
        {
            if (chas < 10000) return "0" + chas.ToString();
            else return chas.ToString();
        }
        //-------------------------------------------------------------- Преобразоваие шага в понятный для трансивера вид
        private string strStep(int step)
        {
            if (step < 10) return "00" + step.ToString();
            if (step < 100) return "0" + step.ToString();
            return step.ToString();
        }
        //-------------------------------------------------------------------
        private float swr(string rez)
        {
            rez = rez.Substring(3, 3);
            int w = int.Parse(rez);

            float rez_graf = 0.0f;
            if ((6 < w) && (w < 10)) rez_graf = 1.1f;   //1 кубик
            if ((14 < w) && (18 > w)) rez_graf = 1.25f;  //2 кубик
            if ((23 < w) && (27 > w)) rez_graf = 1.4f;  //3 кубик
            if ((31 < w) && (35 > w)) rez_graf = 1.5f;  //4 кубик
            if ((39 < w) && (43 > w)) rez_graf = 1.67f;  //5 кубик
            if ((47 < w) && (51 > w)) rez_graf = 1.83f;  //7
            if ((56 < w) && (60 > w)) rez_graf = 2.0f;  //8

            if ((64 < w) && (68 > w)) rez_graf = 2.14f;  //9
            if ((72 < w) && (76 > w)) rez_graf = 2.28f;  //10
            if ((80 < w) && (84 > w)) rez_graf = 2.42f;  //11
            if ((88 < w) && (92 > w)) rez_graf = 2.57f;  //12
            if ((97 < w) && (101 > w)) rez_graf = 2.71f; //13
            if ((105 < w) && (109 > w)) rez_graf = 2.86f;    //14
            if ((103 < w) && (107 > w)) rez_graf = 3.0f;    //15

            if ((113 < w) && (117 > w)) rez_graf = 3.14f;    //16
            if ((122 < w) && (125 > w)) rez_graf = 3.28f;    //17
            if ((130 < w) && (134 > w)) rez_graf = 3.42f;
            if ((138 < w) && (142 > w)) rez_graf = 3.57f;
            if ((146 < w) && (150 > w)) rez_graf = 3.71f;
            if ((154 < w) && (158 > w)) rez_graf = 3.86f;
            if ((162 < w) && (166 > w)) rez_graf = 4.0f;
            if ((170 < w) && (175 > w)) rez_graf = 4.14f;
            if ((179 < w) && (183 > w)) rez_graf = 4.28f;

            if ((187 < w) && (191 > w)) rez_graf = 4.42f;
            if ((195 < w) && (199 > w)) rez_graf = 4.57f;

            if ((203 < w) && (207 > w)) rez_graf = 4.71f;
            if ((212 < w) && (216 > w)) rez_graf = 4.86f;

            if ((220 < w) && (224 > w)) rez_graf = 5.0f;
            if ((228 < w) && (232 > w)) rez_graf = 5.14f;

            if ((236 < w) && (240 > w)) rez_graf = 5.28f;
            if ((244 < w) && (248 > w)) rez_graf = 5.42f;
            if (w == 255) rez_graf = 5.57f;
            return rez_graf;
        }
        //-------------------------------------------------------------------
        public void run()
        {
            int s;
            int ex = 0;
            string rez;
            float rez_graf;

            List<String> RM = new List<string>();

            

            form1.Invoke(new Action(() => {
                form1.Text = "КСВ FT450D - Считывание настроек трансивера";
            }));

            String tune = form1.sendUart("AC;");        //Положение тюнера
            String mode = form1.sendUart("MD0;");       //Модуляция
            String power = form1.sendUart("EX048;");    //Мощность
            String frenc = form1.sendUart("FA;");       //Частота
            String fast = form1.sendUart("FS;");         //Положение Fast


            try
            {
                _serialPort.Write("VS0;");//установить VFO-A
                Thread.Sleep(100);
                _serialPort.Write("AC000;");//отключение тюнера к хуям
                Thread.Sleep(100);
                _serialPort.Write("AI0;");//Отключить автоинформацию
                Thread.Sleep(100);
                _serialPort.Write("EX048005;");//Установить 10ВТ
                Thread.Sleep(100);
                _serialPort.Write("MD04;");//Установить FM
                Thread.Sleep(100);
                _serialPort.Write("FS1;");//Установить Fast включить
                Thread.Sleep(100);
                string outData = "FA" + chastota(start) + "000;";
                _serialPort.Write(outData);
                Thread.Sleep(100);
                _serialPort.Write("TX1;"); //Переходим на передачу
                Thread.Sleep(100);


            }
            catch (Exception r) { }

            form1.Invoke(new Action(() => {
                form1.Text = "КСВ FT450D - Измерение";
            }));
            rez_graf = 0.0f;
            //Просканируем частоты
            for (s = start; s < stop; s=s+step)
            {
                if (stopKran) goto endIzm;
                try
                {
                    
                    Thread.Sleep(100);
                    rez = form1.sendUart("RM6;");

                    String f = "EU"+strStep(step)+";";
                    _serialPort.Write(f);

                    ex = s;
                    chart1.Invoke(new Action(() => {
                        chart1.Series[0].Points.AddXY(s, swr(rez));
                    }));
                }
                catch (Exception l)
                {
                    //indata = "Fuck";
                }
            }

            //Добьем последнюю точку
            if(stop != ex)
            {
                int pos = stop - ex;
                try
                {
                    String f = "EU" + strStep(pos) + ";";
                    _serialPort.Write(f);
                    Thread.Sleep(100);
                    rez = form1.sendUart("RM6;");

                    chart1.Invoke(new Action(() => {
                        chart1.Series[0].Points.AddXY(stop, swr(rez));
                    }));
                
                }
                catch (Exception l)
                {
                    //indata = "Fuck";
                }
            }
            // Завершение измерения
            endIzm:

            form1.Invoke(new Action(() =>
            {
                form1.Text = "КСВ FT450D - Завершение измерения";
            }));
            try
            {
                _serialPort.Write("TX0;");  //Переходим на прием
                Thread.Sleep(100);
                _serialPort.Write(mode + ";");  //Установить модуляцию до сканирования
                Thread.Sleep(100);
                _serialPort.Write(power + ";");  //Мощность до сканирования
                Thread.Sleep(100);
                _serialPort.Write(frenc + ";");   //Частота до сканирования
                Thread.Sleep(100);
                _serialPort.Write(tune + ";");  //тюнер до сканирования
                Thread.Sleep(100);
                _serialPort.Write(fast + ";");  //FAST до сканирования
            }
            catch (Exception o) { }

            form1.end = true;
        }
    }
}
