using PracticalTraining.Models;
using System.Web;
using System.Collections.Generic;

namespace PracticalTraining.ViewModels
{
    public class OwnerViewModel
    {
        public Dictionary<string, string> Management = new Dictionary<string, string>();
        public Dictionary<string, string> Documentation = new Dictionary<string, string>();

        public OwnerViewModel()
        {
            Management =  new Dictionary<string, string> { { "Персонал", "StaffInfoes" }, { "Помещения", "PlaceInfoes" }, { "Услуги", "ServiceInfoes" }, 
                                                              { "Способы оплаты", "PaymentInfoes" }, { "О фотостудии", "BaseInfoes"}, { "Управление паролями",  "UserInfoes"} };
            Documentation = new Dictionary<string, string> { { "Отчет о посещаемости помещений", "DownloadPlaceReport"}, 
                                                             { "Отчет о потоке клиентов", "DownloadClientReport" }, 
                                                             { "Табель учета рабочего времени", "DownloadTimeReport" }, 
                                                             { "Табель расчета оплаты труда", "DownloadPayReport" }};

        }


    }
}
