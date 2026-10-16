using BD_TRANSACCIONES.DAL;


namespace BD_TRANSACCIONES.BLL
{
    public class MonedaBLL
    {
        private readonly MonedaDAL monedaDAL;

        public MonedaBLL(MonedaDAL monedaDAL)
        {
            this.monedaDAL = monedaDAL;
        }
    }
}
