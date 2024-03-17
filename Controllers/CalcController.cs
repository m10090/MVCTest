using Microsoft.AspNetCore.Mvc;

namespace testMvc.Controllers
{
    public class CalcController : Controller
    {
        public ActionResult Index()
        {
            TempData["result"] = Convert.ToString(TempData["result"]);
            TempData["firstNumber"] = Convert.ToString(TempData["firstNumber"] ?? 0.0);
            TempData["secondNumber"] = Convert.ToString(TempData["secondNumber"] ?? 0.0);
            TempData["operation"] = (TempData["operation"] ?? "");
            return View();
        }

        public ActionResult calculate(string firstNumber, string secondNumber, string operation)
        {
            double num1 = Convert.ToDouble(firstNumber);
            double num2 = Convert.ToDouble(secondNumber);
            double result = 0;
            switch (operation)
            {
                case "add":
                    result = num1 + num2;
                    break;
                case "subtract":
                    result = num1 - num2;
                    break;
                case "multiply":
                    result = num1 * num2;
                    break;
                case "divide":
                    result = num1 / num2;
                    break;
            }
            TempData["result"] = Convert.ToString(result);
            TempData["firstNumber"] = Convert.ToString(num1);
            TempData["secondNumber"] = Convert.ToString(num2);
            TempData["operation"] = Convert.ToString(operation);
            return View();
        }
    }

}
