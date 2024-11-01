namespace TP5.Repositorio
{
    public interface IProductoRepository
    {
        public bool ValidarID(int id);
        public List <Producto> ListarProductos();
        public void InsertProducto(Producto producto);
        public Producto ObtenerProducto(int id);
        public void EliminarProducto(int id);
        
    }
}