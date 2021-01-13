using System;
using System.Collections.Generic;
using System.IO;
using E_Players_AspNETCore.Interfaces;

namespace E_Players_AspNETCore.Models
{
    public class Equipe : EplayersBase , IEquipe
    {
        public int IdEquipe { get; set; }
        public string Nome { get; set; }
        public string Imagem { get; set; }

        private const string PATH = "Database/equipe.csv";

        public Equipe()
        {
            CreateFolderAndFile(PATH);
        }

        public void Create(Equipe e)
        {
            // Preparamos um array de string para o metodo AppendAllLines
            string[] linha = { Prepare(e) };
            // Acrescentamos nova linha 
            File.AppendAllLines(PATH, linha);
        }
        // Criamos o método para preparar a linha do CSV
        private string Prepare(Equipe e)
        {
            return $"{e.IdEquipe};{e.Nome};{e.Imagem}";
        }

        public void Delete(int id)
        {
            List<string> linhas = ReadAllLinesCSV(PATH);
            // 2;SNK;snk.jpg
            // Removemos as linhas com o codigo comparado
            // To String -> converte para texto (string)
            linhas.RemoveAll(x => x.Split(";")[0] == id.ToString());
            //Reescrevemos o CSV com a lista alterada
            RewriteCSV(PATH, linhas);
        }

        public List<Equipe> ReadAll()
        {
            List<Equipe> equipes = new List<Equipe>();

            // Lemos todos as linhas do CSV
            string[] linhas = File.ReadAllLines(PATH);

            foreach (string item in linhas)
            {
                // 1;Vivokeyd;vivo.jpg
                // [0] = 1
                // [1] = vivoKeud
                // [2] = vivo.jpg

                string[] linha = item.Split(";");
                Equipe novaEquipe = new Equipe();
                novaEquipe.IdEquipe = Int32.Parse(linha[0]);
                novaEquipe.Nome   = linha[1];
                novaEquipe.Imagem = linha[2];

                equipes.Add(novaEquipe);
            }
            return equipes;
        }

        public void Update(Equipe e)
        {
            List<string> linhas = ReadAllLinesCSV(PATH);
            // 2;SNK;snk.jpg
            // Removemos as linhas com o codigo comparado
            linhas.RemoveAll(x => x.Split(";")[0] == e.IdEquipe.ToString());
            // Add nas linhas a equipe alterada
            linhas.Add( Prepare(e) );
            //Reescrevemos o CSV com a lista alterada
            RewriteCSV(PATH, linhas);
        }
    }
}