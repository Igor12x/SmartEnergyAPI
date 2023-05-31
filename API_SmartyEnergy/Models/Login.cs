using Microsoft.VisualStudio.Services.WebApi.Jwt;
using MySql.Data.MySqlClient;
using System.ComponentModel.DataAnnotations;

namespace SmartEnergyAPI.Models
{
    public class Login
    {
        private string cpf, senha;

        private readonly string connectionString = "server=esn509vmysql; database=smartenergy; user id=aluno; password=Senai1234";

        public Login(string cpf, string senha)
        {
            this.cpf = cpf;
            this.senha = senha;
        }

        public Cliente ValidarLogin(Login loginCliente)
        {
            using (MySqlConnection conexao = new MySqlConnection(connectionString))
            {
                try
                {
                    conexao.Open();

                    string query = "SELECT * FROM cliente WHERE cpf = @cpf and senha = @senha";
                    MySqlCommand command = new MySqlCommand(query, conexao);
                    command.Parameters.AddWithValue("@cpf", loginCliente.Cpf);
                    command.Parameters.AddWithValue("@senha", loginCliente.Senha);

                    using (MySqlDataReader leitor = command.ExecuteReader())
                    {
                        if (leitor.Read())
                        {
                            Cliente cliente = new Cliente(
                                leitor["nome"].ToString(),
                                leitor["sobrenome"].ToString(),
                                leitor["cpf"].ToString(),
                                leitor["email"].ToString(),
                                leitor["telefone"].ToString(),
                                leitor["senha"].ToString(),
                                int.Parse(leitor["codigo"].ToString()));

                            return cliente;
                        }
                        else
                        {
                            throw new InvalidCredentialsException("As credenciais fornecidas são inválidas. Verifique se o CPF e a senha estão corretas.");
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    throw new Exception("Erro ao executar consulta no banco de dados", ex);
                }
            }
        }

        [Required(ErrorMessage = "O campo CPF é obrigatório.")]
        public string Cpf { get => cpf; set => cpf = value; }

        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        public string Senha { get => senha; set => senha = value; }
    }
}
