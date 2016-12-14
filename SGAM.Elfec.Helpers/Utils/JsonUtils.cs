using Newtonsoft.Json;
using SGAM.Elfec.Model.WebServices.Converters;
using SGAM.Elfec.Model.WebServices.JsonContractResolver;
using System;

namespace SGAM.Elfec.Helpers.Utils
{
    public class JsonUtils
    {
        private static WeakReference<JsonSerializerSettings> _settings;

        public static JsonSerializerSettings Settings
        {
            get
            {
                JsonSerializerSettings jsonSettings = null;
                _settings?.TryGetTarget(out jsonSettings);
                if (jsonSettings != null)
                    return jsonSettings;
                jsonSettings = new JsonSerializerSettings()
                {
                    ContractResolver = new SnakeCasePropertyNamesContractResolver(),
                    NullValueHandling = NullValueHandling.Ignore
                };
                jsonSettings.Converters.Add(new SnakeCaseEnumConverter { AllowIntegerValues = true });
                jsonSettings.Converters.Add(new EntityConverter());
                _settings?.SetTarget(jsonSettings);
                if (_settings == null)
                    _settings = new WeakReference<JsonSerializerSettings>(jsonSettings);
                return jsonSettings;
            }
        }
    }
}