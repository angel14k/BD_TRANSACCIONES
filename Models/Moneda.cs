namespace BD_TRANSACCIONES.Models
{
    public class Moneda
    {
        public int pk_id_moneda_in { get; set; }
        public string tipo_moneda_ch { get; set; }
        public DateTime fecha_creacion_dt { get; set; }
        public DateTime fecha_modificacion_dt { get; set; }
    }
}
