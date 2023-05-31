using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.VisualStudio.Services.WebApi.Jwt;
using MimeKit;
using MySql.Data.MySqlClient;
using SmartEnergyAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API_SmartyEnergy.Models
{
    public class RecuperarSenha
    {
        private readonly string _host;
        private readonly int _port;
        private readonly bool _useSsl;
        private readonly string _usuarioEmail;
        private readonly string _senha;
        private string email, senha;

        private static string enderecoConexao = "server=esn509vmysql; database=smartenergy; user id=aluno; password=Senai1234";

        public RecuperarSenha(string host, int port, bool useSsl, string usuarioEmail, string senha)
        {
            _host = host;
            _port = port;
            _useSsl = useSsl;
            _usuarioEmail = usuarioEmail;
            _senha = senha;
        }

        [JsonConstructor]
        public RecuperarSenha(string email, string senha)
        {
            this.email = email;
            this.senha = senha;
        }

        public static string EnviarCodigoVerificacao(string emailCliente)
        {
            using (var conexao = new MySqlConnection(enderecoConexao))
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
                    using (var qry = new MySqlCommand("SELECT * FROM cliente WHERE email = @email", conexao))
                    {
                        qry.Parameters.AddWithValue("@email", emailCliente);

                        using (var leitor = qry.ExecuteReader())
                        {
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
                    }
                }
                catch (MySqlException ex)
                {
                    throw new Exception("Erro ao executar consulta no banco de dados", ex);
                }
            }
        }


        public string RedefinirSenha(RecuperarSenha novaSenhaCliente)
        {
            using (var conexao = new MySqlConnection(enderecoConexao))
            {
                try
                {
                    conexao.Open();

                    using (var qry = new MySqlCommand("UPDATE CLIENTE SET SENHA = @senha WHERE EMAIL = @email", conexao))
                    {
                        qry.Parameters.AddWithValue("@senha", novaSenhaCliente.Senha);
                        qry.Parameters.AddWithValue("@email", novaSenhaCliente.Email);

                        int linhasAfetadas = qry.ExecuteNonQuery();
                        if (linhasAfetadas > 0)
                        {
                            return "Senha alterada com sucesso";
                        }
                        else
                        {
                            return "Não foi possível cadastrar";
                        }
                    }
                }
                catch (MySqlException e)
                {
                    throw new Exception("Erro ao executar consulta no banco de dados", e);
                }
            }
        }

        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O campo Email deve ser um endereço de e-mail válido.")]
        public string Email { get => email; set => email = value; }

        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        public string Senha { get => senha; set => senha = value; }
    }
}
