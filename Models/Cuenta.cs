namespace BD_TRANSACCIONES.Models
{
    public class Cuenta
    {
        public int pk_id_cuenta_in { get; set; }
        public int fk_id_moneda_in { get; set; }
        public string numero_cuenta_vc { get; set; }
        public decimal saldo_de { get; set; }
        public DateTime fecha_creacion_dt { get; set; }
        public DateTime fecha_modificacion_dt { get; set; }

    }
}
