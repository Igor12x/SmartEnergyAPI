using MySql.Data.MySqlClient;
using System.Text.Json.Serialization;

namespace SmartEnergyAPI.Models
{
    public class Cliente
    {



        private string nome, cpf, email, senha, telefone, sobrenome;
        private int codigo;


        [JsonConstructor]
        public Cliente(string nome, string sobrenome, string cpf, string email, string telefone, string senha)
        {
            Nome = nome;
            Sobrenome = sobrenome;
            Cpf = cpf;
            Email = email;
            Telefone = telefone;
            Senha = senha;
        }
        public Cliente(string nome, string sobrenome, string cpf, string email, string telefone, string senha, int codigo)
        {
            Nome = nome;
            Sobrenome = sobrenome;
            Cpf = cpf;
            Email = email;
            Telefone = telefone;
            Senha = senha;
            Codigo = codigo;
        }


        public Cliente() { }


        public string Nome { get => nome; set => nome = value; }
        public string Cpf { get => cpf; set => cpf = value; }
        public string Email { get => email; set => email = value; }
        public string Telefone { get => telefone; set => telefone = value; }
        public string Senha { get => senha; set => senha = value; }
        public int Codigo { get => codigo; set => codigo = value; }
        public string Sobrenome { get => sobrenome; set => sobrenome = value; }

       
    }
}
