﻿using System;

namespace libTracer
{
    public class Computations
    {
        public Single Time { get; set; }
        public Shape Object { get; set; }
        public TPoint Point { get; set; }
        public TVector EyeV { get; set; }
        public TVector NormalV { get; set; }
        public Boolean Inside { get; set; }
    }
}
