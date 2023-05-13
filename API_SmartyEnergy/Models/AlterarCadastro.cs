using MySql.Data.MySqlClient;
using SmartEnergyAPI.Models;

namespace API_SmartyEnergy.Models
{
    public class AlterarCadastro
    {
        private string email;
        private string telefone;
        public string Email { get => email; set => email = value; }
        public string Telefone { get => telefone; set => telefone = value; }

        public AlterarCadastro(string email, string telefone)
        {
            Email = email;
            Telefone = telefone;
        }

        static MySqlConnection conexao = new MySqlConnection("server=esn509vmysql ;database=smartenergy ;user id=aluno; password=Senai1234");
        

        internal static Cliente Alterar(AlterarCadastro AlterarCadastro, int id)
        {
            try
            {
                conexao.Open();
                MySqlCommand qryUpdate = new MySqlCommand(

                    "UPDATE CLIENTE SET email = @email, telefone = @telefone WHERE codigo = @cod", conexao);

                qryUpdate.Parameters.AddWithValue("@email", AlterarCadastro.Email);
                qryUpdate.Parameters.AddWithValue("@telefone", AlterarCadastro.Telefone);
                qryUpdate.Parameters.AddWithValue("@cod", id);

                int linhasAfetadas = qryUpdate.ExecuteNonQuery();



                if (linhasAfetadas > 0)
                {
                    MySqlCommand qrySelect = new MySqlCommand(

                   "SELECT * FROM CLIENTE WHERE codigo = @cod", conexao);
                    qrySelect.Parameters.AddWithValue("@cod", id);

                    MySqlDataReader leitor = qrySelect.ExecuteReader();

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
                    else { throw new Exception("Não foi possível alterar"); }
                }
                else
                { throw new Exception("Não foi possível alterar"); }
            }
            catch (Exception e)
            {
                if (conexao.State == System.Data.ConnectionState.Open)
                    conexao.Close();
                throw new Exception("Erro ao alterar" + " >>>>>>>>>>>>>>>>>" + e, e);
            }
            finally
            {
                if (conexao != null)
                {
                    conexao.Close();
                }
            }
        }
    }
}
