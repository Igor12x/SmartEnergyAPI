using MySql.Data.MySqlClient;
using System;
using System.ComponentModel.DataAnnotations;

namespace API_SmartyEnergy.Models {
    public class CompanhiaEletrica {
        [Required(ErrorMessage = "O campo TarifaTUSD é obrigatório.")]
        public double TarifaTUSD { get; set; }

        [Required(ErrorMessage = "O campo TarifaTE é obrigatório.")]
        public double TarifaTE { get; set; }

        [Required(ErrorMessage = "O campo Pis é obrigatório.")]
        public double Pis { get; set; }

        [Required(ErrorMessage = "O campo Cofins é obrigatório.")]
        public double Cofins { get; set; }

        [Required(ErrorMessage = "O campo ICMS é obrigatório.")]
        public double Icms { get; set; }

        public CompanhiaEletrica(double tarifaTUSD, double tarifaTE, double pis, double cofins, double icms) {
            TarifaTUSD = tarifaTUSD;
            TarifaTE = tarifaTE;
            Pis = pis;
            Cofins = cofins;
            Icms = icms;
        }

        internal static CompanhiaEletrica Buscar(int id) {
            string connectionString = "server=esn509vmysql; database=smartenergy; user id=aluno; password=Senai1234";

            using (MySqlConnection conexao = new MySqlConnection(connectionString)) {
                try {
                    conexao.Open();

                    MySqlCommand qry = new MySqlCommand(
                        "SELECT * FROM companhia c INNER JOIN residencia r ON c.codigo = r.FK_COMPANHIA_codigo WHERE r.codigo = @cod", conexao);

                    qry.Parameters.AddWithValue("@cod", id);

                    using (MySqlDataReader leitor = qry.ExecuteReader()) {
                        if (leitor.Read()) {
                            return new CompanhiaEletrica(
                                double.Parse(leitor["tarifa_TUSD"].ToString()),
                                double.Parse(leitor["tarifa_TE"].ToString()),
                                double.Parse(leitor["pis"].ToString()),
                                double.Parse(leitor["cofins"].ToString()),
                                double.Parse(leitor["icms"].ToString())
                            );
                        } else {
                            throw new Exception("Não foi encontrada nenhuma companhia para o código especificado.");
                        }
                    }
                } catch (Exception e) {
                    throw new Exception("Erro ao buscar companhia.", e);
                }
            }
        }
    }
}
