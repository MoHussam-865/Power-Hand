﻿using MyDatabase.Models;
using Prism.Events;

namespace Power_Hand.Features.FeatureApp.FeatureEditClient.Channels
{
    public class EditClientPageShareClientChannel : PubSubEvent<Client?>
    {
    }
}