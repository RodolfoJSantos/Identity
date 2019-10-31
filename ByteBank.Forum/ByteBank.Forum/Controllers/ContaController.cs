using ByteBank.Forum.Models;
using ByteBank.Forum.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.Mvc;

namespace ByteBank.Forum.Controllers
{
	public class ContaController : Controller
	{
		public ActionResult Registrar()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Registrar(ContaRegistrarViewModel modelo)
		{
			if (ModelState.IsValid)
			{
				var dbcontext = new IdentityDbContext<UsuarioAplicacao>("DefaultConnection");
				var userStore = new UserStore<UsuarioAplicacao>(dbcontext);
				var userManager = new UserManager<UsuarioAplicacao>(userStore);

				var novoUsuario = new UsuarioAplicacao();

				novoUsuario.UserName = modelo.UserName;
				novoUsuario.Email = modelo.Email;
				novoUsuario.NomeCompleto = modelo.NomeCompleto;

				userManager.Create(novoUsuario, modelo.Senha);

				RedirectToAction("Index", "Home");
			}
			//algo errado devolvemos o modelo
			return View(modelo);
		}
	}
}