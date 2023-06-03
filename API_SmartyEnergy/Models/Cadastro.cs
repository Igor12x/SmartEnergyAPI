using MySql.Data.MySqlClient;
using System.Text.Json.Serialization;

namespace SmartEnergyAPI.Models
{
    public class Cadastro
    {
        private readonly string connectionString = "server=esn509vmysql ;database=smartenergy ;user id=aluno; password=Senai1234";

        public Cliente Cadastrar(Cliente cliente)
        {
            using (MySqlConnection conexao = new MySqlConnection(connectionString))
            {
                try
                {
                    conexao.Open();

                    MySqlCommand verificarCpfExistente = new MySqlCommand("SELECT COUNT(*) FROM CLIENTE WHERE cpf = @cpf", conexao);
                    verificarCpfExistente.Parameters.AddWithValue("@cpf", cliente.Cpf);

                    int quantidadeClientesComCpf = Convert.ToInt32(verificarCpfExistente.ExecuteScalar());

                    if (quantidadeClientesComCpf > 0)
                    {
                        throw new Exception("Já existe um cliente com o mesmo CPF cadastrado.");
                    }

                    MySqlCommand qry = new MySqlCommand(
                        "INSERT INTO CLIENTE (nome, sobrenome, cpf, senha, email, telefone) VALUES (@nome, @sobrenome, @cpf, @senha, @email, @telefone)",
                        conexao);
                    qry.Parameters.AddWithValue("@nome", cliente.Nome);
                    qry.Parameters.AddWithValue("@sobrenome", cliente.Sobrenome);
                    qry.Parameters.AddWithValue("@cpf", cliente.Cpf);
                    qry.Parameters.AddWithValue("@senha", cliente.Senha);
                    qry.Parameters.AddWithValue("@email", cliente.Email);
                    qry.Parameters.AddWithValue("@telefone", cliente.Telefone);

                    int linhasAfetadas = qry.ExecuteNonQuery();
                    if (linhasAfetadas > 0)
                    {
                        return cliente;
                    }
                    else
                    {
                        throw new Exception("Não foi possível cadastrar o cliente.");
                    }
                }
                catch (MySqlException ex)
                {
                    throw new Exception("Erro ao cadastrar o cliente", ex);
                }
            }
        }
    }
}
