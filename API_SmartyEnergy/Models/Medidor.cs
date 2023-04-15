using MySql.Data.MySqlClient;

namespace API_SmartyEnergy.Models
{
    public class Medidor
    {

        //precisamos incluir o atribudo da chave estrangeira residência
        private int Codigo { get; set; }
        private double Consumo { get; set; }
        private string Registro_dia { get; set; }
        private string Registro_horario { get; set; }
        private string ConsumoDiario { get; set; }

        public Medidor(int codigo, double consumo, string registro_dia, string registro_horario)
        {
            Codigo = codigo;
            Consumo = consumo;
            Registro_dia = registro_dia;
            Registro_horario = registro_horario;
        }

        public Medidor(string consumoDiario)
        {
            this.ConsumoDiario = consumoDiario;
        }

        internal static object buscarConsumo(int id)
        {
            //string de conexão
            MySqlConnection conexao = new MySqlConnection("server=esn509vmysql ;database=db_smart_energy2 ;user id=aluno; password=Senai1234");

            try
            {

                conexao.Open();
                MySqlCommand qry = new MySqlCommand(
                    "SELECT * FROM MEDIDOR WHERE codigo = @cod", conexao);
                qry.Parameters.AddWithValue("@cod", id);

                

                MySqlDataReader leitor = qry.ExecuteReader();

                if (leitor.Read())
                {
                    /*criei variavéis apenas para organizar melhor, é possível colocar o leitor 
                     * dentro dos parâmetros do objeto Arduino*/

                    int codigo = int.Parse(leitor["codigo"].ToString());
                    double consumo = double.Parse(leitor["consumo"].ToString());
                    string registro_dia = (leitor["registro_dia"].ToString());
                    string registro_horario = (leitor["registro_horario"].ToString());

                    Medidor a = new Medidor(codigo, consumo, registro_dia, registro_horario);

                    conexao.Close();


                    return a;
                } else {
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

                    Medidor medidor = new Medidor(leitor["SUM(consumo)"].ToString());

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
