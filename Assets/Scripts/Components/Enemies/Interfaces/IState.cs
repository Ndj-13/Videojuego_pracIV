﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components.Enemies.Interfaces
{
    public interface IState
    {
        public void Enter();
        public void Exit();
        public void Update();
        public void FixedUpdate();
    }
}