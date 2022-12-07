using GeekSeatWitchSaga.Models;
using GeekSeatWitchSaga.Models.GeekSeatViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace GeekSeatWitchSaga.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {            
            return View();
        }
        public static List<int> KilledVillager = new List<int>();

        [HttpPost]
        public IActionResult GetAndFillKilledVillager(int[] Ages, int[] Years)
        {
            GeekSeatViewModel geekSeatViewModel = new GeekSeatViewModel();
            string rata = "";
            for(int i = 0; i < Ages.Length; i++)
            {
                int born1 = Years[i] - Ages[i];
                int killed1 = GetAndFillKilledVillagers(born1);
                geekSeatViewModel.killedVillagers.Add(new KilledVillager { Age = Ages[i], Year = Years[i], 
                    Born = born1, Killed = killed1 });
                rata += string.IsNullOrWhiteSpace(rata) ? killed1.ToString() : "+" + killed1.ToString();
            }
            double res = geekSeatViewModel.killedVillagers.Select(x => x.Killed).Average();




            return Json(new {Data = geekSeatViewModel.killedVillagers, Rata2 = rata, 
                CountData = geekSeatViewModel.killedVillagers.Count(), Result = res < 0 ? -1 : res });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public int GetAndFillKilledVillagers(int index)
        {
            int result = 0;
            //if data invalid
            if (index < 0)
            {
                result = -1;
            }
            //fill data if the list is still empty
            else if (KilledVillager.Count() == 0)
            {
                for (int i = 0; i < index; i++)
                {
                    if (i == 0 || i == 1)
                        KilledVillager.Add(1);
                    else
                        KilledVillager.Add(KilledVillager[i - 1] + KilledVillager[i - 2]);
                }
                result = KilledVillager.Sum();
            }//fill the new data if the index is greater than current count data
            else if (index > KilledVillager.Count())
            {
                for (int i = KilledVillager.Count(); i < index; i++)
                {
                    if (i == 1)
                        KilledVillager.Add(1);
                    else
                        KilledVillager.Add(KilledVillager[i - 1] + KilledVillager[i - 2]);
                }
                result = KilledVillager.Sum();
            }//get the sum of data from first to destination index data
            else
            {
                result = KilledVillager.Where(x => x <= KilledVillager.ElementAt(index - 1)).Sum();
            }

            return result;
        }
    }
}
