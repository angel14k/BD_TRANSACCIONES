using BD_TRANSACCIONES.Models;
using Microsoft.Data.SqlClient;

namespace BD_TRANSACCIONES.DAL
{
    public class CuentaDAL
    {
        private readonly string cadenaConexion;

        public CuentaDAL(IConfiguration configuration)
        {
            cadenaConexion = configuration.GetConnectionString("ConexionBD");
        }



        // METODO PARA INSERTAR UNA CUENTA
        public void InsertarCuenta(Cuenta cuenta)
        {
            using (SqlConnection cn = new SqlConnection(cadenaConexion))
            {
                string sql = @"INSERT INTO CUENTA
                                    (pk_id_cuenta_in,
                                    fk_id_moneda_in,
                                    numero_cuenta_vc,
                                    saldo_de,
                                    fecha_creacion_dt,
                                    fecha_modificacion_dt)
                                VALUES
                                    (@id,
                                     @moneda,
                                     @numero,
                                     @saldo,
                                     GETDATE(),
                                     GETDATE())";
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@id", cuenta.pk_id_cuenta_in);
                    cmd.Parameters.AddWithValue("@moneda", cuenta.fk_id_moneda_in);
                    cmd.Parameters.AddWithValue("@numero", cuenta.numero_cuenta_vc);
                    cmd.Parameters.AddWithValue("@saldo", cuenta.saldo_de);

                    cn.Open();

                    cmd.ExecuteNonQuery();

                }
            }
        }


        // metodo que hace deposito de una cuenta primero hacemos y luego un registro de movimiento

        public void Deposito(int idCuenta, decimal monto)
        {
            using (SqlConnection cn = new SqlConnection(cadenaConexion))
            {
                string sql = @"UPDATE CUENTA
                                SET saldo_de = saldo_de + @monto
                                WHERE pk_id_cuenta_in = @idCuenta";

                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@monto", monto);
                    cmd.Parameters.AddWithValue("@idcuenta", idCuenta);


                    cn.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }


        public void RegistrarMovimiento(int idCuenta, decimal monto)
        {
            using (SqlConnection cn = new SqlConnection(cadenaConexion))
            {
                string sql = @"INSERT INTO MOVIMIENTO
                                    (pk_id_movimiento_in,
                                     fk_id_cuenta_in,
                                     tipo_movimiento_vc,
                                     monto_de,
                                     fecha_dt,
                                     fecha_creacion_dt,
                                     fecha_modificacion_dt)
                                VALUES
                                    (@idMovimiento,
                                     @idCuenta,
                                     'ABONO',
                                     @monto,
                                     GETDATE(),
                                     GETDATE(),
                                     GETDATE())";

                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue(@"idMovimiento", 4);
                    cmd.Parameters.AddWithValue(@"idCuenta", idCuenta);
                    cmd.Parameters.AddWithValue(@"monto", monto);

                    cn.Open();

                    cmd.ExecuteNonQuery();

                }
            }
        }



        // metodo que hace un retiro de una cuenta

        public void Retiro(int idCuenta, decimal monto)
        {
            using (SqlConnection cn = new SqlConnection(cadenaConexion))
            {
                string sql = @"UPDATE CUENTA
                                    SET saldo_de = saldo_de - @monto
                                      WHERE pk_id_cuenta_in = @idcuenta";

                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@idcuenta", idCuenta);
                    cmd.Parameters.AddWithValue("@monto", monto);

                    cn.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }


        public void RegistrarRetiro(int idCuenta, decimal monto)
        {
            using (SqlConnection cn = new SqlConnection(cadenaConexion))
            {
                string sql = @"INSERT INTO MOVIMIENTO
                                    (pk_id_movimiento_in,
                                    fk_id_cuenta_in,
                                    tipo_movimiento_vc,
                                    monto_de,
                                    fecha_dt,
                                    fecha_creacion_dt,
                                    fecha_modificacion_dt)
                                VALUES
                                    (
                                    @idMovimiento,
                                    @idCuenta,
                                    'RETIRO',
                                    @monto,
                                    GETDATE(),
                                    GETDATE(),
                                    GETDATE())";
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@idMovimiento", 5);
                    cmd.Parameters.AddWithValue("@idCuenta", idCuenta);
                    cmd.Parameters.AddWithValue("@monto", monto);


                    cn.Open();

                    cmd.ExecuteNonQuery();


                }

            }

        }


        // Transferir saldo entre cuentas


