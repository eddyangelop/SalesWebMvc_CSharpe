using System;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;
using SalesWebMvc.Services.Exceptions;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SalesWebMvc.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly ISellerService _ISellerService;
        private readonly DepartmentService _departmentService;
        private readonly IDepartmentService _departmentServiceAdo;

        public SellersController(SellerService sellerService, ISellerService IsellerService, DepartmentService departmentService, IDepartmentService departmentServiceAdo)
        {
            _sellerService = sellerService;
            _ISellerService = IsellerService;
            _departmentService = departmentService;
            _departmentServiceAdo = departmentServiceAdo;

        }


        // GET: Sellers
        public IActionResult Index()
        {
            var result = _ISellerService.GetSellers();
            return View(result);
        }



        // GET: Sellers/Create
        public IActionResult Create()
        {
            // lembra que eu te disse, que tu precisa passar algo dentro do construtor da classe ? não ta chegando nada lá 
            /* isso aqui antes era assim
              var departments = await _departmentService.FindAllAsync(); aqui é uma lista de departamentos, que vai entrar aqui dentro
            var ViewModel = new SellerFormViewModel { Departments = departments };
             */
            // agora deu outro erro, porque a lista ta vazia
            // isso daqui tu vai substituir pelo nosso getDepartments()
            //var departments = new List<Department>() { new Department() { Id = 1, Name = "teste" } };

            // nesse caso ele esperava um objeto do tipo SellerFormViewModel que dentro tem uma lista de departments

            // 2. tu sempre tem que se atentar qual é o tipo de objeto que ta esperando lá na classe
            //var ViewModel = new SellerFormViewModel { Departments = departments };

            // posso mandar vazia, pois existe um construtor vazio
            // se eu coloco apenas uma virgula repara que aparece todas as opções se eu apertar nas setinhas ou pra cima
            // ai entendendo isso, perceba que tua view foi informada que ela precisa receber uma classe
            // mas como eu mudei lá agora eu poderia passar um department se eu quiser, entendeu ? mais ou menos
            var viewModel = new SellerFormViewModel { Departments = _departmentServiceAdo.GetDepartments() };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, Name, Email, BirthDate, BaseSalary")] Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var result =  _ISellerService.CreateSeller(seller);

                var mensagem = "ADICIONADO VENDEDOR" + seller.Name;
                TempData["Mensagem"] = mensagem;

                return RedirectToAction(nameof(Index));
                
            }
            return View(seller);

        }



        // GET: Sellers/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            List<Department> departments = await _departmentService.FindAllAsync();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }
            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }
            try
            {
                await _sellerService.UpdateAsync(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }



        // GET: Sellers/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _sellerService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

   

        // GET: Sellers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(obj);
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }
    }
}
