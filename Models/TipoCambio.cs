namespace BD_TRANSACCIONES.Models
{
    public class TipoCambio
    {
        public int pk_id_tipo_cambio_in { get; set; }
        public int fk_id_moneda_origen_in { get; set; }
        public int fk_id_moneda_destino_in { get; set; }
        public decimal valor_de { get; set; }
        public DateTime fecha_dt { get; set; }
        public DateTime fecha_creacion_dt { get; set; }
        public DateTime fecha_modificacion_dt { get; set; }
    }
}
