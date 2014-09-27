using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VarcalSys_System.Domain;
using VarcalSys_System.Domain.Interfaces;
using VarcalSys_System.Domain.Services;
using VarcalSys_System.Web.Models;


namespace VarcalSys_System.Web.Controllers
{
    public class ContatoController : Controller
    {
        private readonly IContatoService _contatoService = new ContatoService();


        public ActionResult Index()
        {
            var contatos = _contatoService.FindAll();
            if (!contatos.Any())
            {
                TempData["info"] = "Nenhuma mensagem recebida";
            }

            return View(contatos);
        }


        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Create(ContatoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["errors"] = "Não foi possivel enviar a mensagem";
                return RedirectToAction("Create");
            }

            try
            {
                var contato = new Contato();
                contato.Nome = model.Nome;
                contato.Email = model.Email;
                contato.Telefone = string.IsNullOrEmpty(contato.Telefone) ? "" : model.Telefone.Replace("(", "").Replace(")", "").Replace("-", "");
                contato.Assunto = model.Assunto;
                contato.Comentario = model.Comentario;


                var id = _contatoService.Save(contato);

                TempData["info"] = "Mensagem enviada com sucesso";
                return RedirectToAction("Create", "Contato");
            }
            catch (Exception)
            {
                TempData["errors"] = "Não foi possivel enviar sua mensagem";
                return RedirectToAction("Create", "Contato");
            }
            
        }

        public ActionResult Details(int id)
        {
            //if (!id.HasValue)
            //{
            //    TempData["errors"] = "Ocorreu um erro ao acessar as informações";
            //    return RedirectToAction("Index");
            //}

            var contato = _contatoService.Find(id);

            if (contato == null)
            {
                TempData["errors"] = "Ocorreu um erro ao acessar as informações";
                return RedirectToAction("Index");
            }

            return View(new ContatoViewModel(contato));
        }

        public ActionResult Delete(int id)
        {
            var contato = _contatoService.Find(id);
            if (contato == null)
            {
                TempData["errors"] = "Ocorreu um erro ao tentar excluir o contato";
                return RedirectToAction("Index");
            }

            _contatoService.Delete(contato);
            TempData["info"] = "Contato excluído com sucesso";
            return RedirectToAction("Index");
        }
    }
}
