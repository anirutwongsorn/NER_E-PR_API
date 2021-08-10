using System;
using ner_api.Entities;

namespace ner_pr_api.Dtos.OutputDtos
{
    public class TimeSettingDto
    {
        public string previousTime { get; set; }
        public string initTime { get; set; }
        public string endTime { get; set; }
        public string nextTime { get; set; }

        public static TimeSettingDto FromTbSetting(TbPrTimeSetting model) => new TimeSettingDto
        {
            previousTime = ((DateTime)model.InitTime).AddMinutes(-1).ToString("HH:mm"),
            initTime = ((DateTime)model.InitTime).ToString("HH:mm"),
            endTime = ((DateTime)model.EndTime).ToString("HH:mm"),
            nextTime = ((DateTime)model.EndTime).AddMinutes(1).ToString("HH:mm"),
        };

    }
}