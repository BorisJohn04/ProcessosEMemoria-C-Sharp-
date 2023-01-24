using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Speech.Recognition;
using System.Diagnostics;
namespace horus3
{
   
    public partial class ProcessFilter : Form
    {
        
        public static int memoryLimit;
        public static Label textAlert;
        public static Form formAlert;
        public static Button exitProgram;
        public static string programInHighUse;
        public static Button fixedProgram;
        public string exitProcess;
        public static double RAM_memorysize;
        public static string name;
        public static string[] JSP = new string[10000];
        public static int i;
        public static int amount;
        public static object memoria;
        public static string prcName;
        public static ListView listView1;
        public static ListViewItem listViewItem2;
        public static Label label1;
        public static Button button;
        public static ProgressBar usage;
        public static List<string> listaNomes = new List<string>();
        public static List<double> countMemory = new List<double>();
        public static List<double> memorySize = new List<double>();
        public static double use;
        public double soma;
        public static Form FormDetail;
        public static ProgressBar statusUsage;
        public static Label nameDetail;
        public static string nameThread;
        public static Label memoryValue;
        

        private System.Timers.Timer tempo;
        private System.Timers.Timer tempo2;
        public ProcessFilter()
        {

            InitializeComponent();
            tempo = new System.Timers.Timer();
            tempo.Interval = 3000;
            tempo.Elapsed += processRegister;
            tempo.AutoReset = true;

            tempo2 = new System.Timers.Timer();
            tempo2.Interval = 3000;
            tempo2.Elapsed += showProcessDetail;
            tempo2.AutoReset = true;
        }

        private void processRegister(Object source, System.Timers.ElapsedEventArgs e)
        {
            update();
            barProcess();

        }

        public void killProcess()
        {

            foreach (var exitName in Process.GetProcessesByName(exitProcess))    
            {
                exitName.Kill();            
            }

        }

