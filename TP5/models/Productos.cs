public class Producto
{
    private int idProducto;
    private string descripcion;
    private int precio;

    public int Precio { get => precio; set => precio = value; }
    public int IdProducto { get => idProducto; set => idProducto = value; }
    public string Descripcion { get => descripcion; set => descripcion = value; }
}