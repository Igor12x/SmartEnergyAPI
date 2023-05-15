using MySql.Data.MySqlClient;
using System.Text.Json.Serialization;

namespace API_SmartyEnergy.Models
{
    public class Medidor
    {
        private double consumo;
        private int idResidencia;

        public double Consumo { get => consumo; set => consumo = value; }
        public int IdResidencia { get => idResidencia; set => idResidencia = value; }

        public Medidor(double consumo)
        {
            Consumo = consumo;
        }

        [JsonConstructor]
        public Medidor(double consumo, int idResidencia)
        {
            this.consumo = consumo;
            this.idResidencia = idResidencia;
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
        
        internal static Boolean GravarConsumo(Medidor medidorResidencia)
        {
            MySqlConnection conexao = new MySqlConnection("server=esn509vmysql ;database=smartenergy ;user id=aluno; password=Senai1234");

            try
            {
                conexao.Open();
                MySqlCommand qry = new MySqlCommand(
                    "INSERT INTO MEDIDOR(consumo, registro_dia, registro_horario, medicao_atual, FK_RESIDENCIA_codigo) VALUES (@consumo, CURDATE(), CURTIME(), 32552, @idResidencia);", conexao);
                qry.Parameters.AddWithValue("@consumo", medidorResidencia.consumo);
                qry.Parameters.AddWithValue("@idResidencia", medidorResidencia.idResidencia);

                MySqlDataReader leitor = qry.ExecuteReader();

                if (leitor.Read())
                { 
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao gravar consumo", e);
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
