using BD_TRANSACCIONES.DAL;
using BD_TRANSACCIONES.Models;

namespace BD_TRANSACCIONES.BLL
{
    public class CuentaBLL
    {
        private readonly CuentaDAL cuentaDAL;

        public CuentaBLL(CuentaDAL cuentaDAL)
        {
            this.cuentaDAL = cuentaDAL;
        }


        // registrar una cuenta
        public void InsertarCuenta(Cuenta cuenta)
        {
            cuentaDAL.InsertarCuenta(cuenta);
        }


        // realizar un deposito
        public void Deposito(int idCuenta, decimal monto)
        {
            cuentaDAL.Deposito(idCuenta, monto);
        }

        public void RegistrarMovimiento(int idCuenta, decimal monto)
        {
            cuentaDAL.RegistrarMovimiento(idCuenta, monto);
        }


        // realiza un retiro 
        public void Retiro(int idCuenta, decimal monto)
        {
            cuentaDAL.Retiro(idCuenta, monto);
        }

        public void RegistrarRetiro(int idCuenta, decimal monto)
        {
            cuentaDAL.RegistrarRetiro(idCuenta, monto);
        }


        //realiza una transferencia entre dos cuentas

        public void Transferencia(int idCuentaOrigen, int idCuentaDestino, decimal monto)
        {
            cuentaDAL.Transferencia(idCuentaOrigen, idCuentaDestino, monto);
        }

        public void RegistrarMovimientoTransferencia(int idMovimiento, int idCuenta, string tipoMovimiento, decimal monto)
        {
            cuentaDAL.RegistrarMovimientoTransferencia(idMovimiento, idCuenta, tipoMovimiento, monto);
        }



        // aqui consultamos saldo en otras palabras buscamos de que cuenta queremos ver el saldo

        public Cuenta ConsultarSaldo(int idCuenta)
        {
            return cuentaDAL.ConsultarSaldo(idCuenta);
        }



        // aqui consultamos los movimientos en otras palabras nos lanzara una lista de todos
        // los movimientos realizados

        public List<Movimiento> ConsultarMovimiento(int idCuenta)
        {
            return cuentaDAL.ConsultarMovimientos(idCuenta);
        }
    }
}
