using Doantour.Models;

namespace Doantour.ServiceUnit
{
    public class CountryService
    {
        private readonly Hachutravelcontext _context;

        public CountryService(Hachutravelcontext hachutravelcontext)
        {
            _context = hachutravelcontext;
        }
    }
}
