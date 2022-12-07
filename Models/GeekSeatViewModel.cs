using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekSeatWitchSaga.Models.GeekSeatViewModel
{
    public class GeekSeatViewModel
    {
        public List<KilledVillager> killedVillagers { get; set; }

        public GeekSeatViewModel()
        {
            this.killedVillagers = new List<KilledVillager>();
        }
    }

    public class KilledVillager
    {
        public int Age { get; set; }
        public int Year { get; set; }
        public int Born { get; set; }
        public int Killed { get; set; }

    }
}
