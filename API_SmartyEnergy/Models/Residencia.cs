using MySql.Data.MySqlClient;

namespace API_SmartyEnergy.Models
{
    public class Residencia
    {
        private int codigo;
        private string logradouro;
        private int numero;
        private string complemento;
        private string bairro;
        private string uf;
        private string municipio;
        private string cep;

        public int Codigo { get => codigo; set => codigo = value; }
        public string Logradouro { get => logradouro; set => logradouro = value; }
        public int Numero { get => numero; set => numero = value; }
        public string Complemento { get => complemento; set => complemento = value; }
        public string Cep { get => cep; set => cep = value; }
        public string Municipio { get => municipio; set => municipio = value; }
        public string Uf { get => uf; set => uf = value; }
        public string Bairro { get => bairro; set => bairro = value; }

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
        internal static List<Residencia> listar(int idCliente)
        {
            List<Residencia> residencias = new List<Residencia>();
            MySqlConnection conexao = new MySqlConnection("server=esn509vmysql ;database=db_smart_energy2 ;user id=aluno; password=Senai1234");
            try
            {
                conexao.Open();
                MySqlCommand qry = new MySqlCommand(
                    "SELECT * FROM RESIDENCIA WHERE FK_CLIENTE_codigo = @cod", conexao);
                qry.Parameters.AddWithValue("@cod", idCliente);

                MySqlDataReader leitor = qry.ExecuteReader();

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
                conexao.Close();

                return residencias;

            }
            catch (Exception e)
            {
                throw new Exception("Erro ao buscar a última fatura.", e);
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
