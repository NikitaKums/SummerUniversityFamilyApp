using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public enum Relation
    {
        [Display(Name = "Isa")]
        Isa,
        [Display(Name = "Ema")]
        Ema,
        [Display(Name = "Vanaisa")]
        Vanaisa,
        [Display(Name = "Vanaema")]
        Vanaema,
        [Display(Name = "Onu")]
        Onu,
        [Display(Name = "Tädi")]
        Tädi,
        [Display(Name = "Vend")]
        Vend,
        [Display(Name = "Õde")]
        Õde,
        [Display(Name = "Tütar")]
        Tütar,
        [Display(Name = "Poeg")]
        Poeg,
        [Display(Name = "Abikaasa")]
        Abikaasa,
        [Display(Name = "Lapselaps")]
        Lapselaps,
        [Display(Name = "Muu")]
        Muu
    }
}