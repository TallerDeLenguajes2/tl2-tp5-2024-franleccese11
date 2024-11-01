using TP5.Repositorio; 


public class PresupuestoDetalle
{
    private Producto producto;
    private int cantidad;

    
    public int Cantidad { get => cantidad; set => cantidad = value; }

    public void AsignarProducto(int id)
    {
        var repositorioProductos = new ProductoRepository();
        producto = repositorioProductos.ObtenerProducto(id);
        
    }

    public Producto obtenerProducto()
    {
        return producto;
    }
    
}   