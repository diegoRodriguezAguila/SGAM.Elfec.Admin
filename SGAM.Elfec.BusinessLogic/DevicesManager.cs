﻿using SGAM.Elfec.DataAccess.WebServices;
using SGAM.Elfec.DataAccess.WebServices.ApiEndpoints;
using SGAM.Elfec.Model;
using SGAM.Elfec.Model.Enums;
using SGAM.Elfec.Security;
using System;
using System.Collections.Generic;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;

namespace SGAM.Elfec.BusinessLogic
{
    public class DevicesManager
    {
        /// <summary>
        /// Obtiene todos los dipositivos, ya sea por medio de la caché o de webservices
        /// </summary>
        public static IObservable<IList<Device>> GetAllDevices()
        {
            var parameters = new Dictionary<string, string>();
            parameters["sort"] = "-status,name";

            return RestEndpointFactory
                .Create<IDevicesEndpoint>(SessionManager.Instance.CurrentLoggedUser)
                .GetAllDevices(parameters).ToObservable()
                .SubscribeOn(TaskPoolScheduler.Default)
                .InterpretingErrors();
        }
        /// <summary>
        /// Autoriza remotamente un dispositivo
        /// </summary>
        /// <param name="deviceToAuth"></param>
        public static IObservable<Device> AuthorizeDevice(Device deviceToAuth)
        {
            return RestEndpointFactory
                    .Create<IDevicesEndpoint>(SessionManager.Instance.CurrentLoggedUser)
                    .UpdateDevice(deviceToAuth.Imei, PrepareDeviceForAuth(deviceToAuth))
                    .ToObservable()
                    .SubscribeOn(TaskPoolScheduler.Default)
                    .InterpretingErrors();
        }

        /// <summary>
        /// Actualiza el estado de un dispositivo
        /// </summary>
        /// <param name="deviceId">id dispositivo (imei/android id)</param>
        /// <param name="newStatus">estado nuevo</param>
        /// <returns>observable del restulado de la operación</returns>
        public static IObservable<Device> UpdateDeviceStatus(string deviceId, DeviceStatus newStatus)
        {
            return RestEndpointFactory.Create<IDevicesEndpoint>(SessionManager
                .Instance.CurrentLoggedUser)
                .UpdateDevice(deviceId, new Device { Status = newStatus })
                .ToObservable()
                .SubscribeOn(TaskPoolScheduler.Default)
                .InterpretingErrors();
        }

        /// <summary>
        /// Crea una copia de solo los valores actualizables en la autorización de un dispositivo
        /// </summary>
        /// <param name="deviceToAuth"></param>
        /// <returns></returns>
        private static Device PrepareDeviceForAuth(Device deviceToAuth)
        {
            return new Device()
            {
                Name = deviceToAuth.Name,
                PhoneNumber = deviceToAuth.PhoneNumber,
                IdCiscoAsa = deviceToAuth.IdCiscoAsa,
                ScreenSize = deviceToAuth.ScreenSize,
                ScreenResolution = deviceToAuth.ScreenResolution,
                Camera = deviceToAuth.Camera,
                SdMemoryCard = deviceToAuth.SdMemoryCard,
                GmailAccount = deviceToAuth.GmailAccount,
                Comments = deviceToAuth.Comments,
                Status = DeviceStatus.Authorized
            };
        }
    }
}
