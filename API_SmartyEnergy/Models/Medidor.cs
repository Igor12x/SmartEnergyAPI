using MySql.Data.MySqlClient;

namespace API_SmartyEnergy.Models
{
    public class Medidor
    {

        private double Consumo { get; set; }

        public Medidor(double consumo)
        {
            Consumo = consumo;
        }


        internal static object buscarConsumo(int id)
        {
            //string de conexão
            MySqlConnection conexao = new MySqlConnection("server=esn509vmysql ;database=db_smart_energy2 ;user id=aluno; password=Senai1234");

            try
            {

                conexao.Open();
                MySqlCommand qry = new MySqlCommand(
                    "SELECT SUM(consumo) FROM MEDIDOR WHERE FK_RESIDENCIA_codigo = @cod", conexao);
                qry.Parameters.AddWithValue("@cod", id);



                MySqlDataReader leitor = qry.ExecuteReader();

                if (leitor.Read())
                {
                    /*criei variavéis apenas para organizar melhor, é possível colocar o leitor 
                     * dentro dos parâmetros do objeto Arduino*/

                    double consumo = double.Parse(leitor["SUM(consumo)"].ToString());

                    Medidor a = new Medidor(consumo);

                    conexao.Close();


                    return a;
                }
                else
                {
                    throw new Exception("Não foi possível encontrar seu consumo");
                }
            }
            catch (Exception e)
            {

                /*precisamos alterar o método para retornar um object,
                 *pois retornando uma array de Arduino não é possível devolver uma mensagem de erro para ser identificado*/
                throw new Exception("Erro ao buscar o seu consumo", e);
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
        internal static object buscarConsumoDiario(int id)
        {
            //string de conexão
            MySqlConnection conexao = new MySqlConnection("server=esn509vmysql ;database=db_smart_energy2 ;user id=aluno; password=Senai1234");

            try
            {
                conexao.Open();
                MySqlCommand qry = new MySqlCommand(
                    "SELECT SUM(consumo) FROM MEDIDOR WHERE registro_dia = CURDATE() AND FK_RESIDENCIA_codigo = @cod", conexao);
                qry.Parameters.AddWithValue("@cod", id);



                MySqlDataReader leitor = qry.ExecuteReader();

                if (leitor.Read())
                {
                    /*criei variavéis apenas para organizar melhor, é possível colocar o leitor 
                     * dentro dos parâmetros do objeto Arduino*/

                    Medidor medidor = new Medidor(double.Parse(leitor["SUM(consumo)"].ToString()));

                    conexao.Close();

                    return medidor;

                }
                else
                {
                    return "Nenhum consumo encontrado";
                }



            }
            catch (Exception e)
            {

                /*precisamos alterar o método para retornar um object,
                 *pois retornando uma array de Arduino não é possível devolver uma mensagem de erro para ser identificado*/
                return e.Message;
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
