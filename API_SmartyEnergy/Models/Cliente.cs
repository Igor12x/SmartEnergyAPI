using System.ComponentModel.DataAnnotations;

namespace SmartEnergyAPI.Models
{
    public class Cliente
    {
        private int codigo;
        private string nome, sobrenome, cpf, email, senha, telefone;

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

        public int Codigo { get => codigo; set => codigo = value; }

        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        public string Nome { get => nome; set => nome = value; }

        [Required(ErrorMessage = "O campo Sobrenome é obrigatório.")]
        public string Sobrenome { get => sobrenome; set => sobrenome = value; }

        [Required(ErrorMessage = "O campo CPF é obrigatório.")]
        public string Cpf { get => cpf; set => cpf = value; }

        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O campo Email deve ser um endereço de e-mail válido.")]
        public string Email { get => email; set => email = value; }

        [Required(ErrorMessage = "O campo Telefone é obrigatório.")]
        public string Telefone { get => telefone; set => telefone = value; }

        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        public string Senha { get => senha; set => senha = value; }
    }
}
