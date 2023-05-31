using MySql.Data.MySqlClient;
using SmartEnergyAPI.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace API_SmartyEnergy.Models
{
    public class AlterarCadastro
    {
        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo Telefone é obrigatório.")]
        public string Telefone { get; set; }

        private readonly string connectionString = "server=esn509vmysql; database=smartenergy; user id=aluno; password=Senai1234";

        public AlterarCadastro(string email, string telefone)
        {
            Email = email;
            Telefone = telefone;
        }

        public Cliente Alterar(int id)
        {
            using (MySqlConnection conexao = new MySqlConnection(connectionString))
            {
                try
                {
                    conexao.Open();
                    MySqlCommand qryUpdate = new MySqlCommand(
                        "UPDATE CLIENTE SET email = @email, telefone = @telefone WHERE codigo = @cod", conexao);
                    qryUpdate.Parameters.AddWithValue("@cod", id);
                    qryUpdate.Parameters.AddWithValue("@email", Email);
                    qryUpdate.Parameters.AddWithValue("@telefone", Telefone);

                    int linhasAfetadas = qryUpdate.ExecuteNonQuery();

                    if (linhasAfetadas > 0)
                    {
                        MySqlCommand qrySelect = new MySqlCommand(
                            "SELECT * FROM CLIENTE WHERE codigo = @cod", conexao);
                        qrySelect.Parameters.AddWithValue("@cod", id);

                        using (MySqlDataReader leitor = qrySelect.ExecuteReader())
                        {
                            if (leitor.Read())
                            {
                                Cliente clienteAtualizado = new Cliente(
                                    leitor["nome"].ToString(),
                                    leitor["sobrenome"].ToString(),
                                    leitor["cpf"].ToString(),
                                    leitor["email"].ToString(),
                                    leitor["telefone"].ToString(),
                                    leitor["senha"].ToString(),
                                    int.Parse(leitor["codigo"].ToString()));

                                return clienteAtualizado;
                            }
                            else
                            {
                                throw new Exception("Não foi possível alterar");
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("Não foi possível alterar");
                    }
                }
                catch (MySqlException ex)
                {
                    throw new Exception("Erro ao alterar", ex);
                }
            }
        }
    }
}
