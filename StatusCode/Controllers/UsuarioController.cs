using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StatusCode.Models;

namespace StatusCode.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private SistemaContext DbSistema = new SistemaContext();

        [HttpGet]
        public ActionResult<List<Usuario>> RequererTodos()
        {
            return Ok(DbSistema); 
        }

        [HttpGet]
        public ActionResult<Usuario> RequererUmPelaId(int Id)
        {
            if(Id == null)
            {
                return NotFound();
            }
            else
            {
               return Ok(DbSistema);
            }
        }

        [HttpPost]
        public ActionResult<Usuario> PublicarUm(Usuario Usuario)
        {
            var User = DbSistema.Usuario.Find(Usuario.Cpf);
            if (User == null)
            {
                DbSistema.Usuario.Add(Usuario);
                return Created("",Usuario);
            }
            else
            {
                return Conflict(string.Format("cpf ja existe {0}", Usuario.Id));

            }
        
        }

        [HttpDelete]
        public ActionResult<Usuario> DeletarUmPelaId(int Id, Usuario Usuario)
        {
            var identidade = DbSistema.Usuario.Find(Id);
            if(identidade == null)
            {
                return NotFound();
            }
            else{
                return Unauthorized(Usuario);
            }
        }

        [HttpPut]
        public ActionResult<Usuario> SubstituirUmPelaId(int Id, Usuario Usuario)
        {
            var user = DbSistema.Usuario.Find(Usuario.Id);
            var cpf = DbSistema.Usuario.Find(Usuario.Cpf);

            if (Id != null)
            {
                return Ok(DbSistema);
            }
            else if(user == null){
                return NotFound();
            }
            else if(cpf != null)
            {
                return Conflict();
            }
            else
            {
                return BadRequest();
            }

        }
    }
}
