﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SmartEnergyAPI.Models {
    public class Cliente {
        static MySqlConnection conexao = new MySqlConnection("server=esn509vmysql ;database=db_smart_energy2 ;user id=aluno; password=Senai1234");

        private string nome, cpf, email, senha, telefone;
        private int codigo;

        [JsonConstructor]
        public Cliente(string nome, string cpf, string email, string telefone, string senha) {
            this.nome = nome;
            this.cpf = cpf;
            this.email = email;
            this.telefone = telefone;
            this.senha = senha;
        }
        public Cliente(string nome, string cpf, string email, string telefone, string senha, int codigo)
        {
            this.nome = nome;
            this.cpf = cpf;
            this.email = email;
            this.telefone = telefone;
            this.senha = senha;
            this.codigo = codigo;
        }
        public Cliente() { }

        public string Nome { get => nome; set => nome = value; }
        public string Cpf { get => cpf; set => cpf = value; }
        public string Email { get => email; set => email = value; }
        public string Telefone { get => telefone; set => telefone = value; }
        public string Senha { get => senha; set => senha = value; }
        public int Codigo { get => codigo; set => codigo = value; }
    }
}