        public void Transferencia(int idCuentaorigen, int idCuentaDestino, decimal monto)
        {
            using (SqlConnection cn = new SqlConnection(cadenaConexion))
            {
                string sql = @"UPDATE CUENTA
                                    SET saldo_de = saldo_de - @monto
                                    WHERE pk_id_cuenta_in = @idOrigen;

                               UPDATE CUENTA
                                    SET saldo_de = saldo_de + @monto
                                    WHERE pk_id_cuenta_in = @idDestino;";

                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {

                    cmd.Parameters.AddWithValue("@monto", monto);
                    cmd.Parameters.AddWithValue("@idOrigen", idCuentaorigen);
                    cmd.Parameters.AddWithValue("@idDestino", idCuentaDestino);


                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }

        }



        public void RegistrarMovimientoTransferencia(int idMovimiento, int idCuenta, string tipoMovimiento, decimal monto)
        {
            using (SqlConnection cn = new SqlConnection(cadenaConexion))
            {
                string sql = @"INSERT INTO MOVIMIENTO
                                    (pk_id_movimiento_in,
                                     fk_id_cuenta_in,
                                     tipo_movimiento_vc, 
                                     monto_de,
                                     fecha_dt,
                                     fecha_creacion_dt,
                                     fecha_modificacion_dt)
                                VALUES
                                    (@idMovimiento,
                                     @idCuenta,
                                     @tipoMovimiento,
                                     @monto,
                                     GETDATE(),         
                                     GETDATE(),
                                     GETDATE())";

                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@idMovimiento", idMovimiento);
                    cmd.Parameters.AddWithValue("@idCuenta", idCuenta);
                    cmd.Parameters.AddWithValue("@tipoMovimiento", tipoMovimiento);
                    cmd.Parameters.AddWithValue("@monto", monto);


                    cn.Open();

                    cmd.ExecuteNonQuery();
                }

            }

        }


        // Consulta de saldos, aqui vamos a hacer una busqueda

        public Cuenta ConsultarSaldo(int idCuenta)
        {
            Cuenta cuenta = new Cuenta();

            using (SqlConnection cn = new SqlConnection(cadenaConexion))
            {
                string sql = @"SELECT
                                    pk_id_cuenta_in,
                                    fk_id_moneda_in,
                                    numero_cuenta_vc,
                                    saldo_de,
                                    fecha_creacion_dt,
                                    fecha_modificacion_dt
                               FROM CUENTA
                               WHERE pk_id_cuenta_in = @idCuenta";

                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@idCuenta", idCuenta);
                    cn.Open();


                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            cuenta.pk_id_cuenta_in = Convert.ToInt32(dr["pk_id_cuenta_in"]);
                            cuenta.fk_id_moneda_in = Convert.ToInt32(dr["fk_id_moneda_in"]);
                            cuenta.numero_cuenta_vc = dr["numero_cuenta_vc"].ToString();
                            cuenta.saldo_de = Convert.ToDecimal(dr["saldo_de"]);
                            cuenta.fecha_creacion_dt = Convert.ToDateTime(dr["fecha_creacion_dt"]);
                            cuenta.fecha_modificacion_dt = Convert.ToDateTime(dr["fecha_modificacion_dt"]);
                        }

                    }

                }
            }

            return cuenta;

        }




        // aqui consultamos una lista de movimientos

        public List<Movimiento> ConsultarMovimientos(int idCuenta)
        {
            List<Movimiento> lista = new List<Movimiento>();

            using (SqlConnection cn = new SqlConnection(cadenaConexion))
            {
                string sql = @"SELECT
                                    pk_id_movimiento_in,
                                    fk_id_cuenta_in,
                                    tipo_movimiento_vc,
                                    monto_de,   
                                    fecha_dt,
                                    fecha_creacion_dt,
                                    fecha_modificacion_dt
                                FROM MOVIMIENTO
                            WHERE fk_id_cuenta_in = @idCuenta";

                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@idCuenta", idCuenta);

                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {



                        while (dr.Read())
                        {
                            Movimiento movimiento = new Movimiento();

                            movimiento.pk_id_movimiento_in = Convert.ToInt32(dr["pk_id_movimiento_in"]);
                            movimiento.fk_id_cuenta_in = Convert.ToInt32(dr["fk_id_cuenta_in"]);
                            movimiento.tipo_movimiento_vc = dr["tipo_movimiento_vc"].ToString();
                            movimiento.monto_de = Convert.ToDecimal(dr["monto_de"]);
                            movimiento.fecha_dt = Convert.ToDateTime(dr["fecha_dt"]);
                            movimiento.fecha_creacion_dt = Convert.ToDateTime(dr["fecha_creacion_dt"]);
                            movimiento.fecha_modificacion_dt = Convert.ToDateTime(dr["fecha_modificacion_dt"]);

                            lista.Add(movimiento);

                        }
                    }
                }
            }
            return lista;
        }




    }
}
