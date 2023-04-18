using MySql.Data.MySqlClient;
using SmartAPI.Models;

namespace API_SmartyEnergy.Models
{
    public class CompanhiaEletrica
    {
        private double TarifaTUSD { get; set; }
        private double TarifaTE { get; set; }
        private double pis { get; set; }
        private double cofins { get; set; }
        private double icms { get; set; }

        public CompanhiaEletrica(double tarifaTUSD, double tarifaTE, double pis, double cofins, double icms)
        {
            TarifaTUSD = tarifaTUSD;
            TarifaTE = tarifaTE;
            this.pis = pis;
            this.cofins = cofins;
            this.icms = icms;
        }

        internal static CompanhiaEletrica buscar(int id)
        {
            MySqlConnection conexao = new MySqlConnection("server=esn509vmysql; database=db_smart_energy2; user id=aluno; password=Senai1234");
            try
            {

                conexao.Open();

                MySqlCommand qry = new MySqlCommand(
                   "Select * from companhia WHERE FK_RESIDENCIA_codigo = @cod", conexao);

                qry.Parameters.AddWithValue("@cod", id);

                MySqlDataReader leitor = qry.ExecuteReader();

                if (leitor.Read())
                {
                    return new CompanhiaEletrica(double.Parse(leitor["tarifa_tusd"].ToString()),
                                                double.Parse(leitor["tarifa_tusd"].ToString()),
                                                double.Parse(leitor["pis"].ToString()),
                                                double.Parse(leitor["cofins"].ToString()),
                                                double.Parse(leitor["icms"].ToString())
                                                );
                }
                else
                {
                    throw new Exception("Não foi encontrada nenhuma companhia para o código especificado.");
                }
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao buscar companhia.", e);
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
