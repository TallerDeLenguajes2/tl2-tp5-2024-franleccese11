public class Presupuesto
{

    public Presupuesto()
    {
        detalle = new List<PresupuestoDetalle>();
    }
    private int idPresupuesto;
    string nombreDestinatario;
    private List <PresupuestoDetalle> detalle;
    public int IdPresupuesto { get => idPresupuesto; set => idPresupuesto = value; }
    public string NombreDestinatario { get => nombreDestinatario; set => nombreDestinatario = value; }

    public float MontoPresupuesto()
    {
        float monto=0;

        foreach (PresupuestoDetalle item in detalle)
        {   
            monto = monto + item.Cantidad * item.Producto.Precio;
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
        foreach (PresupuestoDetalle item in detalle)
        {   
            cant = item.Cantidad;
        }
        return cant;
    }

    
    
}