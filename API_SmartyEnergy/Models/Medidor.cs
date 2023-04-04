using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace API_SmartyEnergy.Models
{
    public class Medidor
    {
        //string de conexão
        static MySqlConnection conexao = new MySqlConnection("server=localhost ;database=db_smart_energy ;user id=root; password=9133");

        //precisamos incluir o atribudo da chave estrangeira residência
        public int codigo;
        public double consumo;
        public string registro_dia; //espera receber apenas a data
        public string registro_horario; //aqui apenas o horário

        public Medidor(int codigo, double consumo, string registro_dia, string registro_horario)
        {
            this.codigo = codigo;
            this.consumo = consumo;
            this.registro_dia = registro_dia;
            this.registro_horario = registro_horario;
        }   

        internal static object buscarConsumo(int id)
        {
            try
            {
                conexao.Open();
                MySqlCommand qry = new MySqlCommand(
                    "SELECT * FROM MEDIDOR WHERE codigo = @cod", conexao);
                qry.Parameters.AddWithValue("@cod", id);

                List<Medidor> lista = new List<Medidor>();

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

                    lista.Add(a);
                } // fim do if

                conexao.Close();

                return lista;

            }
            catch (Exception e)
            {

                /*precisamos alterar o método para retornar um object,
                 *pois retornando uma array de Arduino não é possível devolver uma mensagem de erro para ser identificado*/
                return e.Message;
            }
        }
    }
}