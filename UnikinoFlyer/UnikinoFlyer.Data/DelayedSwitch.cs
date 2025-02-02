﻿using System;
using System.Collections.Generic;
using System.Text;
using MaxLib.DB;
using MaxLib.DB.Helper;

namespace UnikinoFlyer.Data
{
    [DbClass("DelayedSwitch")]
    public class DelayedSwitch
    {
        [DbProp(primaryKey:true, autoGenerated:true)]
        public int Id { get; set; }

        [DbProp]
        public DateTime Upload { get; set; }

        [DbProp]
        public DateTime Due { get; set; }

        [DbProp]
        public string Image { get; set; }

        [DbProp]
        public string Comment { get; set; }

        [DbProp]
        public DateTime? UploadedAt { get; set; }
    }
}
