using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DryCleaningWebSite.Attributes;

namespace DryCleaningWebSite.ViewModils
{
    public class OrderDetailsViewModel
    {
        [Display(Name = "Номер наряд-заказа")]
        public int Id{ get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Оформил сотрудник")]
        public string Employee { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Статус наряд заказа")]
        public string StatusOrder { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Дополнительная стоимость")]
        public string AdditionalCost { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "К оплате")]
        public string FinalPrice { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Статус оплаты")]
        public string StatusPay { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Филиал")]
        public string Filial { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Дата приёма")]
        public string DateOfReceipt { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Дата выдачи")]
        public string DateOfPlainIssue { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Изменённая дата выдачи")]
        public string DateOfAffectedPlainIssue { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Клиент")]
        public string Client { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Тип вещи")]
        public string ThingType { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Размер вещи")]
        public string ThingSize { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Комплектность вещи")]
        public string ThingComplect { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Наличие этикетки")]
        public string ManufacturerMarking { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Степень загрязнения")]
        public string PollutionDegree { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Выгар")]
        public string BurnoutDegree { get; set; }
        
        [DataType(DataType.Text)]
        [Display(Name = "Наличие клеевых деталей")]
        public string GlueParts { get; set; }

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

        [DataType(DataType.Text)]
        [Display(Name = "Процент эксплуатационного износа")]
        public string OperationalWearDegree { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Вид услуги")]
        public string ServiceType { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Действующая акция")]
        public string Stock { get; set; }
        
        [Display(Name = "Фактическая дата выдачи")]
        [DataType(DataType.Text)]
        public string DateOfIssue { get; set; }
    }
}
