using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace SpamEmail
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Email e1, e2, e3, e4;

        private void Form1_Load(object sender, EventArgs e)
        {
            panel2.Hide();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<Email> dsEmailBot = new List<Email>();
            e1 = new Email(txtEmail1.Text, txtPass1.Text);
            e2 = new Email(txtEmail2.Text, txtPass2.Text);
            e3 = new Email(txtEmail3.Text, txtPass3.Text);
            e4 = new Email(txtEmail4.Text, txtPass4.Text);

            dsEmailBot.Add(e1);
            dsEmailBot.Add(e2);
            dsEmailBot.Add(e3);
            dsEmailBot.Add(e4);

            try
            {
                int packet = int.Parse(txtNumPacket.Text);
                string emailTarget = txtEmailTarget.Text;
                
                foreach (Email email in dsEmailBot)
                {
                    CheckForIllegalCrossThreadCalls = false;
                    Thread t1 = new Thread(()=> {
                        for(int i = 0; i< packet; i++)
                        {
                            try
                            {
                                MailMessage msg = new MailMessage();
                                msg.From = new MailAddress(email.EmailBot);

                                msg.To.Add(new MailAddress(emailTarget));
                                if (cbRandom.Checked)
                                {
                                    msg.Subject = randomTitle();
                                    msg.Body = randomContent();
                                }
                                else
                                {
                                    msg.Subject = txtTieuDe.Text;
                                    msg.Body = txtNoiDung.Text;
                                }


                                SmtpClient sc = new SmtpClient("smtp.gmail.com", 587);
                                sc.EnableSsl = true;
                                sc.UseDefaultCredentials = false;
                                sc.Credentials = new NetworkCredential(email.EmailBot, email.Password);
                                sc.DeliveryMethod = SmtpDeliveryMethod.Network;
                                sc.Send(msg);
                                txtNoiDung.Text = "Email has been sent successfully.";
                            }
                            catch (Exception ex)
                            {
                                txtNoiDung.Text = "ERROR: " + ex.Message;
                            }
                        }                       

                    });

                    t1.IsBackground = true;
                    t1.Start();

                    txtNoiDung.Text = "...Đang gởi...";
                    t1.Join();


                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Số lần gởi phải là số!!!");
            }
            
        }

        
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            txtTieuDe.Text = randomTitle();
            txtNoiDung.Text = randomContent();
        }

        private string randomTitle()
        {
            Random rd = new Random();
            const string allowedChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz0123456789!@$?_-";
            char[] chars = new char[10];

            for (int i = 0; i < 10; i++)
            {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }

            return new string(chars);
        }

        private string randomContent()
        {
            Random rd = new Random();
            const string allowedChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz0123456789!@$?_-";
            char[] chars = new char[2000];

            for (int i = 0; i < 2000; i++)
            {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }

            return new string(chars);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            txtEmail1.Enabled = true;
            txtEmail2.Enabled = true;
            txtEmail3.Enabled = true;
            txtEmail4.Enabled = true;

            txtPass1.Enabled = true;
            txtPass2.Enabled = true;
            txtPass3.Enabled = true;
            txtPass4.Enabled = true;

            btnChuanBiBot.Enabled = true;

            panel2.Hide();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {            
            string emailBot1 = txtEmail1.Text;
            string emailBot2 = txtEmail2.Text;
            string emailBot3 = txtEmail3.Text;
            string emailBot4 = txtEmail4.Text;

            string passBot1 = txtPass1.Text;
            string passBot2 = txtPass2.Text;
            string passBot3 = txtPass3.Text;
            string passBot4 = txtPass4.Text;

            if (emailBot1.Length == 0 || emailBot2.Length == 0
                || emailBot3.Length == 0 || emailBot4.Length == 0
                || passBot1.Length == 0 || passBot2.Length == 0
                || passBot3.Length == 0 || passBot4.Length == 0)
            {
                MessageBox.Show("Không được bỏ trống!!");
                return;
            }
            else if (isGoogleEmail(emailBot1) ==false || isGoogleEmail(emailBot2) == false ||
                isGoogleEmail(emailBot3) == false || isGoogleEmail(emailBot4) == false )
            {
                MessageBox.Show("Email không hợp lệ!!");
            }
            else
            {
                panel2.Show();
                txtEmail1.Enabled = false;
                txtEmail2.Enabled = false;
                txtEmail3.Enabled = false;
                txtEmail4.Enabled = false;

                txtPass1.Enabled = false;
                txtPass2.Enabled = false;
                txtPass3.Enabled = false;
                txtPass4.Enabled = false;

                btnChuanBiBot.Enabled = false;
            }

            
            
        }
        private bool isGoogleEmail(string email)
        {
            HashSet<string> invalidAddresses = new HashSet<string>() { "@gmail", "@hotmail", "@yahoo" }; 
            if (invalidAddresses.Any(i => email.IndexOf(i, StringComparison.CurrentCultureIgnoreCase) > -1))
            {
                return true;
            }
            else
            {
                return false;
            }            
        }
    }

    
}
