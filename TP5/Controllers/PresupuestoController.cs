using System.ComponentModel;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using TP5.Repositorio;
namespace TP5.Controllers;
[ApiController]
[Route("[controller]")]

public class PresupuestoController: ControllerBase
{
    private PresupuestoRepository presupuestoRepo;

    public PresupuestoController()
    {
        presupuestoRepo = new PresupuestoRepository();
    }

    [HttpPost("api/Presupuesto")]
    public ActionResult CrearPresupuesto([FromBody] Presupuesto presupuesto)
    {
        
        presupuestoRepo.CrearPresupuesto(presupuesto);
        return Ok();
    }

    [HttpPost("/api/Presupuesto/{id}/ProductoDetalle")]
    public ActionResult AgregarDetallePresupuest([FromBody] PresupuestoDetalle detalle,int id)
    {
        presupuestoRepo.AgregarDetalle(detalle,id);
        return Ok();
    }
    
    [HttpGet("/api/presupuesto")]
    public ActionResult GetPresupuesto()
    {
        return Ok(presupuestoRepo.ListarPresupuestos());
    }

}