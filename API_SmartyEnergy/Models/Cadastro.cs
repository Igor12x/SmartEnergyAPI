    using MySql.Data.MySqlClient;
using SmartEnergyAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_SmartEnergy.Models
{
    public class Cadastro
    {
        static MySqlConnection conexao = new MySqlConnection("server=esn509vmysql; database=smartenergy; user id=aluno; password=Senai1234");

        internal static String Cadastrar(Cliente cliente) {
            try {
                conexao.Open();
               
                MySqlCommand qry = new MySqlCommand(
                    "INSERT INTO CLIENTE (nome, sobrenome, cpf, senha, email, telefone) values(@nome, @sobrenome, @cpf, @senha, @email, @telefone)", conexao);
                qry.Parameters.AddWithValue("@nome", cliente.Nome);
                qry.Parameters.AddWithValue("@sobrenome", cliente.Sobrenome);
                qry.Parameters.AddWithValue("@cpf", cliente.Cpf);
                qry.Parameters.AddWithValue("@senha", cliente.Senha);
                qry.Parameters.AddWithValue("@email", cliente.Email);
                qry.Parameters.AddWithValue("@telefone", cliente.Telefone);

                int linhasAfetadas = qry.ExecuteNonQuery();
                if (linhasAfetadas > 0)
                {
                    return "Cadastrado com sucesso";
                }
                else { return "Não foi possível cadastrar"; }
                
            } catch (Exception e) {

                /*precisamos alterar o método para retornar um object,
                 *pois retornando uma array de Arduino não é possível devolver uma mensagem de erro para ser identificado*/

                if (conexao.State == System.Data.ConnectionState.Open)
                    conexao.Close();
                throw new Exception("Erro ao cadastrar o cliente", e);
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

