using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using VarcalSys_System.Util;
using VarcalSys_System.Util.Interfaces;

namespace VarcalSys_System.Domain.Repositories
{
    public class ContatoRepository:Contexto, IRepository<Contato>
    {
        public string Save(Contato entity)
        {
            var retorno = "";
            if (entity.Id <= 0)
            {
                retorno = Insert(entity);
            }
            if (entity.Id > 0)
            {
                retorno =UpDate(entity);
            }
            return retorno;
        }

        private string Insert(Contato entity)
        {
            try
            {
                CleanParameter();
                AddParameter("@Nome",entity.Nome);
                AddParameter("@Email", entity.Email);
                AddParameter("@Assunto",entity.Assunto);
                AddParameter("@Telefone",entity.Telefone);
                AddParameter("@Comentario",entity.Comentario);
                return ExecCommand(CommandType.StoredProcedure, "uspContatoInserir").ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private string UpDate(Contato entity)
        {
            try
            {
                CleanParameter();
                AddParameter("@Id",entity.Id);
                AddParameter("@Nome", entity.Nome);
                AddParameter("@Email", entity.Email);
                AddParameter("@Assunto", entity.Assunto);
                AddParameter("@Telefone", entity.Telefone);
                AddParameter("@Comentario", entity.Comentario);
                return ExecCommand(CommandType.StoredProcedure, "uspContatoInserir").ToString();
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string Delete(Contato entity)
        {
            
            try
            {
                CleanParameter();
                return ExecCommand(CommandType.Text, "Delete from tblcontato where Id = " + entity.Id).ToString();
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }

        public Contato Find(int id)
        {
            return FindAll().FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Contato> FindAll()
        {
            try
            {
                var dtContato = ExecQuery(CommandType.StoredProcedure, "uspContatoListarTodos");
                var contatos = new List<Contato>();
                foreach (DataRow row in dtContato.Rows)
                {
                    var contato = new Contato();
                    contato.Id = Convert.ToInt32(row["Id"]);
                    contato.Nome = row["Nome"].ToString();
                    contato.Email = row["Email"].ToString();
                    contato.Telefone = row["Telefone"].ToString();
                    contato.Assunto = row["Assunto"].ToString();
                    contato.Comentario = row["Comentario"].ToString();
                    contatos.Add(contato);
                }
                return contatos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
