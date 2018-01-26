using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace FT450D
{
    public partial class Form1 : Form
    {
        public String version = "0001";
        List<String> list_uart = new List<String>();
        static SerialPort _serialPort;                      //Эксземляр класса ком порт
        public bool end = false;
        public bool stopKran = false;
        Form2 form2 = new Form2();
        Form3 formOtchet = new Form3();
        Scan scan;
        Thread p;
        private string inseirial;
        int[] brate = new int[] {4800, 9600, 19200, 38400};
        private String strRate="";
        private String strPort = "";


        public Form1()
        {
            InitializeComponent();
            // Добавить линии на чарт КСВ 1,5; 2; 3
            // По умолчанию диапазон 7000 - 7200
        }

        // Шлем сторку в урат и ждем ответа --------------------------------------------------------------------
        public string sendUart(String data)
        {
            String indata = "";
            int count = 0;
            bool mon = true;
            if ((_serialPort == null) || (_serialPort.IsOpen == false)) return indata;
            //indata = "Error";
            try
            {
                inseirial = "";
                _serialPort.Write(data);
                while(mon)
                {
                    if (inseirial.IndexOf(";") >0)
                    {
                        indata = inseirial.Substring(0,inseirial.Length-1);
                        mon = false;
                    }
                    Thread.Sleep(10);
                    count++;
                    if (count == 100)
                        mon = false;
                }

                //indata = _serialPort.ReadTo(";");
            }
            catch (Exception e)
            {
                //indata = "Fuck";
            }
            return indata;
        }
        //------------------------------------------------------------------------------------------------------
        private bool checkOpenCOMport(String COM)
        {
            bool rez = false;
            SerialPort sp = new SerialPort();
            sp.PortName = COM;
            try
            {
                sp.Open();
                if (sp.IsOpen == true)
                {
                    rez = true;
                    sp.Close();
                }
            }
            catch (Exception w) { }
            sp = null;
            return rez;
        }
        //-----------------------------------------------------------------------------------------------------
        private String getRespoonFromTrans(String port,int rate)
        {
            String resp = "Нет ответа";
            //-------------------------------------------------------------------
            _serialPort = new SerialPort();

            // Allow the user to set the appropriate properties.
            _serialPort.PortName = port;
            _serialPort.BaudRate = rate;
            _serialPort.Parity = (Parity)Enum.Parse(typeof(Parity), "None", true);
            _serialPort.DataBits = 8;
            _serialPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), "One", true);
            _serialPort.Handshake = (Handshake)Enum.Parse(typeof(Handshake), "None", true);

            // Set the read/write timeouts
            _serialPort.ReadTimeout = 1500;
            _serialPort.WriteTimeout = 1500;

            _serialPort.Open();

            _serialPort.DataReceived += _serialPort_DataReceived;
            string id = sendUart("ID;");
            if (id != "") resp = id;

            _serialPort.DataReceived -= _serialPort_DataReceived;
            _serialPort.Close();

            return resp;
        }



        // Проверка наличия на UART прибора --------------------------------------------------------------------
        bool checkDevice(String port)
        {
        bool rez = false;

        _serialPort = new SerialPort();

        // Allow the user to set the appropriate properties.
            _serialPort.PortName = port;
            _serialPort.BaudRate = 38400;
            _serialPort.Parity = (Parity)Enum.Parse(typeof(Parity), "None", true);
            _serialPort.DataBits = 8;
            _serialPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), "One", true);
            _serialPort.Handshake = (Handshake)Enum.Parse(typeof(Handshake), "None", true);

            // Set the read/write timeouts
            _serialPort.ReadTimeout = 1500;
            _serialPort.WriteTimeout = 1500;

            _serialPort.Open();

            _serialPort.DataReceived += _serialPort_DataReceived;
            string id = sendUart("ID;");

            if ((id == "ID0244") || (id == "ID0241"))
            {
                rez = true;
                goto sss;
            }
            _serialPort.DataReceived -= _serialPort_DataReceived;
            _serialPort.Close();
            sss:
            
            return rez;
        }
        //----------------------------------------------------------------------------------------------------
        private void _serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            inseirial = inseirial + _serialPort.ReadExisting();
        }

        //---------------------------------------------------------------------------------------------------
        private void readyWork()
        {
            tbStart.Enabled = true;
            tbStop.Enabled = true;
            tbStep.Enabled = true;
            btnStart.Enabled = true;

            this.Text = "КСВ FT450D - Готов к работе";
        }

        //Начальная настройка элментов UI на форме
        private void notReadyWork()
        {
            tbStart.Enabled = false;
            tbStop.Enabled = false;
            tbStep.Enabled = false;
            btnStart.Enabled = false;
        }
        // Обнаружение прибора ------------------------------------------------------------------------------
        void findDevice()
        {
            List<String> rResp = new List<string>();
            list_uart.Clear();  //Очистка листа
            string _COM;
            int _rate;


            foreach (string s in SerialPort.GetPortNames())
            {
                list_uart.Add(s);  //Заносим существующие в системе UARTы
            }
            
            //Если портов нет в системе
            if (list_uart.Count == 0)
            {
                MessageBox.Show("COMport не обнаружен");
                return;
            }

            //Проверим порты на открытые и закрытые, на окрытые даём запрос
            for(int w=0; w<list_uart.Count; w++)
            {
                if (checkOpenCOMport(list_uart.ElementAt(w)))
                {
                    for(int e=0; e<brate.Length; e++)
                    {
                        this.Text = "КСВ FT450D  - Поиск трансивера. Сканируем порт "+ list_uart.ElementAt(w) + " битрейт "+ brate[e].ToString();
                        rResp.Add(list_uart.ElementAt(w)+" "+brate[e].ToString()+" "+getRespoonFromTrans(list_uart.ElementAt(w), brate[e]));
                    }
                }
                else rResp.Add(list_uart.ElementAt(w) + " Закрыт");
            }

            //Проверим результат скана

            foreach (string s in rResp)
            {
                int y = s.IndexOf("ID");
                if (y > 0)
                {
                    string temp = s.Substring(y);
                    if((temp=="ID0244")||(temp == "ID0241"))
                    {
                        // Обнаружение прошло удачно
                        temp = s;
                        int end = temp.IndexOf(" ");
                        _COM = temp.Substring(0, end);
                        temp = temp.Substring(end + 1);
                        end = temp.IndexOf(" ");
                        temp = temp.Substring(0, end);
                        _rate = int.Parse(temp);

                        _serialPort.PortName = _COM;
                        _serialPort.BaudRate = _rate;
                        _serialPort.Parity = (Parity)Enum.Parse(typeof(Parity), "None", true);
                        _serialPort.DataBits = 8;
                        _serialPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), "One", true);
                        _serialPort.Handshake = (Handshake)Enum.Parse(typeof(Handshake), "None", true);

                        // Set the read/write timeouts
                        _serialPort.ReadTimeout = 200;
                        _serialPort.WriteTimeout = 200;

                        _serialPort.Open();

                        _serialPort.DataReceived += _serialPort_DataReceived;

                        readyWork();

                        return;
                    }
                }
                
            }
            //-------------------- Трансивер не найден
            //MessageBox.Show("Показать отчёт сканирования портов?","Трансивер не найден", MessageBoxButtons.YesNo);

            DialogResult dialogResult = MessageBox.Show("Показать отчёт сканирования портов?", "Трансивер не найден", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                formOtchet.textToTextbox(rResp);
                formOtchet.ShowDialog();
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }

        }

        //-------------------------------------------------------------- Кнопка начать
        private void button2_Click(object sender, EventArgs e)
        {
            if (btnStart.Text == "Стоп")
            {
                btnStart.Enabled = false;
                scan.setStopKran(true);
                //p.Join();
            }
            else
            {
                //Проверка на открытый порт
                if ((_serialPort == null) || (_serialPort.IsOpen == false))
                {
                    tbStart.Enabled = false;
                    tbStop.Enabled = false;
                    tbStep.Enabled = false;
                    btnStart.Enabled = false;
                    return;
                }

                end = false;
                chart1.Series[0].Points.Clear();
                chart1.ChartAreas[0].AxisX.Minimum = int.Parse(tbStart.Text);
                chart1.ChartAreas[0].AxisX.Maximum = int.Parse(tbStop.Text);


                btnFind.Enabled = false;
                btnSettings.Enabled = false;
                btnStart.Text = "Стоп";

                tbStart.Enabled = false;
                tbStep.Enabled = false;
                tbStop.Enabled = false;

                scan = new Scan(this, _serialPort, chart1);
                p = new Thread(scan.run);
                p.IsBackground = true;


                scan.setScanData(int.Parse(tbStart.Text), int.Parse(tbStop.Text), int.Parse(tbStep.Text));
                p.Start();

                timer1.Enabled = true;
            }

        }
        //-------------------------------------------------------------- Найти трансивер
        private void btnFind_Click(object sender, EventArgs e)
        {
            findDevice();
        }
        //-------------------------------------------------------------- Кнопка настройки CAT
        private void btnSettings_Click(object sender, EventArgs e)
        {
            //form2.ShowDialog();
        }
        //--------------------------------------------------------------- Колбек таймера
        private void timer1_Tick(object sender, EventArgs e)
        {
            if(end)
            {
                timer1.Enabled = false;
                btnFind.Enabled = true;
                btnSettings.Enabled = true;
                btnStart.Text = "Начать";

                tbStart.Enabled = true;
                tbStep.Enabled = true;
                tbStop.Enabled = true;
                btnStart.Enabled = true;

                stopKran = false;
                end = false;
                this.Text = "КСВ FT450D";
            }
        }
        //--------------------------------------------------------------- 
        private void Form1_Load(object sender, EventArgs e)
        {
            // Загрузка формы
            // Загрузка настроек
            if (File.Exists("settings.ini"))
            {

            }
            else
            {
                //Файла нет, создадим его

            }


            if (File.Exists("comport.ini"))
            {
                String[] indata = File.ReadAllLines("comport.ini");
                strPort = indata[0];
                int rate = int.Parse(indata[1]);
                // провести тест
                string d = "";
                d = getRespoonFromTrans(strPort, rate);
                if ((d == "ID0244") || (d == "ID0241"))
                {
                    //Все ОК - работаем
                    readyWork();
                }
                else
                {
                    notReadyWork();
                    findDevice();
                }
            }
            else
            {
                //Файла нет, создадим его
                notReadyWork();

            }



            // Chart
            chart1.ChartAreas[0].AxisX.Minimum = int.Parse(tbStart.Text);
            chart1.ChartAreas[0].AxisX.Maximum = int.Parse(tbStop.Text);
            chart1.ChartAreas[0].AxisY.Minimum = 0.0f;
            chart1.ChartAreas[0].AxisY.Maximum = 6.0f;  //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
            chart1.ChartAreas[0].AxisY.Interval = 0.5f;

            //Нвстройки Оси X
            //Выключим возможность использования курсора по оси X
            chart1.ChartAreas[0].CursorX.IsUserEnabled = false;
            //Выключим возможность мастабирования по выделенному интервалу
            chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = false;
            //Выключим мастабирование по оси X
            chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = false;
            //Добавим полосу прокрутки
            chart1.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = false;



            //Настройки оси Y
            //Выключим возможность использования курсора по оси Y
            chart1.ChartAreas[0].CursorY.IsUserEnabled = false;
            //Выключим возможность мастабирования по выделенному интервалу
            chart1.ChartAreas[0].CursorY.IsUserSelectionEnabled = false;
            //Выключим мастабирование по оси Y
            chart1.ChartAreas[0].AxisY.ScaleView.Zoomable = false;
            //Добавим полосу прокрутки
            chart1.ChartAreas[0].AxisY.ScrollBar.IsPositionedInside = false;

            chart1.Series[0].BorderWidth = 3;

            chart1.Series[0].Points.AddXY(int.Parse(tbStart.Text), 50);
            chart1.Series[0].Points.AddXY(int.Parse(tbStop.Text), 50);



        }

        //-------------------------------------------------------------------------------- Только цифры tbStart
        private void tbStart_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 47 || e.KeyChar >= 59) && e.KeyChar != 8)
                e.Handled = true;
        }
        //-------------------------------------------------------------------------------- Только цифры tbStop
        private void tbStop_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 47 || e.KeyChar >= 59) && e.KeyChar != 8)
                e.Handled = true;
        }
        //-------------------------------------------------------------------------------- Только цифры tbStop
        private void tbStep_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 47 || e.KeyChar >= 59) && e.KeyChar != 8)
                e.Handled = true;
        }
    }
}
