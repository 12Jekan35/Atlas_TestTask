using Atlas_TestTask.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Atlas_TestTask.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult CalcByDailyRate()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Result(string loanSum, string loanTerm, string loanRate)
        {
            double _loanSum = default;
            double _loanRate = default;
            int _loanTerm = default;
            if (!double.TryParse(loanSum, out _loanSum) && _loanSum > 100)
            {
                return BadRequest("Неправильный формат: Сумма займа");
            }
            if (!int.TryParse(loanTerm, out _loanTerm) && _loanTerm > 0)
            {
                return BadRequest("Неправильный формат: Срок займа");
            }
            if (!double.TryParse(loanRate, out _loanRate)  && _loanRate >= 0 && _loanRate <= 100)
            {
                return BadRequest("Неправильный формат: Ставка");
            }
            var payments = Payment.ComputePaymentsByCredit(_loanSum, _loanRate, _loanTerm);
            return View(new PaymentsViewModel(payments));
        }
        [HttpPost]
        public IActionResult ResultForDailyRate(string loanSum, string loanTerm, string loanRate, string paymentStep)
        {
            double _loanSum = default;
            double _loanRate = default;
            int _paymentStep = default;
            int _loanTerm = default;
            if (!double.TryParse(loanSum, out _loanSum) && _loanSum > 100)
            {
                return BadRequest("Неправильный формат: Сумма займа");
            }
            if (!int.TryParse(loanTerm, out _loanTerm) && _loanTerm > 0)
            {
                return BadRequest("Неправильный формат: Срок займа");
            }
            if (!double.TryParse(loanRate, out _loanRate) && _loanRate >= 0 && _loanRate <= 100)
            {
                return BadRequest("Неправильный формат: Ставка");
            }
            if (!int.TryParse(paymentStep, out _paymentStep) && _paymentStep >= 0 && _loanTerm < _paymentStep && _loanTerm % _paymentStep != 0)
            {
                return BadRequest("Неправильный формат: Шаг платежа и/или срок займа");
            }
            var payments = Payment.ComputePaymentsByCreditForDailyRate(_loanSum, _loanRate, _loanTerm, _paymentStep);
            return View(new PaymentsViewModel(payments));
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}