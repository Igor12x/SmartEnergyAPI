using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MySql.Data.MySqlClient;

namespace API_SmartyEnergy.Models
{
    public class Residencia
    {
        [Required(ErrorMessage = "O campo Código é obrigatório.")]
        public int Codigo { get; set; }

        [Required(ErrorMessage = "O campo Logradouro é obrigatório.")]
        public string Logradouro { get; set; }

        [Required(ErrorMessage = "O campo Número é obrigatório.")]
        public int Numero { get; set; }

        public string Complemento { get; set; }

        [Required(ErrorMessage = "O campo CEP é obrigatório.")]
        public string Cep { get; set; }

        [Required(ErrorMessage = "O campo Município é obrigatório.")]
        public string Municipio { get; set; }

        [Required(ErrorMessage = "O campo UF é obrigatório.")]
        public string Uf { get; set; }

        [Required(ErrorMessage = "O campo Bairro é obrigatório.")]
        public string Bairro { get; set; }

        public Residencia(int codigo, string logradouro, int numero, string complemento, string cep, string municipio, string uf, string bairro)
        {
            Codigo = codigo;
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Cep = cep;
            Municipio = municipio;
            Uf = uf;
            Bairro = bairro;
        }

        internal static List<Residencia> Listar(int idCliente)
        {
            List<Residencia> residencias = new List<Residencia>();
            string connectionString = "server=esn509vmysql; database=smartenergy; user id=aluno; password=Senai1234";

            using (MySqlConnection conexao = new MySqlConnection(connectionString))
            {
                try
                {
                    conexao.Open();
                    MySqlCommand qry = new MySqlCommand(
                        "SELECT * FROM RESIDENCIA WHERE FK_CLIENTE_codigo = @cod", conexao);
                    qry.Parameters.AddWithValue("@cod", idCliente);

                    using (MySqlDataReader leitor = qry.ExecuteReader())
                    {
                        while (leitor.Read())
                        {
                            Residencia r = new Residencia(
                                int.Parse(leitor["codigo"].ToString()),
                                leitor["logradouro"].ToString(),
                                int.Parse(leitor["numero"].ToString()),
                                leitor["complemento"].ToString(),
                                leitor["cep"].ToString(),
                                leitor["municipio"].ToString(),
                                leitor["uf"].ToString(),
                                leitor["bairro"].ToString()
                            );

                            residencias.Add(r);
                        }
                    }

                    return residencias;
                }
                catch (MySqlException ex)
                {
                    throw new Exception("Erro ao buscar as residências.", ex);
                }
            }
        }
        internal static bool VerificarCadastro(string cpf)
        {
            string connectionString = "server=localhost; database=smartenergy; user id=root; password=9133";

            using (MySqlConnection conexao = new MySqlConnection(connectionString))
            {
                try
                {
                    conexao.Open();

                    MySqlCommand qryCliente = new MySqlCommand(
                        "SELECT codigo FROM CLIENTE WHERE cpf = @cpf", conexao);
                    qryCliente.Parameters.AddWithValue("@cpf", cpf);

                    int clienteCodigo = Convert.ToInt32(qryCliente.ExecuteScalar());

                    MySqlCommand qryResidencia = new MySqlCommand(
                        "SELECT COUNT(*) FROM RESIDENCIA WHERE FK_CLIENTE_codigo = @cod", conexao);
                    qryResidencia.Parameters.AddWithValue("@cod", clienteCodigo);

                    int count = Convert.ToInt32(qryResidencia.ExecuteScalar());

                    return count > 0;
                }
                catch (MySqlException ex)
                {
                    throw new Exception("Erro ao verificar se o usuário possui uma residência cadastrada.", ex);
                }
            }
        }


    }
}
