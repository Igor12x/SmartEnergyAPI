using Microsoft.VisualStudio.Services.WebApi.Jwt;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartEnergyAPI.Models {
    public class Login {
        static MySqlConnection conexao = new MySqlConnection("server=esn509vmysql; database=db_smart_energy2; user id=aluno; password=Senai1234");

        private string cpf;
        private string senha;

        public string Cpf { get => cpf; set => cpf = value; }
        public string Senha { get => senha; set => senha = value; }

        public Login(string cpf, string senha) {
            Cpf = cpf;
            Senha = senha;
        }

        internal Cliente validarLogin(Login login) {
            try {
                conexao.Open();
            } catch (MySqlException ex) {
                throw new Exception("Erro ao abrir conexão com o banco de dados", ex);
            }

            try {
                MySqlCommand qry = new MySqlCommand(
                    "SELECT * FROM cliente WHERE cpf = @cpf and senha = @senha", conexao);
                qry.Parameters.AddWithValue("@cpf", login.Cpf);
                qry.Parameters.AddWithValue("@senha", login.Senha);

                MySqlDataReader leitor = qry.ExecuteReader();

                if (leitor.Read()) {
                    Cliente cliente = new Cliente(
                        leitor["nome"].ToString(),
                        leitor["cpf"].ToString(),
                        leitor["email"].ToString(),
                        leitor["telefone"].ToString());
                    return cliente;
                } else {
                    throw new InvalidCredentialsException("As credenciais fornecidas são inválidas. Verifique se o CPF e a senha estão corretos.");
                }
            } catch (MySqlException ex) {
                throw new Exception("Erro ao executar consulta no banco de dados", ex);
            } finally {
                try {
                    if (conexao.State == System.Data.ConnectionState.Open) {
                        conexao.Close();
                    }
                } catch (MySqlException ex) {
                    throw new Exception("Erro ao fechar conexão com o banco de dados", ex);
                }
            }
        }


    }
}
