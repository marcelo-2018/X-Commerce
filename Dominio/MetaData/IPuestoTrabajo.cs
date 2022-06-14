using System.ComponentModel.DataAnnotations;

namespace Dominio.MetaData
{
    public interface IPuestoTrabajo
    {
        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        int Codigo { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        [StringLength(250, ErrorMessage = "El campo {0} debe ser menor {1} caracteres.")]
        string Descripcion { get; set; }
    }
}
