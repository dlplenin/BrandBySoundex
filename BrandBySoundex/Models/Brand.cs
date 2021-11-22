
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrandBySoundex.Models
{
    public class Brand
    {
        public int Id { get; set; }

        public string Marca { get; set; }

        [NotMapped]
        [DisplayName("Estrictez en coincidencia")]
        public int DifferenceSoundex { get; set; }
    }
}
