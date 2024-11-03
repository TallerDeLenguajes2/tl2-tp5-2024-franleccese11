using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using TP5.Repositorio;
namespace TP5.Controllers;
[ApiController]
[Route("[controller]")]

public class ProductoController : ControllerBase
{
    private ProductoRepository prodRepository;

    public ProductoController()
    {
        prodRepository = new ProductoRepository();
    }

    [HttpPost("api/Producto")]
    public ActionResult CrearProducto([FromBody]Producto producto)
    {
        prodRepository.InsertProducto(producto);
        return Ok();
    }

    [HttpGet("api/Producto")]
    public ActionResult GetProductos()
    {
        return Ok(prodRepository.ListarProductos());
    }

    [HttpPut("api/producto/{id}")]
    public ActionResult ModificarProducto([FromBody] string nuevoNombre,int id)
    {
        if(!prodRepository.ValidarID(id))
        {
            return NotFound($"El producto con ID:{id} no existe");
        }
        Producto prod= prodRepository.ObtenerProducto(id);
        prod.Descripcion = nuevoNombre;
        prodRepository.UpdateProducto(prod);
        return Ok();
    }




}