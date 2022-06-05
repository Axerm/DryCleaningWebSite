using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DryCleaningWebSite.Models
{
    public class Order
    {
        public int Id { get; set; }
        public Employee Employee { get; set; }
        public int? FilialId { get; set; }
        public Filial Filial { get; set; }

        public DateTime DateOfReceipt { get; set; } // Дата приёма
        public DateTime DateOfPlainIssue { get; set; } // Дата выдачи
        public DateTime? DateOfAffectedPlainIssue { get; set; } // Дата выдачи

        public int? ClientId{ get; set; }
        public Client Client { get; set; }
        public int? ThingTypeId { get; set; }
        public ThingType ThingType { get; set; }
        public int ThingSize { get; set; }
        public string ThingComplect { get; set; }
        public int? ManufacturerMarkingId { get; set; }
        public ManufacturerMarking ManufacturerMarking { get; set; }
        public int? PollutionDegreeId { get; set; }
        public PollutionDegree PollutionDegree { get; set; }
        public int? BurnoutDegreeId { get; set; }
        public BurnoutDegree BurnoutDegree { get; set; }
        public int? GluePartsId { get; set; }
        public GlueParts GlueParts { get; set; } // клеевые детали
        public bool HasBloodWine { get; set; }
        public bool HasPaintBlackPasta { get; set; }
        public bool HasOilFat { get; set; }
        public bool HasShine { get; set; } //ласы, блеск, лоск
        public bool HasSpotUnknownOrigin { get; set; }
        public bool HasAbrasion { get; set; } // вытертость, белесость, истёртость
        public bool HasDeformation { get; set; }
        public string DefectsBefore { get; set; }
        public string DefectsAfter{ get; set; }
        public string RemovableFittings { get; set; } // съёмная фурнитура
        public string NonRemovableFittings { get; set; } // не съёмная фурнитура
        public int? OperationalWearDegreeId { get; set; }
        public OperationalWearDegree OperationalWearDegree { get; set; } // процент эксплуатационного износа
        public int? ServiceTypeId { get; set; }
        public ServiceType ServiceType { get; set; }
        public int? StockId { get; set; }
        public Stock Stock { get; set; }
        public int? StatusOrderId { get; set; }
        public StatusOrder StatusOrder { get; set; }
        public int? StatusPayId { get; set; }
        public StatusPay StatusPay { get; set; }


        public decimal AdditionalCost { get; set; }
        public decimal? FinalPrice { get; set; }


        public DateTime? DateOfIssue { get; set; } // фактическая дата выдачи

        public List<OrderAdditionalInfo> OrderAdditionalInfos { get; set; }
    }
}
