﻿using Geolocation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geolocation.Services.Interfaces
{
    public interface IGeolocationDetailsManager
    {
        GeolocationDetails Create(string ipOrUrl);
    }
}
