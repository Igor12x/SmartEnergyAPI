using MySql.Data.MySqlClient;

namespace SmartAPI.Models {
    public class Fatura {
        private string valorUltimaFatura;
        private string consumoUltimaFatura;
        public string ValorUltimaFatura{ get => valorUltimaFatura; set => valorUltimaFatura = value; }
        public string ConsumoUltimaFatura { get => consumoUltimaFatura; set => consumoUltimaFatura = value; }

        static MySqlConnection conexao = new MySqlConnection("server=esn509vmysql ;database=db_smart_energy2 ;user id=aluno; password=Senai1234");

        public Fatura(string valorUltimaFatura, string consumoUltimaFatura) {
            ValorUltimaFatura = valorUltimaFatura;
            ConsumoUltimaFatura = consumoUltimaFatura;
        }

        internal static Fatura buscarValorUltimaFatura(int id) {
            try {

                conexao.Open();
             
                MySqlCommand qry = new MySqlCommand(
                    $@"SELECT valor, (SELECT MAX(medicao_mes_atual) FROM fatura)-(SELECT MAX(medicao_mes_anterior) FROM fatura) AS consumoFaturaAltural
                    FROM fatura
                    WHERE CODIGO = (SELECT MAX(codigo) FROM fatura) and FK_RESIDENCIA_codigo = @cod;", conexao);

                qry.Parameters.AddWithValue("@cod", id);

                MySqlDataReader leitor = qry.ExecuteReader();

                if (leitor.Read()) {
                    string valorUltimaFatura = leitor["valor"].ToString();
                    string consumoUltimaFatura = leitor["consumoFaturaAltural"].ToString();

                    return new Fatura(valorUltimaFatura, consumoUltimaFatura);
                } else {
                    throw new Exception("Não foi encontrada nenhuma fatura para o código especificado.");
                }
            } catch (Exception e) {

                throw new Exception("Erro ao buscar a última fatura.", e);

            } finally {
                if(conexao != null) {
                    conexao.Close();
                }                
            }
        }
    }
}
