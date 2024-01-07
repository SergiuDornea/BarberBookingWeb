using Microsoft.AspNetCore.Mvc.RazorPages;
using BarberBookingWeb.Data;
namespace BarberBookingWeb.Models
{
    public class ServiciuStiluriPageModel : PageModel
    {
        public List<StilAtribuitServiciu> StilAtribuitServiciuList;
        public void PopulateStilAtribuitServiciu(BarberBookingWebContext context,
        Serviciu serviciu)
        {
            var allStiluri = context.Stil;
            var serviciuStiluri = new HashSet<int>(
            serviciu.ServiciuStiluri.Select(c => c.StilID)); //
            StilAtribuitServiciuList = new List<StilAtribuitServiciu>();
            foreach (var cat in allStiluri)
            {
                StilAtribuitServiciuList.Add(new StilAtribuitServiciu
                {
                    StilID = cat.ID,
                    Denumire = cat.DenumireStil,
                    Atribuire = serviciuStiluri.Contains(cat.ID)
                });
            }
        }
        public void UpdateServiciuStiluri(BarberBookingWebContext context,
        string[] selectedStiluri, Serviciu serviciuToUpdate)
        {
            if (selectedStiluri == null)
            {
                serviciuToUpdate.ServiciuStiluri = new List<ServiciuStil>();
                return;
            }
            var selectedStiluriHS = new HashSet<string>(selectedStiluri);
            var serviciuStiluri = new HashSet<int>
            (serviciuToUpdate.ServiciuStiluri.Select(c => c.Stil.ID));
            foreach (var cat in context.Stil)
            {
                if (selectedStiluriHS.Contains(cat.ID.ToString()))
                {
                    if (!serviciuStiluri.Contains(cat.ID))
                    {
                        serviciuToUpdate.ServiciuStiluri.Add(
                        new ServiciuStil
                        {
                            ServiciuID = serviciuToUpdate.ID,
                            StilID = cat.ID
                        });
                    }
                }
                else
                {
                    if (serviciuStiluri.Contains(cat.ID))
                    {
                        ServiciuStil courseToRemove
                        = serviciuToUpdate
                        .ServiciuStiluri
                        .SingleOrDefault(i => i.StilID == cat.ID);
                        context.Remove(courseToRemove);
                    }
                }
            }
        }
    }
}