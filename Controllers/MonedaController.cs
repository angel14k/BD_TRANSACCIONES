using BD_TRANSACCIONES.BLL;
using Microsoft.AspNetCore.Mvc;



namespace BD_TRANSACCIONES.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class MonedaController
    {
        private readonly MonedaBLL monedaBLL;

        public MonedaController(MonedaBLL monedaBLL)
        {
            this.monedaBLL = monedaBLL;
        }



    }
}
