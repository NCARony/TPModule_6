﻿using BO;
using System.Collections.Generic;


namespace TP1_Module_6.Models
{
    public class SamouraiVM
    {
        public Samourai Samourai { get; set; }
        public List<Arme> Armes { get; set; }
        public int? IdSelectedArme { get; set; }
    }
}