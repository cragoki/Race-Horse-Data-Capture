﻿using Core.Entities;
using Core.Enums;
using Core.Models.Settings;
using System;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface IConfigurationService
    {
        RacingPostSettings GetRacingPostSettings();
        bool SavePermitted();
        void AddBatch(Guid batchId, string diagnostics);
        DateTime GetLastBackfillDate();
        Task UpdateBackfillDate(DateTime newDate);
        MailSettings GetMailSettings();
        Task<bool> UpdateJob(JobEnum job);
        Task<JobEntity> GetJobInfo(JobEnum job);

     }
}