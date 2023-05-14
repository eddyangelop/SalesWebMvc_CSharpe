using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite.Internal.UrlMatches;

namespace SalesWebMvc.Controllers
{
    public class TesteController : Controller
    {
        public TesteController()
        {
            
        }

        public IActionResult Index()
        {
            string nome = SeuNome("Eddy", "Angelo");

            TempData["Nome"] = nome;
            TempData["MensagemBoasVindas"] = "Bem vindo a pagina de testee";


            return View();
        }

        public IActionResult NomeCompleto(string nome, string sobreNome)
        {
            TempData["NomeCompleto"] = nome + " " + sobreNome;

            return View();
        }

        public string SeuNome(string nome, string sobreNome)
        {
            string nomeCompleto = nome + " " + sobreNome;

            return nomeCompleto;
        }
    }
}
