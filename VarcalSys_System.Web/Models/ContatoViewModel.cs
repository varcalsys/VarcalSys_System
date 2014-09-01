using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VarcalSys_System.Domain;

namespace VarcalSys_System.Web.Models
{
    public class ContatoViewModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = " * Campo obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = " * Campo obrigatório")]
        [RegularExpression(@"^([\w\-]+\.)*[\w\- ]+@([\w\- ]+\.)+([\w\-]{2,3})$", ErrorMessage = " * Email inválido")]
        public string Email { get; set; }

        public string Telefone { get; set; }

        [Required(ErrorMessage = " * Campo obrigatório")]
        public string Assunto { get; set; }

        [Required(ErrorMessage = " * Campo obrigatório")]
        public string Comentario { get; set; }

        public ContatoViewModel()
        {

        }

        public ContatoViewModel(Contato entity)
        {
            Nome = entity.Nome;
            Email = entity.Email;
            Telefone = entity.Telefone;
            Assunto = entity.Assunto;
            Comentario = entity.Comentario;
        }
    }
}