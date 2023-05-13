using MySql.Data.MySqlClient;

namespace API_SmartyEnergy.Models
{
    public class Medidor
    {
        private double consumo;

        public double Consumo { get => consumo; set => consumo = value; }

        public Medidor(double consumo)
        {
            Consumo = consumo;
        }
        internal static Medidor buscarConsumo(int id)
        {
            MySqlConnection conexao = new MySqlConnection("server=esn509vmysql ;database=smartenergy ;user id=aluno; password=Senai1234");

            try
            {
                conexao.Open();
                MySqlCommand qry = new MySqlCommand(
                    "SELECT SUM(consumo) FROM medidor WHERE FK_RESIDENCIA_codigo = @cod AND MONTH(registro_dia) = MONTH(now())", conexao);
                qry.Parameters.AddWithValue("@cod", id);

                MySqlDataReader leitor = qry.ExecuteReader();

                if (leitor.Read())
                {
                    double consumo = double.Parse(leitor["SUM(consumo)"].ToString());

                    return new Medidor(consumo);
                }
                else
                {
                    throw new Exception("Não foi possível encontrar seu consumo");
                }
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao buscar o seu consumo", e);
            }
            finally
            {
                if (conexao != null)
                {
                    conexao.Close();
                }
            }
        }
        internal static object buscarConsumoDiario(int id)
        {
            MySqlConnection conexao = new MySqlConnection("server=esn509vmysql ;database=smartenergy ;user id=aluno; password=Senai1234");

            try
            {
                conexao.Open();
                MySqlCommand qry = new MySqlCommand(
                    "SELECT SUM(consumo) FROM MEDIDOR WHERE registro_dia = CURDATE() AND FK_RESIDENCIA_codigo = @cod", conexao);
                qry.Parameters.AddWithValue("@cod", id);



                MySqlDataReader leitor = qry.ExecuteReader();

                if (leitor.Read())
                {
                    Medidor medidor = new Medidor(double.Parse(leitor["SUM(consumo)"].ToString()));

                    return medidor;
                }
                else
                {
                    return "Nenhum consumo encontrado";
                }
            }
            catch (Exception e)
            {
                return e.Message;
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
