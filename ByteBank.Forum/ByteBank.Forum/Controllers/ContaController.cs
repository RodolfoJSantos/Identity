using ByteBank.Forum.Models;
using ByteBank.Forum.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Host.SystemWeb;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.Mvc;
using System.Web;

namespace ByteBank.Forum.Controllers
{
	public class ContaController : Controller
	{
		private UserManager<UsuarioAplicacao> _userManager; //Backfield

		public UserManager<UsuarioAplicacao> UserManager
		{
			get
			{
				if (_userManager == null)
				{
					var contextoOwin = HttpContext.GetOwinContext();
				    _userManager = contextoOwin.GetUserManager<UserManager<UsuarioAplicacao>>();
				}
				return _userManager;
			}
			set
			{
				_userManager = value;
			}
		}
		public ActionResult Registrar()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Registrar(ContaRegistrarViewModel modelo)
		{
			if (ModelState.IsValid)
			{
				

				var novoUsuario = new UsuarioAplicacao();

				novoUsuario.UserName = modelo.UserName;
				novoUsuario.Email = modelo.Email;
				novoUsuario.NomeCompleto = modelo.NomeCompleto;

				UserManager.Create(novoUsuario, modelo.Senha);

				RedirectToAction("Index", "Home");
			}
			//algo errado devolvemos o modelo
			return View(modelo);
		}
	}
}