using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.WebSockets;
using System.Net.Sockets;
using Fleck;
using System.Net;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace FileTrans
{
    public partial class Form1 : Form
    {
        WebSocketServer server;
        private List<IWebSocketConnection> sockets = new List<IWebSocketConnection>();
        private string localIp;
        List<SocketUser> users = new List<SocketUser>();
        StringBuilder sb = new StringBuilder();
        String filename = "";

        private string GetTime()
        {
            return "[" + DateTime.Now.ToString("HH:mm:ss") + "]";
        }

        public Form1()
        {
            InitializeComponent();
            OpenWlan();
            localIp = Dns.GetHostEntry(Dns.GetHostName()).AddressList.Where(a => a.AddressFamily == AddressFamily.InterNetwork).First().ToString();
            this.Text = "局域网文件传输助手 " + localIp + ":7181";
            server = new WebSocketServer($"ws://{localIp}:7181");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            server.Start(socket =>
            {
                socket.OnOpen = () =>
                {
                    sockets.Add(socket);
                    var user = new SocketUser
                    {
                        Connection = socket,
                        Ip = socket.ConnectionInfo.ClientIpAddress + ":" + socket.ConnectionInfo.ClientPort,
                        Uid = socket.ConnectionInfo.Id
                    };
                    users.Add(user);
                    statusBar.Invoke(new Action(() =>
                    {
                        tips.Text = $"{socket.ConnectionInfo.ClientIpAddress + ":" + socket.ConnectionInfo.ClientPort}已连接!";
                    }));
                    userList.Invoke(new Action(() =>
                    {
                        userList.Items.Add(user.Ip);
                        userList.Update();
                    }));
                };
                socket.OnClose = () =>
                {
                    sockets.Remove(socket);
                    statusBar.Invoke(new Action(() =>
                    {
                        tips.Text = $"{socket.ConnectionInfo.ClientIpAddress + ":" + socket.ConnectionInfo.ClientPort}已断开!";
                    }));
                    userList.Invoke(new Action(() =>
                    {
                        userList.Items.Remove(users.First(u => u.Uid == socket.ConnectionInfo.Id).Ip);
                        userList.Update();
                    }));
                    users.Remove(users.First(u => u.Uid == socket.ConnectionInfo.Id));
                };
                socket.OnMessage = message =>
                {
                    if (message.Contains("CDDATA"))
                    {
                        switch (message.Substring(message.IndexOf("/") + 1, message.IndexOf("]") - 1 - message.IndexOf("/")))
                        {
                            case "filename":
                                filename = message.Substring(message.IndexOf("]") + 1, message.Length - message.IndexOf("]") - 1);
                                statusBar.Invoke(new Action(() =>
                                {
                                    tips.Text = $"来自{socket.ConnectionInfo.ClientIpAddress }的文件：{filename}";
                                }));
                                break;
                            case "txt":
                                sb.Append($"{socket.ConnectionInfo.ClientIpAddress + GetTime()}:{message.Substring(message.IndexOf("]") + 1)}");
                                sb.Append(Environment.NewLine);
                                chatBox.Invoke(new Action(() =>
                                {
                                    chatBox.Text = sb.ToString();
                                }));
                                break;
                        }
                    }
                    //sockets.ToList().ForEach(s => s.Send("Echo: " + message));
                };
                socket.OnBinary = file =>
                {
                    string path = Path.Combine(Directory.GetCurrentDirectory(), filename);
                    //创建一个文件流
                    FileStream fs = new FileStream(path, FileMode.Create);
                    //将byte数组写入文件中
                    fs.Write(file, 0, file.Length);
                    //所有流类型都要关闭流，否则会出现内存泄露问题
                    fs.Flush();
                    fs.Close();
                    fs.Dispose();
                    if (filename.EndsWith(".jpg") || filename.EndsWith(".png") || filename.EndsWith(".gif") || filename.EndsWith(".jpeg") || filename.EndsWith(".bmp"))
                    {
                        new Task(new Action(() =>
                        {
                            new ImgPreview($"来自{socket.ConnectionInfo.ClientIpAddress }的文件：{filename}", path).ShowDialog();
                        })).Start();
                    }
                };
            });
        }

        private void sendMessageAll_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(sendText.Text.Trim()))
            {
                MessageBox.Show("发送文本不能为空！");
                return;
            }
            sockets.ToList().ForEach(s => s.Send("[CDDATA/txt]" + sendText.Text.Trim()));
            sb.Append($"我{GetTime()}:{sendText.Text.Trim()}");
            sb.Append(Environment.NewLine);
            chatBox.Text = sb.ToString();
        }

        private void sendFileAll_Click(object sender, EventArgs e)
        {
            OpenFileDialog opd = new OpenFileDialog();
            opd.Multiselect = true;
            if (opd.ShowDialog() == DialogResult.OK)
            {
                foreach (var f in opd.FileNames)
                {
                    sockets.ToList().ForEach(ss => ss.Send("[CDDATA/filename]" + Path.GetFileName(f)));
                    var input = File.Open(f, FileMode.Open);
                    byte[] s = new byte[input.Length];
                    input.Read(s, 0, s.Length);
                    foreach (var socket in sockets.ToList())
                    {
                        socket.Send(s);
                    }
                    input.Close();
                    sb.Append($"我{GetTime()}:文件 {Path.GetFileName(f)} 已发送!");
                    sb.Append(Environment.NewLine);
                    chatBox.Text = sb.ToString();
                }
            }
        }

        private void OpenWlan()
        {
            try
            {
                Process p = new Process();
                ProcessStartInfo info = new ProcessStartInfo();
                info.CreateNoWindow = true;
                info.UseShellExecute = false;
                info.RedirectStandardInput = true;
                info.RedirectStandardOutput = true;
                info.FileName = "cmd.exe";              
                p.StartInfo = info;
                p.Start();
                p.StandardInput.WriteLine("netsh wlan set hostednetwork mode=allow ssid=mywifi3 key=12345678");                
                p.StandardInput.WriteLine("exit");
                p.WaitForExit();
                Console.WriteLine(p.StandardOutput.ReadToEnd());
                info.FileName = "cmd.exe";
                p.Start();
                p.StandardInput.WriteLine("netsh wlan start hostednetwork");
                p.StandardInput.WriteLine("exit");
                p.WaitForExit();
                p.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("共享wifi开启失败!");
            }
        }

        private void CloseWlan()
        {
            try
            {
                Process p = new Process();
                ProcessStartInfo info = new ProcessStartInfo();
                info.CreateNoWindow = true;
                info.UseShellExecute = false;
                info.RedirectStandardInput = true;
                info.RedirectStandardOutput = true;
                info.FileName = "cmd.exe";
                p.StartInfo = info;
                p.Start();
                p.StandardInput.WriteLine("netsh wlan stop hostednetwork");
                p.StandardInput.WriteLine("exit");
                p.WaitForExit();
                p.Close();
            }
            catch (Exception ex)
            {
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            CloseWlan();
        }

        private void sendMessage_Click(object sender, EventArgs e)
        {
            if (userList.SelectedItem == null)
            {
                MessageBox.Show("请先选择发送对象!");
                return;
            }
            if (string.IsNullOrWhiteSpace(sendText.Text.Trim()))
            {
                MessageBox.Show("发送文本不能为空！");
                return;
            }
            var ip = userList.SelectedItem.ToString();
            var user = users.First(u => u.Ip == ip);
            sockets.First(s => user.Uid == s.ConnectionInfo.Id).Send("[CDDATA/txt]" + sendText.Text.Trim());
            sb.Append($"我对{user.Ip}{GetTime()}:{sendText.Text.Trim()}");
            sb.Append(Environment.NewLine);
            chatBox.Text = sb.ToString();
        }

        private void sendFile_Click(object sender, EventArgs e)
        {
            if (userList.SelectedItem == null)
            {
                MessageBox.Show("请先选择发送对象!");
                return;
            }
            var ip = userList.SelectedItem.ToString();
            var user = users.First(u => u.Ip == ip);

            OpenFileDialog opd = new OpenFileDialog();
            opd.Multiselect = true;
            if (opd.ShowDialog() == DialogResult.OK)
            {
                foreach (var f in opd.FileNames)
                {
                    sockets.First(u => user.Uid == u.ConnectionInfo.Id).Send("[CDDATA/filename]" + Path.GetFileName(f));
                    var input = File.Open(f, FileMode.Open);
                    byte[] s = new byte[input.Length];
                    input.Read(s, 0, s.Length);
                    sockets.First(u => user.Uid == u.ConnectionInfo.Id).Send(s);
                    input.Close();
                    sb.Append($"我对{user.Ip}{GetTime()}:文件 {Path.GetFileName(f)} 已发送!");
                    sb.Append(Environment.NewLine);
                    chatBox.Text = sb.ToString();
                }
            }
        }
    }
}
