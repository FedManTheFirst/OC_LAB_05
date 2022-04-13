using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

class MainUIThreadForm : Form
{
    [STAThread]
    //Запуск приложения
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new MainUIThreadForm());
    }
    private IntPtr firstThreadFormHandle;
    private IntPtr secondThreadFormHandle;
    private IntPtr thirdThreadFormHandle;
    private IntPtr fourthThreadFormHandle;
    //Планировщик задач
    public MainUIThreadForm()
    {
        Text = "Планировщик задач";
        Button button;
        //Кнопки потоков первый поток
        Controls.Add(button = new Button { Name = "Start", Text = "Первый поток", AutoSize = true, Location = new Point(10, 10) });
        button.Click += (s, e) =>
        {
            //Запуск потокоа
            if (firstThreadFormHandle == IntPtr.Zero)
            {
                Form form = new Form
                {
                    Text = "Первый поток",
                    Location = new Point(450, 100),
                    StartPosition = FormStartPosition.Manual,
                };
                form.HandleCreated += FirstThreadFormHandleCreated;
                form.HandleDestroyed += FirstThreadFormHandleDestroyed;
                form.RunInNewThread(false);
            }
        };
        //Второй поток
        Controls.Add(button = new Button { Name = "Start", Text = "Второй поток", AutoSize = true, Location = new Point(10, 40) });
        button.Click += (s, e) =>
        {
            //Запуск потокоа
            if (secondThreadFormHandle == IntPtr.Zero)
            {
                Form form = new Form
                {
                    Text = "Второй поток",
                    Location = new Point(750, 100),
                    StartPosition = FormStartPosition.Manual,
                };
                form.HandleCreated += SecondThreadFormHandleCreated;
                form.HandleDestroyed += SecondThreadFormHandleDestroyed;
                form.RunInNewThread(false);
            }
        };
        //Третий поток
        Controls.Add(button = new Button { Name = "Start", Text = "Третий поток", AutoSize = true, Location = new Point(10, 70) });
        button.Click += (s, e) =>
        {
            //Запуск потокоа
            if (thirdThreadFormHandle == IntPtr.Zero)
            {
                Form form = new Form
                {
                    Text = "Третий поток",
                    Location = new Point(1050, 100),
                    StartPosition = FormStartPosition.Manual,
                };
                form.HandleCreated += ThirdThreadFormHandleCreated;
                form.HandleDestroyed += ThirdThreadFormHandleCreated;
                form.RunInNewThread(false);
            }
        };
        //Четвёртый поток
        Controls.Add(button = new Button { Name = "Start", Text = "Четвёртый поток", AutoSize = true, Location = new Point(10, 100) });
        button.Click += (s, e) =>
        {
            //Запуск потокоа
            if (fourthThreadFormHandle == IntPtr.Zero)
            {
                Form form = new Form
                {
                    Text = "Четвёртый поток",
                    Location = new Point(1350, 100),
                    StartPosition = FormStartPosition.Manual,
                };
                form.HandleCreated += FourthThreadFormHandleCreated;
                form.HandleDestroyed += FourthThreadFormHandleDestroyed;
                form.RunInNewThread(false);
            }
        };
        //Кнопка стопа 1
        Controls.Add(button = new Button { Name = "Stop1", Text = "Остановить поток 1", AutoSize = true, Location = new Point(150, 10), Enabled = false });
        button.Click += (s, e) =>
        {
            if (firstThreadFormHandle != IntPtr.Zero)
                PostMessage(firstThreadFormHandle, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
        };
        //Кнопка стопа 2
        Controls.Add(button = new Button { Name = "Stop2", Text = "Остановить поток 2", AutoSize = true, Location = new Point(150, 40), Enabled = false });
        button.Click += (s, e) =>
        {
            if (secondThreadFormHandle != IntPtr.Zero)
                PostMessage(secondThreadFormHandle, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
        };
        //Кнопка стопа 3
        Controls.Add(button = new Button { Name = "Stop3", Text = "Остановить поток 3", AutoSize = true, Location = new Point(150, 70), Enabled = false });
        button.Click += (s, e) =>
        {
            if (thirdThreadFormHandle != IntPtr.Zero)
                PostMessage(thirdThreadFormHandle, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
        };
        //Кнопка стопа 4
        Controls.Add(button = new Button { Name = "Stop4", Text = "Остановить поток 4", AutoSize = true, Location = new Point(150, 100), Enabled = false });
        button.Click += (s, e) =>
        {
            if (fourthThreadFormHandle != IntPtr.Zero)
                PostMessage(fourthThreadFormHandle, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
        };
    }


    //Первый поток
    void FirstThreadFormHandleCreated(object sender, EventArgs e)
    {
        Control second = sender as Control;
        firstThreadFormHandle = second.Handle;
        second.HandleCreated -= FirstThreadFormHandleCreated;
        EnableStopButton1(true);
    }

    void FirstThreadFormHandleDestroyed(object sender, EventArgs e)
    {
        Control second = sender as Control;
        firstThreadFormHandle = IntPtr.Zero;
        second.HandleDestroyed -= FirstThreadFormHandleDestroyed;
        EnableStopButton1(false);
    }

    void EnableStopButton1(bool enabled)
    {
        if (InvokeRequired)
            BeginInvoke((Action)(() => EnableStopButton1(enabled)));
        else
        {
            Control stopButton = Controls["Stop1"];
            if (stopButton != null)
                stopButton.Enabled = enabled;
        }
    }
    //Второй поток
    void SecondThreadFormHandleCreated(object sender, EventArgs e)
    {
        Control second = sender as Control;
        secondThreadFormHandle = second.Handle;
        second.HandleCreated -= SecondThreadFormHandleCreated;
        EnableStopButton2(true);
    }

    void SecondThreadFormHandleDestroyed(object sender, EventArgs e)
    {
        Control second = sender as Control;
        secondThreadFormHandle = IntPtr.Zero;
        second.HandleDestroyed -= SecondThreadFormHandleDestroyed;
        EnableStopButton2(false);
    }

    void EnableStopButton2(bool enabled)
    {
        if (InvokeRequired)
            BeginInvoke((Action)(() => EnableStopButton2(enabled)));
        else
        {
            Control stopButton = Controls["Stop2"];
            if (stopButton != null)
                stopButton.Enabled = enabled;
        }
    }
    //Третий поток
    void ThirdThreadFormHandleCreated(object sender, EventArgs e)
    {
        Control second = sender as Control;
        thirdThreadFormHandle = second.Handle;
        second.HandleCreated -= ThirdThreadFormHandleCreated;
        EnableStopButton3(true);
    }

    void EnableStopButton3(bool enabled)
    {
        if (InvokeRequired)
            BeginInvoke((Action)(() => EnableStopButton3(enabled)));
        else
        {
            Control stopButton = Controls["Stop3"];
            if (stopButton != null)
                stopButton.Enabled = enabled;
        }
    }

    void ThirdThreadFormHandleDestroyed(object sender, EventArgs e)
    {
        Control second = sender as Control;
        thirdThreadFormHandle = IntPtr.Zero;
        second.HandleDestroyed -= ThirdThreadFormHandleDestroyed;
        EnableStopButton3(false);
    }
    //Четвёртый поток
    void FourthThreadFormHandleCreated(object sender, EventArgs e)
    {
        Control second = sender as Control;
        fourthThreadFormHandle = second.Handle;
        second.HandleCreated -= FourthThreadFormHandleCreated;
        EnableStopButton4(true);
    }

    void FourthThreadFormHandleDestroyed(object sender, EventArgs e)
    {
        Control second = sender as Control;
        fourthThreadFormHandle = IntPtr.Zero;
        second.HandleDestroyed -= FourthThreadFormHandleDestroyed;
        EnableStopButton4(false);
    }

    void EnableStopButton4(bool enabled)
    {
        if (InvokeRequired)
            BeginInvoke((Action)(() => EnableStopButton4(enabled)));
        else
        {
            Control stopButton = Controls["Stop4"];
            if (stopButton != null)
                stopButton.Enabled = enabled;
        }
    }

    const int WM_CLOSE = 0x0010;
    [DllImport("User32.dll")]
    extern static IntPtr PostMessage(IntPtr hWnd, int message, IntPtr wParam, IntPtr lParam);
}

internal static class FormExtensions
{
    private static void ApplicationRunProc(object state)
    {
        Application.Run(state as Form);
    }

    public static void RunInNewThread(this Form form, bool isBackground)
    {
        if (form == null)
            throw new ArgumentNullException("form");
        if (form.IsHandleCreated)
            throw new InvalidOperationException("Form is already running.");
        Thread thread = new Thread(ApplicationRunProc);
        thread.SetApartmentState(ApartmentState.STA);
        thread.IsBackground = isBackground;
        thread.Start(form);
    }
}