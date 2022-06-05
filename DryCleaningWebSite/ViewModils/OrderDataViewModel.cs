using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DryCleaningWebSite.Attributes;

namespace DryCleaningWebSite.ViewModils
{
    public class OrderDataViewModel
    {

        [Required(ErrorMessage = "Укажите дату приёма")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM.dd.yyyy}")]
        [DateAttributeCustomRatherNowCheck]
        [Display(Name = "Дата приёма")]
        public DateTime DateOfReceipt { get; set; }

        [Required(ErrorMessage = "Укажите дату выдачи")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM.dd.yyyy}")]
        [DateAttributeCustomRatherNowCheck]
        [Display(Name = "Дата выдачи")]
        public DateTime DateOfPlainIssue { get; set; }

        [Display(Name = "Клиент")]
        public int ClientId { get; set; }

        [Display(Name = "Тип вещи")]
        public int ThingTypeId { get; set; }

        [Required(ErrorMessage = "Укажите размер вещи")]
        [Range(0, 100)]
        [Display(Name = "Размер вещи")]
        public int ThingSize { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Комплектность вещи")]
        public string ThingComplect { get; set; }

        [Display(Name = "Наличие этикетки")]
        public int ManufacturerMarkingId { get; set; }

        [Display(Name = "Степень загрязнения")]
        public int PollutionDegreeId { get; set; }

        [Display(Name = "Выгар")]
        public int BurnoutDegreeId { get; set; }

        [Display(Name = "Наличие клеевых деталей")]
        public int GluePartsId { get; set; }

        [Display(Name = "На изделии присутствует кровь/вино")]
        public bool HasBloodWine { get; set; }

        [Display(Name = "На изделии присутствует краска/чернила/паста")]
        public bool HasPaintBlackPasta { get; set; }

        [Display(Name = "На изделии присутствует масло/жир/ГСМ")]
        public bool HasOilFat { get; set; }

        [Display(Name = "На изделии присутствуют ласы/блеск/лоск")]
        public bool HasShine { get; set; } //ласы, блеск, лоск

        [Display(Name = "На изделии присутствует пятна неизвестного происхождения")]
        public bool HasSpotUnknownOrigin { get; set; }

        [Display(Name = "На изделии присутствует вытертость/белесость/истёртость")]
        public bool HasAbrasion { get; set; } // вытертость, белесость, истёртость

        [Display(Name = "На изделии присутствует деформация ткани/пиллин")]
        public bool HasDeformation { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Дефекты, выясленные при приёме изделия, возникшие в процессе эксплуатации")]
        public string DefectsBefore { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Возможные скрытые дефекты, которые могут проявиться после химической чистки")]
        public string DefectsAfter { get; set; }
        
        [DataType(DataType.MultilineText)]
        [Display(Name = "Наличие съёмной фурнитуры")]
        public string RemovableFittings { get; set; } // съёмная фурнитура

        [DataType(DataType.MultilineText)]
        [Display(Name = "Наличие несъёмной фурнитуры")]
        public string NonRemovableFittings { get; set; } // не съёмная фурнитура

        [Display(Name = "Процент эксплуатационного износа")]
        public int OperationalWearDegreeId { get; set; }

        [Display(Name = "Вид услуги")]
        public int ServiceTypeId { get; set; }

        [Display(Name = "Действующая акция")]
        public int StockId { get; set; }

        [Display(Name = "Дополнительная стоимость")]
        public decimal AdditionalCost { get; set; }
    }
}
