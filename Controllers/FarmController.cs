using System.Security.Claims;
using doan_ttcn.Data;
using doan_ttcn.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace doan_ttcn.Controllers;
public class FarmController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult LoadFarm(string farm)
    {
        return farm switch
        {

            "caudat" => PartialView("CauDatfarm"),
            "truongphuc" => PartialView("TPfarm"),
            "traimat" => PartialView("TraiMatfarm"),
            "lartis" => PartialView("Lartisfarm"),
            _ => PartialView("CauDatfarm")
        };
    }
}
