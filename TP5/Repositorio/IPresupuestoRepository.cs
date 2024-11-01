namespace TP5.Repositorio
{
    public interface IPresupuestoRepository
    {
        public void CrearPresupuesto(Presupuesto presupuesto);
        public List<Presupuesto> ListarPresupuestos();
        public List<PresupuestoDetalle> ObtenerDetalle(int id);
        public void EliminarPresupuesto(int id);
    }
}