using MySql.Data.MySqlClient;
using System;
using System.ComponentModel.DataAnnotations;

namespace SmartAPI.Models {
    public class Fatura {
        [Required(ErrorMessage = "O campo Valor da Última Fatura é obrigatório.")]
        public string ValorUltimaFatura { get; set; }

        [Required(ErrorMessage = "O campo Consumo da Última Fatura é obrigatório.")]
        public string ConsumoUltimaFatura { get; set; }

        public Fatura(string valorUltimaFatura, string consumoUltimaFatura) {
            ValorUltimaFatura = valorUltimaFatura;
            ConsumoUltimaFatura = consumoUltimaFatura;
        }

        internal static Fatura BuscarValorUltimaFatura(int id) {
            using (MySqlConnection conexao = new MySqlConnection("server=esn509vmysql ;database=smartenergy;user id=aluno; password=Senai1234")) {
                try {
                    conexao.Open();

                    MySqlCommand qry = new MySqlCommand(
                        $@"SELECT valor, (SELECT MAX(medicao_mes_atual) FROM fatura)-(SELECT MAX(medicao_mes_anterior) FROM fatura) AS consumoFaturaAltural
                   FROM fatura
                   WHERE CODIGO = (SELECT MAX(codigo) FROM fatura) and FK_RESIDENCIA_codigo = @cod;", conexao);

                    qry.Parameters.AddWithValue("@cod", id);

                    using (MySqlDataReader leitor = qry.ExecuteReader()) {
                        if (leitor.Read()) {
                            string valorUltimaFatura = leitor["valor"].ToString();
                            string consumoUltimaFatura = leitor["consumoFaturaAltural"].ToString();

                            return new Fatura(valorUltimaFatura, consumoUltimaFatura);
                        } else {
                            throw new Exception("Não foi encontrada nenhuma fatura para o código de residência especificado.");
                        }
                    }
                } catch (Exception e) {
                    throw new Exception("Erro ao buscar a última fatura: " + e.Message);
                }
            }
        }

    }
}