        public void barProcess()
        {
            progressBar1.Invoke(new Action(()=> progressBar1.Value = int.Parse(soma.ToString())));
        
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button_Click(object sender, EventArgs e)
        {
           
        }

        public void update()
        {
            memorySize.Clear();
            listaNomes.Clear();//limpa a lista de nomes de processos
            for (int q = 0; q < amount; q++)
            {
                listView1.Invoke(new Action(() => listView1.Items[q].SubItems.Clear()));//limda os valores de memória na lista
            }
            amount = 0;//retorna para quantidade zero 
            foreach (var process in Process.GetProcesses())//refaz a checagem
            {
               
             
                amount += 1;//adiciona mais 1 na lista
                string name = process.ProcessName;//cria um novo recipiente para passagem dos nomes
                listView1.Invoke(new Action(() => listView1.Items[amount - 1].Text = name.ToString()));
                listaNomes.Add(name);
                double memory = process.PrivateMemorySize64 / 1024 / 1024;
                memorySize.Add(memory);
                for (int i = 0; i < amount; i++)
                {
                    listView1.Invoke(new Action(() => listView1.Items[amount - 1].SubItems.Add(memory.ToString())));
                }
            }
            label1.Invoke(new Action(() => label1.Text = amount.ToString()));
            //atualiza barra de progresso
            if (usage.InvokeRequired)
            {
                foreach (var discord in Process.GetProcessesByName("Discord"))
                {
                    if(discord.ProcessName == "Discord")
                    {
                        var value1 = discord.PrivateMemorySize64 / 1024 / 1024;
                        usage.Invoke(new Action(() => usage.Value = int.Parse(value1.ToString())));
                    }
                    else 
                    {
                        usage.Invoke(new Action(() => usage.Value = 0));
                    }                 
                }
            }
            
            if (textBox1.InvokeRequired)
            {
                soma = 0;
                textBox1.Invoke(new Action(() => textBox1.Text = ""));
                countMemory.Clear();
                foreach (var count2 in Process.GetProcesses())
                {
                    double newCount = count2.PrivateMemorySize64 / 1024 / 1024;
                    countMemory.Add(newCount);
                    foreach(double newCount2 in countMemory)
                    {
                        soma += newCount2;
                    }
                }

                textBox1.Invoke(new Action(()=>  textBox1.Text = soma.ToString()));
            }

            alertHighUseMemory();
        }

        private int processor()
        {
            var processor = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            
            int returnValue = (int)processor.NextValue();
            return returnValue;
            progressBar1.Invoke(new Action(() => progressBar1.Value = returnValue));
        }
        private void button1_Click(object sender, EventArgs e) //botão de iniciar cria a interface
        {
            construtorInterface();
            button4.BackColor = Color.Red;
        }

        public void construtorInterface()
        {
            //cria a lista
            //button = new Button();
            //this.Controls.Add(button);
            //button.Size = new System.Drawing.Size(30, 100);
            //button.Text = "Atualizar";
            //button.Location = new System.Drawing.Point(400, 50);
            //button.Click += button_Click;
            //Placa editorial
            //ativar botão de detalhes
            button1.Visible = true;
            Form FormWindow = new Form();
            //this.Controls.Add(FormWindow);
            FormWindow.Size = new System.Drawing.Size(500, 250);
            FormWindow.Location = new System.Drawing.Point(700, 40);
            FormWindow.BackColor = Color.Silver;
            FormWindow.Visible = true;
            FormWindow.Text = "Desenvolvedores";

            //Alerta
            formAlert = new Form();
            formAlert.Text = "Programa em alto uso de memória";
            formAlert.Size = new System.Drawing.Size(550, 250);
            formAlert.Location = new System.Drawing.Point(300, 40);
            formAlert.BackColor = Color.Silver;
            formAlert.Visible = true;

       
            textAlert = new Label();
            this.Controls.Add(textAlert);
            textAlert.Location = new System.Drawing.Point(0, 0);
            textAlert.Size = new System.Drawing.Size(600, 30);
            textAlert.Text = "Este programa, '"+use+"', está usando muita memória";
            textAlert.Parent = formAlert;
            textAlert.Visible = false;

            //exitProgram = new Button();
           //exitProgram.Location = new System.Drawing.Point(5, 70);
            //textAlert.Size = new System.Drawing.Size(30, 30);
            //exitProgram.Text = "Encerrar";
            //exitProgram.Parent = formAlert;
            //exitProgram.Click += new EventHandler(this.fecharProgramaEmAltoUso_Click);//adicionando método em um botão criado pelo código
            //exitProgram.Visible = false;
            //instruções
            Form FormWindow2 = new Form();
            //this.Controls.Add(FormWindow);
            FormWindow2.Size = new System.Drawing.Size(550, 250);
            FormWindow2.Location = new System.Drawing.Point(300, 40);
            FormWindow2.BackColor = Color.Silver;
            FormWindow2.Visible = true;
            FormWindow2.Text = "Instruções";

            //form da lista de processo
            Form formList = new Form();
            formList.Size = new System.Drawing.Size(450, 400);
            formList.BackColor = Color.Silver;
            formList.Text = "Lista de processos";
            formList.Enabled = true;
            formList.Visible = true;
            //
            Label guia = new Label();
            guia = new Label();
            this.Controls.Add(guia);
            guia.Location = new System.Drawing.Point(0, 0);
            guia.Size = new System.Drawing.Size(600, 30);
            guia.Text = "Este software simples faz a listagem de processos ativos";
            guia.Parent = FormWindow2;

            Label guia2 = new Label();
            guia2 = new Label();
            this.Controls.Add(guia2);
            guia2.Location = new System.Drawing.Point(0, 30);
            guia2.Size = new System.Drawing.Size(600, 30);
            guia2.Text = "na máquina, listando o nome do processo ativo, a quantidade";
            guia2.Parent = FormWindow2;

            Label guia3 = new Label();
            guia3 = new Label();
            this.Controls.Add(guia3);
            guia3.Location = new System.Drawing.Point(0, 58);
            guia3.Size = new System.Drawing.Size(600, 30);
            guia3.Text = "de memória consumida pelo mesmo";
            guia3.Parent = FormWindow2;

            //conteudo da placa
            ListView listaPlaca = new ListView();
            ListViewItem listaContain = new ListViewItem();
            this.Controls.Add(listaPlaca);
            listaPlaca.Parent = FormWindow;
            listaPlaca.Size = new System.Drawing.Size(500, 200);
            listaPlaca.Columns.Add("Nomes", 400);
            //listaPlaca.Columns[0].Text = "Nomes";
            listaPlaca.Columns[0].Width = 599;
            listaPlaca.BackColor = Color.Silver;
            //listaPlaca.Items.AddRange(new ListViewItem[] { listaContain });
            listaPlaca.View = View.Details;

            label1 = new Label();
            this.Controls.Add(label1);
            label1.Location = new System.Drawing.Point(350, 19);
            label1.Size = new System.Drawing.Size(120, 30);
            Label label2 = new Label();
            label2 = new Label();
            this.Controls.Add(label2);
            label2.Location = new System.Drawing.Point(500, 40);
            label2.Size = new System.Drawing.Size(120, 30);
            label2.Text = "Clique em iniciar";

            //LISTA
            listView1 = new ListView();
            listViewItem2 = new ListViewItem();
            listViewItem2.SubItems.AddRange(JSP);
            this.Controls.Add(listView1);
            listView1.Columns.Add("Processos", 270);
            listView1.Columns.Add("Memória", 100);
            listView1.View = View.Details;

            //listView1.Items.AddRange(new ListViewItem[] {listViewItem2});

            listView1.Size = new System.Drawing.Size(400, 320);
            //.Columns[1].Width = 100;
            listView1.BackColor = Color.Silver;
            listView1.GridLines = true;
            listView1.Location = new System.Drawing.Point(12, 10);
            label2.Enabled = false;
            listView1.Parent = formList;

            List<string> devs = new List<string>();
            devs.Add("Luiz Renan Silva Chagas");

            //progressBar
            usage = new ProgressBar();
            this.Controls.Add(usage);
            usage.Location = new System.Drawing.Point(12, 420);
            usage.Width = 400;
            usage.Maximum = 10000;
            usage.Minimum = 0;

            //taxa de uso de memória
            Form FormWindow3 = new Form();
            //this.Controls.Add(FormWindow);
            FormWindow3.Size = new System.Drawing.Size(500, 250);
            FormWindow3.Location = new System.Drawing.Point(700, 40);
            FormWindow3.BackColor = Color.Silver;
            FormWindow3.Visible = true;
            FormWindow3.Text = "Detalhes";
            //listBox
            ListBox boxList = new ListBox();
            this.Controls.Add(boxList);
            boxList.Size = new System.Drawing.Size(500, 250);
            boxList.Parent = FormWindow3;
            boxList.BackColor = Color.AliceBlue;


            foreach (var names in devs)
            {
                listaPlaca.Items.Add(names.ToString());
            }
            //Adiciona os processos e a quantidade de memória usada por cada um
            foreach (var process in Process.GetProcesses())
            {
                List<string> nickNames = new List<string>();
                nickNames.Add(process.ProcessName);
                foreach (string nome in nickNames)
                {
                    listView1.Items.Add(nome.ToString());
                    listaNomes.Add(nome.ToString());
                    amount += 1;
                }

                double memory = process.PrivateMemorySize64 / 1024 / 1024;
                List<double> amountMemory = new List<double>();
                amountMemory.Add(memory);

                for (int i = 0; i < amount; i++)
                {
                    listView1.Items[i].SubItems.Add(memory.ToString());
                }

                foreach(double memoryCount in amountMemory)
                {
                    soma += memoryCount;
                    countMemory.Add(memoryCount);
                }

                textBox1.Text = soma.ToString();

            }
            label1.Invoke(new Action(() => label1.Text = amount.ToString()));

            //moldura do painel de que mostra o total de processos
            TextBox totalBox = new TextBox();
            this.Controls.Add(totalBox);
            totalBox.Size = new System.Drawing.Size(90, 10);
            totalBox.Location = new System.Drawing.Point(300, 10);
            totalBox.BackColor = Color.Silver;
            totalBox.Text = label1.Text;
            label1.Parent = totalBox;
            totalBox.SendToBack();
            label1.BringToFront();
        }
        public void showProcessDetail(Object source, System.Timers.ElapsedEventArgs e)
        {
            if (statusUsage.InvokeRequired)
            {
                foreach (var use in Process.GetProcessesByName(nameThread))
                {
                    double averageUse = 0;
                    averageUse = use.PrivateMemorySize64 / 1024 / 1024;
                    statusUsage.Invoke(new Action(() => statusUsage.Value = int.Parse(averageUse.ToString())));
                    memoryValue.Invoke(new Action(() => memoryValue.Text = averageUse.ToString()));
                }
            }

        }

        public void constructor()
        {
            //neste método é construida a interface de detalhes sobre o processso
            //acima são adicionadas as informações
            listView1.FullRowSelect = true;
            FormDetail = new Form();
            //this.Controls.Add(FormDetail);
            FormDetail.Size = new System.Drawing.Size(600, 300);
            FormDetail.BackColor = Color.Silver;
            FormDetail.Visible = true;

            nameDetail = new Label();
            this.Controls.Add(nameDetail);
            nameDetail.Width = 800;
            nameDetail.Parent = FormDetail;
            nameThread = listView1.SelectedItems[0].Text.ToString(); 
            nameDetail.Text = nameThread;
            exitProcess = nameThread.ToString();


            statusUsage = new ProgressBar();
            this.Controls.Add(statusUsage);
            statusUsage.Size = new System.Drawing.Size(200, 20);
            statusUsage.Parent = FormDetail;
            statusUsage.BackColor = Color.BlueViolet;
            statusUsage.Location = new System.Drawing.Point(200, 5);
            statusUsage.BringToFront();
            statusUsage.Minimum = 0;
            statusUsage.Maximum = 10000;

            memoryValue = new Label();
            string ram;
            this.Controls.Add(memoryValue);
            memoryValue.Width = 800;
            memoryValue.Parent = FormDetail;
            memoryValue.Location = new System.Drawing.Point(500, 5);
            //ram = listView1.SelectedItems[0].SubItems.ToString();
            
            memoryValue.BringToFront();
            
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            tempo2.Enabled = true;
            constructor();
        }

        private void progressBar1_Click_1(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tempo.Enabled = true;
            button4.BackColor = Color.Green;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tempo.Enabled = false;
            button4.BackColor = Color.Red;
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            killProcess();
        }

        public void alertHighUseMemory()//verifica qual programa está usando memória em excesso
        {
            foreach(var highUse in Process.GetProcesses())
            {
                if (highUse.PrivateMemorySize64 /1024/1024 > memoryLimit)
                {
                    //Invoke(new Action(() => exitProgram.Visible = true));
                    textAlert.Invoke(new Action(() => textAlert.Visible = true));
                    textAlert.Invoke(new Action(() => textAlert.Text = "Este programa, '"+highUse.ProcessName.ToString()+"', esta em alto uso"));
                    programInHighUse = highUse.ProcessName;
                }
            }
        }

        void fecharProgramaEmAltoUso_Click(object sender, EventArgs e)//herda o nome do programa em alto uso na variável programInHighUse e o encerra
        {
            foreach(var i in Process.GetProcessesByName('"' + programInHighUse + '"'))
            {
                i.Kill();
            }
        }

        public void fixedMemory()
        {
            memoryLimit = int.Parse(textBox2.Text.ToString());
        }

        private void button6_Click(object sender, EventArgs e)
        {
            fixedMemory();
            textBox2.Enabled = false;
        }
    }
}
