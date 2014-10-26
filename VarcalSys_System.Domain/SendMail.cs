﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace VarcalSys_System.Domain
{
    public class SendMail
    {
        const string nomeRemetente = "VarçalSys Soluções";
        const string emailRemetente = "contato@varcalsys.com.br";
        const string senha = "cleber30";
        const string SMTP = "webmail.varcalsys.com.br";
        const string assuntoMensagem = "Recebimento Confirmado";
        const string conteudoMensagem = "Seu email foi encaminhado para o setor responsável com sucesso";
        string emailDestinatario = "";

        //Cria objeto com os dados do SMTP
        SmtpClient objSmtp = new SmtpClient();
        
        NetworkCredential credentials = new NetworkCredential(emailRemetente, senha);

        public void EnviarEmail(Contato contato)
        {

            emailDestinatario = contato.Email;
            //Criacao do email
            var objEmail = new MailMessage();
            objEmail.From = new MailAddress(nomeRemetente + "<" + emailRemetente + ">");
            objEmail.To.Add(emailDestinatario);
            objEmail.Priority = MailPriority.Normal;
            objEmail.IsBodyHtml = true;
            objEmail.Subject = assuntoMensagem;
            objEmail.Body = conteudoMensagem;
            //Evitar caracteres estranhos
            objEmail.SubjectEncoding = Encoding.GetEncoding("ISO-8859-1");
            objEmail.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");

            //Alocamos o endereço do host para enviar os e-mails  
            objSmtp.Credentials = credentials;
            objSmtp.Host = SMTP;
            objSmtp.Port = 587;
                //objSmtp.EnableSsl = true;

            Enviar(objEmail);
        }

        public void EnviaEmailResponsavel(Contato contato)
        {
            //Criacao do email
            var objEmail = new MailMessage();
            objEmail.From = new MailAddress(nomeRemetente + "<" + emailRemetente + ">");
            objEmail.To.Add("faleconosco@varcalsys.com.br");
            objEmail.Priority = MailPriority.Normal;
            objEmail.IsBodyHtml = true;
            objEmail.Subject = assuntoMensagem;
            objEmail.Body = "Você recebeu uma nova mensagem";
            //Evitar caracteres estranhos
            objEmail.SubjectEncoding = Encoding.GetEncoding("ISO-8859-1");
            objEmail.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");

            //Alocamos o endereço do host para enviar os e-mails  
            objSmtp.Credentials = credentials;
            objSmtp.Host = SMTP;
            objSmtp.Port = 587;
            //objSmtp.EnableSsl = true;

           Enviar(objEmail);         
        }


        private void Enviar(MailMessage objEmail)
        {
            try
            {
                objSmtp.Send(objEmail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //excluímos o objeto de e-mail da memória
                objEmail.Dispose();
                //anexo.Dispose();
            }
        }        
    }
}
