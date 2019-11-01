using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_HKIII_Auction.Models
{
    public class HistoryDal
    {
        Context context = new Context();
        public List<HistoryAuction> GetHistoryAuctions()
        {
            return context.HistoryAuctions.ToList();
        }
    }
}