﻿using Power_Hand.Models;
using Prism.Events;

namespace Power_Hand.Data.SharedData
{
    public class SelectedEmployeeToDeleteChannel : PubSubEvent<Employee>
    {
    }
}