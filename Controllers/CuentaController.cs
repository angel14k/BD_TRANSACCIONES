using BD_TRANSACCIONES.BLL;
using BD_TRANSACCIONES.Models;
using Microsoft.AspNetCore.Mvc;

namespace BD_TRANSACCIONES.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CuentaController : ControllerBase
    {

        private readonly CuentaBLL cuentaBLL;

        public CuentaController(CuentaBLL cuentaBLL)
        {
            this.cuentaBLL = cuentaBLL;
        }

        // registrar una cuenta
        [HttpPost]
        public IActionResult InsertarCuenta(Cuenta cuenta)
        {
            cuentaBLL.InsertarCuenta(cuenta);

            return Ok();

        }


        // realizar un deposito aqui hacemos que dos movimiento:
        // aumentar saldo, y registrar el movimiento
        [HttpPost("Deposito")]
        public IActionResult Deposito(int idCuenta, decimal monto)
        {
            cuentaBLL.Deposito(idCuenta, monto);

            cuentaBLL.RegistrarMovimiento(idCuenta, monto);

            return Ok();
        }


        // realizar un retiro aqui hacemos de dos movimiento:
        // retirar saldo y registrar el movimiento
        [HttpPost("Retiro")]
        public IActionResult Retiro(int idCuenta, decimal monto)
        {
            cuentaBLL.Retiro(idCuenta, monto);

            cuentaBLL.RegistrarRetiro(idCuenta, monto);

            return Ok();
        }


        // realizar una transferencia entre dos cuentas aqui hacemos dos movimientos:
        // retiramos saldo de cuenta origen y abonamos monto a cuenta destino 
        // y registramos movimientos

        [HttpPost("Transferencia")]
        public IActionResult Transferencia(int idCuentaOrigen, int idCuentaDestino, decimal monto)
        {
            cuentaBLL.Transferencia(idCuentaOrigen, idCuentaDestino, monto);

            cuentaBLL.RegistrarMovimientoTransferencia(6, idCuentaOrigen, "RETIRO", monto);
            cuentaBLL.RegistrarMovimientoTransferencia(7, idCuentaDestino, "ABONO", monto);

            return Ok();
        }


        // realizar una consulta de saldo a una cuenta en especifica

        [HttpGet("ConsultarSaldo")]
        public IActionResult ConsultarSaldo(int idCuenta)
        {

            Cuenta cuenta = cuentaBLL.ConsultarSaldo(idCuenta);

            return Ok(cuenta);
        }


        [HttpGet("ConsultarMovimientos")]
        public IActionResult ConsultarMovimientos(int idCuenta)
        {
            List<Movimiento> movimientos = cuentaBLL.ConsultarMovimiento(idCuenta);

            return Ok(movimientos);
        }

    }
}
