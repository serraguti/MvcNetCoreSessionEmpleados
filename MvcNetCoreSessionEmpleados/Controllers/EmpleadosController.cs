using Microsoft.AspNetCore.Mvc;
using MvcNetCoreSessionEmpleados.Extensions;
using MvcNetCoreSessionEmpleados.Models;
using MvcNetCoreSessionEmpleados.Repositories;

namespace MvcNetCoreSessionEmpleados.Controllers
{
    public class EmpleadosController : Controller
    {
        private RepositoryEmpleados repo;

        public EmpleadosController(RepositoryEmpleados repo)
        {
            this.repo = repo;
        }

        public async Task<IActionResult> SessionSalarios(int? salario)
        {
            if (salario != null)
            {
                //NECESITAMOS ALMACENAR EL SALARIO DEL EMPLEADO 
                //Y LA SUMA TOTAL DE SALARIOS QUE TENGAMOS
                int sumaSalarial = 0;
                //PREGUNTAMOS SI YA TENEMOS LA SUMA ALMACENADA EN SESSION
                if (HttpContext.Session.GetString("SUMASALARIAL") != null)
                {
                    //SI YA EXISTE LA SUMA SALARIAL, RECUPERAMOS 
                    //SU VALOR
                    sumaSalarial =
                        HttpContext.Session.GetObject<int>("SUMASALARIAL");
                }
                //REALIZAMOS LA SUMA
                sumaSalarial += salario.Value;
                //ALMACENAMOS EL NUEVO VALOR DE LA SUMA SALARIAL
                //DENTRO DE SESSION
                HttpContext.Session.SetObject("SUMASALARIAL", sumaSalarial);
                ViewData["MENSAJE"] = "Salario almacenado: " + salario.Value;
            }
            List<Empleado> empleados =
                await this.repo.GetEmpleadosAsync();
            return View(empleados);
        }

        public IActionResult SumaSalarial()
        {
            return View();
        }
    }
}
