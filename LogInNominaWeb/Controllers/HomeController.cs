using LogInNominaWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LogInNominaWeb.Controllers
{
    public class HomeController : Controller
    {        
        public ActionResult Index()
        {
            return View();
        }      
              
        public ActionResult AltaCuenta()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AltaCuenta(IFormCollection usuario)
        {
            Cuenta cuenta = new Cuenta()
            {
                nombre = usuario["name"],
                clave = usuario["pass"],
            };
            CuentaABM mant = new CuentaABM();
            mant.Alta(cuenta);
            return RedirectToAction("Index");
        }        
        public ActionResult Inicio(IFormCollection login)
        {
            Cuenta cuenta = new Cuenta();
            CuentaABM mant = new CuentaABM();
            cuenta = mant.Leer(login["name"]);
            if (cuenta == null)
            {
                return RedirectToAction("Error1");
            }
            else
            {
                if (cuenta.clave != login["pass"])
                {
                    return RedirectToAction("Error2");

                }
                else
                {
                    return View(cuenta);
                }
            }
        }
        public ActionResult Error1()
        {
            return View();
        }
        public ActionResult Error2()
        {
            return View();
        }
        public ActionResult AltaRegistro(string usuario)
        {
            Cuenta cuenta = new Cuenta()
            {
                nombre = usuario,
                clave = null
            };
            return View(cuenta);
        }
        [HttpPost]
        public ActionResult AltaRegistroOK (IFormCollection registro)
        {
            NominaABM nomina = new NominaABM();
            Empleado pers = new Empleado
            {
                dni = registro["dni"],
                nombre = registro["nombre"],
                domicilio = registro["domicilio"],
                fechanac = DateTime.Parse(registro["fechanac"]),
                fechaing = DateTime.Parse(registro["fechaing"]),
                puesto = registro["puesto"],
                salario = int.Parse(registro["salario"]),
                usuario = registro["usuario"]
            };            
            nomina.Alta(pers);
            return View(pers);
        }        
        public ActionResult Leer(string usuario)
        {
            NominaABM nomina = new NominaABM();
            List<Empleado> lista = nomina.LeerTodo(usuario);            
            if (lista.Count() == 0)
            {                               
                Empleado pers = new Empleado()
                {
                    usuario = usuario,
                    dni = null                    
                };
                lista.Add(pers);
            }            
            return View(lista);
        }        
        public ActionResult Modificacion (string usuario, string dni)
        {
            NominaABM nomina = new NominaABM();
            Empleado pers = new Empleado();
            pers = nomina.Leer(dni, usuario);
            return View(pers);
        }
        public ActionResult ModificacionOK (IFormCollection registro)
        {
            NominaABM nomina = new NominaABM();
            Empleado pers = new Empleado
            {
                dni = registro["dni"],
                nombre = registro["nombre"],
                domicilio = registro["domicilio"],
                fechanac = DateTime.Parse(registro["fechanac"]),
                fechaing = DateTime.Parse(registro["fechaing"]),
                puesto = registro["puesto"],
                salario = int.Parse(registro["salario"]),
                usuario = registro["usuario"]
            };
            nomina.Modificar(pers);
            return View(pers);
        }
        
        public ActionResult Baja (string usuario, string dni)
        {
            NominaABM nomina = new NominaABM();
            Empleado pers = new Empleado();
            pers = nomina.Leer(dni, usuario);
            nomina.Borrar(usuario,dni);            
            return View(pers);
        }       
    }
}