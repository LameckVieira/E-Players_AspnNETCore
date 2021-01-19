using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using E_Players_AspNETCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Players_AspNETCore.Controllers
{
    [Route("Jogador")]
    public class JogadorController : Controller
    {

        Jogador jogadorModel = new Jogador();

        Equipe equipeModel = new Equipe();

        public IActionResult Index()
        {
            ViewBag.Equipes   = equipeModel.ReadAll();
            ViewBag.Jogadores = jogadorModel.ReadAll();
            return View();
        }

        [Route("Cadastrar")]
        public IActionResult Cadastrar(IFormCollection form)
        {
            Jogador novoJogador     = new Jogador();
            novoJogador.IdJogador   = Int32.Parse(form["IdJogador"]);
            novoJogador.IdEquipe    = Int32.Parse(form["IdEquipe"]);
            novoJogador.Nome        = form["Nome"];
            novoJogador.Email       = form["Email"];
            novoJogador.Senha       = form["Senha"];

            jogadorModel.Create(novoJogador);            
            ViewBag.Jogadores = jogadorModel.ReadAll();

            return LocalRedirect("~/Jogador");
        }
        
        [Route("jogador/{id}")]
        public IActionResult Excluir(int id)
        {
            jogadorModel.Delete(id);
            return LocalRedirect("~/Jogador");
        }
    }
}