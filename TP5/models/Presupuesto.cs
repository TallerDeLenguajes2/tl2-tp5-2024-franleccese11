public class Presupuesto
{

    public Presupuesto()
    {
        listaDetalle = new List<PresupuestoDetalle>();
    }
    private int idPresupuesto;
    string nombreDestinatario;
    private List <PresupuestoDetalle> listaDetalle;

    private DateTime fecha;
    public int IdPresupuesto { get => idPresupuesto; set => idPresupuesto = value; }
    public string NombreDestinatario { get => nombreDestinatario; set => nombreDestinatario = value; }
    public DateTime Fecha { get => fecha; set => fecha = value; }

    public void agregarDetalle(PresupuestoDetalle detalle)
    {
        listaDetalle.Add(detalle);
    }

    public List<PresupuestoDetalle> ObtenerListaDetalle()
    {
        return listaDetalle;
    }

    public float MontoPresupuesto()
    {
        float monto=0;

        foreach (PresupuestoDetalle item in listaDetalle)
        {   
            monto = monto + item.Cantidad * item.obtenerProducto().Precio;
        }
        return monto;
    }

    public float MontoPresupuestoConIva()
    {
        float montoConIva=0 ;
        montoConIva = MontoPresupuesto() * 1.21f;
        return montoConIva;
    }

    public int cantidadProductos()
    {
        int cant = 0;
        foreach (PresupuestoDetalle item in listaDetalle)
        {   
            cant = item.Cantidad;
        }
        return cant;
    }

    
    
}