﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Events;

namespace Power_Hand.Features.FeatureApp.FeatureCasher.Channels
{
    public class BarcodeChannel: PubSubEvent<string?>
    {
    }
}
