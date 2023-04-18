using MySql.Data.MySqlClient;

namespace API_SmartyEnergy.Models
{
    public class Residencia
    {
        public int Codigo { get; set; }
        public string Logradouro { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }
        public string Cep { get; set; }
        public string Municipio { get; set; }
        public string Uf { get; set; }
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

        public List<Residencia> listarResidencias(int id)
        {

            //string de conexão
            List<Residencia> residencias = new List<Residencia>();
            MySqlConnection conexao = new MySqlConnection("server=esn509vmysql ;database=db_smart_energy2 ;user id=aluno; password=Senai1234");
            try
            {
                conexao.Open();
                MySqlCommand qry = new MySqlCommand(
                    "SELECT * FROM RESIDENCIA WHERE FK_CLIENTE_codigo = @cod", conexao);
                qry.Parameters.AddWithValue("@cod", id);

                MySqlDataReader leitor = qry.ExecuteReader();

                while (leitor.Read())
                {
                    /*criei variavéis apenas para organizar melhor, é possível colocar o leitor 
                     * dentro dos parâmetros do objeto Arduino*/


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
                conexao.Close();

                return residencias;

            }
            catch (Exception e)
            {

                /*precisamos alterar o método para retornar um object,
                 *pois retornando uma array de Arduino não é possível devolver uma mensagem de erro para ser identificado*/
                throw new Exception("Erro ao buscar a última fatura.", e);
            }
            finally
            {
                //Fechar a conexão com o banco de dados
                if (conexao != null)
                {
                    conexao.Close();
                }
            }
        }
    }
}
