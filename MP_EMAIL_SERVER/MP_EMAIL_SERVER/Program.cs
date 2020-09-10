using System;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;

namespace MP_EMAIL_SERVER
{

    class Program
    {

        static string RECIEVING_EMAIL;
        static string CODE_TO_BE_SENT;
        static string TYPE_OF_MAIL;

        static void Main(string[] args)
        {
            serverMAIN();
        }




        static void sendBETAConfirmation()
        {
            var verificationEMAIL = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("dy.testingemail@gmail.com", "Batuhan2020!"),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("dy.testingemail@gmail.com"),
                Subject = "Welcone!",
                Body = "<h1><center>Hello! You are now enrolled in the CLOSED BETA for MassPass!</center></h1>" +
    "<h1 style='text-align:center;'><span style='color: #ffffff;'><strong><span style='background-color: #ff0000;font-size:15px;'>‎ ‏‏‎ MassPass ‏‏‎  ‏‏‎  </span></strong></span></h1>" // Mass Pass Tab
    ,
                IsBodyHtml = true,
            };
            //mailMessage.To.Add("carter.brett@sbs.org ");
            //mailMessage.To.Add("luca.riggs@sbs.org");
            mailMessage.To.Add("julie.salam@sbs.org");
            //carter.brett@sbs.org 
            verificationEMAIL.Send(mailMessage);

        }




        static void sendVerification(String email, String code)
        {


            var verificationEMAIL = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("dy.testingemail@gmail.com", "Batuhan2020!"),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("dy.testingemail@gmail.com"),
                Subject = "MassPass Verification",
                Body = "<h1><center>Verification Code : </center></h1>" +
                "<h2 style='font-family:'Courier New':font-size:100px;><center>" + code + "</center></h2>" +
                "<h1 style='text-align:center;'><span style='color: #ffffff;'><strong><span style='background-color: #ff0000;font-size:15px;'>‎ ‏‏‎ MassPass ‏‏‎  ‏‏‎  </span></strong></span></h1>" // Mass Pass Tab
                ,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(RECIEVING_EMAIL);
            verificationEMAIL.Send(mailMessage);
            Console.WriteLine("SENT VERIFICATION TO: " + RECIEVING_EMAIL + " TYPE: " + TYPE_OF_MAIL);

            //var attachment = new Attachment("/Users/dennisyilmaz/Projects/MP_EMAIL_SERVER/MP_EMAIL_SERVER/images/logo.png", MediaTypeNames.Image.Jpeg);
            //mailMessage.Attachments.Add(attachment);
            //verificationEMAIL.Send("dy.testingemail@gmail.com", "spernatounited@icloud.com", "MassPass Verification",
            //"MassPass authentication code : ");
        }

        static void printstatus(String printable)
        {
            Console.WriteLine(printable);
        }


        static void serverMAIN()
        {
            TcpListener server = null;

                // Set the TcpListener on port 13000.
                Int32 port = 13000;
                IPAddress localAddr = IPAddress.Parse("0.0.0.0");

                // TcpListener server = new TcpListener(port);
                server = new TcpListener(localAddr, port);

                // Start listening for client requests.
                server.Start();

                // Buffer for reading data
                Byte[] bytes = new Byte[256];
                String data = null;

                // Enter the listening loop.
                while (true)
                {
                    Console.WriteLine("test");
                    Console.Write("Waiting for a connection... ");

                    // Perform a blocking call to accept requests.
                    // You could also use server.AcceptSocket() here.
                    TcpClient client = server.AcceptTcpClient();
                    
                    //server.AcceptSocket();

                    Console.WriteLine("recieved");

                    data = null;
                    NetworkStream stream = client.GetStream();

                    int i;

                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                        Console.WriteLine("Received: {0}", data);
                        string[] words = data.Split("$%$");
                        CODE_TO_BE_SENT = words[0];
                        RECIEVING_EMAIL = words[1];
                        TYPE_OF_MAIL = words[2];
                        Console.WriteLine("CODE: " + CODE_TO_BE_SENT + " | EMAIL: " + RECIEVING_EMAIL + " | TYPE: " + TYPE_OF_MAIL);
                        if (TYPE_OF_MAIL == "verification")
                        {
                            sendVerification(RECIEVING_EMAIL, CODE_TO_BE_SENT);
                            
                        }
                        else if (TYPE_OF_MAIL == "beta")
                        {

                        };
                        
                    }

                    // Shutdown and end connection
                    
                }
                
            }



        
    }




}