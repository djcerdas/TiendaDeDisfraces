using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using TiendaDeDisfraces.Models;

namespace TiendaDeDisfraces.Controllers{
public class ProductoController:Controller{
private readonly TiendaDeDisfracesContext _c;
public ProductoController(TiendaDeDisfracesContext c){_c=c;}
public IActionResult Index(){return View(_c.Productos.ToList());}
public IActionResult Create(){
 {
                ViewBag.TiposProducto = new List<SelectListItem>
    {
        new SelectListItem { Value = "1", Text = "Disfraz" },
        new SelectListItem { Value = "2", Text = "Accesorio" },
        new SelectListItem { Value = "3", Text = "Maquillaje" }
    };

                ViewBag.TiposPublico = new List<SelectListItem>
    {
        new SelectListItem { Value = "1", Text = "Adulto" },
        new SelectListItem { Value = "2", Text = "Niño" },
        new SelectListItem { Value = "3", Text = "Todo Público" }
    };


                return View();
 }
}
        [HttpPost]public IActionResult Create(Producto p){_c.Productos.Add(p);_c.SaveChanges();return RedirectToAction("Index");}
public IActionResult Edit(int id){
ViewBag.TiposProducto = new List<SelectListItem>
{
    new SelectListItem { Value = "1", Text = "Disfraz" },
    new SelectListItem { Value = "2", Text = "Accesorio" },
    new SelectListItem { Value = "3", Text = "Maquillaje" }
};

            ViewBag.TiposPublico = new List<SelectListItem>
{
    new SelectListItem { Value = "1", Text = "Adulto" },
    new SelectListItem { Value = "2", Text = "Niño" }
};
return View(_c.Productos.Find(id));}
[HttpPost]public IActionResult Edit(Producto p){_c.Update(p);_c.SaveChanges();return RedirectToAction("Index");}
public IActionResult Delete(int id){return View(_c.Productos.Find(id));}
[HttpPost,ActionName("Delete")]public IActionResult DeleteConfirmed(int id){var p=_c.Productos.Find(id);_c.Productos.Remove(p);_c.SaveChanges();return RedirectToAction("Index");}
}
}

