using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.VisualStudio.Services.WebApi.Jwt;
using MimeKit;
using MySql.Data.MySqlClient;
using SmartEnergyAPI.Models;

namespace API_SmartyEnergy.Models
{
    public class RecuperarSenha
    {
        private readonly string _host;
        private readonly int _port;
        private readonly bool _useSsl;
        private readonly string _usuarioEmail;
        private readonly string _senha;

        static MySqlConnection conexao = new MySqlConnection("server=127.0.0.1; port=3306; database=smartenergy; user id=root; password=1234");

        public RecuperarSenha(string host, int port, bool useSsl, string usuarioEmail, string senha)
        {
            _host = host;
            _port = port;
            _useSsl = useSsl;
            _usuarioEmail = usuarioEmail;
            _senha = senha;
        }

        internal static string EnviarCodigoVerificacao(string emailCliente)
        {
            try
            {
                conexao.Open();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Erro ao abrir conexão com o banco de dados", ex);
            }

            try
            {
                MySqlCommand qry = new MySqlCommand(
                    "SELECT * FROM cliente WHERE email = @email", conexao);
                qry.Parameters.AddWithValue("@email", emailCliente);

                MySqlDataReader leitor = qry.ExecuteReader();

                if (leitor.Read())
                {
                    var numAleatorio = new Random();
                    var codigoVerificacao = numAleatorio.Next(1000, 9999).ToString();

                    using (var client = new SmtpClient())
                    {
                        client.Connect("smtp.office365.com", 587, SecureSocketOptions.StartTls);
                        client.Authenticate("suporte.smartenergy@hotmail.com", "Smart1234");

                        var message = new MimeMessage();
                        message.From.Add(new MailboxAddress("Smart Energy", "suporte.smartenergy@hotmail.com"));
                        message.To.Add(new MailboxAddress("", emailCliente));
                        message.Subject = "Codigo de verificação - Smart Energy";
                        message.Body = new TextPart("html")
                        {
                            Text = string.Format("Seu código de verificação é: <strong>{0}</strong>", codigoVerificacao)
                        };

                        client.Send(message);
                        client.Disconnect(true);
                    }

                    return codigoVerificacao;
                }
                else
                {
                    throw new InvalidCredentialsException("As credenciais fornecidas são inválidas. Verifique se o e-mail digitado está correto.");
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("Erro ao executar consulta no banco de dados", ex);
            }
            finally
            {
                try
                {
                    if (conexao.State == System.Data.ConnectionState.Open)
                    {
                        conexao.Close();
                    }
                }
                catch (MySqlException ex)
                {
                    throw new Exception("Erro ao fechar conexão com o banco de dados", ex);
                }
            }            
        }
    }

    
}
