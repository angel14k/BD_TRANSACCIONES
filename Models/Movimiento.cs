namespace BD_TRANSACCIONES.Models
{
    public class Movimiento
    {
        public int pk_id_movimiento_in { get; set; }
        public int fk_id_cuenta_in { get; set; }
        public string tipo_movimiento_vc { get; set; }
        public decimal monto_de { get; set; }
        public DateTime fecha_dt { get; set; }
        public DateTime fecha_creacion_dt { get; set; }
        public DateTime fecha_modificacion_dt { get; set; }

    }
}
